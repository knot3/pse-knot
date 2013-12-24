using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
using Knot3.Utilities;

namespace Knot3.Core
{
	/// <summary>
	/// Eine Hilfsklasse für Dateioperationen.
	/// </summary>
	public static class FileUtility
	{
        #region Properties

		/// <summary>
		/// Das Einstellungsverzeichnis.
		/// </summary>
		public static string SettingsDirectory {
			get {
				string directory;
				if (MonoHelper.IsRunningOnMono ()) {
					directory = Environment.GetEnvironmentVariable ("HOME") + "/.knot3/";
				} else {
					directory = Environment.GetFolderPath (System.Environment.SpecialFolder.Personal) + "\\Knot3\\";
				}
				Directory.CreateDirectory (directory);
				return directory;
			}
		}

		/// <summary>
		/// Das Spielstandverzeichnis.
		/// </summary>
		public static string SavegameDirectory {
			get {
				string directory = SettingsDirectory + Separator + "Savegames";
				Directory.CreateDirectory (directory);
				return directory;
			}
		}

		/// <summary>
		/// Das Bildschirmfotoverzeichnis.
		/// </summary>
		public static string ScreenshotDirectory {
			get {
				string directory;
				if (MonoHelper.IsRunningOnMono ()) {
					directory = Environment.GetEnvironmentVariable ("HOME");
				} else {
					directory = Environment.GetFolderPath (System.Environment.SpecialFolder.MyPictures) + "\\Knot3\\";
				}
				Directory.CreateDirectory (directory);
				return directory;
			}
		}

		public static string BaseDirectory {
			get {
				if (baseDirectory != null) {
					return baseDirectory;
				} else {
					string cwd = Directory.GetCurrentDirectory ();
					string[] binDirectories = new string[] {"Debug", "Release", "bin"};
					foreach (string dir in binDirectories) {
						if (cwd.EndsWith (dir)) {
							cwd = cwd.Substring (0, cwd.Length - dir.Length - 1);
						}
					}
					// Environment.CurrentDirectory = cwd;
					Console.WriteLine (cwd);
					baseDirectory = cwd;
					return cwd;
				}
			}
		}

		private static string baseDirectory = null;

		public static char Separator { get { return Path.DirectorySeparatorChar; } }

        #endregion

        #region Methods

		/// <summary>
		/// Konvertiert einen Namen eines Knotens oder einer Challenge in einen gültigen Dateinamen durch Weglassen ungültiger Zeichen.
		/// </summary>
		public static string ConvertToFileName (string name)
		{
			char[] arr = name.ToCharArray ();
			arr = Array.FindAll<char> (arr, (c => (char.IsLetterOrDigit (c) 
				|| char.IsWhiteSpace (c) 
				|| c == '-'))
			);
			return new string (arr);
		}

		/// <summary>
		/// Liefert einen Hash-Wert zu der durch filename spezifizierten Datei.
		/// </summary>
		public static string GetHash (string filename)
		{
			return string.Join ("\n", FileUtility.ReadFrom (filename)).ToMD5Hash ();
		}
		
		public static string ToMD5Hash (this string TextToHash)
		{
			if (string.IsNullOrEmpty (TextToHash)) {
				return string.Empty;
			}

			MD5 md5 = new MD5CryptoServiceProvider ();
			byte[] textToHash = Encoding.Default.GetBytes (TextToHash);
			byte[] result = md5.ComputeHash (textToHash); 

			return System.BitConverter.ToString (result); 
		}
		
		public static IEnumerable<string> ReadFrom (string file)
		{
			string line;
			using (var reader = File.OpenText(file)) {
				while ((line = reader.ReadLine()) != null) {
					yield return line;
				}
			}
		}

        #endregion

	}
}

