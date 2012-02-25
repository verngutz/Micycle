using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiInGameMenu : MiScreen
    {
        private MiAnimatingComponent cursor;
        private MiAnimatingComponent resumeButtonGraphic;
        private MiAnimatingComponent goToMainMenuButtonGraphic;
        private MiAnimatingComponent quitGameButtonGraphic;

        private MiButton resumeButton;
        private MiButton goToMainMenuButton;
        private MiButton quitGameButton;

        public MiInGameMenu(Micycle game)
            : base(game)
        {
            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 200, 50, 1, 0, 0, 0);
            cursor.Visible = false;

            //
            // Resume Button
            //
            resumeButton = new MiButton();
            resumeButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    return null;
                });
            resumeButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);

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
            quitGameButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);

            //
            // Go To Main Menu Button
            //
            goToMainMenuButton = new MiButton();
            goToMainMenuButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    Game.ToDraw.RemoveLast();
                    Game.ToUpdate.Push(game.StartScreen);
                    Game.ToDraw.AddLast(game.StartScreen);
                    Game.ScriptEngine.ExecuteScript(game.StartScreen.EntrySequence);
                    return null;
                });
            goToMainMenuButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);
        }

        public override IEnumerator<int> EntrySequence()
        {
            entrySequenceMutex = true;
            resumeButtonGraphic.MoveEnabled = true;
            goToMainMenuButtonGraphic.MoveEnabled = true;
            quitGameButtonGraphic.MoveEnabled = true;
            resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.MoveTimer + 20, 50));
            goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.MoveTimer + 20, 150));
            quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.MoveTimer + 20, 250));
            yield return 20;
            cursor.Visible = true;
            resumeButtonGraphic.MoveEnabled = false;
            goToMainMenuButtonGraphic.MoveEnabled = false;
            quitGameButtonGraphic.MoveEnabled = false;
            ActiveButton = resumeButton;
            entrySequenceMutex = false;
        }

        public override IEnumerator<int> Pressed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                exitSequenceMutex = true;
                cursor.Visible = false;
                resumeButtonGraphic.MoveEnabled = true;
                goToMainMenuButtonGraphic.MoveEnabled = true;
                quitGameButtonGraphic.MoveEnabled = true;
                cursor.MoveEnabled = true;
                resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.MoveTimer + 20, -100));
                goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.MoveTimer + 20, -100));
                quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.MoveTimer + 20, -100));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, resumeButtonGraphic.Position.Y));
                yield return 20;
                resumeButtonGraphic.MoveEnabled = false;
                goToMainMenuButtonGraphic.MoveEnabled = false;
                quitGameButtonGraphic.MoveEnabled = false;
                cursor.MoveEnabled = false;
                ActiveButton.Pressed();
                exitSequenceMutex = false;
            }
        }

        public override IEnumerator<int> Cancelled()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                ActiveButton = resumeButton;
                Game.ScriptEngine.ExecuteScript(new MiScript(Pressed));
            }
        }

        public override IEnumerator<int> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, resumeButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = resumeButton;
            }

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, goToMainMenuButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = goToMainMenuButton;
            }
        }

        public override IEnumerator<int> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, quitGameButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = quitGameButton;
            }
            else if (ActiveButton == resumeButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, goToMainMenuButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = goToMainMenuButton;
            }
        }

        public override void LoadContent()
        {
            resumeButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            goToMainMenuButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            resumeButtonGraphic.Update(gameTime);
            goToMainMenuButtonGraphic.Update(gameTime);
            quitGameButtonGraphic.Update(gameTime);
            cursor.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            resumeButtonGraphic.Draw(gameTime);
            goToMainMenuButtonGraphic.Draw(gameTime);
            quitGameButtonGraphic.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
