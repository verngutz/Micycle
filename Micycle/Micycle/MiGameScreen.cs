using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;
using MiGui;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        private MiButton factory;
        private MiButton school;
        private MiButton city;
        private MiButton rnd;
        

        private MiAnimatingComponent cursor;
        private MiButton resumeButton;
        private MiButton goToMainMenuButton;
        private MiButton quitGameButton;
        private MiButton activeButton;


        private MiEventQueue eventQueue;
        private MicycleControllerState old;

        public MiGameScreen(Micycle game) : base(game) 
        {
            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 200, 50, 1, 0, 0, 0);
            cursor.Visible = false;
            cursor.Enabled = false;

            //
            // Factory
            //
            factory = new MiButton(game, 100, 400, 0.5f, 0, 0, 0);

            //
            // School
            //
            school = new MiButton(game, 400, 300, 0.5f, 0, 0, 0);

            //
            // City
            //
            city = new MiButton(game, 400, 50, 0.5f, 0, 0, 0);

            //
            // Rnd
            //
            rnd = new MiButton(game, 700, 400, 0.5f, 0, 0, 0);

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

            

            old = MicycleController.GetState();
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
            if (factory.Enabled)
                factory.Update(gameTime);

            if (school.Enabled)
                school.Update(gameTime);

            if (city.Enabled)
                city.Update(gameTime);

            if (rnd.Enabled)
                rnd.Update(gameTime);

            if (cursor.Enabled)
                cursor.Update(gameTime);

            resumeButton.Update(gameTime);
            goToMainMenuButton.Update(gameTime);
            quitGameButton.Update(gameTime);
            cursor.Update(gameTime);
            MicycleControllerState newState = MicycleController.GetState();
            if (old.IsReleased(MicycleControls.B) && newState.IsPressed(MicycleControls.B) && activeButton == null)
            {
                eventQueue.AddEvent(new MiEvent(ButtonEntrance), 20);
                eventQueue.AddEvent(new MiEvent(CursorEntrance), 0); 
            }

            if (old.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A) && activeButton == resumeButton)
            {
                eventQueue.AddEvent(activeButton.Pressed, 20);
            }

            if (old.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A) && (activeButton == goToMainMenuButton))
            {
                eventQueue.AddEvent(ResumeGame, 20);
                eventQueue.AddEvent(activeButton.Pressed, 0);
            }
            if (old.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A) && (activeButton == quitGameButton))
            {
                eventQueue.AddEvent(activeButton.Pressed, 0);
            }
            if (old.IsReleased(MicycleControls.DOWN) && newState.IsPressed(MicycleControls.DOWN) && activeButton == resumeButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                eventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
            }

            if (old.IsReleased(MicycleControls.DOWN) && newState.IsPressed(MicycleControls.DOWN) && activeButton == goToMainMenuButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToQuitGameButton), 20);
                eventQueue.AddEvent(new MiEvent(SetQuitGameButtonAsActive), 0);
            }

            if (old.IsReleased(MicycleControls.UP) && newState.IsPressed(MicycleControls.UP) && activeButton == quitGameButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                eventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
            }

            if (old.IsReleased(MicycleControls.UP) && newState.IsPressed(MicycleControls.UP) && activeButton == goToMainMenuButton)
            {
                eventQueue.AddEvent(new MiEvent(MoveCursorResumeButton), 20);
                eventQueue.AddEvent(new MiEvent(SetResumeButtonAsActive), 0);
            }

            old = MicycleController.GetState();
            MiEvent nextEvent = eventQueue.GetNextEvent();
            if (nextEvent != null)
                nextEvent();
            base.Update(gameTime);
        }
        public override void LoadContent()
        {

            resumeButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            goToMainMenuButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButton.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            factory.AddTexture(Game.Content.Load<Texture2D>("Factory"), 0);
            school.AddTexture(Game.Content.Load<Texture2D>("School"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("City"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("RnD"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();

            resumeButton.Draw(gameTime);
            goToMainMenuButton.Draw(gameTime);
            quitGameButton.Draw(gameTime);
            cursor.Draw(gameTime);

            if (factory.Visible)
                factory.Draw(gameTime);

            if (school.Visible)
                school.Draw(gameTime);

            if (city.Visible)
                city.Draw(gameTime);

            if (rnd.Visible)
                rnd.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
