using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        Texture2D factory;
        Texture2D school;
        Texture2D city;
        Texture2D rnd;

        public MiGameScreen(Micycle game) : base(game) { }

        public override void LoadContent()
        {
            factory = Game.Content.Load<Texture2D>("Factory");
            school = Game.Content.Load<Texture2D>("School");
            city = Game.Content.Load<Texture2D>("City");
            rnd = Game.Content.Load<Texture2D>("RnD");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(school, new Rectangle(250, 250, 100, 100), Color.White );
            Game.SpriteBatch.Draw(factory, new Rectangle(250, 250, 100, 100), Color.White);
            Game.SpriteBatch.Draw(city, new Rectangle(250, 250, 100, 100), Color.White);
            Game.SpriteBatch.Draw(rnd, new Rectangle(250, 250, 100, 100), Color.White);
            base.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
