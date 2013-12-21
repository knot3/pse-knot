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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Die Zeichenreihenfolge der Elemente der grafischen Benutzeroberfläche.
    /// </summary>
    public enum DisplayLayer
    {
        /// <summary>
        /// Steht für die hinterste Ebene bei der Zeichenreihenfolge.
        /// </summary>
        None=0,
        /// <summary>
        /// Steht für eine Ebene hinter der Spielwelt, z.B. um
        /// Hintergrundbilder darzustellen.
        /// </summary>
        Background=10,
        /// <summary>
        /// Steht für die Ebene in der die Spielwelt dargestellt wird.
        /// </summary>
        GameWorld=20,
        /// <summary>
        /// Steht für die Ebene in der die Dialoge dargestellt werden.
        /// Dialoge werden vor der Spielwelt gezeichnet, damit der Spieler damit interagieren kann.
        /// </summary>
        Dialog=30,
        /// <summary>
        /// Steht für die Ebene in der Menüs gezeichnet werden. Menüs werden innerhalb von Dialogen angezeigt, müssen also davor gezeichnet werden, damit sie nicht vom Hintergrund des Dialogs verdeckt werden.
        /// </summary>
        Menu=40,
        /// <summary>
        /// Steht für die Ebene in der Menüeinträge gezeichnet werden. Menüeinträge werden vor Menüs gezeichnet.
        /// </summary>
        MenuItem=50,
        /// <summary>
        /// Steht für die Ebene in der Untermenüs gezeichnet werden. Untermenüs befinden sich in einer Ebene vor Menüeinträgen.
        /// </summary>
        SubMenu=60,
        /// <summary>
        /// Steht für die Ebene in der Untermenüeinträge gezeichnet werden. Untermenüeinträge befinden sich in einer Ebene vor Untermenüs.
        /// </summary>
        SubMenuItem=70,
        /// <summary>
        /// Zum Anzeigen zusätzlicher Informationen bei der (Weiter-)Entwicklung oder beim Testen (z.B. ein FPS-Counter).
        /// </summary>
        Overlay=80,
        /// <summary>
        /// Die Maus ist das Hauptinteraktionswerkzeug, welches der Spieler
        /// ständig verwendet. Daher muss die Maus bei der Interaktion immer
        /// im Vordergrund sein. Cursor steht für die vorderste Ebene.
        /// </summary>
        Cursor=90,
    }
}

