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

        protected MiEventQueue eventQueue;
        public MiEventQueue EventQueue { get { return eventQueue; } }

        private Stack<MiGameState> toDraw;
        private Stack<MiGameState> toUpdate;

        public Stack<MiGameState> ToDraw { get { return toDraw; } }
        public Stack<MiGameState> ToUpdate { get { return toUpdate; } }

        protected MiInputHandler inputHandler;
        public MiInputHandler InputHandler { get { return inputHandler; } }

        public MiGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            toDraw = new Stack<MiGameState>();
            toUpdate = new Stack<MiGameState>();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
    }
}
