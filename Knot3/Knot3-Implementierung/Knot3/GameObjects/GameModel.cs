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

using Knot3.Core;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Repräsentiert ein 3D-Modell in einer Spielwelt.
	/// </summary>
	public abstract class GameModel : IGameObject
	{
		#region Properties

		GameObjectInfo IGameObject.Info { get { return Info; } }

		/// <summary>
		/// Die Transparenz des Modells.
		/// </summary>
		public float Alpha { get; set; }

		/// <summary>
		/// Die Farbe des Modells.
		/// </summary>
		public Color BaseColor { get; set; }

		/// <summary>
		/// Die Auswahlfarbe des Modells.
		/// </summary>
		public Color HighlightColor { get; set; }

		/// <summary>
		/// Die Intensität der Auswahlfarbe.
		/// </summary>
		public float HighlightIntensity { get; set; }

		/// <summary>
		/// Die Modellinformationen wie Position, Skalierung und der Dateiname des 3D-Modells.
		/// </summary>
		public GameModelInfo Info { get; protected set; }

		/// <summary>
		/// Die Klasse des XNA-Frameworks, die ein 3D-Modell repräsentiert.
		/// </summary>
		public virtual Model Model { get { return ModelHelper.LoadModel (screen, Info.Modelname); } }

		/// <summary>
		/// Die Spielwelt, in der sich das 3D-Modell befindet.
		/// </summary>
		public World World
		{
			get { return _world; }
			set {
				_world = value;
				_world.Camera.OnViewChanged += OnViewChanged;
				OnViewChanged ();
			}
		}

		private World _world;

		/// <summary>
		/// Die Weltmatrix des 3D-Modells in der angegebenen Spielwelt.
		/// </summary>
		public Matrix WorldMatrix
		{
			get {
				UpdatePrecomputed ();
				return _worldMatrix;
			}
		}

		protected IGameScreen screen;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues 3D-Modell in dem angegebenen Spielzustand mit den angegebenen Modellinformationen.
		/// </summary>
		public GameModel (IGameScreen screen, GameModelInfo info)
		{
			this.screen = screen;
			Info = info;

			// default values
			Alpha = 1f;
			BaseColor = Color.Transparent;
			HighlightColor = Color.Transparent;
			HighlightIntensity = 0f;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gibt die Mitte des 3D-Modells zurück.
		/// </summary>
		public virtual Vector3 Center ()
		{
			Vector3 center = Vector3.Zero;
			int count = Model.Meshes.Count;
			foreach (ModelMesh mesh in Model.Meshes) {
				center += mesh.BoundingSphere.Center / count;
			}
			return center / Info.Scale + Info.Position;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public virtual void Update (GameTime time)
		{
		}

		/// <summary>
		/// Zeichnet das 3D-Modell in der angegebenen Spielwelt mit dem aktuellen Rendereffekt der Spielwelt.
		/// </summary>
		public virtual void Draw (GameTime time)
		{
			if (Info.IsVisible) {
				if (InCameraFrustum) {
					screen.CurrentRenderEffects.CurrentEffect.DrawModel (this, time);
				}
			}
		}

		/// <summary>
		/// Überprüft, ob der Mausstrahl das 3D-Modell schneidet.
		/// </summary>
		public virtual GameObjectDistance Intersects (Ray ray)
		{
			foreach (BoundingSphere sphere in Bounds) {
				float? distance = ray.Intersects (sphere);
				if (distance != null) {
					GameObjectDistance intersection = new GameObjectDistance () {
						Object=this, Distance=distance.Value
					};
					return intersection;
				}
			}
			return null;
		}

		#endregion

		#region Cache

		private Vector3 _scale;
		private Angles3 _rotation;
		private Vector3 _position;
		private Matrix _worldMatrix;
		private BoundingSphere[] _bounds;
		private bool _inFrustum;

		public virtual BoundingSphere[] Bounds
		{
			get {
				UpdatePrecomputed ();
				return _bounds;
			}
		}

		protected bool InCameraFrustum
		{
			get {
				return _inFrustum;
			}
		}

		private void UpdatePrecomputed ()
		{
			if (Info.Scale != _scale || Info.Rotation != _rotation || Info.Position != _position) {

				// world matrix
				_worldMatrix = Matrix.CreateScale (Info.Scale)
				               * Matrix.CreateFromYawPitchRoll (Info.Rotation.Y, Info.Rotation.X, Info.Rotation.Z)
				               * Matrix.CreateTranslation (Info.Position);

				// bounding spheres
				_bounds = Model.Bounds ().ToArray ();
				for (int i = 0; i < _bounds.Length; ++i) {
					_bounds [i] = _bounds [i].Scale (Info.Scale).Rotate (Info.Rotation).Translate ((Vector3)Info.Position);
				}

				// attrs
				_scale = Info.Scale;
				_rotation = Info.Rotation;
				_position = Info.Position;
			}
		}

		private void OnViewChanged ()
		{
			// camera frustum
			_inFrustum = false;
			foreach (BoundingSphere _sphere in Bounds) {
				var sphere = _sphere;
				if (World.Camera.ViewFrustum.FastIntersects (ref sphere)) {
					_inFrustum = true;
					break;
				}
			}
		}

		#endregion
	}
}

