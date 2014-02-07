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
using Knot3.GameObjects;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Screens
{
	/// <summary>
	/// Der Spielzustand, der die Auflistung der Mitwirkenden darstellt.
	/// </summary>
	public class ErrorScreen : GameScreen
	{
		ErrorDialog dialog;

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CreditsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public ErrorScreen (Knot3Game game, Exception ex)
		: base(game)
		{
			string msg = CreateMessage (ex);
			dialog = new ErrorDialog (screen: this, drawIndex: DisplayLayer.Dialog, message: msg);
		}

		#endregion

		#region Methods

		private string CreateMessage (Exception ex)
		{
			return ex.ToString ();
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
		}

		/// <summary>
		/// Fügt das Menü mit den Mitwirkenden in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, dialog);
		}

		#endregion
	}
}
