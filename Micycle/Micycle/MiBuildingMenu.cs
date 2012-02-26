using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    public abstract class MiBuildingMenu : MiScreen
    {
        private static int centerX;
        private static int centerY;

        private const int RADIUS = 100;

        private const int UP_BUTTON_WIDTH = 100;
        private const int UP_BUTTON_HEIGHT = 75;
        private const float UP_BUTTON_SCALE = 0.5f;
        private static int UP_BUTTON_CENTER_X { get { return centerX; } }
        private static int UP_BUTTON_CENTER_Y { get { return centerY - RADIUS; } }
        private static int UP_BUTTON_X { get { return UP_BUTTON_CENTER_X - (int)(UP_BUTTON_SCALE * UP_BUTTON_WIDTH / 2); } }
        private static int UP_BUTTON_Y { get { return UP_BUTTON_CENTER_Y - (int)(UP_BUTTON_SCALE * UP_BUTTON_HEIGHT / 2); } }

        private const int DOWN_BUTTON_WIDTH = 100;
        private const int DOWN_BUTTON_HEIGHT = 75;
        private const float DOWN_BUTTON_SCALE = 0.5f;
        private static int DOWN_BUTTON_CENTER_X { get { return centerX; } }
        private static int DOWN_BUTTON_CENTER_Y { get { return centerY + RADIUS; } }
        private static int DOWN_BUTTON_X { get { return DOWN_BUTTON_CENTER_X - (int)(DOWN_BUTTON_SCALE * DOWN_BUTTON_WIDTH / 2); } }
        private static int DOWN_BUTTON_Y { get { return DOWN_BUTTON_CENTER_Y - (int)(DOWN_BUTTON_SCALE * DOWN_BUTTON_HEIGHT / 2); } }  

        private const int LEFT_BUTTON_WIDTH = 100;
        private const int LEFT_BUTTON_HEIGHT = 75;
        private const float LEFT_BUTTON_SCALE = 0.5f;
        private static int LEFT_BUTTON_CENTER_X { get { return centerX - RADIUS; } }
        private static int LEFT_BUTTON_CENTER_Y { get { return centerY; } }
        private static int LEFT_BUTTON_X { get { return LEFT_BUTTON_CENTER_X - (int)(LEFT_BUTTON_SCALE * LEFT_BUTTON_WIDTH / 2); } }
        private static int LEFT_BUTTON_Y { get { return LEFT_BUTTON_CENTER_Y - (int)(LEFT_BUTTON_SCALE * LEFT_BUTTON_HEIGHT / 2); } }

        private const int RIGHT_BUTTON_WIDTH = 100;
        private const int RIGHT_BUTTON_HEIGHT = 75;
        private const float RIGHT_BUTTON_SCALE = 0.5f;
        private static int RIGHT_BUTTON_CENTER_X { get { return centerX + RADIUS; } }
        private static int RIGHT_BUTTON_CENTER_Y { get { return centerY; } }
        private static int RIGHT_BUTTON_X { get { return RIGHT_BUTTON_CENTER_X - (int)(RIGHT_BUTTON_SCALE * RIGHT_BUTTON_WIDTH / 2); } }
        private static int RIGHT_BUTTON_Y { get { return RIGHT_BUTTON_CENTER_Y - (int)(RIGHT_BUTTON_SCALE * RIGHT_BUTTON_HEIGHT / 2); } }

        private const int CANCEL_BUTTON_WIDTH = 100;
        private const int CANCEL_BUTTON_HEIGHT = 75;
        private const float CANCEL_BUTTON_SCALE = 0.5f;
        private static int CANCEL_BUTTON_CENTER_X { get { return centerX; } }
        private static int CANCEL_BUTTON_CENTER_Y { get { return centerY; } }
        private static int CANCEL_BUTTON_X { get { return CANCEL_BUTTON_CENTER_X - (int)(CANCEL_BUTTON_SCALE * CANCEL_BUTTON_WIDTH / 2); } }
        private static int CANCEL_BUTTON_Y { get { return CANCEL_BUTTON_CENTER_Y - (int)(CANCEL_BUTTON_SCALE * CANCEL_BUTTON_HEIGHT / 2); } }

        protected MiAnimatingComponent cursor;
        protected MiAnimatingComponent upButtonGraphic;
        protected MiAnimatingComponent downButtonGraphic;
        protected MiAnimatingComponent leftButtonGraphic;
        protected MiAnimatingComponent rightButtonGraphic;
        protected MiAnimatingComponent cancelButtonGraphic;

        protected MiButton upButton;
        protected MiButton downButton;
        protected MiButton leftButton;
        protected MiButton rightButton;
        private MiButton cancelButton;

        public MiBuildingMenu(Micycle game, int center_x, int center_y)
            : base(game)
        {
            centerX = center_x;
            centerY = center_y;

            cursor = new MiAnimatingComponent(game, CANCEL_BUTTON_X, CANCEL_BUTTON_Y, 0.5f, 0, 0, 0);
            upButtonGraphic = new MiAnimatingComponent(game, UP_BUTTON_X, UP_BUTTON_Y, UP_BUTTON_SCALE, 0, 0, 0);
            downButtonGraphic = new MiAnimatingComponent(game, DOWN_BUTTON_X, DOWN_BUTTON_Y, DOWN_BUTTON_SCALE, 0, 0, 0);
            leftButtonGraphic = new MiAnimatingComponent(game, LEFT_BUTTON_X, LEFT_BUTTON_Y, LEFT_BUTTON_SCALE, 0, 0, 0);
            rightButtonGraphic = new MiAnimatingComponent(game, RIGHT_BUTTON_X, RIGHT_BUTTON_Y, RIGHT_BUTTON_SCALE, 0, 0, 0);
            cancelButtonGraphic = new MiAnimatingComponent(game, CANCEL_BUTTON_X, CANCEL_BUTTON_Y, CANCEL_BUTTON_SCALE, 0, 0, 0);

            upButton = new MiButton();
            downButton = new MiButton();
            leftButton = new MiButton();
            rightButton = new MiButton();
            cancelButton = new MiButton();
            cancelButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Pop();
                    Game.ToDraw.RemoveLast();
                    return null;
                });

            ActiveButton = cancelButton;
        }

        public override IEnumerator<int> Pressed()
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

        public override IEnumerator<int> Cancelled()
        {
            if (entrySequenceMutex || exitSequenceMutex)
            {
                yield break;
            }
            else
            {
                ActiveButton = cancelButton;
                Game.ScriptEngine.ExecuteScript(new MiScript(Pressed));
            }
        }

        public override IEnumerator<int> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == cancelButton || ActiveButton == leftButton || ActiveButton == rightButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, upButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, upButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = upButton;
            }
            else if (ActiveButton == downButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = cancelButton;
            }
        }

        public override IEnumerator<int> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == cancelButton || ActiveButton == leftButton || ActiveButton == rightButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, downButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, downButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = downButton;
            }
            else if (ActiveButton == upButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = cancelButton;
            }
        }

        public override IEnumerator<int> Lefted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == cancelButton || ActiveButton == upButton || ActiveButton == downButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, leftButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, leftButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = leftButton;
            }
            else if (ActiveButton == rightButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = cancelButton;
            }
        }

        public override IEnumerator<int> Righted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            else if (ActiveButton == cancelButton || ActiveButton == upButton || ActiveButton == downButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rightButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rightButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = rightButton;
            }
            else if (ActiveButton == leftButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, cancelButtonGraphic.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = cancelButton;
            }
        }

        public override void LoadContent()
        {
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            upButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            downButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            leftButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            rightButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            cancelButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            upButtonGraphic.Update(gameTime);
            downButtonGraphic.Update(gameTime);
            leftButtonGraphic.Update(gameTime);
            rightButtonGraphic.Update(gameTime);
            cancelButtonGraphic.Update(gameTime);
            cursor.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            upButtonGraphic.Draw(gameTime);
            downButtonGraphic.Draw(gameTime);
            leftButtonGraphic.Draw(gameTime);
            rightButtonGraphic.Draw(gameTime);
            cancelButtonGraphic.Draw(gameTime);
            cursor.Draw(gameTime);
        }
    }
}
