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
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.KnotData
{
	public interface ISavegameIO<Savegame, SavegameMetaData>
	{
		#region Properties

		/// <summary>
		/// Aufzählung der Dateierweiterungen.
		/// </summary>
		IEnumerable<string> FileExtensions { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Speichert einen Spielstand.
		/// </summary>
		void Save (Savegame knot);

		/// <summary>
		/// Lädt einen Spielstand.
		/// </summary>
		Savegame Load (string filename);

		/// <summary>
		/// Lädt die Metadaten eines Spielstands.
		/// </summary>
		SavegameMetaData LoadMetaData (string filename);

		#endregion
	}
}
