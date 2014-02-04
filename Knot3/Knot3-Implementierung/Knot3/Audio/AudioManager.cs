using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.IO;

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
using Knot3.Widgets;
using Knot3.Utilities;
using Knot3.Development;

namespace Knot3.Audio
{
	public class AudioManager : DrawableGameScreenComponent
	{
		/// <summary>
		/// Eine Zuordnung zwischen dem Typ der Audiodateien und den Ordnern unter "Content/",
		/// in denen sich die Audiodateien befinden.
		/// </summary>
		private static readonly Dictionary<Sound, string> AudioDirectories
		= new Dictionary<Sound, string> {
			{ Sound.CreativeMusic,			"Music/Creative" },
			{ Sound.ChallengeMusic,			"Music/Challenge" },
			{ Sound.MenuMusic,				"Music/Menu" },
			{ Sound.PipeMoveSound,			"Sound/Pipe/Move" },
			{ Sound.PipeInvalidMoveSound,	"Sound/Pipe/Invalid-Move" },
		};

		// Enthält alle gefunden Audiodateien, sortiert nach ihrem Zweck
		private static Dictionary<Sound, HashSet<IAudioFile>> AudioFiles
		    = new Dictionary<Sound, HashSet<IAudioFile>> ();

		/// <summary>
		/// Die aktuell verwendete Hintergrundmusik.
		/// </summary>
		public Sound BackgroundMusic
		{
			get {
				return _backgroundMusic;
			}
			set {
				if (value != Sound.None && value != _backgroundMusic) {
					_backgroundMusic = value;
					StartBackgroundMusic ();
				}
			}
		}

		private static Sound _backgroundMusic = Sound.None;

		/// <summary>
		/// Enthält die Playlist, die aktuell abgespielt wird,
		/// oder null, falls keine Playlist abgespielt wird.
		/// </summary>
		public static IPlaylist Playlist { get; set; }

		private static Dictionary<Sound, float> VolumeMap = new Dictionary<Sound, float> ();

		/// <summary>
		/// Erstellt einen neuen AudioManager für den angegebenen Spielzustand.
		/// </summary>
		public AudioManager (IGameScreen screen)
		: base(screen, DisplayLayer.None)
		{
			if (AudioFiles.Count == 0) {
				// Erstelle für alle Enum-Werte von Sound ein HashSet
				foreach (Sound soundType in typeof(Sound).ToEnumValues<Sound>()) {
					AudioFiles [soundType] = new HashSet<IAudioFile> ();
					VolumeMap [soundType] = ValidVolume (Options.Default ["volume", soundType.ToString (), 1]);
				}

				// Suche nach XNA-Audio-Dateien
				FileUtility.SearchFiles (".", new string[] {".xnb"}, AddXnaAudioFile);

				// Suche nach OGG-Dateien
				FileUtility.SearchFiles (".", new string[] {".ogg"}, AddOggAudioFile);
			}
		}

		private void AddXnaAudioFile (string filepath)
		{
			filepath = filepath.Replace(".xnb", String.Empty).Replace(@"Content\", String.Empty).Replace("Content/", String.Empty).Replace(@"\", "/");

			foreach (KeyValuePair<Sound,string> pair in AudioDirectories) {
				Sound soundType = pair.Key;
				string directory = pair.Value;
				if (filepath.ToLower ().Contains (directory.ToLower ())) {
					string name = Path.GetFileName (filepath);
					LoadXnaSoundEffect (filepath, name, soundType);
					break;
				}
			}
		}

		private void LoadXnaSoundEffect (string filepath, string name, Sound soundType)
		{
			try {
				// versuche, die Audiodatei als "SoundEffect" zu laden
				SoundEffect soundEffect = Screen.Content.Load<SoundEffect> (filepath);
				AudioFiles [soundType].Add (new SoundEffectFile (name, soundEffect, soundType));
				Log.Debug ("Load sound effect (" + soundType + "): " + filepath);
			}
			catch (Exception ex) {
				Log.Debug (ex);
			}
		}

		private void AddOggAudioFile (string filepath)
		{
			filepath = filepath.Replace (@"\", "/");

			foreach (KeyValuePair<Sound,string> pair in AudioDirectories) {
				Sound soundType = pair.Key;
				string directory = pair.Value;
				if (filepath.ToLower ().Contains (directory.ToLower ())) {
					string name = Path.GetFileName (filepath);
					LoadOggAudioFile (filepath, name, soundType);
					break;
				}
			}
		}

		private void LoadOggAudioFile (string filepath, string name, Sound soundType)
		{
			try {
				// erstelle ein AudioFile-Objekt
				Log.Debug ("Load ogg audio file (" + soundType + "): " + filepath);
				AudioFiles [soundType].Add (new OggVorbisFile (name, filepath, soundType));
			}
			catch (Exception ex) {
				// egal, warum das laden nicht klappt; mehr als die Fehlermeldung anzeigen
				// macht wegen einer fehlenden Musikdatei keinen Sinn
				Log.Debug ("Failed to load ffmpeg audio file (" + soundType + "): " + filepath);
				Log.Debug (ex.ToString ());
			}
		}

		private void StartBackgroundMusic ()
		{
			if (Playlist != null) {
				Playlist.Stop ();
			}
			Log.Debug ("Background Music: " + BackgroundMusic);
			Playlist = new LoopPlaylist (AudioFiles [BackgroundMusic]);
			Playlist.Shuffle ();
			Playlist.Start ();
		}

		public void PlaySound (Sound sound)
		{
			Log.Debug ("Sound: " + sound);
			if (AudioFiles [sound].Count > 0) {
				AudioFiles [sound].RandomElement ().Play ();
			}
			else {
				Log.Debug ("There are no audio files for: " + sound);
			}
		}

		public override void Update (GameTime time)
		{
			if (Playlist != null) {
				Playlist.Update (time);
			}
			base.Update (time);
		}

		protected override void UnloadContent ()
		{
			Log.Debug ("UnloadContent ()");
			Playlist.Stop ();
			base.UnloadContent ();
		}

		public static float Volume (Sound soundType)
		{
			return VolumeMap [soundType];
		}

		public static void SetVolume (Sound soundType, float volume)
		{
			volume = ValidVolume (volume);
			VolumeMap [soundType] = volume;
			Options.Default ["volume", soundType.ToString (), 1] = volume;
			Log.Debug ("Set Volume (" + soundType + "): " + volume);
		}

		public static float ValidVolume (float volume)
		{
			return MathHelper.Clamp (volume, 0.0f, 2.0f);
		}
	}
}
