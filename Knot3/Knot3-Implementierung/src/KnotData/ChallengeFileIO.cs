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

	public class ChallengeFileIO : IChallengeIO
	{
		public virtual IEnumerable<KnotStringIO> KnotStringIO
		{
			get;
			set;
		}

		public ChallengeFileIO()
		{
		}

		public virtual void Save(Challenge challenge)
		{
			throw new System.NotImplementedException();
		}

		public virtual Challenge Load(string filename)
		{
			throw new System.NotImplementedException();
		}

		public virtual ChallengeMetaData LoadMetaData(string filename)
		{
			throw new System.NotImplementedException();
		}

	}
}

