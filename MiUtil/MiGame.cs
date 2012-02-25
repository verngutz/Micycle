using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MiUtil
{
    public abstract class MiGame : Game
    {
        
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        private Stack<MiScreen> toDraw;
        private Stack<MiScreen> toUpdate;

        public Stack<MiScreen> ToDraw { get { return toDraw; } }
        public Stack<MiScreen> ToUpdate { get { return toUpdate; } }

        protected MiInputHandler inputHandler;
        public MiInputHandler InputHandler { get { return inputHandler; } }

        private MiScriptEngine scriptEngine;
        public MiScriptEngine ScriptEngine { get { return scriptEngine; } }

        public MiGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            MiResolution.Init(ref graphics);

            Content.RootDirectory = "Content";

            toDraw = new Stack<MiScreen>();
            toUpdate = new Stack<MiScreen>();

            scriptEngine = new MiScriptEngine(this);
        }

        protected override void Initialize()
        {
            if(inputHandler == null)
                throw new InvalidOperationException("Input Handler Not Initialized");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            scriptEngine.Update(gameTime);

            inputHandler.Focused = ToUpdate.Peek();
            inputHandler.Update(gameTime);

            foreach (MiScreen screen in ToUpdate)
                screen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MiResolution.BeginDraw();
            SpriteBatch.Begin();

            foreach (MiScreen screen in ToDraw)
                screen.Draw(gameTime);
#if DEBUG
            // Draw frame rate
            int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(Content.Load<SpriteFont>("Default"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 575), Color.Black);
            // End draw frame rate
#endif

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
