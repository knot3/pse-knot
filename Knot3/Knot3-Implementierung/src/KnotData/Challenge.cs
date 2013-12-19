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

	public class Challenge
	{
		public virtual Knot Start
		{
			get;
			private set;
		}

		public virtual Knot Target
		{
			get;
			private set;
		}

		private SortedList<int, String> highscore
		{
			get;
			set;
		}

		private IChallengeIO format
		{
			get;
			set;
		}

        public virtual IEnumerator<KeyValuePair<String, int>> Highscore
		{
			get;
			set;
		}

		public virtual ChallengeMetaData MetaData
		{
			get;
			private set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual IEnumerable<Knot> Knot
		{
			get;
			set;
		}

		public virtual ChallengeMetaData ChallengeInfo
		{
			get;
			set;
		}

		public Challenge(ChallengeMetaData meta, Knot start, Knot target)
		{
		}

		public virtual void AddToHighscore(string name, int time)
		{
			throw new System.NotImplementedException();
		}

	}
}

