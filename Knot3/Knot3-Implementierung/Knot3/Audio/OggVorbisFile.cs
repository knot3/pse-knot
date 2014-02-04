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

using OggSharp;

using Knot3.Development;

namespace Knot3.Audio
{
	public class OggVorbisFile : IAudioFile
	{
		public string Name { get; private set; }

		public SoundState State { get { return internalFile.State; } }

		private SoundEffectFile internalFile;

		public OggVorbisFile (string name, string filepath, Sound soundType)
		{
			Name = name;
			string cachefile = FileUtility.DecodedMusicCache 
                             + FileUtility.Separator.ToString()
                             + soundType.ToString()
                             + "_"
                             + name.GetHashCode().ToString()
                             + ".wav";

			byte[] data;
			try {
				Log.Debug ("Read from cache: ", cachefile);
				data = File.ReadAllBytes (cachefile);
			}
			catch (Exception) {
				Log.Debug ("Decode: ", name);
				OggDecoder decoder = new OggDecoder ();
				decoder.Initialize (TitleContainer.OpenStream (filepath));
				data = decoder.SelectMany (chunk => chunk.Bytes.Take (chunk.Length)).ToArray ();
				using (MemoryStream stream = new MemoryStream())
				using (BinaryWriter writer = new BinaryWriter(stream)) {
					WriteWave (writer, decoder.Stereo ? 2 : 1, decoder.SampleRate, data);
					stream.Position = 0;
					data = stream.ToArray ();
				}
				File.WriteAllBytes (cachefile, data);
			}

			using (MemoryStream stream = new MemoryStream(data)) {
				stream.Position = 0;
				SoundEffect soundEffect = SoundEffect.FromStream (stream);
				internalFile = new SoundEffectFile (name, soundEffect, soundType);
			}
		}

		public void Play ()
		{
			internalFile.Play ();
		}

		public void Stop ()
		{
			internalFile.Stop ();
		}

		public void Update (GameTime time)
		{
			internalFile.Update (time);
		}

		private static void WriteWave (BinaryWriter writer, int channels, int rate, byte[] data)
		{
			writer.Write (new char[4] { 'R', 'I', 'F', 'F' });
			writer.Write ((int)(36 + data.Length));
			writer.Write (new char[4] { 'W', 'A', 'V', 'E' });

			writer.Write (new char[4] { 'f', 'm', 't', ' ' });
			writer.Write ((int)16);
			writer.Write ((short)1);
			writer.Write ((short)channels);
			writer.Write ((int)rate);
			writer.Write ((int)(rate * ((16 * channels) / 8)));
			writer.Write ((short)((16 * channels) / 8));
			writer.Write ((short)16);

			writer.Write (new char[4] { 'd', 'a', 't', 'a' });
			writer.Write ((int)data.Length);
			writer.Write (data);
		}
	}
}
