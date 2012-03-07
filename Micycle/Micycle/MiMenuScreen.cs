using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MiUtil;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        private const float BACKGROUND_SCALE = 0.67f;

        private const int NEW_GAME_BUTTON_X = 258;
        private const int NEW_GAME_BUTTON_Y = 548;
        private const int NEW_GAME_BUTTON_WIDTH = 640;
        private const int NEW_GAME_BUTTON_HEIGHT = 240;
        private const float NEW_GAME_BUTTON_SCALE = 0.4f;
        private const float NEW_GAME_BUTTON_ORIGIN_X = NEW_GAME_BUTTON_WIDTH / 2;
        private const float NEW_GAME_BUTTON_ORIGIN_Y = NEW_GAME_BUTTON_HEIGHT / 2;

        private const int QUIT_GAME_BUTTON_X = 558;
        private const int QUIT_GAME_BUTTON_Y = 548;
        private const int QUIT_GAME_BUTTON_WIDTH = 640;
        private const int QUIT_GAME_BUTTON_HEIGHT = 240;
        private const float QUIT_GAME_BUTTON_SCALE = 0.4f;
        private const float QUIT_GAME_BUTTON_ORIGIN_X = QUIT_GAME_BUTTON_WIDTH / 2;
        private const float QUIT_GAME_BUTTON_ORIGIN_Y = QUIT_GAME_BUTTON_HEIGHT / 2;

        private const float BUTTON_CLICK_RESIZE = 1.3f;

        private MiGameScreen gameScreen;

        private MiAnimatingComponent background;

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
            background = new MiAnimatingComponent(game, 0, 0, BACKGROUND_SCALE, 0, 0, 0);
            background.AlphaChangeEnabled = true;

            //
            // New Game Button
            //
            newGameButton = new MiButton();
            newGameButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    gameScreen.Reset();
                    Game.ToUpdate.Push(gameScreen);
                    Game.ToDraw.AddLast(gameScreen);
                    gameScreen.Initialize();
                    return null;
                });
            newGameButtonBase = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, NEW_GAME_BUTTON_ORIGIN_X, NEW_GAME_BUTTON_ORIGIN_Y);
            newGameButtonHover = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, NEW_GAME_BUTTON_ORIGIN_X, NEW_GAME_BUTTON_ORIGIN_Y, 0);
            newGameButtonClick = new MiAnimatingComponent(game, NEW_GAME_BUTTON_X, NEW_GAME_BUTTON_Y, NEW_GAME_BUTTON_SCALE, 0, NEW_GAME_BUTTON_ORIGIN_X, NEW_GAME_BUTTON_ORIGIN_Y, 0);
            newGameButtonHover.AlphaChangeEnabled = true;

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
            quitGameButtonBase = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, QUIT_GAME_BUTTON_ORIGIN_X, QUIT_GAME_BUTTON_ORIGIN_Y);
            quitGameButtonHover = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, QUIT_GAME_BUTTON_ORIGIN_X, QUIT_GAME_BUTTON_ORIGIN_Y, 0);
            quitGameButtonClick = new MiAnimatingComponent(game, QUIT_GAME_BUTTON_X, QUIT_GAME_BUTTON_Y, QUIT_GAME_BUTTON_SCALE, 0, QUIT_GAME_BUTTON_ORIGIN_X, QUIT_GAME_BUTTON_ORIGIN_Y, 0);
            quitGameButtonHover.AlphaChangeEnabled = true;

            ActiveButton = newGameButton;
        }

        public override IEnumerator<ulong> EntrySequence()
        {
            entrySequenceMutex = true;
            background.AlphaChangeEnabled = true;
            background.AlphaOverTime.Keys.Add(new CurveKey(background.AlphaChangeTimer + 40, 255));
            quitGameButtonBase.AlphaChangeEnabled = true;
            quitGameButtonBase.AlphaOverTime.Keys.Add(new CurveKey(background.AlphaChangeTimer + 40, 255));
            newGameButtonBase.AlphaChangeEnabled = true;
            newGameButtonBase.AlphaOverTime.Keys.Add(new CurveKey(background.AlphaChangeTimer + 40, 255));
            newGameButtonHover.AlphaOverTime.Keys.Clear();
            newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer, 0));
            newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
            newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Oscillate;
            yield return 40;
            background.AlphaChangeEnabled = false;
            quitGameButtonBase.AlphaChangeEnabled = false;
            newGameButtonBase.AlphaChangeEnabled = false;
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
                MiAnimatingComponent thisbase;
                MiAnimatingComponent otherbase;
                MiAnimatingComponent hover;
                MiAnimatingComponent click;
                float scale;
                if (ActiveButton == newGameButton)
                {
                    thisbase = newGameButtonBase;
                    otherbase = quitGameButtonBase;
                    hover = newGameButtonHover;
                    click = newGameButtonClick;
                    scale = NEW_GAME_BUTTON_SCALE;
                    yield return 20;
                }
                else if (ActiveButton == quitGameButton)
                {
                    thisbase = quitGameButtonBase;
                    otherbase = newGameButtonBase;
                    hover = quitGameButtonHover;
                    click = quitGameButtonClick;
                    scale = QUIT_GAME_BUTTON_SCALE;
                    yield return 20;
                }
                else
                {
                    yield break;
                }

                background.AlphaChangeEnabled = true;
                background.AlphaOverTime.Keys.Add(new CurveKey(background.AlphaChangeTimer + 40, 0));
                thisbase.AlphaChangeEnabled = true;
                thisbase.AlphaOverTime.Keys.Add(new CurveKey(thisbase.AlphaChangeTimer + 40, 0));
                otherbase.AlphaChangeEnabled = true;
                otherbase.AlphaOverTime.Keys.Add(new CurveKey(otherbase.AlphaChangeTimer + 40, 0));
                hover.AlphaOverTime.Keys.Add(new CurveKey(hover.AlphaChangeTimer + 20, 255));
                hover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                click.AlphaChangeEnabled = true;
                click.AlphaOverTime.Keys.Add(new CurveKey(click.AlphaChangeTimer, 0));
                click.AlphaOverTime.Keys.Add(new CurveKey(click.AlphaChangeTimer + 20, 255));
                click.ScaleEnabled = true;
                click.ScalingOverTime.Keys.Add(new CurveKey(click.ScaleTimer + 20, scale * BUTTON_CLICK_RESIZE));
                yield return 20;
                hover.AlphaChangeEnabled = true;
                hover.AlphaOverTime.Keys.Add(new CurveKey(hover.AlphaChangeTimer + 20, 0));
                click.AlphaChangeEnabled = true;
                click.AlphaOverTime.Keys.Add(new CurveKey(click.AlphaChangeTimer + 40, 0));
                click.ScaleEnabled = true;
                click.ScalingOverTime.Keys.Add(new CurveKey(click.ScaleTimer + 40, scale * 20));
                click.ScalingOverTime.Keys.Add(new CurveKey(click.ScaleTimer + 60, scale));
                yield return 20;
                background.AlphaChangeEnabled = false;
                thisbase.AlphaChangeEnabled = false;
                otherbase.AlphaChangeEnabled = false;
                yield return 60;
                click.AlphaChangeEnabled = false;
                click.ScaleEnabled = false;
                ActiveButton.Pressed();
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
                quitGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(quitGameButtonHover.AlphaChangeTimer + 20, 0));
                quitGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Constant;
                newGameButtonHover.AlphaOverTime.Keys.Clear();
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer, 0));
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 40, 255));
                newGameButtonHover.AlphaOverTime.PostLoop = CurveLoopType.Oscillate;
                ActiveButton = newGameButton;
                yield return 40;
            }
        }

        public override IEnumerator<ulong> Righted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == newGameButton)
            {
                ActiveButton = null;
                newGameButtonHover.AlphaOverTime.Keys.Add(new CurveKey(newGameButtonHover.AlphaChangeTimer + 20, 0));
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
            background.AddTexture(Game.Content.Load<Texture2D>("MenuScreen\\SCREEN"), 0);

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
            background.Update(gameTime);

            newGameButtonBase.Update(gameTime);
            newGameButtonHover.Update(gameTime);
            newGameButtonClick.Update(gameTime);

            quitGameButtonBase.Update(gameTime);
            quitGameButtonHover.Update(gameTime);
            quitGameButtonClick.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            background.Draw(gameTime);

            newGameButtonBase.Draw(gameTime);
            newGameButtonHover.Draw(gameTime);
            newGameButtonClick.Draw(gameTime);

            quitGameButtonBase.Draw(gameTime);
            quitGameButtonHover.Draw(gameTime);
            quitGameButtonClick.Draw(gameTime);
        }
    }
}
