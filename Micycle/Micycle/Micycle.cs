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

using MiUtil;

namespace Micycle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    
    public class Micycle : MiGame
    {
        private MiScreen startScreen;
        internal MiScreen StartScreen { get { return startScreen; } }

        protected override void Initialize()
        {
            // Set the game resolution
            MiResolution.SetVirtualResolution(1200, 900);
            MiResolution.SetResolution(800, 600);

            // Initialize Input Handler
            inputHandler = new MicycleInputHandler(this);

            // Initialize screens
            startScreen = new MiMenuScreen(this);

            // Set active screen
            ToDraw.AddLast(startScreen);
            ToUpdate.Push(startScreen);

            ScriptEngine.ExecuteScript(new MiScript(startScreen.EntrySequence));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            startScreen.LoadContent();
            base.LoadContent();
        }
    }
}
