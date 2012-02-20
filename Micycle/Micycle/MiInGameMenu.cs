using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiInGameMenu : MiScreen
    {
        public MiMenuScreen MenuScreen { get; set; }

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
                    Game.ToDraw.Pop();
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
                    Game.ToDraw.Pop();
                    Game.ToDraw.Pop();
                    Game.ToUpdate.Push(MenuScreen);
                    Game.ToDraw.Push(MenuScreen);
                    Game.ScriptEngine.ExecuteScript(MenuScreen.EntrySequence);
                    return null;
                });
            goToMainMenuButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);
        }

        public IEnumerator<int> EntrySequence()
        {
            resumeButtonGraphic.MoveEnabled = true;
            goToMainMenuButtonGraphic.MoveEnabled = true;
            quitGameButtonGraphic.MoveEnabled = true;
            resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.Time + 20, 50));
            goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.Time + 20, 150));
            quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 20, 250));
            yield return 20;
            cursor.Visible = true;
            resumeButtonGraphic.MoveEnabled = false;
            goToMainMenuButtonGraphic.MoveEnabled = false;
            quitGameButtonGraphic.MoveEnabled = false;
            ActiveButton = resumeButton;
        }

        public override IEnumerator<int> Pressed()
        {
            cursor.Visible = false;
            resumeButtonGraphic.MoveEnabled = true;
            goToMainMenuButtonGraphic.MoveEnabled = true;
            quitGameButtonGraphic.MoveEnabled = true;
            cursor.MoveEnabled = true;
            resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.Time + 20, -100));
            goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.Time + 20, -100));
            quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 20, -100));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButtonGraphic.Position.Y));
            yield return 20;
            resumeButtonGraphic.MoveEnabled = false;
            goToMainMenuButtonGraphic.MoveEnabled = false;
            quitGameButtonGraphic.MoveEnabled = false;
            cursor.MoveEnabled = false;
            ActiveButton.Pressed();
        }

        public override IEnumerator<int> Cancelled()
        {
            ActiveButton = resumeButton;
            return Pressed();
        }

        public override IEnumerator<int> Upped()
        {
            if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = resumeButton;
            }
            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = goToMainMenuButton;
            }
        }

        public override IEnumerator<int> Downed()
        {
            if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, quitGameButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = quitGameButton;
            }
            else if (ActiveButton == resumeButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButtonGraphic.Position.Y));
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
            if (resumeButtonGraphic.Visible)
                resumeButtonGraphic.Draw(gameTime);

            if (goToMainMenuButtonGraphic.Visible)
                goToMainMenuButtonGraphic.Draw(gameTime);

            if (quitGameButtonGraphic.Visible)
                quitGameButtonGraphic.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
