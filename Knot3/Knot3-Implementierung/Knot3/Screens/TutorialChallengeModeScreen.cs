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
	/// Eine Einführung in das Spielen von Challenges.
	/// Der Spieler wird dabei durch Anweisungen an das Lösen von Challenges herangeführt.
	/// </summary>
	public class TutorialChallengeModeScreen : ChallengeModeScreen
	{
		#region Constructors

		public TutorialChallengeModeScreen (Knot3Game game, Challenge challenge)
		: base(game, challenge)
		{
			throw new System.NotImplementedException ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Fügt die Tutoriellanzeige in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
