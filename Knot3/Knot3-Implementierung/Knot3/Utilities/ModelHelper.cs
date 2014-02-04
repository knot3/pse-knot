using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using Knot3.Development;

namespace Knot3.Utilities
{
	public static class ModelHelper
	{
		public static string[] ValidQualities = new string[] {
			"low",
			"medium",
			"high"
		};

		public static string Quality
		{
			get { return Options.Default ["video", "model-quality", "medium"]; }
		}

		private static Dictionary<string, ContentManager> contentManagers = new Dictionary<string, ContentManager> ();
		private static HashSet<string> invalidModels = new HashSet<string> ();

		public static Model LoadModel (this IGameScreen screen, string name)
		{
			ContentManager content;
			if (contentManagers.ContainsKey (screen.CurrentRenderEffects.CurrentEffect.ToString ())) {
				content = contentManagers [screen.CurrentRenderEffects.CurrentEffect.ToString ()];
			}
			else {
				contentManagers [screen.CurrentRenderEffects.CurrentEffect.ToString ()] = content = new ContentManager (screen.Content.ServiceProvider, screen.Content.RootDirectory);
			}

			Model model = LoadModel (content, screen.CurrentRenderEffects.CurrentEffect, name + "-" + Quality);
			if (model == null) {
				model = LoadModel (content, screen.CurrentRenderEffects.CurrentEffect, name);
			}
			return model;
		}

		private static Model LoadModel (ContentManager content, IRenderEffect pp, string name)
		{
			if (invalidModels.Contains (name)) {
				return null;
			}
			else {
				try {
					Model model = content.Load<Model> ("Models/" + name);
					pp.RemapModel (model);
					return model;
				}
				catch (ContentLoadException) {
					Log.WriteLine ("Warning: Model " + name + " does not exist!");
					invalidModels.Add (name);
					return null;
				}
			}
		}
	}
}
