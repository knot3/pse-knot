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

	public class OptionInfo
	{
		private ConfigFile configFile
		{
			get;
			set;
		}

		public virtual string Section
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string DefaultValue
		{
			get;
			set;
		}

		public virtual string Value
		{
			get;
			set;
		}

		public virtual ConfigFile ConfigFile
		{
			get;
			set;
		}

		public OptionInfo(string section, string name, string defaultValue, ConfigFile configFile)
		{
		}

	}
}

