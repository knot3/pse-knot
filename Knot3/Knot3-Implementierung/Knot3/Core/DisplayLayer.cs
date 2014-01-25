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
	/// Die Zeichenreihenfolge der Elemente der grafischen Benutzeroberfläche.
	/// </summary>
	public class DisplayLayer : IEquatable<DisplayLayer>
	{
		#region Enumeration Values

		/// <summary>
		/// Steht für die hinterste Ebene bei der Zeichenreihenfolge.
		/// </summary>
		public static readonly DisplayLayer None = new DisplayLayer (0, "None");
		/// <summary>
		/// Steht für eine Ebene hinter der Spielwelt, z.B. um
		/// Hintergrundbilder darzustellen.
		/// </summary>
		public static readonly DisplayLayer Background = new DisplayLayer (10, "Background");
		/// <summary>
		/// Steht für die Ebene in der die Spielwelt dargestellt wird.
		/// </summary>
		public static readonly DisplayLayer GameWorld = new DisplayLayer (20, "GameWorld");
		public static readonly DisplayLayer ScreenUI = new DisplayLayer (30, "ScreenUI");
		/// <summary>
		/// Steht für die Ebene in der die Dialoge dargestellt werden.
		/// Dialoge werden vor der Spielwelt gezeichnet, damit der Spieler damit interagieren kann.
		/// </summary>
		public static readonly DisplayLayer Dialog = new DisplayLayer (50, "Dialog");
		/// <summary>
		/// Steht für die Ebene in der Menüs gezeichnet werden. Menüs werden innerhalb von Dialogen angezeigt, müssen also davor gezeichnet werden, damit sie nicht vom Hintergrund des Dialogs verdeckt werden.
		/// </summary>
		public static readonly DisplayLayer Menu = new DisplayLayer (10, "Menu");
		/// <summary>
		/// Steht für die Ebene in der Menüeinträge gezeichnet werden. Menüeinträge werden vor Menüs gezeichnet.
		/// </summary>
		public static readonly DisplayLayer MenuItem = new DisplayLayer (20, "MenuItem");
		/// <summary>
		/// Zum Anzeigen zusätzlicher Informationen bei der (Weiter-)Entwicklung oder beim Testen (z.B. ein FPS-Counter).
		/// </summary>
		public static readonly DisplayLayer Overlay = new DisplayLayer (300, "Overlay");
		/// <summary>
		/// Die Maus ist das Hauptinteraktionswerkzeug, welches der Spieler
		/// ständig verwendet. Daher muss die Maus bei der Interaktion immer
		/// im Vordergrund sein. Cursor steht für die vorderste Ebene.
		/// </summary>
		public static readonly DisplayLayer Cursor = new DisplayLayer (301, "Cursor");

		#endregion

		#region Static Attributes


		#endregion

		#region Properties

		public int Index { get; private set; }

		public string Description { get; private set; }

		#endregion

		#region Constructors

		private DisplayLayer (int index, string desciption)
		{
			Index = index;
			Description = desciption;
		}

		private DisplayLayer (DisplayLayer layer1, DisplayLayer layer2)
		{
			Index = layer1.Index + layer2.Index;
			Description = layer1.Description + "+" + layer2.Description;
		}

		#endregion

		#region Methods and Operators

		public override string ToString ()
		{
			return Description;
		}

		public static DisplayLayer operator + (DisplayLayer layer1, DisplayLayer layer2)
		{
			return new DisplayLayer (layer1, layer2);
		}

		public static DisplayLayer operator + (DisplayLayer layer, Widget widget)
		{
			return new DisplayLayer (widget.Index, layer);
		}

		public static DisplayLayer operator + (Widget widget, DisplayLayer layer)
		{
			return new DisplayLayer (widget.Index, layer);
		}

		public static DisplayLayer operator * (DisplayLayer layer, int i)
		{
			return new DisplayLayer (layer.Index * i, "(" + layer + "*" + i + ")");
		}

		public static bool operator == (DisplayLayer a, DisplayLayer b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null)) {
				return false;
			}

			// Return true if the fields match:
			return a.Index == b.Index;
		}

		public static bool operator != (DisplayLayer d1, DisplayLayer d2)
		{
			return !(d1 == d2);
		}

		public bool Equals (DisplayLayer other)
		{
			return other != null && Index == other.Index;
		}

		public override bool Equals (object other)
		{
			return other != null && Equals (other as DisplayLayer);
		}

		public static implicit operator string (DisplayLayer layer)
		{
			return layer.Description;
		}

		public static implicit operator int (DisplayLayer layer)
		{
			return layer.Index;
		}

		public override int GetHashCode ()
		{
			return Description.GetHashCode ();
		}

		#endregion
	}
}

