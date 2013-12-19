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

	public class DistinctOptionInfo : OptionInfo
	{
		public virtual HashSet<string> ValidValues
		{
			get;
			set;
		}

		public override string Value
		{
			get;
			set;
		}

		public DistinctOptionInfo(string section, string name, string defaultValue, IEnumerable<string> validValues, ConfigFile configFile)
		{
		}

	}
}

