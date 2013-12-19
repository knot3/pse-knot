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

	public class ConfigFile
	{
		public virtual void SetOption(string section, string option, string value)
		{
			throw new System.NotImplementedException();
		}

		public virtual bool GetOption(string section, string option, bool defaultValue)
		{
			throw new System.NotImplementedException();
		}

		public virtual string GetOption(string section, string option, string defaultValue)
		{
			throw new System.NotImplementedException();
		}

		public virtual void SetOption(string section, string option, bool _value)
		{
			throw new System.NotImplementedException();
		}

	}
}

