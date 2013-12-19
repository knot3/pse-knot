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


namespace KnotData
{

	public class ChallengeMetaData
	{
		public virtual string Name
		{
			get;
			set;
		}

		public virtual KnotMetaData Start
		{
			get;
			set;
		}

		public virtual KnotMetaData Target
		{
			get;
			set;
		}

		public virtual IChallengeIO Format
		{
			get;
			set;
		}

		public virtual string Filename
		{
			get;
			set;
		}

        public virtual IEnumerator<KeyValuePair<String, int>> Highscore
		{
			get;
			set;
		}

		public ChallengeMetaData(string name, KnotMetaData start, KnotMetaData target, string filename, IChallengeIO format)
		{
		}

	}
}

