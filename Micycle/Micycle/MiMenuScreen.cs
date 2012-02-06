using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        private Texture2D background;
        private MiAnimatingComponent cursor;
        private MiButton newGameButton;
        private MiButton quitGameButton;
        private MiButton activeButton;

        private MiControllerState old;

        public MiMenuScreen(Micycle game)
            : base(game)
        {
            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 300, 350);
            cursor.Visible = false;
            cursor.Enabled = false;

            //
            // New Game Button
            //
            newGameButton = new MiButton(game, -100, 350);
            newGameButton.Pressed += new MiEvent(GoToMiGameScreen);

            //
            // Quit Game Button
            //
            quitGameButton = new MiButton(game, -100, 460);
            quitGameButton.Pressed += new MiEvent(Game.Exit);

            //
            // Event Queue
            //
            Game.EventQueue.AddEvent(new MiEvent(ButtonEntrance), 50);
            Game.EventQueue.AddEvent(new MiEvent(CursorEntrance), 0);

            activeButton = newGameButton;

            old = MicycleController.GetState();
        }

        private void ButtonEntrance()
        {
            newGameButton.XPositionOverTime.Keys.Add(new CurveKey(50, 300));
            quitGameButton.XPositionOverTime.Keys.Add(new CurveKey(50, 400));
        }

        private void CursorEntrance()
        {
            newGameButton.Enabled = false;
            quitGameButton.Enabled = false;
            cursor.Visible = true;
            cursor.Enabled = false;
        }

        private void MoveCursorToExitButton()
        {
            activeButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, quitGameButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, quitGameButton.Position.Y));
        }

        private void SetExitButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = quitGameButton;
        }

        private void MoveCursorToNewGameButton()
        {
            activeButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, newGameButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 25, newGameButton.Position.Y));
        }

        private void SetNewGameButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = newGameButton;
        }

        private void GoToMiGameScreen()
        {
            Game.ActiveScreen = (Game as Micycle).GameScreen;
        }

        private void ClearButtonsFromScreen()
        {
            newGameButton.Enabled = true;
            quitGameButton.Enabled = true;
            cursor.Visible = false;
            newGameButton.XPositionOverTime.Keys.Add(new CurveKey(newGameButton.Time + 50, -100));
            quitGameButton.XPositionOverTime.Keys.Add(new CurveKey(quitGameButton.Time + 50, -100));
        }

        private void PressActiveButton()
        {
            newGameButton.Enabled = false;
            quitGameButton.Enabled = false;
            activeButton.Pressed();
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            newGameButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(newGameButton.Enabled)
                newGameButton.Update(gameTime);

            if(quitGameButton.Enabled)
                quitGameButton.Update(gameTime);

            if(cursor.Enabled)
                cursor.Update(gameTime);

            MiControllerState newState = MicycleController.GetState();
            if (old.IsReleased(MicycleControls.UP) && newState.IsPressed(MicycleControls.UP) && activeButton != newGameButton)
            {
                Game.EventQueue.AddEvent(new MiEvent(MoveCursorToNewGameButton), 25);
                Game.EventQueue.AddEvent(new MiEvent(SetNewGameButtonAsActive), 0);
            }

            if (old.IsReleased(MicycleControls.DOWN) && newState.IsPressed(MicycleControls.DOWN) && activeButton != quitGameButton)
            {
                Game.EventQueue.AddEvent(new MiEvent(MoveCursorToExitButton), 25);
                Game.EventQueue.AddEvent(new MiEvent(SetExitButtonAsActive), 0);
            }

            if (old.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A))
            {
                Game.EventQueue.AddEvent(new MiEvent(ClearButtonsFromScreen), 50);
                Game.EventQueue.AddEvent(new MiEvent(PressActiveButton), 0);
            }

            old = MicycleController.GetState();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);

            if(newGameButton.Visible)
                newGameButton.Draw(gameTime);

            if(quitGameButton.Visible)
                quitGameButton.Draw(gameTime);

            if(cursor.Visible)
                cursor.Draw(gameTime);

            Game.SpriteBatch.End();
        }
    }
}
