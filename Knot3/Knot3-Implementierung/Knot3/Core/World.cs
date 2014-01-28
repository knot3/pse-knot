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
	/// Repräsentiert eine Spielwelt, in der sich 3D-Modelle befinden und gezeichnet werden können.
	/// </summary>
	public sealed class World : DrawableGameScreenComponent, IEnumerable<IGameObject>
	{
		#region Properties

		/// <summary>
		/// Die Kamera dieser Spielwelt.
		/// </summary>
		public Camera Camera
		{
			get {
				return _camera;
			}
			set {
				_camera = value;
				useInternalCamera = false;
			}
		}

		private Camera _camera;
		private bool useInternalCamera = true;

		/// <summary>
		/// Die Liste von Spielobjekten.
		/// </summary>
		public HashSet<IGameObject> Objects { get; set; }

		private IGameObject _selectedObject;

		/// <summary>
		/// Das aktuell ausgewählte Spielobjekt.
		/// </summary>
		public IGameObject SelectedObject
		{
			get {
				return _selectedObject;
			}
			set {
				if (_selectedObject != value) {
					_selectedObject = value;
					SelectionChanged (_selectedObject);
					Redraw = true;
				}
			}
		}

		public float SelectedObjectDistance
		{
			get {
				if (SelectedObject != null) {
					Vector3 toTarget = SelectedObject.Center () - Camera.Position;
					return toTarget.Length ();
				}
				else {
					return 0;
				}
			}
		}

		/// <summary>
		/// Der aktuell angewendete Rendereffekt.
		/// </summary>
		public IRenderEffect CurrentEffect { get; set; }

		public Action<IGameObject> SelectionChanged = (o) =>
		{
		};

		public bool Redraw { get; set; }

		public Bounds Bounds { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Spielwelt im angegebenen Spielzustand.
		/// </summary>
		public World (IGameScreen screen, DisplayLayer drawIndex, IRenderEffect effect, Bounds bounds)
		: base (screen, drawIndex)
		{
			// die Kamera für diese Spielwelt
			_camera = new Camera (screen, this);

			// die Liste der Spielobjekte
			Objects = new HashSet<IGameObject> ();

			CurrentEffect = effect;

			// Die relative Standard-Position und Größe
			Bounds = bounds;

			Screen.Game.FullScreenChanged += () => viewportCache.Clear ();
		}

		public World (IGameScreen screen, DisplayLayer drawIndex, IRenderEffect effect)
		: this (screen, drawIndex, effect, DefaultBounds(screen))
		{
		}

		public World (IGameScreen screen, DisplayLayer drawIndex, Bounds bounds)
		: this (screen, drawIndex, DefaultEffect(screen), bounds)
		{
		}

		public World (IGameScreen screen, IRenderEffect effect, Bounds bounds)
		: this (screen, DisplayLayer.GameWorld, effect, bounds)
		{
		}

		public World (IGameScreen screen, IRenderEffect effect)
		: this (screen, DisplayLayer.GameWorld, effect, DefaultBounds(screen))
		{
		}

		public World (IGameScreen screen, Bounds bounds)
		: this (screen, DisplayLayer.GameWorld, DefaultEffect(screen), bounds)
		{
		}

		private static Bounds DefaultBounds (IGameScreen screen)
		{
			return new Bounds (
			           position: new ScreenPoint (screen, Vector2.Zero),
			           size: new ScreenPoint (screen, Vector2.One)
			       );
		}

		private static IRenderEffect DefaultEffect (IGameScreen screen)
		{
			// suche den eingestellten Standardeffekt heraus
			string effectName = Options.Default ["video", "knot-shader", "default"];
			IRenderEffect effect = RenderEffectLibrary.CreateEffect (screen: screen, name: effectName);
			return effect;
		}

		#endregion

		#region Methods

		public void Add (IGameObject obj)
		{
			if (obj != null) {
				Objects.Add (obj);
				obj.World = this;
			}
		}

		public void Remove (IGameObject obj)
		{
			if (obj != null) {
				Objects.Remove (obj);
			}
		}

		/// <summary>
		/// Ruft auf allen Spielobjekten die Update()-Methode auf.
		/// </summary>
		public override void Update (GameTime time)
		{
			if (Screen.PostProcessingEffect is FadeEffect) {
				Redraw = true;
			}

			// run the update method on all game objects
			foreach (IGameObject obj in Objects) {
				obj.Update (time);
			}
		}

		private Dictionary<Point,Dictionary<Vector4, Viewport>> viewportCache
		    = new Dictionary<Point,Dictionary<Vector4, Viewport>> ();

		public Viewport Viewport
		{
			get {
				PresentationParameters pp = Screen.Device.PresentationParameters;
				Point resolution = new Point (pp.BackBufferWidth, pp.BackBufferHeight);
				Vector4 key = Bounds.Vector4;
				if (!viewportCache.ContainsKey (resolution)) {
					viewportCache [resolution] = new Dictionary<Vector4, Viewport> ();
				}
				if (!viewportCache [resolution].ContainsKey (key)) {
					Rectangle subScreen = Bounds.Rectangle;
					viewportCache [resolution] [key] = new Viewport (subScreen.X, subScreen.Y, subScreen.Width, subScreen.Height) {
						MinDepth = 0,
						MaxDepth = 1
					};
				}
				return viewportCache [resolution] [key];
			}
		}

		/// <summary>
		/// Ruft auf allen Spielobjekten die Draw()-Methode auf.
		/// </summary>
		public override void Draw (GameTime time)
		{
			if (Redraw) {
				Redraw = false;

				//Screen.BackgroundColor = CurrentEffect is CelShadingEffect ? Color.CornflowerBlue : Color.Black;

				// begin the knot render effect
				CurrentEffect.Begin (time);

				foreach (IGameObject obj in Objects) {
					obj.World = this;
					obj.Draw (time);
				}

				// end of the knot render effect
				CurrentEffect.End (time);
			}
			else {
				Screen.PostProcessingEffect.DrawLastFrame (time);
			}
		}

		/// <summary>
		/// Liefert einen Enumerator über die Spielobjekte dieser Spielwelt.
		/// [returntype=IEnumerator<IGameObject>]
		/// </summary>
		public IEnumerator<IGameObject> GetEnumerator ()
		{
			foreach (IGameObject obj in flat(Objects)) {
				yield return obj;
			}
		}

		private IEnumerable<IGameObject> flat (IEnumerable<IGameObject> enumerable)
		{
			foreach (IGameObject obj in enumerable) {
				if (obj is IEnumerable<IGameObject>) {
					foreach (IGameObject subobj in flat(obj as IEnumerable<IGameObject>)) {
						yield return subobj;
					}
				}
				else {
					yield return obj;
				}
			}
		}
		// Explicit interface implementation for nongeneric interface
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // Just return the generic version
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			if (useInternalCamera) {
				yield return Camera;
			}
		}

		/// <summary>
		/// Gibt einen Iterator über alle Spielobjekte zurück, der so sortiert ist, dass die
		/// Spielobjekte, die der angegebenen 2D-Position am nächsten sind, am Anfang stehen.
		/// Dazu wird die 2D-Position in eine 3D-Position konvertiert.
		/// </summary>
		public IEnumerable<IGameObject> FindNearestObjects (Vector2 nearTo)
		{
			Dictionary<float, IGameObject> distances = new Dictionary<float, IGameObject> ();
			foreach (IGameObject obj in this) {
				if (obj.Info.IsSelectable) {
					// Berechne aus der angegebenen 2D-Position eine 3D-Position
					Vector3 position3D = Camera.To3D (
					                         position: nearTo,
					                         nearTo: obj.Center ()
					                     );
					// Berechne die Distanz zwischen 3D-Mausposition und dem Spielobjekt
					float distance = Math.Abs ((position3D - obj.Center ()).Length ());
					distances [distance] = obj;
				}
			}
			if (distances.Count > 0) {
				IEnumerable<float> sorted = distances.Keys.OrderBy (k => k);
				foreach (float where in sorted) {
					yield return distances [where];
					// Console.WriteLine ("where=" + where + " = " + distances [where].Center ());
				}
			}
			else {
				yield break;
			}
		}

		/// <summary>
		/// Gibt einen Iterator über alle Spielobjekte zurück, der so sortiert ist, dass die
		/// Spielobjekte, die der angegebenen 3D-Position am nächsten sind, am Anfang stehen.
		/// </summary>
		public IEnumerable<IGameObject> FindNearestObjects (Vector3 nearTo)
		{
			Dictionary<float, IGameObject> distances = new Dictionary<float, IGameObject> ();
			foreach (IGameObject obj in this) {
				if (obj.Info.IsSelectable) {
					// Berechne die Distanz zwischen 3D-Mausposition und dem Spielobjekt
					float distance = Math.Abs ((nearTo - obj.Center ()).Length ());
					distances [distance] = obj;
				}
			}
			if (distances.Count > 0) {
				IEnumerable<float> sorted = distances.Keys.OrderBy (k => k);
				foreach (float where in sorted) {
					yield return distances [where];
				}
			}
			else {
				yield break;
			}
		}

		#endregion
	}
}

