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

	public class Camera : GameScreenComponent
	{
		public virtual Vector3 Position
		{
			get;
			set;
		}

		public virtual Vector3 Target
		{
			get;
			set;
		}

		public virtual float FoV
		{
			get;
			set;
		}

		public virtual Matrix ViewMatrix
		{
			get;
			set;
		}

		public virtual Matrix WorldMatrix
		{
			get;
			set;
		}

		public virtual Matrix ProjectionMatrix
		{
			get;
			set;
		}

		public virtual Vector3 ArcballTarget
		{
			get;
			set;
		}

		public virtual BoundingFrustum ViewFrustum
		{
			get;
			set;
		}

		private World World
		{
			get;
			set;
		}

		public virtual Angles3 Rotation
		{
			get;
			set;
		}

		public virtual Vector3 TargetDirection()
		{
			throw new System.NotImplementedException();
		}

		public virtual float TargetDistance()
		{
			throw new System.NotImplementedException();
		}

		public Camera(GameScreen screen, World world)
		{
		}

		public virtual void Update(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual Ray GetMouseRay(Vector2 mousePosition)
		{
			throw new System.NotImplementedException();
		}

	}
}

