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
using Knot3.Audio;

namespace Knot3.Screens
{
	/// <summary>
	/// Die Klasse AudioSettingsScreen steht für den Spielzustand, der die Audio-Einstellungen repräsentiert.
	/// </summary>
	public class AudioSettingsScreen : SettingsScreen
	{
		#region Properties

		/// <summary>
		/// Das Menü, das die Einstellungen enthält.
		/// </summary>
		private VerticalMenu settingsMenu { get; set; }

		private Dictionary<string, HashSet<Sound>> soundCategories = new Dictionary<string, HashSet<Sound>>()
		{
			{
				"Music",
				new HashSet<Sound>() {
					Sound.CreativeMusic,
					Sound.ChallengeMusic,
					Sound.MenuMusic,
				}
			}, {
				"Sound",
				new HashSet<Sound>()
				{
					Sound.PipeMoveSound,
				}
			},
		};

		private Action UpdateSliders = () => {};

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues AudioSettingsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public AudioSettingsScreen (Knot3Game game)
		: base(game)
		{
			MenuName = "Audio";

			settingsMenu = new VerticalMenu (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			settingsMenu.RelativePosition = () => new Vector2 (0.400f, 0.180f);
			settingsMenu.RelativeSize = () => new Vector2 (0.500f, 0.770f);
			settingsMenu.RelativePadding = () => new Vector2 (0.010f, 0.010f);
			settingsMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			settingsMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			settingsMenu.ItemAlignX = HorizontalAlignment.Left;
			settingsMenu.ItemAlignY = VerticalAlignment.Center;

			foreach (KeyValuePair<string, HashSet<Sound>> soundCategory in soundCategories) {
				string volumeName = soundCategory.Key;
				HashSet<Sound> sounds = soundCategory.Value;

				SliderItem slider = new SliderItem (
				    screen: this,
				    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
				    text: volumeName,
				    max: 100,
				    min: 0,
				    step: 1,
				    value: 50
				);
				slider.OnValueChanged = () => {
					float volume = (float)slider.Value / 100f;
					foreach (Sound sound in sounds) {
						AudioManager.SetVolume (soundType: sound, volume: volume);
					}
				};
				settingsMenu.Add (slider);
				UpdateSliders += () => {
					float volume = 0f;
					foreach (Sound sound in sounds) {
						volume += AudioManager.Volume (soundType: sound) * 100f;
					}
					volume /= sounds.Count;
					slider.Value = (int)volume;
				};
			}
			UpdateSliders();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Fügt das Menü mit den Einstellungen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime GameTime)
		{
			base.Entered (previousScreen, GameTime);
			AddGameComponents (GameTime, settingsMenu);
			UpdateSliders();
		}

		#endregion
	}
}

