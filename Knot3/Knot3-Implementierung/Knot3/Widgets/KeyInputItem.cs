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
using Knot3.KnotData;
using Knot3.Utilities;
using Knot3.Development;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein Menüeintrag, der einen Tastendruck entgegennimmt und in der enthaltenen Option als Zeichenkette speichert.
	/// </summary>
	public sealed class KeyInputItem : InputItem
	{
		#region Properties

		/// <summary>
		/// Die Option in einer Einstellungsdatei.
		/// </summary>
		private KeyOptionInfo option { get; set; }

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public override float NameWidth
		{
			get { return Math.Min (0.70f, 1.0f - ValueWidth); }
			set { throw new ArgumentException ("You can't change the NameWidth of a KeyInputItem!"); }
		}

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public override float ValueWidth
		{
			get { return 3 * Bounds.Size.Relative.Y / Bounds.Size.Relative.X; }
			set { throw new ArgumentException ("You can't change the ValueWidth of a KeyInputItem!"); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und der Eingabeoption Pflicht.
		/// </summary>
		public KeyInputItem (IGameScreen screen, DisplayLayer drawOrder, string text, KeyOptionInfo option)
		: base(screen, drawOrder, text, (option as DistinctOptionInfo).Value)
		{
			this.option = option;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Speichert die aktuell gedrückte Taste in der Option.
		/// </summary>
		public override void OnKeyEvent (List<Keys> keys, KeyEvent keyEvent, GameTime time)
		{
			if (keys.Count > 0) {
				Keys key = keys [0];
				if (KnotInputHandler.CurrentKeyAssignment.Count == 0) {
					KnotInputHandler.ReadKeyAssignments ();
				}
				Log.Debug ("Trying to assign key: ", key);
				if (KnotInputHandler.CurrentKeyAssignment.ContainsKey (key) && option.Value != key) {
					Log.Debug ("Key ", key, " is already assigned to: ", KnotInputHandler.CurrentKeyAssignment [key]);
				}
				else {
					Log.Debug ("Key ", key, " => ", option.Name);
					option.Value = key;
					InputText = (option as DistinctOptionInfo).Value;
					IsInputEnabled = false;
					OnValueChanged ();
					OnValueSubmitted ();
					KnotInputHandler.ReadKeyAssignments ();
				}
			}
		}


		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			if (IsInputEnabled) {
				ValidKeys.Clear ();
				IsInputEnabled = false;
			}
			else {
				ValidKeys.AddRange (typeof(Keys).ToEnumValues<Keys> ());
				IsInputEnabled = true;
				InputText = String.Empty;
			}
		}

		#endregion
	}
}
