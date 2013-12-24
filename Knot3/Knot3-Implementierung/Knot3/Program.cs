using System;
using System.Collections.Generic;
using System.Linq;

using Knot3.Core;

namespace Knot3
{
	static class Program
	{
		private static Knot3Game game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new Knot3Game ();
			game.Run ();
		}
	}
}
