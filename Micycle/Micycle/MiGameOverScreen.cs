using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    abstract class MiGameOverScreen : MiDialogScreen
    {
        protected const int WIDTH = 800;
        protected const int HEIGHT = 400;

        protected const int CURSOR_PADDING = 50;

        protected virtual string GAME_OVER_STRING { get { return "Game Over"; } }
        private static readonly Vector2 GAME_OVER_POSITION = MiResolution.Center + new Vector2(0, -HEIGHT / 2);
        protected abstract Vector2 GameOverOrigin { get; }

        private const string ECONOMY_SCORE = "Economy Score";
        private static readonly Vector2 ECONOMY_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 40);
        private string economy;
        private static readonly Vector2 ECONOMY_SCORE_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 40);

        private const string TECHNOLOGY_SCORE = "Technology Score";
        private static readonly Vector2 TECHNOLOGY_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 80);
        private string technology;
        private static readonly Vector2 TECHNOLOGY_SCORE_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 80);

        private const string EMPLOYMENT_SCORE = "Employment Score";
        private static readonly Vector2 EMPLOYMENT_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 120);
        private string employment;
        private static readonly Vector2 EMPLOYMENT_SCORE_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 120);

        private const string EDUCATION_SCORE = "Education Score";
        private static readonly Vector2 EDUCATION_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 160);
        private string education;
        private static readonly Vector2 EDUCATION_SCORE_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 160);

        private const string TOTAL_SCORE = "Average Score";
        private static readonly Vector2 TOTAL_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 200);
        private string average;
        private static readonly Vector2 TOTAL_SCORE_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 200);

        private const string GRADE = "Grade";
        private static readonly Vector2 GRADE_POSITION = MiResolution.Center + new Vector2(-WIDTH / 2, -HEIGHT / 2 + 240);
        private string grade;
        private static readonly Vector2 GRADE_NUMBER_POSITION = MiResolution.Center + new Vector2(WIDTH / 2 - 100, -HEIGHT / 2 + 240);

        private MicycleGameSystem system;

        public MiGameOverScreen(Micycle game, MicycleGameSystem system)
            : base(game, WIDTH, HEIGHT)
        {
            this.system = system;
        }

        public void CalculateScore()
        {
            economy = (int)(system.EconomyGoalProgress * 100) + "%";
            technology = (int)(system.TechnologyGoalProgress * 100) + "%";
            employment = (int)(system.EmploymentGoalProgress * 100) + "%";
            education = (int)(system.EducationGoalProgress * 100) + "%";
            int averageScore = (int)((system.EconomyGoalProgress + system.TechnologyGoalProgress + system.EmploymentGoalProgress + system.EducationGoalProgress) / 4 * 100);
            average = averageScore + "%";
            if (averageScore >= 97)
                grade = "A+";
            else if (averageScore >= 92)
                grade = "A";
            else if (averageScore >= 87)
                grade = "B+";
            else if (averageScore >= 80)
                grade = "B";
            else if (averageScore >= 73)
                grade = "C+";
            else if (averageScore >= 67)
                grade = "C";
            else if (averageScore >= 60)
                grade = "D";
            else
                grade = "F";
        }

        public override IEnumerator<ulong> Escaped()
        {
            return Cancelled();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.SpriteBatch.DrawString(MenuFont, GAME_OVER_STRING, GAME_OVER_POSITION, Color.Black, 0, GameOverOrigin, 1, SpriteEffects.None, 0);

            Game.SpriteBatch.DrawString(MenuFont, ECONOMY_SCORE, ECONOMY_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, economy, ECONOMY_SCORE_POSITION, Color.Black);

            Game.SpriteBatch.DrawString(MenuFont, TECHNOLOGY_SCORE, TECHNOLOGY_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, technology, TECHNOLOGY_SCORE_POSITION, Color.Black);

            Game.SpriteBatch.DrawString(MenuFont, EMPLOYMENT_SCORE, EMPLOYMENT_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, employment, EMPLOYMENT_SCORE_POSITION, Color.Black);

            Game.SpriteBatch.DrawString(MenuFont, EDUCATION_SCORE, EDUCATION_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, education, EDUCATION_SCORE_POSITION, Color.Black);

            Game.SpriteBatch.DrawString(MenuFont, TOTAL_SCORE, TOTAL_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, average, TOTAL_SCORE_POSITION, Color.Black);

            Game.SpriteBatch.DrawString(MenuFont, GRADE, GRADE_POSITION, Color.Black);
            Game.SpriteBatch.DrawString(MenuFont, grade, GRADE_NUMBER_POSITION, Color.Black);
        }
    }
}
