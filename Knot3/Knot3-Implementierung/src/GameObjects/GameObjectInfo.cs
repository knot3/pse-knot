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

	public abstract class GameObjectInfo : IEquatable<GameObjectInfo>
	{
		public virtual bool IsMovable
		{
			get;
			set;
		}

		public virtual bool IsSelectable
		{
			get;
			set;
		}

		public virtual bool IsVisible
		{
			get;
			set;
		}

		public virtual Vector3 Position
		{
			get;
			set;
		}

		public virtual bool Equals(C other)
		{
			throw new System.NotImplementedException();
		}

	}
}

