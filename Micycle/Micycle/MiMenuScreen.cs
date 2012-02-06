using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        Texture2D background;
        MiAnimating cursor;
        MiButton newGameButton;
        MiButton quitGameButton;
        MiButton activeButton;

        MiEventQueue eventQueue;

        KeyboardState old;

        public MiMenuScreen(MiGame game)
            : base(game)
        {
            //
            // Cursor
            //
            cursor = new MiAnimating(game, 300, 350, 1, 0, 0, 0);
            cursor.Visible = false;
            cursor.Enabled = false;

            //
            // New Game Button
            //
            newGameButton = new MiButton(game, -100, 350, 1, 0, 0, 0);
            newGameButton.SpriteQueueEnabled = false;

            //
            // Quit Game Button
            //
            quitGameButton = new MiButton(game, -100, 460, 1, 0, 0, 0);
            quitGameButton.SpriteQueueEnabled = false;

            //
            // Event Queue
            //
            eventQueue = new MiEventQueue(5);
            eventQueue.AddEvent(new MiEvent(ButtonEntrance), 100);
            eventQueue.AddEvent(new MiEvent(CursorEntrance), 0);

            activeButton = newGameButton;

            old = Keyboard.GetState();
        }

        private void ButtonEntrance()
        {
            newGameButton.XPositionOverTime.Keys.Add(new CurveKey(100, 300));
            quitGameButton.XPositionOverTime.Keys.Add(new CurveKey(100, 400));
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
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 50, quitGameButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 50, quitGameButton.Position.Y));
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
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 50, newGameButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 50, newGameButton.Position.Y));
        }

        private void SetNewGameButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = newGameButton;
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
            newGameButton.Update(gameTime);
            quitGameButton.Update(gameTime);
            cursor.Update(gameTime);

            if (old.IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Up) && activeButton != newGameButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToNewGameButton), 50);
                eventQueue.AddEvent(new MiEvent(SetNewGameButtonAsActive), 0);
            }
            if (old.IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Down) && activeButton != quitGameButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToExitButton), 50);
                eventQueue.AddEvent(new MiEvent(SetExitButtonAsActive), 0);
            }

            old = Keyboard.GetState();

            MiEvent nextEvent = eventQueue.GetNextEvent();
            if (nextEvent != null)
                nextEvent();
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);
            newGameButton.Draw(gameTime);
            quitGameButton.Draw(gameTime);
            cursor.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
