using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        private const int NEW_GAME_BUTTON_X = 130;
        private const int NEW_GAME_BUTTON_Y = 500;
        private const int NEW_GAME_BUTTON_WIDTH = 640;
        private const int NEW_GAME_BUTTON_HEIGHT = 240;
        private const float NEW_GAME_BUTTON_SCALE = 0.4f;
        private const float NEW_GAME_BUTTON_ORIGIN_X = NEW_GAME_BUTTON_SCALE * NEW_GAME_BUTTON_WIDTH / 2;
        private const float NEW_GAME_BUTTON_ORIGIN_Y = NEW_GAME_BUTTON_SCALE * NEW_GAME_BUTTON_HEIGHT / 2;

        private const int QUIT_GAME_BUTTON_X = 420;
        private const int QUIT_GAME_BUTTON_Y = 500;
        private const int QUIT_GAME_BUTTON_WIDTH = 640;
        private const int QUIT_GAME_BUTTON_HEIGHT = 240;
        private const float QUIT_GAME_BUTTON_SCALE = 0.4f;
        private const float QUIT_GAME_BUTTON_ORIGIN_X = QUIT_GAME_BUTTON_SCALE * QUIT_GAME_BUTTON_WIDTH / 2;
        private const float QUIT_GAME_BUTTON_ORIGIN_Y = QUIT_GAME_BUTTON_SCALE * QUIT_GAME_BUTTON_HEIGHT / 2;

        private MiGameScreen gameScreen;

        private Texture2D background;

        private MiAnimatingComponent newGameButtonBase;
        private MiAnimatingComponent newGameButtonHover;
        private MiAnimatingComponent newGameButtonClick;

        private MiAnimatingComponent quitGameButtonBase;
        private MiAnimatingComponent quitGameButtonHover;
        private MiAnimatingComponent quitGameButtonClick;

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
            // New Game Button
            //
            newGameButton = new MiButton();
            newGameButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    Game.ToUpdate.Push(gameScreen);
                    Game.ToDraw.AddLast(gameScreen);
                    gameScreen.Initialize();
                    return null;
                });
            newGameButtonBase = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, 0, 0);
            newGameButtonHover = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, 0, 0, 0);
            newGameButtonClick = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, NEW_GAME_BUTTON_ORIGIN_X, NEW_GAME_BUTTON_ORIGIN_Y, 0);
            newGameButtonHover.AlphaChangeEnabled = true;
            newGameButtonClick.AlphaChangeEnabled = true;
            newGameButtonClick.MoveEnabled = true;
            newGameButtonClick.ScaleEnabled = true;

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
            quitGameButtonBase = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, 0, 0);
            quitGameButtonHover = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, 0, 0, 0);
            quitGameButtonClick = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, QUIT_GAME_BUTTON_ORIGIN_X, QUIT_GAME_BUTTON_ORIGIN_Y, 0);
            quitGameButtonHover.AlphaChangeEnabled = true;
            quitGameButtonClick.AlphaChangeEnabled = true;
            quitGameButtonClick.MoveEnabled = true;
            quitGameButtonClick.ScaleEnabled = true;

            ActiveButton = newGameButton;
        }

        public override IEnumerator<ulong> EntrySequence()
        {
            entrySequenceMutex = true;
            newGameButtonHover.AlphaOverTime.Keys.Clear();
            newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer, 0));
            newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
            newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Oscillate;
            yield return 0;
            entrySequenceMutex = false;
        }

        public override IEnumerator<ulong> Pressed()
        {
            ulong yieldVal = 60;
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                exitSequenceMutex = true;
                if (ActiveButton == newGameButton)
                {
                    newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
                    newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                    newGameButtonClick.AlphaOverTime.Keys.Clear();
                    newGameButtonClick.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer, 0));
                    newGameButtonClick.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
                    newGameButtonClick.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonClick.MoveTimer, NEW_GAME_BUTTON_X));
                    newGameButtonClick.YPositionOverTime.Keys.Add(new CurveKey(newGameButtonClick.MoveTimer, NEW_GAME_BUTTON_Y));
                    newGameButtonClick.XPositionOverTime.Keys.Add(new CurveKey(newGameButtonClick.MoveTimer + yieldVal, NEW_GAME_BUTTON_X - 3 * NEW_GAME_BUTTON_SCALE * NEW_GAME_BUTTON_WIDTH / 2));
                    newGameButtonClick.YPositionOverTime.Keys.Add(new CurveKey(newGameButtonClick.MoveTimer + yieldVal, NEW_GAME_BUTTON_Y - 3 * NEW_GAME_BUTTON_SCALE * NEW_GAME_BUTTON_HEIGHT / 2));
                    newGameButtonClick.ScalingOverTime.Keys.Add(new CurveKey(newGameButtonClick.ScaleTimer, NEW_GAME_BUTTON_SCALE));
                    newGameButtonClick.ScalingOverTime.Keys.Add(new CurveKey(newGameButtonClick.ScaleTimer + yieldVal, NEW_GAME_BUTTON_SCALE * 4));
                }
                else if (ActiveButton == quitGameButton)
                {
                    quitGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer + 40, 255));
                    quitGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                    quitGameButtonClick.AlphaOverTime.Keys.Clear();
                    quitGameButtonClick.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer, 255));
                    quitGameButtonClick.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer + 40, 255));
                    quitGameButtonClick.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonClick.MoveTimer, QUIT_GAME_BUTTON_X));
                    quitGameButtonClick.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonClick.MoveTimer, QUIT_GAME_BUTTON_Y));
                    quitGameButtonClick.XPositionOverTime.Keys.Add(new CurveKey(quitGameButtonClick.MoveTimer + yieldVal, QUIT_GAME_BUTTON_X - 3 * QUIT_GAME_BUTTON_SCALE * QUIT_GAME_BUTTON_WIDTH / 2));
                    quitGameButtonClick.YPositionOverTime.Keys.Add(new CurveKey(quitGameButtonClick.MoveTimer + yieldVal, QUIT_GAME_BUTTON_Y - 3 * QUIT_GAME_BUTTON_SCALE * QUIT_GAME_BUTTON_HEIGHT / 2));
                    quitGameButtonClick.ScalingOverTime.Keys.Add(new CurveKey(quitGameButtonClick.ScaleTimer, QUIT_GAME_BUTTON_SCALE));
                    quitGameButtonClick.ScalingOverTime.Keys.Add(new CurveKey(quitGameButtonClick.ScaleTimer + yieldVal, QUIT_GAME_BUTTON_SCALE * 4));
                }
                yield return yieldVal;
                ActiveButton.Pressed();
                newGameButtonClick.AlphaOverTime.Keys.Clear();
                quitGameButtonClick.AlphaOverTime.Keys.Clear();
                newGameButtonClick.XPositionOverTime.Keys.Clear();
                newGameButtonClick.YPositionOverTime.Keys.Clear();
                quitGameButtonClick.XPositionOverTime.Keys.Clear();
                quitGameButtonClick.YPositionOverTime.Keys.Clear();
                newGameButtonClick.ScalingOverTime.Keys.Clear();
                quitGameButtonClick.ScalingOverTime.Keys.Clear();
                exitSequenceMutex = false;
            }
        }

        public override IEnumerator<ulong> Lefted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == quitGameButton)
            {
                ActiveButton = null;
                quitGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer + 40, 0));
                quitGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                newGameButtonHover.AlphaOverTime.Keys.Clear();
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer, 0));
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
                newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Oscillate;
                ActiveButton = newGameButton;
                yield return 0;
            }
        }

        public override IEnumerator<ulong> Righted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == newGameButton)
            {
                ActiveButton = null;
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 0));
                newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                quitGameButtonHover.AlphaOverTime.Keys.Clear();
                quitGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer, 0));
                quitGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer + 40, 255));
                quitGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Oscillate;
                ActiveButton = quitGameButton;
                yield return 40;
            }
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen\\SCREEN");

            newGameButtonBase.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\START\\BUTTON"), 0);
            newGameButtonHover.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\START\\HOVER_INDICATOR"), 0);
            newGameButtonClick.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\START\\CLICK_INDICATOR"), 0);

            quitGameButtonBase.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\EXIT\\BUTTON"), 0);
            quitGameButtonHover.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\EXIT\\HOVER_INDICATOR"), 0);
            quitGameButtonClick.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\EXIT\\CLICKED_INDICATOR"), 0);

            gameScreen.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            newGameButtonBase.Update(gameTime);
            newGameButtonHover.Update(gameTime);
            newGameButtonClick.Update(gameTime);

            quitGameButtonBase.Update(gameTime);
            quitGameButtonHover.Update(gameTime);
            quitGameButtonClick.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);

            newGameButtonBase.Draw(gameTime);
            newGameButtonHover.Draw(gameTime);
            newGameButtonClick.Draw(gameTime);

            quitGameButtonBase.Draw(gameTime);
            quitGameButtonHover.Draw(gameTime);
            quitGameButtonClick.Draw(gameTime);
        }
    }
}
