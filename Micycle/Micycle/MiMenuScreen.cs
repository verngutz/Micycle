using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        private MiGameScreen gameScreen;

        private Texture2D background;

        private MiAnimatingComponent cursor;
        private MiAnimatingComponent newGameButtonGraphic;
        private MiAnimatingComponent quitGameButtonGraphic;

        private MiButton newGameButton;
        private MiButton quitGameButton;

        public MiMenuScreen(Micycle game)
            : base(game)
        {
            //
            // Game Screen
            //
            gameScreen = new MiGameScreen(game);

            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 300, 350);
            cursor.Visible = false;

            //
            // New Game Button
            //
            newGameButton = new MiButton();
            newGameButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.Pop();
                    Game.ToUpdate.Push(gameScreen);
                    Game.ToDraw.Push(gameScreen);
                    gameScreen.Initialize();
                    return null;
                });
            newGameButtonGraphic = new MiAnimatingComponent(game, -100, 350);

            //
            // Quit Game Button
            //
            quitGameButton = new MiButton();
            quitGameButton.Pressed += new MiScript(
                delegate
                {
                    Game.Exit();
                    return null;
                });
            quitGameButtonGraphic = new MiAnimatingComponent(game, -100, 460);

            ActiveButton = newGameButton;
        }

        public override IEnumerator<int> EntrySequence()
        {
            entrySequenceMutex = true;
            newGameButtonGraphic.MoveEnabled = true;
            quitGameButtonGraphic.MoveEnabled = true;
            newGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonGraphic.MoveTimer + 50, 300));
            quitGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.MoveTimer + 50, 400));
            yield return 50;
            cursor.Visible = true;
            cursor.MoveEnabled = false;
            newGameButtonGraphic.MoveEnabled = false;
            quitGameButtonGraphic.MoveEnabled = false;
            entrySequenceMutex = false;
        }

        public override IEnumerator<int> Pressed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield return 0;
            }
            else
            {
                exitSequenceMutex = true;
                cursor.Visible = false;
                newGameButtonGraphic.MoveEnabled = true;
                quitGameButtonGraphic.MoveEnabled = true;
                newGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonGraphic.MoveTimer + 50, -100));
                quitGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.MoveTimer + 50, -100));
                yield return 50;
                newGameButtonGraphic.MoveEnabled = false;
                quitGameButtonGraphic.MoveEnabled = false;
                ActiveButton.Pressed();
                exitSequenceMutex = false;
            }
        }

        public override IEnumerator<int> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, newGameButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, newGameButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = newGameButton;
            }
        }

        public override IEnumerator<int> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == newGameButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, quitGameButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, quitGameButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = quitGameButton;
            }
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            newGameButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            gameScreen.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            newGameButtonGraphic.Update(gameTime);
            quitGameButtonGraphic.Update(gameTime);
            cursor.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);

            if(newGameButtonGraphic.Visible)
                newGameButtonGraphic.Draw(gameTime);

            if(quitGameButtonGraphic.Visible)
                quitGameButtonGraphic.Draw(gameTime);

            if(cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
