using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiGameOverScreen : MiDialogScreen
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 400;

        private const int CURSOR_PADDING = 50;

        private MiButton resumeButton;
        private const string RESUME_STRING = "Continue Playing";
        private static readonly Vector2 RESUME_POSITION = MiResolution.Center + new Vector2(-WIDTH / 3, HEIGHT - 200);
        private Vector2 resumeOrigin;

        private MiButton goToMainMenuButton;
        private const string GO_TO_MAIN_MENU_STRING = "Return to Main Menu";
        private static readonly Vector2 GO_TO_MAIN_MENU_POSITION = MiResolution.Center + new Vector2(0, HEIGHT / 2);
        private Vector2 goToMainMenuOrigin;

        private MiButton quitGameButton;
        private const string QUIT_GAME_BUTTON_STRING = "Quit Game";
        private static readonly Vector2 QUIT_GAME_POSITION = MiResolution.Center + new Vector2(WIDTH / 3, HEIGHT / 2);
        private Vector2 quitGameOrigin;

        private const string GAME_OVER_STRING = "Time's Up";
        private static readonly Vector2 GAME_OVER_POSITION = MiResolution.Center + new Vector2(0, -HEIGHT / 2);
        private Vector2 gameOverOrigin;

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

            //
            // Resume Button
            //
            resumeButton = new MiButton();
            resumeButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    system.Enabled = true;
                    return null;
                });

            //
            // Quit Button
            //
            quitGameButton = new MiButton();
            quitGameButton.Pressed += new MiScript(
                delegate
                {
                    Game.Exit();
                    return null;
                });

            //
            // Go To Main Menu Button
            //
            goToMainMenuButton = new MiButton();
            goToMainMenuButton.Pressed += new MiScript(
                delegate
                {
                    if (Game.ToUpdate.Peek() is MiBuildingMenu)
                    {
                        Game.ToUpdate.Pop();
                        Game.ToDraw.RemoveLast();
                    }
                    Game.ToUpdate.Pop();
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    Game.ToDraw.RemoveLast();
                    Game.ToUpdate.Push(game.StartScreen);
                    Game.ToDraw.AddLast(game.StartScreen);
                    Game.ScriptEngine.ExecuteScript(game.StartScreen.EntrySequence);
                    return null;
                });

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

        public override IEnumerator<ulong> EntrySequence()
        {
            entrySequenceMutex = true;
            ActiveButton = resumeButton;
            Cursor.MoveEnabled = true;
            Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.X - MenuFont.MeasureString(RESUME_STRING).X / 2 - CURSOR_PADDING));
            Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.Y));
            yield return 1;
            Cursor.MoveEnabled = false;
            entrySequenceMutex = false;
        }

        public override IEnumerator<ulong> Pressed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                exitSequenceMutex = true;
                ActiveButton.Pressed();
                exitSequenceMutex = false;
            }
        }

        public override IEnumerator<ulong> Escaped()
        {
            return Cancelled();
        }

        public override IEnumerator<ulong> Cancelled()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                ActiveButton = resumeButton;
                Cursor.MoveEnabled = true;
                Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.Y));
                yield return 1;
                Cursor.MoveEnabled = false;
                Game.ScriptEngine.ExecuteScript(new MiScript(Pressed));
            }
        }

        public override IEnumerator<ulong> Lefted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = resumeButton;
                Cursor.MoveEnabled = true;
                Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, RESUME_POSITION.X - MenuFont.MeasureString(RESUME_STRING).X / 2 - CURSOR_PADDING));
                yield return 20;
                Cursor.MoveEnabled = false;
            }

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = goToMainMenuButton;
                Cursor.MoveEnabled = true;
                Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, GO_TO_MAIN_MENU_POSITION.X - MenuFont.MeasureString(GO_TO_MAIN_MENU_STRING).X / 2 - CURSOR_PADDING));
                yield return 20;
                Cursor.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Righted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = quitGameButton;
                Cursor.MoveEnabled = true;
                Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, QUIT_GAME_POSITION.X - MenuFont.MeasureString(QUIT_GAME_BUTTON_STRING).X / 2 - CURSOR_PADDING));
                yield return 20;
                Cursor.MoveEnabled = false;
            }
            else if (ActiveButton == resumeButton)
            {
                ActiveButton = goToMainMenuButton;
                Cursor.MoveEnabled = true;
                Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, GO_TO_MAIN_MENU_POSITION.X - MenuFont.MeasureString(GO_TO_MAIN_MENU_STRING).X / 2 - CURSOR_PADDING));
                yield return 20;
                Cursor.MoveEnabled = false;
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            resumeOrigin = MenuFont.MeasureString(RESUME_STRING) / 2;
            goToMainMenuOrigin = MenuFont.MeasureString(GO_TO_MAIN_MENU_STRING) / 2;
            quitGameOrigin = MenuFont.MeasureString(QUIT_GAME_BUTTON_STRING) / 2;
            gameOverOrigin = MenuFont.MeasureString(GAME_OVER_STRING) / 2;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.SpriteBatch.DrawString(MenuFont, RESUME_STRING, RESUME_POSITION, Color.Black, 0, resumeOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(MenuFont, GO_TO_MAIN_MENU_STRING, GO_TO_MAIN_MENU_POSITION, Color.Black, 0, goToMainMenuOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(MenuFont, QUIT_GAME_BUTTON_STRING, QUIT_GAME_POSITION, Color.Black, 0, quitGameOrigin, 1, SpriteEffects.None, 0);

            Game.SpriteBatch.DrawString(MenuFont, GAME_OVER_STRING, GAME_OVER_POSITION, Color.Black, 0, gameOverOrigin, 1, SpriteEffects.None, 0);

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
