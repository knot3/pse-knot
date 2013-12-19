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

	public interface IGameObject 
	{
		GameObjectInfo Info { get; }

		World World { get; }

		Vector3 Center();

		void Update(GameTime time);

		void Draw(GameTime time);

		GameObjectDistance Intersects(Ray ray);

	}
}

