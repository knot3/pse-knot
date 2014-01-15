using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
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

namespace Knot3.Utilities
{
	public class FileIndex
	{
		private HashSet<string> hashes;
		private string filename;

		public FileIndex (string filename)
		{
			this.filename = filename;
			try {
				hashes = new HashSet<string> (FileUtility.ReadFrom (filename));
			}
			catch (IOException) {
				hashes = new HashSet<string> ();
			}
		}

		public void Add (string hash)
		{
			hashes.Add (hash);
			Save ();
		}

		public void Remove (string hash)
		{
			hashes.Remove (hash);
			Save ();
		}

		public bool Contains (string hash)
		{
			return hashes.Contains (hash);
		}

		private void Save ()
		{
			File.WriteAllText (filename, string.Join ("\n", hashes));
		}
	}

}

