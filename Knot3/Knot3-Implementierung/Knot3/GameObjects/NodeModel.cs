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

namespace Knot3.GameObjects
{
	/// <summary>
	/// Ein 3D-Modell, das einen Kantenübergang darstellt.
	/// </summary>
	public sealed class NodeModel : GameModel
	{
		#region Properties

		/// <summary>
		/// Enthält Informationen über den darzustellende 3D-Modell des Kantenübergangs.
		/// </summary>
		public new NodeModelInfo Info { get { return base.Info as NodeModelInfo; } set { base.Info = value; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues 3D-Modell mit dem angegebenen Spielzustand und dem angegebenen Informationsobjekt.
		/// [base=screen, info]
		/// </summary>
		public NodeModel (IGameScreen screen, NodeModelInfo info)
		: base(screen, info)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das 3D-Modell mit dem aktuellen Rendereffekt.
		/// </summary>
		public override void Draw (GameTime time)
		{
			Coloring = new GradientColor (Info.EdgeFrom, Info.EdgeTo);
			base.Draw (time);
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			base.Update (time);
		}

		#endregion
	}
}
