using System;
using System.Collections;
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
    public class TextInputDialog : Dialog, IKeyEventListener
    {

        #region Properties

        /// <summary>
        /// Der Text, der durch den Spieler eingegeben wurde.
        /// </summary>
        public string InputText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action KeyEvent { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TextInputDialog (GameScreen screen, DisplayLayer drawOrder, string title, string text, string inputText, Keys[] keys)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnKeyEvent ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

