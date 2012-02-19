using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        public MiGameScreen GameScreen { get; set; }

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
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 300, 350);
            cursor.Enabled = false;
            cursor.Visible = false;
            cursor.MoveEnabled = true;

            //
            // New Game Button
            //
            newGameButton = new MiButton();
            newGameButton.Pressed += new MiEvent(GoToGameScreen);
            newGameButtonGraphic = new MiAnimatingComponent(game, -100, 350);
            newGameButtonGraphic.MoveEnabled = true;

            //
            // Quit Game Button
            //
            quitGameButton = new MiButton();
            quitGameButton.Pressed += new MiEvent(Game.Exit);
            quitGameButtonGraphic = new MiAnimatingComponent(game, -100, 460);
            quitGameButtonGraphic.MoveEnabled = true;

            //
            // Action Events
            //
            Upped += delegate
            {
                if (ActiveButton != newGameButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToNewGameButton), 25);
                    Game.EventQueue.AddEvent(new MiEvent(SetNewGameButtonAsActive), 0);
                }
            };

            Downed += delegate
            {
                if (ActiveButton != quitGameButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToExitButton), 25);
                    Game.EventQueue.AddEvent(new MiEvent(SetExitButtonAsActive), 0);
                }
            };

            Pressed += delegate
            {
                Game.EventQueue.AddEvent(new MiEvent(CursorExit), 0);
                Game.EventQueue.AddEvent(new MiEvent(ButtonExit), 50);
                Game.EventQueue.AddEvent(new MiEvent(FreezeButtons), 0);
                Game.EventQueue.AddEvent(new MiEvent(PressActiveButton), 0);
            };

            ActiveButton = newGameButton;
        }

        public void EntrySequence()
        {
            Game.EventQueue.AddEvent(new MiEvent(ButtonEntrance), 50);
            Game.EventQueue.AddEvent(new MiEvent(CursorEntrance), 0);
            Game.EventQueue.AddEvent(new MiEvent(FreezeButtons), 0);
        }

        private void ButtonEntrance()
        {
            newGameButtonGraphic.Enabled = true;
            quitGameButtonGraphic.Enabled = true;

            newGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonGraphic.Time + 50, 300));
            quitGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 50, 400));
        }

        private void CursorEntrance()
        {
            cursor.Visible = true;
            cursor.Enabled = false;
        }

        private void FreezeButtons()
        {
            newGameButtonGraphic.Enabled = false;
            quitGameButtonGraphic.Enabled = false;
        }

        private void MoveCursorToNewGameButton()
        {
            ActiveButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, newGameButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, newGameButtonGraphic.Position.Y));
        }

        private void SetNewGameButtonAsActive()
        {
            cursor.Enabled = false;
            ActiveButton = newGameButton;
        }

        private void MoveCursorToExitButton()
        {
            ActiveButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, quitGameButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, quitGameButtonGraphic.Position.Y));
        }

        private void SetExitButtonAsActive()
        {
            cursor.Enabled = false;
            ActiveButton = quitGameButton;
        }

        private void GoToGameScreen()
        {
            Enabled = false;
            Visible = false;
            GameScreen.Enabled = true;
            GameScreen.Visible = true;
            Game.ToUpdate.Pop();
            Game.ToDraw.Pop();
            Game.ToUpdate.Push(GameScreen);
            Game.ToDraw.Push(GameScreen);
            Game.InputHandler.Focused = GameScreen;
        }

        private void CursorExit()
        {
            cursor.Visible = false;
        }

        private void ButtonExit()
        {
            newGameButtonGraphic.Enabled = true;
            quitGameButtonGraphic.Enabled = true;
            newGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonGraphic.Time + 50, -100));
            quitGameButtonGraphic.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 50, -100));
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            newGameButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            if(newGameButtonGraphic.Enabled)
                newGameButtonGraphic.Update(gameTime);

            if(quitGameButtonGraphic.Enabled)
                quitGameButtonGraphic.Update(gameTime);

            if(cursor.Enabled)
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
