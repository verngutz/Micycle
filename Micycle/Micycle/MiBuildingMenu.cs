using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    abstract class MiBuildingMenu : MiScreen
    {
        private float centerX;
        private float centerY;

        private const int RADIUS = 100;

        private const int UP_BUTTON_WIDTH = 100;
        private const int UP_BUTTON_HEIGHT = 75;
        private const float UP_BUTTON_SCALE = 0.5f;
        private float UP_BUTTON_CENTER_X { get { return centerX; } }
        private float UP_BUTTON_CENTER_Y { get { return centerY - RADIUS; } }
        protected float UP_BUTTON_X { get { return UP_BUTTON_CENTER_X - UP_BUTTON_SCALE * UP_BUTTON_WIDTH / 2; } }
        protected float UP_BUTTON_Y { get { return UP_BUTTON_CENTER_Y - UP_BUTTON_SCALE * UP_BUTTON_HEIGHT / 2; } }

        private const int DOWN_BUTTON_WIDTH = 100;
        private const int DOWN_BUTTON_HEIGHT = 75;
        private const float DOWN_BUTTON_SCALE = 0.5f;
        private float DOWN_BUTTON_CENTER_X { get { return centerX; } }
        private float DOWN_BUTTON_CENTER_Y { get { return centerY + RADIUS; } }
        protected float DOWN_BUTTON_X { get { return DOWN_BUTTON_CENTER_X - DOWN_BUTTON_SCALE * DOWN_BUTTON_WIDTH / 2; } }
        protected float DOWN_BUTTON_Y { get { return DOWN_BUTTON_CENTER_Y - DOWN_BUTTON_SCALE * DOWN_BUTTON_HEIGHT / 2; } }  

        private const int LEFT_BUTTON_WIDTH = 100;
        private const int LEFT_BUTTON_HEIGHT = 75;
        private const float LEFT_BUTTON_SCALE = 0.5f;
        private float LEFT_BUTTON_CENTER_X { get { return centerX - RADIUS; } }
        private float LEFT_BUTTON_CENTER_Y { get { return centerY; } }
        protected float LEFT_BUTTON_X { get { return LEFT_BUTTON_CENTER_X - LEFT_BUTTON_SCALE * LEFT_BUTTON_WIDTH / 2; } }
        protected float LEFT_BUTTON_Y { get { return LEFT_BUTTON_CENTER_Y - LEFT_BUTTON_SCALE * LEFT_BUTTON_HEIGHT / 2; } }

        private const int RIGHT_BUTTON_WIDTH = 100;
        private const int RIGHT_BUTTON_HEIGHT = 75;
        private const float RIGHT_BUTTON_SCALE = 0.5f;
        private float RIGHT_BUTTON_CENTER_X { get { return centerX + RADIUS; } }
        private float RIGHT_BUTTON_CENTER_Y { get { return centerY; } }
        protected float RIGHT_BUTTON_X { get { return RIGHT_BUTTON_CENTER_X - RIGHT_BUTTON_SCALE * RIGHT_BUTTON_WIDTH / 2; } }
        protected float RIGHT_BUTTON_Y { get { return RIGHT_BUTTON_CENTER_Y - RIGHT_BUTTON_SCALE * RIGHT_BUTTON_HEIGHT / 2; } }

        private const int CANCEL_BUTTON_WIDTH = 100;
        private const int CANCEL_BUTTON_HEIGHT = 75;
        private const float CANCEL_BUTTON_SCALE = 0.5f;
        private float CANCEL_BUTTON_CENTER_X { get { return centerX; } }
        private float CANCEL_BUTTON_CENTER_Y { get { return centerY; } }
        private float CANCEL_BUTTON_X { get { return CANCEL_BUTTON_CENTER_X - CANCEL_BUTTON_SCALE * CANCEL_BUTTON_WIDTH / 2; } }
        private float CANCEL_BUTTON_Y { get { return CANCEL_BUTTON_CENTER_Y - CANCEL_BUTTON_SCALE * CANCEL_BUTTON_HEIGHT / 2; } }

        protected MiAnimatingComponent cursor;
        protected MiAnimatingComponent upButtonGraphic;
        protected MiAnimatingComponent downButtonGraphic;
        protected MiAnimatingComponent leftButtonGraphic;
        protected MiAnimatingComponent rightButtonGraphic;

        protected MiButton upButton;
        public MiButton UpButton { get { return upButton; } }

        protected MiButton downButton;
        public MiButton DownButton { get { return downButton; } }

        protected MiButton leftButton;
        public MiButton LeftButton { get { return leftButton; } }

        protected MiButton rightButton;
        public MiButton RightButton { get { return rightButton; } }

        private MiButton cancelButton;

        private MiInGameMenu inGameMenu;
        private MicycleGameSystem system;

        public MiBuildingMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game)
        {
            centerX = center_x;
            centerY = center_y;
            this.system = system;
            this.inGameMenu = inGameMenu;

            cursor = new MiAnimatingComponent(game, CANCEL_BUTTON_X, CANCEL_BUTTON_Y, 0.5f, 0, 0, 0);
            upButtonGraphic = new MiAnimatingComponent(game, UP_BUTTON_X, UP_BUTTON_Y, UP_BUTTON_SCALE, 0, 0, 0);
            downButtonGraphic = new MiAnimatingComponent(game, DOWN_BUTTON_X, DOWN_BUTTON_Y, DOWN_BUTTON_SCALE, 0, 0, 0);
            leftButtonGraphic = new MiAnimatingComponent(game, LEFT_BUTTON_X, LEFT_BUTTON_Y, LEFT_BUTTON_SCALE, 0, 0, 0);
            rightButtonGraphic = new MiAnimatingComponent(game, RIGHT_BUTTON_X, RIGHT_BUTTON_Y, RIGHT_BUTTON_SCALE, 0, 0, 0);

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

            cursor.Visible = false;
            ActiveButton = cancelButton;
        }

        public override IEnumerator<ulong> Pressed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            exitSequenceMutex = true;
            ActiveButton.Pressed();
            exitSequenceMutex = false;
        }

        public override IEnumerator<ulong> Escaped()
        {
            system.Enabled = false;
            Game.ToUpdate.Push(inGameMenu);
            Game.ToDraw.AddLast(inGameMenu);
            return inGameMenu.EntrySequence();
        }

        public override IEnumerator<ulong> Cancelled()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            cursor.Visible = false;
            cursor.MoveEnabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 1, CANCEL_BUTTON_X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 1, CANCEL_BUTTON_Y));
            yield return 1;
            cursor.MoveEnabled = false;
            ActiveButton = cancelButton;
            Game.ScriptEngine.ExecuteScript(new MiScript(Pressed));
        }

        public override IEnumerator<ulong> Upped()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            ActiveButton = upButton;
            cursor.Visible = true;
            cursor.MoveEnabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, upButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, upButtonGraphic.Position.Y));
            yield return 20;
            cursor.MoveEnabled = false;
        }

        public override IEnumerator<ulong> Downed()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            ActiveButton = downButton;
            cursor.Visible = true;
            cursor.MoveEnabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, downButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, downButtonGraphic.Position.Y));
            yield return 20;
            cursor.MoveEnabled = false;
        }

        public override IEnumerator<ulong> Lefted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            ActiveButton = leftButton;
            cursor.Visible = true;
            cursor.MoveEnabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, leftButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, leftButtonGraphic.Position.Y));
            yield return 20;
            cursor.MoveEnabled = false;
        }

        public override IEnumerator<ulong> Righted()
        {
            if (entrySequenceMutex || exitSequenceMutex)
                yield break;

            ActiveButton = rightButton;
            cursor.Visible = true;
            cursor.MoveEnabled = true;
            cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rightButtonGraphic.Position.X));
            cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rightButtonGraphic.Position.Y));
            yield return 20;
            cursor.MoveEnabled = false;
        }

        public override void LoadContent()
        {
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
            upButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            downButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            leftButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            rightButtonGraphic.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            upButtonGraphic.Update(gameTime);
            downButtonGraphic.Update(gameTime);
            leftButtonGraphic.Update(gameTime);
            rightButtonGraphic.Update(gameTime);
            cursor.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            upButtonGraphic.Draw(gameTime);
            downButtonGraphic.Draw(gameTime);
            leftButtonGraphic.Draw(gameTime);
            rightButtonGraphic.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
