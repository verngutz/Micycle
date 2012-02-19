using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;
using MiGui;

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
            cursor.Enabled = false;
            cursor.MoveEnabled = true;

            //
            // Resume Button
            //
            resumeButton = new MiButton();
            resumeButton.Pressed += new MiEvent(ResumeGame);
            resumeButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);
            resumeButtonGraphic.MoveEnabled = true;

            //
            // Quit Button
            //
            quitGameButton = new MiButton();
            quitGameButton.Pressed += new MiEvent(Game.Exit);
            quitGameButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);
            quitGameButtonGraphic.MoveEnabled = true;

            //
            // Go To Main Menu Button
            //
            goToMainMenuButton = new MiButton();
            goToMainMenuButton.Pressed += new MiEvent(GoToMenuScreen);
            goToMainMenuButtonGraphic = new MiAnimatingComponent(game, 200, -100, 1, 0, 0, 0);
            goToMainMenuButtonGraphic.MoveEnabled = true;

            //
            // Action Events
            //
            Upped += delegate
            {
                if (ActiveButton == quitGameButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                    Game.EventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
                }
                if (ActiveButton == goToMainMenuButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToResumeButton), 20);
                    Game.EventQueue.AddEvent(new MiEvent(SetResumeButtonAsActive), 0);
                }
            };

            Downed += delegate
            {
                if (ActiveButton == resumeButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToGoToMainMenuButton), 20);
                    Game.EventQueue.AddEvent(new MiEvent(SetGoToMainMenuButtonAsActive), 0);
                }
                if (ActiveButton == goToMainMenuButton)
                {
                    Game.EventQueue.AddEvent(new MiEvent(MoveCursorToQuitGameButton), 20);
                    Game.EventQueue.AddEvent(new MiEvent(SetQuitGameButtonAsActive), 0);
                }

            };

            Pressed += delegate
            {
                Game.EventQueue.AddEvent(new MiEvent(ExitSequence), 20);
                Game.EventQueue.AddEvent(new MiEvent(PressActiveButton), 0);
            };

        }

        public void EntrySequence()
        {
            Game.EventQueue.AddEvent(new MiEvent(ButtonEntrance), 20);
            Game.EventQueue.AddEvent(new MiEvent(CursorEntrance), 0);
            Game.EventQueue.AddEvent(new MiEvent(FreezeButtons), 0);

            ActiveButton = resumeButton;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time, resumeButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time, resumeButtonGraphic.Position.Y));
        }

        public void ExitSequence()
        {
            cursor.Visible = false;

            resumeButtonGraphic.Enabled = true;
            goToMainMenuButtonGraphic.Enabled = true;
            quitGameButtonGraphic.Enabled = true;

            resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.Time + 20, -100));
            goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.Time + 20, -100));
            quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 20, -100));
        }

        private void ButtonEntrance()
        {
            resumeButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(resumeButtonGraphic.Time + 20, 50));
            goToMainMenuButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(goToMainMenuButtonGraphic.Time + 20, 150));
            quitGameButtonGraphic.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonGraphic.Time + 20, 250));
        }

        private void CursorEntrance()
        {
            cursor.Visible = true;
        }

        private void FreezeButtons()
        {
            resumeButtonGraphic.Enabled = false;
            goToMainMenuButtonGraphic.Enabled = false;
            quitGameButtonGraphic.Enabled = false;
        }

        public void ResumeGame()
        {
            Enabled = false;
            Visible = false;
            Game.ToUpdate.Pop();
            Game.ToDraw.Pop();
            Game.InputHandler.Focused = Game.ToUpdate.Peek();
        }

        private void MoveCursorToGoToMainMenuButton()
        {
            ActiveButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, goToMainMenuButtonGraphic.Position.Y));
        }

        private void SetGoToMainMenuButtonAsActive()
        {
            cursor.Enabled = false;
            ActiveButton = goToMainMenuButton;
        }

        private void MoveCursorToQuitGameButton()
        {
            ActiveButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, quitGameButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, quitGameButtonGraphic.Position.Y));
        }

        private void SetQuitGameButtonAsActive()
        {
            cursor.Enabled = false;
            ActiveButton = quitGameButton;
        }

        private void MoveCursorToResumeButton()
        {
            ActiveButton = null;
            cursor.Enabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.Time + 20, resumeButtonGraphic.Position.Y));
        }

        private void SetResumeButtonAsActive()
        {
            cursor.Enabled = false;
            ActiveButton = resumeButton;
        }

        private void GoToMenuScreen()
        {
            Enabled = false;
            Visible = false;
            MenuScreen.Enabled = true;
            MenuScreen.Visible = true;
            Game.ToUpdate.Pop();
            Game.ToUpdate.Pop();
            Game.ToDraw.Pop();
            Game.ToDraw.Pop();
            Game.ToUpdate.Push(MenuScreen);
            Game.ToDraw.Push(MenuScreen);
            Game.InputHandler.Focused = MenuScreen;
            MenuScreen.EntrySequence();
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
            if (resumeButtonGraphic.Enabled)
                resumeButtonGraphic.Update(gameTime);

            if (goToMainMenuButtonGraphic.Enabled)
                goToMainMenuButtonGraphic.Update(gameTime);

            if (quitGameButtonGraphic.Enabled)
                quitGameButtonGraphic.Update(gameTime);

            if (cursor.Enabled)
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
