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
        private MiMenuScreen menuScreen;
        private MiGameScreen gameScreen;
        private MiInGameMenu inGameMenu;

        protected override void Initialize()
        {
            // Set the game resolution
            MiResolution.SetVirtualResolution(800, 600);
            MiResolution.SetResolution(800, 600);

            // Initialize Input Handler
            inputHandler = new MicycleInputHandler(this);

            // Initialize screens
            menuScreen = new MiMenuScreen(this);
            gameScreen = new MiGameScreen(this);
            inGameMenu = new MiInGameMenu(this);

            // Attach screens to each other
            menuScreen.GameScreen = gameScreen;
            gameScreen.InGameMenu = inGameMenu;
            inGameMenu.MenuScreen = menuScreen;

            // Set active screen
            ToDraw.Push(menuScreen);
            ToUpdate.Push(menuScreen);

            ScriptEngine.ExecuteScript(new MiScript(menuScreen.EntrySequence));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            menuScreen.LoadContent();
            gameScreen.LoadContent();
            inGameMenu.LoadContent();
            base.LoadContent();
        }
    }
}
