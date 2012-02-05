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
        MiScreen activeScreen;
        MiMenuScreen menuScreen;
        MiGameScreen gameScreen;
        MiInputProxy inputProxy;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize input proxy
            inputProxy = new MiInputProxy(this);
            Components.Add(inputProxy);
            // Initialize screens
            menuScreen = new MiMenuScreen(this);
            gameScreen = new MiGameScreen(this);
            Components.Add(menuScreen);
            Components.Add(gameScreen);
            
            // Set the active screen
            activeScreen = gameScreen;
            activeScreen.Enabled = true;
            activeScreen.Visible = true;

            // Set the game resolution
            MiResolution.Init(ref graphics);
            MiResolution.SetVirtualResolution(800, 600);
            MiResolution.SetResolution(800, 600);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MiResolution.BeginDraw();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
