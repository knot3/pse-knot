using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.Audio.FFmpeg
{
	public class AudioFile : IAudioFile
	{
		public string Name { get; private set; }

		public SoundState State { get; private set; }

		private string Filepath;
		private Process process;

		public AudioFile (string name, string filepath)
		{
			Name = name;
			Filepath = filepath;
		}

		public void Play ()
		{
			Console.WriteLine ("Play: " + Name);
			process = new Process ();
			process.StartInfo.FileName = "ffplay"; 
			process.StartInfo.Arguments = " -nodisp " + Filepath;
			process.StartInfo.UseShellExecute = false;
			process.EnableRaisingEvents = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.CreateNoWindow = true;
			process.Exited += new System.EventHandler ((object sender, EventArgs e) => {
				State = SoundState.Stopped;
			}
			);
			process.Start ();
			State = process.HasExited ? SoundState.Stopped : SoundState.Playing;
		}

		public void Stop ()
		{
			Console.WriteLine ("Stop: " + Name);
			process.Kill ();
			process = null;
			State = SoundState.Stopped;
		}
	}
}

