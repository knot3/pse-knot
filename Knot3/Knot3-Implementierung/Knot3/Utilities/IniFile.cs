using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Knot3.Utilities
{
	public sealed class IniFile : IDisposable
	{
		private string Filename;
		public Dictionary<string, Dictionary<string, string>> Data;

		public IniFile (string filename)
		{
			Data = new Dictionary<string, Dictionary<string, string>> ();
			Filename = filename;
			if (File.Exists (filename)) {
				using (StreamReader reader = new StreamReader(filename)) {
					string section = null;
					while (reader.Peek() != -1) {
						string line = StripComments (reader.ReadLine ().Trim ());
						if (line.StartsWith ("[") && line.EndsWith ("]")) {
							section = line.Substring (1, line.Length - 2);
							if (!Data.ContainsKey (section)) {
								Data [section] = new Dictionary<string,string> ();
							}
						}
						else if (line.Contains ("=")) {
							string[] parts = line.Split ('=');
							if (section != null) {
								Data [section] [parts [0].Trim ()] = parts [1].Trim ();
							}
						}
					}
				}
			}
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		private void Dispose (bool disposing)
		{
			if (disposing) {
				Save ();
			}
		}

		public void Save ()
		{
			using (StreamWriter writer = new StreamWriter(Filename)) {
				foreach (string section in Data.Keys.OrderBy(x => x)) {
					writer.WriteLine ("[" + section + "]");
					foreach (string key  in Data[section].Keys.OrderBy(x => x)) {
						writer.WriteLine (key + "=" + Data [section] [key]);
					}
				}
			}
		}

		private static string StripComments (string line)
		{
			if (line != null) {
				if (line.IndexOf (';') != -1) {
					return line.Remove (line.IndexOf (';')).Trim ();
				}
				return line.Trim ();
			}
			return string.Empty;
		}

		public string this [string section, string key, string defaultValue = null]
		{
			get {
				if (!Data.ContainsKey (section)) {
					Data [section] = new Dictionary<string,string> ();
				}
				if (!Data [section].ContainsKey (key)) {
					Data [section] [key] = defaultValue;
					Save ();
				}
				string value = Data [section] [key];
				return value;
			}
			set {
				if (!Data.ContainsKey (section)) {
					Data [section] = new Dictionary<string,string> ();
				}
				Data [section] [key] = value;
				Save ();
			}
		}
	}
}
