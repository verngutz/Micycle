using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        Texture2D factory;
        Texture2D school;
        Texture2D city;
        Texture2D rnd;

        private MiAnimating cursor;
        private MiButton resumeButton;
        private MiButton goToMainMenuButton;
        private MiButton quitGameButton;
        private MiButton activeButton;

        private MiEventQueue eventQueue;
        private MiGameControllerState old;

        public MiGameScreen(Micycle game) : base(game) 
        {
            //
            // Cursor
            //
            cursor = new MiAnimating(game, 200, 50, 1, 0, 0, 0);
            cursor.Visible = false;
            cursor.Enabled = false;

            //
            // Resume Button
            //
            resumeButton = new MiButton(game, 200, -100, 1, 0, 0, 0);
            resumeButton.SpriteQueueEnabled = false;
            resumeButton.Pressed += new MiEvent(ResumeGame);

            //
            // Quit Button
            //

            quitGameButton = new MiButton(game, 200, -100, 1, 0, 0, 0);
            quitGameButton.SpriteQueueEnabled = false;
            quitGameButton.Pressed += new MiEvent(Game.Exit);


            //
            // Go To Main Menu Button
            //

            goToMainMenuButton = new MiButton(game, 200, -100, 1, 0, 0, 0);
            goToMainMenuButton.SpriteQueueEnabled = false;
            goToMainMenuButton.Pressed += new MiEvent(GoToMiMenuScreen);
            //
            // Active Button
            //

            activeButton = null;

            //
            // Event Queue
            //
            eventQueue = new MiEventQueue(5);
            //eventQueue.AddEvent(new MiEvent(ButtonEntrance), 100);
            //eventQueue.AddEvent(new MiEvent(CursorEntrance), 0);

            

            old = ((Micycle)Game).GameController.GetState();
        }

        public void ButtonEntrance()
        {
            resumeButton.YPositionOverTime.Keys.Add(new CurveKey(resumeButton.Time + 20, 50));
            goToMainMenuButton.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButton.Time + 20, 150));
            quitGameButton.YPositionOverTime.Keys.Add(new CurveKey(quitGameButton.Time + 20, 250));
        }

        public void ResumeGame()
        {
            cursor.Visible = false;
            resumeButton.Enabled = true;
            goToMainMenuButton.Enabled = true;
            quitGameButton.Enabled = true;
            resumeButton.YPositionOverTime.Keys.Add(new CurveKey(resumeButton.Time + 20, -100));
            goToMainMenuButton.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButton.Time + 20, -100));
            quitGameButton.YPositionOverTime.Keys.Add(new CurveKey(quitGameButton.Time + 20, -100));
            activeButton = null;
        }
        private void CursorEntrance()
        {
            resumeButton.Enabled = false;
            goToMainMenuButton.Enabled = false;
            quitGameButton.Enabled = false;
            cursor.Visible = true;
            cursor.Enabled = false;
            activeButton = resumeButton;
        }


        private void MoveCursorToGoToMainMenuButton()
        {
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButton.Position.Y));
        }

        private void SetGoToMainMenuButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = goToMainMenuButton;
        }

        private void MoveCursorToQuitGameButton()
        {
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, quitGameButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, quitGameButton.Position.Y));
        }

        private void SetQuitGameButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = quitGameButton;
        }

        private void MoveCursorResumeButton()
        {
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButton.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButton.Position.Y));
        }

        private void SetResumeButtonAsActive()
        {
            cursor.Enabled = false;
            activeButton = resumeButton;
        }
        private void GoToMiMenuScreen()
        {
            Game.ActiveScreen = (Game as Micycle).MenuScreen;
        }
        public override void Update(GameTime gameTime)
        {
            resumeButton.Update(gameTime);
            goToMainMenuButton.Update(gameTime);
            quitGameButton.Update(gameTime);
            cursor.Update(gameTime);
            MiGameControllerState newState = ((Micycle)Game).GameController.GetState();
            if (old.IsReleased(MiGameControls.B) && newState.IsPressed(MiGameControls.B) && activeButton == null)
            {
                eventQueue.AddEvent(new MiEvent(ButtonEntrance), 20);
                eventQueue.AddEvent(new MiEvent(CursorEntrance), 0); 
            }

            if (old.IsReleased(MiGameControls.A) && newState.IsPressed(MiGameControls.A) && activeButton == resumeButton)
            {
                eventQueue.AddEvent(activeButton.Pressed, 20);
            }

            if (old.IsReleased(MiGameControls.A) && newState.IsPressed(MiGameControls.A) && (activeButton == goToMainMenuButton))
            {
                eventQueue.AddEvent(ResumeGame, 20);
                eventQueue.AddEvent(activeButton.Pressed, 0);
            }
            if (old.IsReleased(MiGameControls.A) && newState.IsPressed(MiGameControls.A) && (activeButton == quitGameButton))
            {
                eventQueue.AddEvent(activeButton.Pressed, 0);
            }
            if (old.IsReleased(MiGameControls.DOWN) && newState.IsPressed(MiGameControls.DOWN) && activeButton == resumeButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                eventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
            }

            if (old.IsReleased(MiGameControls.DOWN) && newState.IsPressed(MiGameControls.DOWN) && activeButton == goToMainMenuButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToQuitGameButton), 20);
                eventQueue.AddEvent(new MiEvent(SetQuitGameButtonAsActive), 0);
            }

            if (old.IsReleased(MiGameControls.UP) && newState.IsPressed(MiGameControls.UP) && activeButton == quitGameButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                eventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
            }

            if (old.IsReleased(MiGameControls.UP) && newState.IsPressed(MiGameControls.UP) && activeButton == goToMainMenuButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorResumeButton), 20);
                eventQueue.AddEvent(new MiEvent(SetResumeButtonAsActive), 0);
            }
            old = ((Micycle)Game).GameController.GetState();
            MiEvent nextEvent = eventQueue.GetNextEvent();
            if (nextEvent != null)
                nextEvent();
            base.Update(gameTime);
        }
        public override void LoadContent()
        {
            factory = Game.Content.Load<Texture2D>("Factory");
            school = Game.Content.Load<Texture2D>("School");
            city = Game.Content.Load<Texture2D>("City");
            rnd = Game.Content.Load<Texture2D>("RnD");
            resumeButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            goToMainMenuButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(school, new Rectangle(250, 250, 100, 100), Color.White );
            Game.SpriteBatch.Draw(factory, new Rectangle(250, 250, 100, 100), Color.White);
            Game.SpriteBatch.Draw(city, new Rectangle(250, 250, 100, 100), Color.White);
            Game.SpriteBatch.Draw(rnd, new Rectangle(250, 250, 100, 100), Color.White);
            resumeButton.Draw(gameTime);
            goToMainMenuButton.Draw(gameTime);
            quitGameButton.Draw(gameTime);
            cursor.Draw(gameTime);
            base.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
