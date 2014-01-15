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
	/// Ein Exportformat für 3D-Drucker.
	/// </summary>
	public class PrinterIO : IKnotIO
	{
		#region Properties

		/// <summary>
		/// Die gültigen Dateiendungen für das 3D-Drucker-Format.
		/// </summary>
		public IEnumerable<string> FileExtensions { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues PrinterIO-Objekt.
		/// </summary>
		public PrinterIO ()
		{
			throw new System.NotImplementedException ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Exportiert den Knoten in einem gültigen 3D-Drucker-Format.
		/// </summary>
		public virtual void Save (Knot knot)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gibt eine IOException zurück.
		/// </summary>
		public virtual Knot Load (string filename)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gibt eine IOException zurück.
		/// </summary>
		public virtual KnotMetaData LoadMetaData (string filename)
		{
			throw new System.NotImplementedException ();
		}

		#endregion
	}
}

