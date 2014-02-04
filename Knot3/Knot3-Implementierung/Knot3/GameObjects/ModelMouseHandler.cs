using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using Knot3.Development;


namespace Knot3.GameObjects
{
	/// <summary>
	/// Ein Inputhandler, der Mauseingaben auf 3D-Modellen verarbeitet.
	/// </summary>
	public sealed class ModelMouseHandler : GameScreenComponent
	{
		private World World;
		private double lastRayCheck = 0;
		private Vector2 lastMousePosition = Vector2.Zero;

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines ModelMouseHandler-Objekts und ordnet dieser ein IGameScreen-Objekt screen zu,
		/// sowie eine Spielwelt world.
		/// </summary>
		public ModelMouseHandler (IGameScreen screen, World world)
		: base(screen, DisplayLayer.None)
		{
			World = world;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			CheckMouseRay (time);
		}

		private void CheckMouseRay (GameTime time)
		{
			double millis = time.TotalGameTime.TotalMilliseconds;
			if (millis > lastRayCheck + 10
			        && (Screen.Input.CurrentInputAction == InputAction.CameraTargetMove
			            || Screen.Input.CurrentInputAction == InputAction.FreeMouse)
			        && InputManager.CurrentMouseState.ToVector2 () != lastMousePosition) {
				//Log.WriteLine (Screen.Input.CurrentInputAction);
				lastRayCheck = millis;
				lastMousePosition = InputManager.CurrentMouseState.ToVector2 ();

				Profiler.ProfileDelegate ["Ray"] = () => {
					UpdateMouseRay (time);
				};
			}
		}

		private void UpdateMouseRay (GameTime time)
		{
			Ray ray = World.Camera.GetMouseRay (InputManager.CurrentMouseState.ToVector2 ());

			GameObjectDistance nearest = null;
			foreach (IGameObject obj in World.Objects) {
				if (obj.Info.IsVisible) {
					GameObjectDistance intersection = obj.Intersects (ray);
					if (intersection != null) {
						if (intersection.Distance > 0 && (nearest == null || intersection.Distance < nearest.Distance)) {
							nearest = intersection;
						}
					}
				}
			}
			if (nearest != null) {
				World.SelectedObject = nearest.Object;
			}
			else {
				World.SelectedObject = null;
			}
		}

		#endregion
	}
}
