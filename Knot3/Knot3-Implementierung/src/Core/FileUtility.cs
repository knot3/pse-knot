using System;
using System.Collections.Generic;
using System.Linq;
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


namespace Core
{

	public class FileUtility
	{
		public static string SettingsDirectory
		{
			get;
			set;
		}

		public static string SavegameDirectory
		{
			get;
			set;
		}

		public static string ScreenshotDirectory
		{
			get;
			set;
		}

		public static string ConvertToFileName(string name)
		{
			throw new System.NotImplementedException();
		}

		public virtual string GetHash(string filename)
		{
			throw new System.NotImplementedException();
		}

	}
}

