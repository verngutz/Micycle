using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiFactoryMenu : MiBuildingMenu
    {
        public MiFactoryMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game, center_x, center_y, system, inGameMenu) { }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "+w", new Vector2(UP_BUTTON_X, UP_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "-w", new Vector2(DOWN_BUTTON_X, DOWN_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "-c", new Vector2(LEFT_BUTTON_X, LEFT_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), "+c", new Vector2(RIGHT_BUTTON_X, RIGHT_BUTTON_Y), Color.Black);
        }
    }
}
