﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiGameOverTimeUp : MiGameOverScreen
    {
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

        protected override string GAME_OVER_STRING { get { return "Game Over: Time's Up"; } }
        private Vector2 gameOverOrigin;
        protected override Vector2 GameOverOrigin { get { return gameOverOrigin; } }

        private MicycleGameSystem system;

        public MiGameOverTimeUp(Micycle game, MicycleGameSystem system)
            : base(game, system)
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
                    Game.ToUpdate.Pop();

                    if (Game.ToUpdate.Peek() is MiBuildingMenu)
                    {
                        Game.ToUpdate.Pop();
                        Game.ToDraw.RemoveLast();
                    }

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
            appearSoundInstance.Play();
            ActiveButton = resumeButton;
            Cursor.MoveEnabled = true;
            Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.X - MenuFont.MeasureString(RESUME_STRING).X / 2 - CURSOR_PADDING));
            Cursor.YPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.Y));
            yield return 1;
            Cursor.MoveEnabled = false;
            entrySequenceMutex = false;
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
                Cursor.XPositionOverTime.Keys.Add(new CurveKey(Cursor.MoveTimer + 1, RESUME_POSITION.X - MenuFont.MeasureString(RESUME_STRING).X / 2 - CURSOR_PADDING));
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
                yield break;

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
        }
    }
}
