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
using Knot3.Audio.XNA;
using Knot3.Audio.Ogg;

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
			{ Sound.CreativeMusic,		"Music-Creative" },
			{ Sound.ChallengeMusic,		"Music-Challenge" },
			{ Sound.MenuMusic,			"Music-Menu" },
			{ Sound.PipeSound,			"Sound-Pipe" },
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
		public AudioManager (GameScreen screen)
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
			filepath = filepath.Replace (".xnb", "").Replace (@"Content\", "").Replace ("Content/", "");

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
				Console.WriteLine ("Load sound effect (" + soundType + "): " + filepath);
			}
			catch (Exception ex) {
				// wenn man versucht, einen "Song" als "SoundEffect" zu laden,
				// dann bekommt man unter Windows eine "ContentLoadException"
				// und unter Linux eine "InvalidCastException"
				if (ex is ContentLoadException || ex is InvalidCastException) {
					LoadXnaSong (filepath, name, soundType);
				}
				else {
					throw;
				}
			}
		}

		private void LoadXnaSong (string filepath, string name, Sound soundType)
		{
			// nur unter Windows
			if (MonoHelper.IsRunningOnMono ()) {
				return;
			}

			try {
				// versuche, die Audiodatei als "Song" zu laden
				Song song = Screen.Content.Load<Song> (filepath);
				AudioFiles [soundType].Add (new SongFile (name, song, soundType));
				Console.WriteLine ("Load song (" + soundType + "): " + filepath);
			}
			catch (Exception ex) {
				// egal, warum das laden nicht klappt; mehr als die Fehlermeldung anzeigen
				// macht wegen einer fehlenden Musikdatei keinen Sinn

				Console.WriteLine ("Failed to load audio file (" + soundType + "): " + filepath);
				Console.WriteLine (ex.ToString ());
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
				Console.WriteLine ("Load ogg audio file (" + soundType + "): " + filepath);
				AudioFiles [soundType].Add (new OggVorbisFile (name, filepath, soundType));
			}
			catch (Exception ex) {
				// egal, warum das laden nicht klappt; mehr als die Fehlermeldung anzeigen
				// macht wegen einer fehlenden Musikdatei keinen Sinn
				Console.WriteLine ("Failed to load ffmpeg audio file (" + soundType + "): " + filepath);
				Console.WriteLine (ex.ToString ());
			}
		}

		private void StartBackgroundMusic ()
		{
			if (Playlist != null) {
				Playlist.Stop ();
			}
			Console.WriteLine ("Background Music: " + BackgroundMusic);
			Playlist = new LoopPlaylist (AudioFiles [BackgroundMusic]);
			Playlist.Start ();
		}

		public void PlaySound (Sound sound)
		{
			if (AudioFiles [sound].Count > 0) {
				AudioFiles [sound].RandomElement ().Play ();
			}
			else {
				Console.WriteLine ("There are no audio files for: " + sound);
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
			Console.WriteLine ("UnloadContent ()");
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
			Console.WriteLine("Set Volume ("+soundType+"): "+volume);
		}

		public static float ValidVolume (float volume)
		{
			return MathHelper.Clamp (volume, 0.0f, 2.0f);
		}
	}
}

