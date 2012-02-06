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
using MiGui;

namespace Micycle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    
    public class Micycle : MiGame
    {
        private MiMenuScreen menuScreen;
        internal MiMenuScreen MenuScreen { get { return menuScreen; } }

        private MiGameScreen gameScreen;
        internal MiGameScreen GameScreen { get { return gameScreen; } }

        private MiGameController gameController;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set the game resolution
            MiResolution.Init(ref graphics);
            MiResolution.SetVirtualResolution(800, 600);
            MiResolution.SetResolution(800, 600);

            // Initialize event queue
            eventQueue = new MiEventQueue(5);

            // Initialize game controller
            gameController = new MiGameController(this);

            // Initialize screens
            menuScreen = new MiMenuScreen(this);
            gameScreen = new MiGameScreen(this);
            
            // Set the active screen
            activeScreen = menuScreen;
            activeScreen.Enabled = true;
            activeScreen.Visible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            menuScreen.LoadContent();
            gameScreen.LoadContent();
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

            activeScreen.Update(gameTime);
            gameController.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MiResolution.BeginDraw();
            activeScreen.Draw(gameTime);

            // Draw frame rate
            SpriteBatch.Begin();
            int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(Content.Load<SpriteFont>("Default"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 575), Color.Black);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
