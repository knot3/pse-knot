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


namespace GameObjects
{
	using Core;

	public class GameModelInfo : GameObjectInfo
	{
		public virtual string Modelname
		{
			get;
			set;
		}

		public virtual Angles3 Rotation
		{
			get;
			set;
		}

		public virtual Vector3 Scale
		{
			get;
			set;
		}

		public GameModelInfo(string modelname, Angles3 rotation, Vector3 scale)
		{
		}

	}
}

