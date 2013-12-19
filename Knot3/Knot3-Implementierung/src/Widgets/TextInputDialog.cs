using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Core;
using GameObjects;
using Screens;
using RenderEffects;
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Ein Dialog, der eine Texteingabe des Spielers entgegennimmt.
    /// </summary>
    public class TextInputDialog : ConfirmDialog
    {

        #region Properties

        /// <summary>
        /// Der Text, der durch den Spieler eingegeben wurde.
        /// </summary>
        public virtual String InputText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public virtual void TextInputDialog (GameScreen screen, DisplayLayer drawOrder, String title, String text, String inputText)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

