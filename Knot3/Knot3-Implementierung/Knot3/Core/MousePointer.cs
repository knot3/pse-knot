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
    /// Repräsentiert einen Mauszeiger.
    /// </summary>
    public class MousePointer : DrawableGameScreenComponent
    {
		private SpriteBatch spriteBatch;

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Mauszeiger für den angegebenen Spielzustand.
        /// </summary>
        public MousePointer (GameScreen screen)
			: base(screen, DisplayLayer.Cursor)
        {
			spriteBatch = new SpriteBatch (screen.Device);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zeichnet den Mauszeiger.
        /// </summary>
        public override void Draw (GameTime time)
        {
			DrawCursor (time);
        }

		private void DrawCursor (GameTime time)
		{
			if (!MonoHelper.IsRunningOnMono ()) {
				spriteBatch.Begin ();
            
				Texture2D cursorTex = Screen.Content.Load<Texture2D> ("cursor");
				if (Screen.Input.GrabMouseMovement || Screen.Input.CurrentInputAction == InputAction.CameraTargetMove
					|| (Screen.Input.CurrentInputAction == InputAction.ArcballMove
					&& (InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed
				    || InputManager.CurrentMouseState.RightButton == ButtonState.Pressed))) {
					spriteBatch.Draw (cursorTex, Screen.Device.Viewport.Center (), Color.White);
				} else {
					spriteBatch.Draw (cursorTex, new Vector2 (InputManager.CurrentMouseState.X, InputManager.CurrentMouseState.Y), Color.White);
				}

				spriteBatch.End ();
			}
		}

        #endregion

    }
}

