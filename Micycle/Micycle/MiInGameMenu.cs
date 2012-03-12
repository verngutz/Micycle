using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiInGameMenu : MiScreen
    {
        private const int WIDTH = 400;
        private const int HEIGHT = 600;

        private const int CORNER_SIZE = 160;

        /**
        private const int PANEL_CORNER_SIZE = 328;
        private const int PANEL_CORNER_ORIGIN = PANEL_CORNER_SIZE / 2;
        private const int PANEL_SIDE_SIZE = 17;
        private const int PANEL_SIDE_HALFSIZE = PANEL_SIDE_SIZE / 2;
        private const float PANEL_SCALE = 0.5f;
        private MiAnimatingComponent panelUpperLeftCorner;
        private MiAnimatingComponent panelUpperRightCorner;
        private MiAnimatingComponent panelLowerLeftCorner;
        private MiAnimatingComponent panelLowerRightCorner;
        private MiAnimatingComponent[] panelLeftSide;
        private MiAnimatingComponent[] panelRightSide;
        private MiAnimatingComponent[] panelTopSide;
        private MiAnimatingComponent[] panelBottomSide;
        private MiAnimatingComponent panelFill;
         */

        private Texture2D panelUpperLeftCorner;
        private Texture2D panelOtherCorner;
        private Texture2D panelSide;
        private Texture2D panelFill;

        private Rectangle panelUpperLeftRect;
        private Rectangle panelUpperRightRect;
        private Rectangle panelLowerLeftRect;
        private Rectangle panelLowerRightRect;
        private Rectangle panelLeftSideRect;
        private Rectangle panelRightSideRect;
        private Rectangle panelTopSideRect;
        private Rectangle panelBottomSideRect;
        private Rectangle panelFillRect;

        private static Vector2 PANEL_CORNER_ORIGIN = new Vector2(163, 163);
        private static Vector2 PANEL_SIDE_ORIGIN = new Vector2(162, 8.5f);
        private static Vector2 PANEL_FILL_ORIGIN = new Vector2(112, 112);

        private MiAnimatingComponent cursorUpperLeftCorner;
        private MiAnimatingComponent cursorLowerRightCorner;
        private const int CURSOR_PADDING = 20;
        private const int CURSOR_WIDTH = 40;
        private const int CURSOR_HEIGHT = 40;
        private const int CURSOR_ORIGIN_X = CURSOR_WIDTH / 2;
        private const int CURSOR_ORIGIN_Y = CURSOR_HEIGHT / 2;
        private const float CURSOR_ROTATE = (float)Math.PI * 3 / 4;

        private SpriteFont menuFont;

        private MiButton resumeButton;
        private const string RESUME_STRING = "Resume Game";
        private static Vector2 resumePosition = MiResolution.Center + new Vector2(0, - HEIGHT / 4);
        private Vector2 resumeOrigin;

        private MiButton goToMainMenuButton;
        private const string GO_TO_MAIN_MENU_STRING = "Return to Main Menu";
        private static Vector2 goToMainMenuPosition = MiResolution.Center + new Vector2(0, 0);
        private Vector2 goToMainMenuOrigin;

        private MiButton quitGameButton;
        private const string QUIT_GAME_BUTTON_STRING = "Quit Game";
        private static Vector2 quitGamePosition = MiResolution.Center + new Vector2(0, HEIGHT / 4);
        private Vector2 quitGameOrigin;

        private MicycleGameSystem system;

        public MiInGameMenu(Micycle game, MicycleGameSystem system)
            : base(game)
        {
            this.system = system;

            //
            // Panel
            //

            /**
            panelUpperLeftCorner = new MiAnimatingComponent(game, MiResolution.Center.X - WIDTH / 2, MiResolution.Center.Y - HEIGHT / 2, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelUpperRightCorner = new MiAnimatingComponent(game, MiResolution.Center.X + WIDTH / 2, MiResolution.Center.Y - HEIGHT / 2, PANEL_SCALE, (float)Math.PI*3/2, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelLowerLeftCorner = new MiAnimatingComponent(game, MiResolution.Center.X - WIDTH / 2, MiResolution.Center.Y + HEIGHT / 2, PANEL_SCALE, (float)Math.PI/2, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelLowerRightCorner = new MiAnimatingComponent(game, MiResolution.Center.X + WIDTH / 2, MiResolution.Center.Y + HEIGHT / 2, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);

            panelLeftSide = new MiAnimatingComponent[(int)Math.Ceiling((HEIGHT - PANEL_CORNER_SIZE * PANEL_SCALE)/(PANEL_SIDE_SIZE * PANEL_SCALE))];
            for (int i = 0; i < panelLeftSide.Length; i++)
                panelLeftSide[i] = new MiAnimatingComponent(game, MiResolution.Center.X - WIDTH / 2 + 1, MiResolution.Center.Y - HEIGHT / 2 + PANEL_CORNER_ORIGIN * PANEL_SCALE + i * PANEL_SIDE_SIZE * PANEL_SCALE, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_SIDE_HALFSIZE);

            panelRightSide = new MiAnimatingComponent[(int)Math.Ceiling((HEIGHT - PANEL_CORNER_SIZE * PANEL_SCALE) / (PANEL_SIDE_SIZE * PANEL_SCALE))];
            for (int i = 0; i < panelLeftSide.Length; i++)
                panelRightSide[i] = new MiAnimatingComponent(game, MiResolution.Center.X + WIDTH / 2 - 1, MiResolution.Center.Y - HEIGHT / 2 + PANEL_CORNER_ORIGIN * PANEL_SCALE + i * PANEL_SIDE_SIZE * PANEL_SCALE, PANEL_SCALE, (float)Math.PI, PANEL_CORNER_ORIGIN, PANEL_SIDE_HALFSIZE);
             */
            panelUpperLeftRect = new Rectangle((int)(MiResolution.Center.X - WIDTH / 2), (int)(MiResolution.Center.Y - HEIGHT / 2), CORNER_SIZE, CORNER_SIZE);
            panelUpperRightRect = new Rectangle((int)(MiResolution.Center.X + WIDTH / 2), (int)(MiResolution.Center.Y - HEIGHT / 2), CORNER_SIZE, CORNER_SIZE);
            panelLowerLeftRect = new Rectangle((int)(MiResolution.Center.X - WIDTH / 2), (int)(MiResolution.Center.Y + HEIGHT / 2), CORNER_SIZE, CORNER_SIZE);
            panelLowerRightRect = new Rectangle((int)(MiResolution.Center.X + WIDTH / 2), (int)(MiResolution.Center.Y + HEIGHT / 2), CORNER_SIZE, CORNER_SIZE);
            panelLeftSideRect = new Rectangle((int)(MiResolution.Center.X - WIDTH / 2), (int)(MiResolution.Center.Y - HEIGHT / 2) + HEIGHT / 2, CORNER_SIZE, HEIGHT - CORNER_SIZE);
            panelRightSideRect = new Rectangle((int)(MiResolution.Center.X + WIDTH / 2), (int)(MiResolution.Center.Y - HEIGHT / 2) + HEIGHT / 2, CORNER_SIZE, HEIGHT - CORNER_SIZE);
            panelTopSideRect = new Rectangle((int)(MiResolution.Center.X - WIDTH / 2) + WIDTH / 2, (int)(MiResolution.Center.Y - HEIGHT / 2), CORNER_SIZE, WIDTH - CORNER_SIZE);
            panelBottomSideRect = new Rectangle((int)(MiResolution.Center.X - WIDTH / 2) + WIDTH / 2, (int)(MiResolution.Center.Y + HEIGHT / 2), CORNER_SIZE, WIDTH - CORNER_SIZE);
            panelFillRect = new Rectangle((int)(MiResolution.Center.X), (int)(MiResolution.Center.Y), WIDTH, HEIGHT);

            //
            // Cursor
            //
            cursorUpperLeftCorner = new MiAnimatingComponent(game, (MiResolution.Center.X - WIDTH / 2) + CURSOR_PADDING, resumePosition.Y, 1, CURSOR_ROTATE, CURSOR_ORIGIN_X, CURSOR_ORIGIN_Y);


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
                    if (Game.ToUpdate.Peek() is MiBuildingMenu)
                    {
                        Game.ToUpdate.Pop();
                        Game.ToDraw.RemoveLast();
                    }
                    Game.ToUpdate.Pop();
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    Game.ToDraw.RemoveLast();
                    Game.ToUpdate.Push(game.StartScreen);
                    Game.ToDraw.AddLast(game.StartScreen);
                    Game.ScriptEngine.ExecuteScript(game.StartScreen.EntrySequence);
                    return null;
                });


            ActiveButton = resumeButton;
        }

        public override IEnumerator<ulong> EntrySequence()
        {
            entrySequenceMutex = true;
            yield return 0;
            entrySequenceMutex = false;
        }

        public override IEnumerator<ulong> Pressed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                exitSequenceMutex = true;
                ActiveButton.Pressed();
                exitSequenceMutex = false;
            }
        }

        public override IEnumerator<ulong> Escaped()
        {
            return Cancelled();
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
                cursorUpperLeftCorner.MoveEnabled = true;
                cursorUpperLeftCorner.YPositionOverTime.Keys.Add(new CurveKey(cursorUpperLeftCorner.MoveTimer + 1, resumePosition.Y));
                yield return 1;
                cursorUpperLeftCorner.MoveEnabled = false;
                Game.ScriptEngine.ExecuteScript(new MiScript(Pressed));
            }
        }

        public override IEnumerator<ulong> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = resumeButton;
                cursorUpperLeftCorner.MoveEnabled = true;
                cursorUpperLeftCorner.YPositionOverTime.Keys.Add(new CurveKey(cursorUpperLeftCorner.MoveTimer + 20, resumePosition.Y));
                yield return 20;
                cursorUpperLeftCorner.MoveEnabled = false;
            }

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = goToMainMenuButton;
                cursorUpperLeftCorner.MoveEnabled = true;
                cursorUpperLeftCorner.YPositionOverTime.Keys.Add(new CurveKey(cursorUpperLeftCorner.MoveTimer + 20, goToMainMenuPosition.Y));
                yield return 20;
                cursorUpperLeftCorner.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield return 0;

            else if (ActiveButton == goToMainMenuButton)
            {
                ActiveButton = quitGameButton;
                cursorUpperLeftCorner.MoveEnabled = true;
                cursorUpperLeftCorner.YPositionOverTime.Keys.Add(new CurveKey(cursorUpperLeftCorner.MoveTimer + 20, quitGamePosition.Y));
                yield return 20;
                cursorUpperLeftCorner.MoveEnabled = false;
            }
            else if (ActiveButton == resumeButton)
            {
                ActiveButton = goToMainMenuButton;
                cursorUpperLeftCorner.MoveEnabled = true;
                cursorUpperLeftCorner.YPositionOverTime.Keys.Add(new CurveKey(cursorUpperLeftCorner.MoveTimer + 20, goToMainMenuPosition.Y));
                yield return 20;
                cursorUpperLeftCorner.MoveEnabled = false;
            }
        }

        public override void LoadContent()
        {
            /**
            panelUpperLeftCorner.AddTexture(Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER00"), 0);

            Texture2D panelCorner1 = Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER01");
            panelUpperRightCorner.AddTexture(panelCorner1, 0);
            panelLowerLeftCorner.AddTexture(panelCorner1, 0);
            panelLowerRightCorner.AddTexture(panelCorner1, 0);

            Texture2D panelSide = Game.Content.Load<Texture2D>("InGameMenu\\panel_side");
            foreach (MiAnimatingComponent leftSide in panelLeftSide)
                leftSide.AddTexture(panelSide, 0);

            foreach (MiAnimatingComponent rightSide in panelRightSide)
                rightSide.AddTexture(panelSide, 0);
             */
            panelUpperLeftCorner = Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER00");
            panelOtherCorner = Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER01");
            panelSide = Game.Content.Load<Texture2D>("InGameMenu\\panel_side");
            panelFill = Game.Content.Load<Texture2D>("InGameMenu\\panel_fill");

            menuFont = Game.Content.Load<SpriteFont>("Default");
            resumeOrigin = menuFont.MeasureString(RESUME_STRING) / 2;
            goToMainMenuOrigin = menuFont.MeasureString(GO_TO_MAIN_MENU_STRING) / 2;
            quitGameOrigin = menuFont.MeasureString(QUIT_GAME_BUTTON_STRING) / 2;

            cursorUpperLeftCorner.AddTexture(Game.Content.Load<Texture2D>("InGameMenu\\button_corner"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            cursorUpperLeftCorner.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            /**
            panelUpperLeftCorner.Draw(gameTime);
            panelUpperRightCorner.Draw(gameTime);
            panelLowerLeftCorner.Draw(gameTime);
            panelLowerRightCorner.Draw(gameTime);

            foreach (MiAnimatingComponent leftSide in panelLeftSide)
                leftSide.Draw(gameTime);

            foreach (MiAnimatingComponent rightSide in panelRightSide)
                rightSide.Draw(gameTime);
            */

            Game.SpriteBatch.Draw(panelUpperLeftCorner, panelUpperLeftRect, null, Color.White, 0, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelOtherCorner, panelUpperRightRect, null, Color.White, (float)Math.PI*3/2, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelOtherCorner, panelLowerLeftRect, null, Color.White, (float)Math.PI/2, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelOtherCorner, panelLowerRightRect, null, Color.White, 0, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelSide, panelTopSideRect, null, Color.White, (float)Math.PI/2, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelSide, panelBottomSideRect, null, Color.White, (float)Math.PI*3/2, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelSide, panelLeftSideRect, null, Color.White, 0, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelSide, panelRightSideRect, null, Color.White, (float)Math.PI, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelFill, panelFillRect, null, Color.White, 0, PANEL_FILL_ORIGIN, SpriteEffects.None, 0);

            Game.SpriteBatch.DrawString(menuFont, RESUME_STRING, resumePosition, Color.Black, 0, resumeOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(menuFont, GO_TO_MAIN_MENU_STRING, goToMainMenuPosition, Color.Black, 0, goToMainMenuOrigin, 1, SpriteEffects.None, 0);
            Game.SpriteBatch.DrawString(menuFont, QUIT_GAME_BUTTON_STRING, quitGamePosition, Color.Black, 0, quitGameOrigin, 1, SpriteEffects.None, 0);

            cursorUpperLeftCorner.Draw(gameTime);
        }
    }
}
