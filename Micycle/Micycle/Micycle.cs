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
        private MiGameScreen gameScreen;
        private MiInGameMenu inGameMenu;

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

            // Initialize Input Handler
            inputHandler = new MicycleInputHandler(this);

            // Initialize event queue
            eventQueue = new MiEventQueue(5);

            // Initialize screens
            menuScreen = new MiMenuScreen(this);
            gameScreen = new MiGameScreen(this);
            inGameMenu = new MiInGameMenu(this);

            // Attach screens to each other
            menuScreen.GameScreen = gameScreen;
            gameScreen.InGameMenu = inGameMenu;
            inGameMenu.MenuScreen = menuScreen;

            // Set active screen
            menuScreen.Visible = true;
            ToDraw.Push(menuScreen);
            menuScreen.Enabled = true;
            ToUpdate.Push(menuScreen);
            InputHandler.Focused = menuScreen;

            menuScreen.EntrySequence();

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
            inGameMenu.LoadContent();
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
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

            inputHandler.Update(gameTime);

            foreach (MiScreen screen in ToUpdate)
                screen.Update(gameTime);

            MiEvent nextEvent = EventQueue.GetNextEvent();
            if (nextEvent != null)
                nextEvent();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MiResolution.BeginDraw();
            SpriteBatch.Begin();

            foreach (MiScreen screen in ToDraw)
                screen.Draw(gameTime);

            // Draw frame rate
            int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(Content.Load<SpriteFont>("Default"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 575), Color.Black);
            // End draw frame rate

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
