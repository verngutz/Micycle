using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiRndMenu : MiBuildingMenu
    {
        private const string RND_FUNDING = "Research Funds";
        private Rectangle rndFundsBar;

        public MiRndMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game, center_x, center_y, system, inGameMenu) 
        {
            rndFundsBar = Stat1Bar;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rndFundsBar.Width = (int)(Stat1Bar.Width * System.GetRndFunding());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.SpriteBatch.DrawString(buildingStatsFont, RND_FUNDING, Stat1TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat1Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, rndFundsBar, Color.Tomato);

            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+w",new Vector2(UP_BUTTON_X, UP_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-w", new Vector2(DOWN_BUTTON_X, DOWN_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-C", new Vector2(LEFT_BUTTON_X, LEFT_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+C", new Vector2(RIGHT_BUTTON_X, RIGHT_BUTTON_Y), Color.Black);
        }
    }
}
