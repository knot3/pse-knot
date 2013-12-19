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

	public class Angles3 : IEquatable<Angles3>
	{
		public virtual float X
		{
			get;
			set;
		}

		public virtual float Y
		{
			get;
			set;
		}

		public virtual float Z
		{
			get;
			set;
		}

		public static Angles3 Zero
		{
			get;
			set;
		}

		public static Angles3 FromDegrees(float X, float Y, float Z)
		{
			throw new System.NotImplementedException();
		}

		public Angles3(float X, float Y, float Z)
		{
		}

		public virtual void ToDegrees(out float X, out float Y, out float Z)
		{
			throw new System.NotImplementedException();
		}

	}
}

