using Microsoft.Xna.Framework.Graphics;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        Texture2D background;

        public MiMenuScreen(Micycle game) : base(game) { }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            base.LoadContent();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
