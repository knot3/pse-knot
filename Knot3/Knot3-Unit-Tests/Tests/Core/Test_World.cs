using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.KnotData;
using Knot3.RenderEffects;

namespace Knot3.UnitTests
{
	[TestFixture]
	public class Test_World
	{
		private IGameScreen screen;
		private IRenderEffect effect;

		public Test_World ()
		{
		}

		[Test, Description("World Add/Remove")]
		public void AddRemoveTest ()
		{
			screen = screen ?? new FakeScreen (null);
			effect = effect ?? new FakeEffect (screen);

			// Erstelle einen Knoten
			Knot knot = new Knot ();

			// Erstelle eine Rasterpunkt-Zuordnung
			NodeMap nodeMap = new NodeMap (knot);

			List<PipeModel> models = new List<PipeModel> ();

			// Erstelle ein paar Pipes
			foreach (Edge edge in knot) {
				PipeModelInfo pipeInfo = new PipeModelInfo (nodeMap: nodeMap, knot: knot, edge: edge);
				PipeModel pipe = new PipeModel (screen: screen, info: pipeInfo);
				models.Add (pipe);
			}
			Assert.AreEqual (knot.Count(), models.Count(), "Für jede Edge eine Pipe");

			return;
			// das hier zu sehr in XNA verwoben, macht als test wahrscheinlich keinen sinn!!

			/*

			World world = new World (screen: screen, effect: effect);

			foreach (PipeModel model in models) {
				world.Add(model);
			}

			Assert.AreEqual (knot.Count(), world.Count(), "Anzahl GameObjects");

			foreach (PipeModel model in models) {
				world.Add(model);
			}

			Assert.AreEqual (knot.Count(), world.Count(), "GameObjects sind Unique");

			foreach (PipeModel model in models) {
				world.Remove(model);
			}

			Assert.AreEqual (0, world.Count(), "Leere World");

			*/
		}
	}
}
