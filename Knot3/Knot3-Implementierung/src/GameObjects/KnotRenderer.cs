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
	using KnotData;

	public class KnotRenderer : IGameObject, IEnumerable<IGameObject>
	{
		public virtual GameObjectInfo Info
		{
			get;
			set;
		}

		public virtual World World
		{
			get;
			set;
		}

		private List<ArrowModel> arrows
		{
			get;
			set;
		}

		private List<NodeModel> nodes
		{
			get;
			set;
		}

		private List<PipeModel> pipes
		{
			get;
			set;
		}

		public virtual Knot Knot
		{
			get;
			set;
		}

		private ModelFactory pipeFactory
		{
			get;
			set;
		}

		private ModelFactory nodeFactory
		{
			get;
			set;
		}

		private ModelFactory arrowFactory
		{
			get;
			set;
		}

		public virtual IEnumerable<ModelFactory> ModelFactory
		{
			get;
			set;
		}

		public virtual NodeMap NodeMap
		{
			get;
			set;
		}

		public virtual IEnumerable<PipeModel> PipeModel
		{
			get;
			set;
		}

		public virtual IEnumerable<ArrowModel> ArrowModel
		{
			get;
			set;
		}

		public virtual IEnumerable<NodeModel> NodeModel
		{
			get;
			set;
		}

		public virtual Vector3 Center()
		{
			throw new System.NotImplementedException();
		}

		public virtual GameObjectDistance Intersects(Ray Ray)
		{
			throw new System.NotImplementedException();
		}

		public virtual void OnEdgesChanged()
		{
			throw new System.NotImplementedException();
		}

		public KnotRenderer(GameScreen screen, GameObjectInfo info)
		{
		}

		public virtual void Update(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual IEnumerator<IGameObject> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

	}
}

