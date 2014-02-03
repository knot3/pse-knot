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
using Knot3.Audio;

namespace Knot3.GameObjects
{
	public class EdgeRectangles : GameScreenComponent, IKeyEventListener
	{
		public Knot Knot { get; set; }

		private Random random = new Random ();

		public EdgeRectangles (GameScreen screen)
		: base(screen, DisplayLayer.None)
		{
			ValidKeys = new List<Keys> ();
			ValidKeys.Add (Keys.N);
		}

		public override void Update (GameTime time)
		{
		}

		public void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			// Soll die Farbe geändert wurde?
			if (   Knot.SelectedEdges.Any () 
                && Keys.N.IsDown ()) {
				int rectId = random.Next ();
				foreach (Edge edge in Knot.SelectedEdges) {
					edge.Rectangles.Add (rectId);
					Console.WriteLine ("edge=" + edge + ", edge.Rectangles=" + string.Join (",", edge.Rectangles));
				}
				Knot.EdgesChanged ();
			}
		}

		public List<Keys> ValidKeys { get; private set; }

		public bool IsKeyEventEnabled { get { return true; } }

		public bool IsModal { get { return false; } }
	}
}
