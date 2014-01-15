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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.KnotData
{
	/// <summary>
	/// Diese Schnittstelle enthält Methoden, die von Speicherformaten für Knoten implementiert werden müssen.
	/// </summary>
	public interface IKnotIO
	{
		#region Properties

		/// <summary>
		/// Aufzählung der Dateierweiterungen.
		/// </summary>
		IEnumerable<string> FileExtensions { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Speichert einen Knoten.
		/// </summary>
		void Save (Knot knot);

		/// <summary>
		/// Lädt einen Knoten.
		/// </summary>
		Knot Load (string filename);

		/// <summary>
		/// Lädt die Metadaten eines Knotens.
		/// </summary>
		KnotMetaData LoadMetaData (string filename);

		#endregion
	}
}

