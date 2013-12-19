using System;
using System.Collections;
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
	using GameObjects;
	using RenderEffects;

	public class World : DrawableGameScreenComponent, IEnumerable<IGameObject>
	{
		public virtual Camera Camera
		{
			get;
			set;
		}

		public virtual List<IGameObject> Objects
		{
			get;
			set;
		}

		public virtual IGameObject SelectedObject
		{
			get;
			set;
		}

		public virtual IRenderEffect CurrentEffect
		{
			get;
			set;
		}

		public virtual void Update(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public World(GameScreen screen)
		{
		}

        public virtual IEnumerator<IGameObject> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

	}
}

