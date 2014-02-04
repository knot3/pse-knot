using System;

namespace Knot3.Development
{
	public static class Log
	{
		public static void WriteLine (string str)
		{
		}

		public static void Write (string str)
		{
		}

		public static void WriteLine (object str)
		{
			WriteLine(str.ToString());
		}

		public static void Write (object str)
		{
			Write(str.ToString());
		}
	}
}
