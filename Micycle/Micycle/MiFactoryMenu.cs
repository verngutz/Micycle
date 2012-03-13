using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiFactoryMenu : MiBuildingMenu
    {
        private const string WORKERS_OVER_CAPACITY = "Workers Over Capacity";
        private const string WORKER_WAGE = "Worker Wage";

        private Rectangle miceWorkersOverCapactiyBar;
        private Rectangle robotWorkersOverCapacityBar;
        private Rectangle workerWageBar;

        public MiFactoryMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game, center_x, center_y, system, inGameMenu) 
        {
            miceWorkersOverCapactiyBar = Stat1Bar;
            robotWorkersOverCapacityBar = Stat1Bar;
            workerWageBar = Stat2Bar;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            miceWorkersOverCapactiyBar.Width = (int)(Stat1Bar.Width * System.GetWorkersCapacity());
            robotWorkersOverCapacityBar.X = miceWorkersOverCapactiyBar.X + miceWorkersOverCapactiyBar.Width;
            robotWorkersOverCapacityBar.Width = (int)(Stat1Bar.Width * System.GetRobotsCapacity());
            workerWageBar.Width = (int)(Stat2Bar.Width * System.GetWorkerWage());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.SpriteBatch.DrawString(buildingStatsFont, WORKERS_OVER_CAPACITY, Stat1TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat1Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, miceWorkersOverCapactiyBar, Color.HotPink);
            Game.SpriteBatch.Draw(buildingStatBar, robotWorkersOverCapacityBar, Color.CadetBlue);

            Game.SpriteBatch.DrawString(buildingStatsFont, WORKER_WAGE, Stat2TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat2Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, workerWageBar, Color.LawnGreen);

            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+w", new Vector2(UP_BUTTON_X, UP_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-w", new Vector2(DOWN_BUTTON_X, DOWN_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-c", new Vector2(LEFT_BUTTON_X, LEFT_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+c", new Vector2(RIGHT_BUTTON_X, RIGHT_BUTTON_Y), Color.Black);
        }
    }
}
