using System;
using System.Diagnostics;

namespace Knot3.Development
{
	public static class Log
	{
		[Conditional("DEBUG")]
		public static void Debug (string str)
		{
		}

		[Conditional("DEBUG")]
		public static void Debug (object str)
		{
			Debug (str.ToString ());
		}

		public static void Message (string str)
		{
		}
	}
}
