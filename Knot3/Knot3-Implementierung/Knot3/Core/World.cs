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
		public Camera Camera {
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
		public List<IGameObject> Objects { get; set; }

		private IGameObject _selectedObject;

		/// <summary>
		/// Das aktuell ausgewählte Spielobjekt.
		/// </summary>
		public IGameObject SelectedObject {
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

		public float SelectedObjectDistance {
			get {
				if (SelectedObject != null) {
					Vector3 toTarget = SelectedObject.Center () - Camera.Position;
					return toTarget.Length ();
				} else {
					return 0;
				} 
			}
		}

		/// <summary>
		/// Der aktuell angewendete Rendereffekt.
		/// </summary>
		public IRenderEffect CurrentEffect { get; set; }

		public Action<IGameObject> SelectionChanged = (o) => {};

		public bool Redraw { get; set; }

		private ResizeEffect resizeEffect;

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt eine neue Spielwelt im angegebenen Spielzustand.
		/// </summary>
		public World (GameScreen screen)
			: base(screen, DisplayLayer.GameWorld)
		{
			// die Kamera für diese Spielwelt
			_camera = new Camera (screen, this);

			// die Liste der Spielobjekte
			Objects = new List<IGameObject> ();
			
			// der Standardeffekt
			if (Options.Default ["video", "cel-shading", false]) {
				CurrentEffect = new CelShadingEffect (screen);
			} else {
				CurrentEffect = new StandardEffect (screen);
			}

			// Die relative Standard-Position und Größe
			resizeEffect = new ResizeEffect (
				screen: screen,
				relativePosition: Vector2.Zero,
				relativeSize: Vector2.One
			);
		}

		public World (GameScreen screen, Vector2 relativePosition, Vector2 relativeSize)
			: this(screen)
		{
			resizeEffect = new ResizeEffect (
				screen: screen,
				relativePosition: relativePosition,
				relativeSize: relativeSize
			);
		}

        #endregion

        #region Methods

		public void Add (IGameObject obj)
		{
			Objects.Add (obj);
			obj.World = this;
		}

		/// <summary>
		/// Ruft auf allen Spielobjekten die Update()-Methode auf.
		/// </summary>
		public override void Update (GameTime time)
		{
			if (Screen.PostProcessingEffect is FadeEffect)
				Redraw = true;

			// run the update method on all game objects
			foreach (IGameObject obj in Objects) {
				obj.Update (time);
			}
		}

		/// <summary>
		/// Ruft auf allen Spielobjekten die Draw()-Methode auf.
		/// </summary>
		public override void Draw (GameTime time)
		{
			if (Redraw) {
				Redraw = false;

				Color background = CurrentEffect is CelShadingEffect ? Color.CornflowerBlue : Color.Black;

				// begin the knot render effect
				resizeEffect.Begin (time);
				CurrentEffect.Begin (background, time);

				foreach (IGameObject obj in Objects) {
					obj.World = this;
					obj.Draw (time);
				}

				// end of the knot render effect
				CurrentEffect.End (time);
				resizeEffect.End (time);
			} else {
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
				} else {
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

        #endregion
	}
}

