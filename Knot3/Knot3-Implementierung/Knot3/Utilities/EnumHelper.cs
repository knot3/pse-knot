using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Reflection;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Utilities
{
	public static class EnumHelper
	{
		public static IEnumerable<string> ToEnumDescriptions<T> (this IEnumerable<T> enumValues)
		{
			foreach (T val in enumValues) {
				yield return val.ToEnumDescription<T> ();
			}
		}

		public static Hashtable GetDescriptionTable<T> ()
		{
			Hashtable table = new Hashtable ();
			foreach (T val in ToEnumValues<T>()) {
				string descr = val.ToEnumDescription<T> ();
				table [val] = descr;
				table [descr] = val;
			}
			return table;
		}

		public static IEnumerable<T> ToEnumValues<T> ()
		{
			Type enumType = typeof(T);

			return enumType.ToEnumValues<T> ();
		}

		public static IEnumerable<T> ToEnumValues<T> (this Type enumType)
		{
			if (enumType.BaseType != typeof(Enum)) {
				throw new ArgumentException ("T must be of type System.Enum");
			}

			Array enumValArray = Enum.GetValues (enumType);

			foreach (int val in enumValArray) {
				yield return (T)Enum.Parse (enumType, val.ToString ());
			}
		}

		public static string ToEnumDescription<T> (this T value)
		{
			Type enumType = typeof(T);

			if (enumType.BaseType != typeof(Enum)) {
				throw new ArgumentException ("T must be of type System.Enum");
			}

			FieldInfo fi = value.GetType ().GetField (value.ToString ());

			DescriptionAttribute[] attributes =
			    (DescriptionAttribute[])fi.GetCustomAttributes (
			        typeof(DescriptionAttribute),
			        false);

			if (attributes != null && attributes.Length > 0) {
				return attributes [0].Description;
			}
			else {
				return value.ToString ();
			}
		}

		public static T ToEnumValue<T> (this string value)
		{
			Type enumType = typeof(T);
			if (enumType.BaseType != typeof(Enum)) {
				throw new ArgumentException ("T must be of type System.Enum");
			}

			T returnValue = default(T);
			foreach (T enumVal in ToEnumValues<T>()) {
				if (enumVal.ToEnumDescription<T> () == value) {
					returnValue = enumVal;
					break;
				}
			}
			return returnValue;
		}
	}
}
