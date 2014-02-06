using System;
using System.Collections.Generic;
using System.Linq;

using Knot3.Core;
using Knot3.Development;

namespace Knot3
{
	static class Program
	{
		private static Knot3Game game;

		/// <summary>
		/// The main entry point for the application.
		///
		/// </summary>
		///
		[STAThread]
		static void Main ()
		{
			Log.Message ("Knot" + Char.ConvertFromUtf32 ('\u00B3').ToString () + " " + Version);
			Log.Message ("Copyright (C) 2013-2014 Tobias Schulz, Maximilian Reuter,\n" +
				"Pascal Knodel, Gerd Augsburg, Christina Erler, Daniel Warzel,\n" +
				"M. Retzlaff, F. Kalka, G. Hoffmann, T. Schmidt, G. Mückl, Torsten Pelzer");

			game = new Knot3Game ();
			game.Run ();
		}

		/// <summary>
		/// Gibt die Versionsnummer zurück.
		/// </summary>
		/// <returns></returns>
		public static string Version
		{
			get {
				return System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
			}
		}
	}
}
