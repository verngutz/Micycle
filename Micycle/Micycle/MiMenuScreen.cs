using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        Texture2D background;
        public MiMenuScreen(MiGame game) : base(game) { }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            base.LoadContent();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);
            base.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
