using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiSchoolMenu : MiBuildingMenu
    {
        private const string EDUCATION_BUDGET = "Education Budget";
        private const string TEACHER_STUDENT_RATIO = "Teacher-Student Ratio";
        private const string STUDENTS_OVER_CAPACITY = "Students Over Capacity";

        private Rectangle educationBudgetBar;
        private Rectangle teacherStudentRatioBar;
        private Rectangle studentsOverCapacityBar;

        public MiSchoolMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game, center_x, center_y, system, inGameMenu) 
        {
            educationBudgetBar = Stat1Bar;
            teacherStudentRatioBar = Stat2Bar;
            studentsOverCapacityBar = Stat3Bar;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            educationBudgetBar.Width = (int)(Stat1Bar.Width * System.GetEducationBudget());
            teacherStudentRatioBar.Width = (int)(Stat2Bar.Width * System.GetTeacherStudentRatio());
            studentsOverCapacityBar.Width = (int)(Stat3Bar.Width * System.GetStudentCapacity());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.SpriteBatch.DrawString(buildingStatsFont, EDUCATION_BUDGET, Stat1TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat1Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, educationBudgetBar, Color.MediumPurple);

            Game.SpriteBatch.DrawString(buildingStatsFont, TEACHER_STUDENT_RATIO, Stat2TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat2Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, teacherStudentRatioBar, Color.Gold); 
            
            Game.SpriteBatch.DrawString(buildingStatsFont, STUDENTS_OVER_CAPACITY, Stat3TextPosition, Color.White);
            Game.SpriteBatch.Draw(buildingStatBarFull, Stat3Bar, Color.White);
            Game.SpriteBatch.Draw(buildingStatBar, studentsOverCapacityBar, Color.HotPink);

            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+b", new Vector2(UP_BUTTON_X, UP_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-b", new Vector2(DOWN_BUTTON_X, DOWN_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "-t", new Vector2(LEFT_BUTTON_X, LEFT_BUTTON_Y), Color.Black);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Fonts\\Default"), "+t", new Vector2(RIGHT_BUTTON_X, RIGHT_BUTTON_Y), Color.Black);
        }
    }
}
