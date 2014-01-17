using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.Core
{
	/// <summary>
	/// Jede Instanz der World-Klasse hält eine für diese Spielwelt verwendete Kamera als Attribut.
	/// Die Hauptfunktion der Kamera-Klasse ist das Berechnen der drei Matrizen, die für die Positionierung
	/// und Skalierung von 3D-Objekten in einer bestimmten Spielwelt benötigt werden, der View-, World- und Projection-Matrix.
	/// Um diese Matrizen zu berechnen, benötigt die Kamera unter Anderem Informationen über die aktuelle Kamera-Position,
	/// das aktuelle Kamera-Ziel und das Field of View.
	/// </summary>
	public sealed class Camera : GameScreenComponent
	{
		#region Properties

		private Vector3 _position;

		/// <summary>
		/// Die Position der Kamera.
		/// </summary>
		public Vector3 Position
		{
			get { return _position; }
			set {
				OnViewChanged ();
				_position = value;
			}
		}

		private Vector3 _target;

		/// <summary>
		/// Das Ziel der Kamera.
		/// </summary>
		public Vector3 Target
		{
			get { return _target; }
			set {
				OnViewChanged ();
				_target = value;
			}
		}

		private float _foV;

		/// <summary>
		/// Das Sichtfeld.
		/// </summary>
		public float FoV
		{
			get { return _foV; }
			set { _foV = MathHelper.Clamp (value, 40, 100); }
		}

		/// <summary>
		/// Die View-Matrix wird über die statische Methode CreateLookAt der Klasse Matrix des XNA-Frameworks
		/// mit Matrix.CreateLookAt (Position, Target, Vector3.Up) berechnet.
		/// </summary>
		public Matrix ViewMatrix { get; private set; }

		/// <summary>
		/// Die World-Matrix wird mit Matrix.CreateFromYawPitchRoll und den drei Rotationswinkeln berechnet.
		/// </summary>
		public Matrix WorldMatrix { get; private set; }

		/// <summary>
		/// Die Projektionsmatrix wird über die statische XNA-Methode Matrix.CreatePerspectiveFieldOfView berechnet.
		/// </summary>
		public Matrix ProjectionMatrix { get; private set; }

		/// <summary>
		/// Berechnet ein Bounding-Frustum, das benötigt wird, um festzustellen, ob ein 3D-Objekt sich im Blickfeld des Spielers befindet.
		/// </summary>
		public BoundingFrustum ViewFrustum { get; private set; }

		/// <summary>
		/// Eine Referenz auf die Spielwelt, für welche die Kamera zuständig ist.
		/// </summary>
		private World World { get; set; }

		/// <summary>
		/// Die Rotationswinkel.
		/// </summary>
		public Angles3 Rotation { get; set; }

		public Vector3 UpVector { get; private set; }

		public Action OnViewChanged = () => {};
		private float aspectRatio;
		private float nearPlane;
		private float farPlane;
		private Vector3 defaultPosition = new Vector3 (400, 400, 700);

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Kamera in einem bestimmten GameScreen für eine bestimmte Spielwelt.
		/// </summary>
		public Camera (GameScreen screen, World world)
		: base(screen, DisplayLayer.None)
		{
			World = world;
			Position = defaultPosition;
			Target = new Vector3 (0, 0, 0);
			UpVector = Vector3.Up;
			Rotation = Angles3.Zero;

			FoV = MathHelper.ToDegrees (MathHelper.PiOver4);
			aspectRatio = screen.Viewport.AspectRatio;
			nearPlane = 0.5f;
			farPlane = 10000.0f;

			UpdateMatrices (null);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Die Blickrichtung.
		/// </summary>
		public Vector3 TargetDirection
		{
			get {
				Vector3 toTarget = Target - Position;
				toTarget.Normalize ();
				return toTarget;
			}
		}

		/// <summary>
		/// Der Abstand zwischen der Kamera und dem Kamera-Ziel.
		/// </summary>
		public float TargetDistance
		{
			get {
				return Position.DistanceTo (Target);
			}
			set {
				Position = Position.SetDistanceTo (Target, value);
			}
		}

		public float ArcballTargetDistance
		{
			get {
				return Position.DistanceTo (ArcballTarget);
			}
			set {
				Position = Position.SetDistanceTo (ArcballTarget, value);
			}
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			UpdateMatrices (time);
			UpdateSmoothMove (time);
		}

		private void UpdateMatrices (GameTime time)
		{
			ViewMatrix = Matrix.CreateLookAt (Position, Target, UpVector);
			WorldMatrix = Matrix.CreateFromYawPitchRoll (Rotation.Y, Rotation.X, Rotation.Z);
			ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView (MathHelper.ToRadians (FoV), aspectRatio, nearPlane, farPlane);
			ViewFrustum = new BoundingFrustum (ViewMatrix * ProjectionMatrix);
		}

		/// <summary>
		/// Berechnet einen Strahl für die angegebenene 2D-Mausposition.
		/// </summary>
		public Ray GetMouseRay (Vector2 mousePosition)
		{
			Viewport viewport = World.Viewport;

			Vector3 nearPoint = new Vector3 (mousePosition, 0);
			Vector3 farPoint = new Vector3 (mousePosition, 1);

			nearPoint = viewport.Unproject (nearPoint, ProjectionMatrix, ViewMatrix, Matrix.Identity);
			farPoint = viewport.Unproject (farPoint, ProjectionMatrix, ViewMatrix, Matrix.Identity);

			Vector3 direction = farPoint - nearPoint;
			direction.Normalize ();

			return new Ray (nearPoint, direction);
		}

		/// <summary>
		/// Eine Position, um die rotiert werden soll, wenn der User die rechte Maustaste gedrückt hält und die Maus bewegt.
		/// </summary>
		public Vector3 ArcballTarget
		{
			get {
				if (World.SelectedObject != null) {
					return World.SelectedObject.Center ();
				}
				else {
					return Vector3.Zero;
				}
			}
		}

		private Vector3? smoothTarget = null;
		private float smoothDistance = 0f;
		private float smoothProgress = 0f;

		public void StartSmoothMove (Vector3 target, GameTime time)
		{
			smoothTarget = target;
			smoothDistance = Math.Abs (Target.DistanceTo (target));
			smoothProgress = 0f;
		}

		public bool InSmoothMove { get { return smoothTarget.HasValue && smoothProgress <= 1f; } }

		private void UpdateSmoothMove (GameTime time)
		{
			if (InSmoothMove) {
				float distance = MathHelper.SmoothStep (0, smoothDistance, smoothProgress);

				smoothProgress += 0.02f;

				//Console.WriteLine ("distance = " + distance);
				Target = Target.SetDistanceTo (
				             target: smoothTarget.Value,
				             distance: Math.Max (0, smoothDistance - distance)
				         );
				World.Redraw = true;
			}
		}

		/// <summary>
		/// Berechne aus einer 2D-Positon (z.b. Mausposition) die entsprechende Position im 3D-Raum.
		/// Für die fehlende dritte Koordinate wird eine Angabe einer weiteren 3D-Position benötigt,
		/// mit der die 3D-(Maus-)Position auf der selben Ebene liegen soll.
		/// </summary>
		public Vector3 To3D (Vector2 position, Vector3 nearTo)
		{
			Vector3 screenLocation = Screen.Viewport.Project (
			                             source: nearTo,
			                             projection: World.Camera.ProjectionMatrix,
			                             view: World.Camera.ViewMatrix,
			                             world: World.Camera.WorldMatrix
			                         );
			Vector3 currentMousePosition = Screen.Viewport.Unproject (
			                                   source: new Vector3 (position, screenLocation.Z),
			                                   projection: World.Camera.ProjectionMatrix,
			                                   view: World.Camera.ViewMatrix,
			                                   world: Matrix.Identity
			                               );
			return currentMousePosition;
		}

		#endregion
	}
}

