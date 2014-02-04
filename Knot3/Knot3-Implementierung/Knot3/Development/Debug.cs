using System;
using System.Diagnostics;
using System.Linq;

namespace Knot3.Development
{
	public static class Log
	{
		private static string lastDebugStr = "";
		private static int lastDebugTimes = 0;

		[Conditional("DEBUG")]
		public static void Debug (params object[] strs)
		{
			string str = string.Join ("", strs);
			if (str == lastDebugStr) {
				++lastDebugTimes;
				if (lastDebugTimes > 100) {
					Console.WriteLine ("[" + lastDebugTimes.ToString () + "x] " + lastDebugStr);
					lastDebugTimes = 0;
				}
			}
			else {
				if (lastDebugTimes > 0) {
					Console.WriteLine (lastDebugTimes.ToString () + "x " + lastDebugStr);
				}
				Console.WriteLine (str);
				lastDebugStr = str;
				lastDebugTimes = 0;
			}
		}

		public static void Message (params object[] strs)
		{
			Console.WriteLine (string.Join ("", strs));
		}
	}
}
