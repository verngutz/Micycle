using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiSchoolMenu : MiBuildingMenu
    {
        public MiSchoolMenu(Micycle game, float center_x, float center_y)
            : base(game, center_x, center_y) { }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "+b", new Vector2(MiBuildingMenu.UP_BUTTON_X, MiBuildingMenu.UP_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "-b", new Vector2(MiBuildingMenu.DOWN_BUTTON_X, MiBuildingMenu.DOWN_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "-t", new Vector2(MiBuildingMenu.LEFT_BUTTON_X, MiBuildingMenu.LEFT_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "+t", new Vector2(MiBuildingMenu.RIGHT_BUTTON_X, MiBuildingMenu.RIGHT_BUTTON_Y), Color.Black);
        }
    }
}
