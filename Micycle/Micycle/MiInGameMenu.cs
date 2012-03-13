using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiInGameMenu : MiDialogScreen
    {
        private const int WIDTH = 400;
        private const int HEIGHT = 600;

        private const int CURSOR_PADDING = 20;

        private MiButton resumeButton;
        private const string RESUME_STRING = "Resume Game";
        private static readonly Vector2 RESUME_POSITION = MiResolution.Center + new Vector2(0, - HEIGHT / 4);
        private Vector2 resumeOrigin;

        private MiButton goToMainMenuButton;
        private const string GO_TO_MAIN_MENU_STRING = "Return to Main Menu";
        private static readonly Vector2 GO_TO_MAIN_MENU_POSITION = MiResolution.Center + new Vector2(0, 0);
        private Vector2 goToMainMenuOrigin;

        private MiButton quitGameButton;
        private const string QUIT_GAME_BUTTON_STRING = "Quit Game";
        private static readonly Vector2 QUIT_GAME_POSITION = MiResolution.Center + new Vector2(0, HEIGHT / 4);
        private Vector2 quitGameOrigin;

        private MicycleGameSystem system;

        public MiInGameMenu(Micycle game, MicycleGameSystem system)
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
        }

        public override IEnumerator<ulong> EntrySequence()
        {
            entrySequenceMutex = true;
            ActiveButton = resumeButton;
            Cursor.MoveEnabled = true;
            Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, (MiResolution.Center.X - WIDTH / 2) + CURSOR_PADDING));
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

        public override IEnumerator<ulong> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = resumeButton;
                Cursor.MoveEnabled = true;
                Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, RESUME_POSITION.Y));
                yield return 20;
                Cursor.MoveEnabled = false;
            }

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = goToMainMenuButton;
                Cursor.MoveEnabled = true;
                Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, GO_TO_MAIN_MENU_POSITION.Y));
                yield return 20;
                Cursor.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = quitGameButton;
                Cursor.MoveEnabled = true;
                Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, QUIT_GAME_POSITION.Y));
                yield return 20;
                Cursor.MoveEnabled = false;
            }
            else if (ActiveButton == resumeButton)
            {
                ActiveButton = goToMainMenuButton;
                Cursor.MoveEnabled = true;
                Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 20, GO_TO_MAIN_MENU_POSITION.Y));
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
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.SpriteBatch.DrawString(MenuFont, RESUME_STRING, RESUME_POSITION, Color.Black, 0, resumeOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(MenuFont, GO_TO_MAIN_MENU_STRING, GO_TO_MAIN_MENU_POSITION, Color.Black, 0, goToMainMenuOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(MenuFont, QUIT_GAME_BUTTON_STRING, QUIT_GAME_POSITION, Color.Black, 0, quitGameOrigin, 1, SpriteEffects.None, 0);
        }
    }
}
