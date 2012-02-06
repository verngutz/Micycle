using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiUtil
{
    public abstract class MiGame : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        protected MiScreen activeScreen;
        public MiScreen ActiveScreen
        {
            set
            {
                activeScreen.Enabled = false;
                activeScreen.Visible = false;
                activeScreen = value;
                activeScreen.Enabled = true;
                activeScreen.Visible = true;
            }
        }

        public MiGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
