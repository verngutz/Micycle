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

        private const int RADIUS = 150;

        private Texture2D background;
        private static readonly Vector2 BACKGROUND_ORIGIN = new Vector2(16, 16);
        private static readonly Color BACKGROUND_COLOR = new Color(255, 255, 255, 200);
        private Rectangle backgroundRectangle;
        private const int BACKGROUND_RECTANGLE_WIDTH = 200;
        private const int BACKGROUND_RECTANGLE_HEIGHT = 250;

        private const int TOP_PADDING = 5;
        private const int LEFT_PADDING = 5;
        private const int RIGHT_PADDING = 5;
        private const int BOTTOM_PADDING = 5;

        private const int TEXT_HEIGHT = 20;
        private const int BAR_THICKNESS = 20;

        protected SpriteFont buildingStatsFont;
        protected Texture2D buildingStatBarFull;
        protected Texture2D buildingStatBar;

        private Vector2 stat1TextPosition;
        protected Vector2 Stat1TextPosition { get { return stat1TextPosition; } }
        private Vector2 stat2TextPosition;
        protected Vector2 Stat2TextPosition { get { return stat2TextPosition; } }
        private Vector2 stat3TextPosition;
        protected Vector2 Stat3TextPosition { get { return stat3TextPosition; } }
        private Rectangle stat1Bar;
        protected Rectangle Stat1Bar { get { return stat1Bar; } }
        private Rectangle stat2Bar;
        protected Rectangle Stat2Bar { get { return stat2Bar; } }
        private Rectangle stat3Bar;
        protected Rectangle Stat3Bar { get { return stat3Bar; } }

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
        protected MicycleGameSystem System { get { return system; } }

        public MiBuildingMenu(Micycle game, float center_x, float center_y, MicycleGameSystem system, MiInGameMenu inGameMenu)
            : base(game)
        {
            centerX = center_x;
            centerY = center_y;
            this.system = system;
            this.inGameMenu = inGameMenu;

            backgroundRectangle = new Rectangle((int)center_x, (int)center_y, BACKGROUND_RECTANGLE_WIDTH, BACKGROUND_RECTANGLE_HEIGHT);

            stat1TextPosition = new Vector2(center_x - BACKGROUND_RECTANGLE_WIDTH / 2 + LEFT_PADDING, center_y - BACKGROUND_RECTANGLE_HEIGHT / 2 + TOP_PADDING);
            stat2TextPosition = stat1TextPosition + new Vector2(0, TEXT_HEIGHT + BAR_THICKNESS);
            stat3TextPosition = stat2TextPosition + new Vector2(0, TEXT_HEIGHT + BAR_THICKNESS);
            stat1Bar = new Rectangle((int)stat1TextPosition.X, (int)stat1TextPosition.Y + TEXT_HEIGHT, BACKGROUND_RECTANGLE_WIDTH - LEFT_PADDING - RIGHT_PADDING, BAR_THICKNESS);
            stat2Bar = new Rectangle((int)stat2TextPosition.X, (int)stat2TextPosition.Y + TEXT_HEIGHT, stat1Bar.Width, stat1Bar.Height);
            stat3Bar = new Rectangle((int)stat3TextPosition.X, (int)stat3TextPosition.Y + TEXT_HEIGHT, stat2Bar.Width, stat2Bar.Height);

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
            background = Game.Content.Load<Texture2D>("BlackOut");

            buildingStatsFont = Game.Content.Load<SpriteFont>("Fonts\\buildingStatFont");
            buildingStatBarFull = Game.Content.Load<Texture2D>("horizontalBar");
            buildingStatBar = Game.Content.Load<Texture2D>("horizontalBar");

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
            Game.SpriteBatch.Draw(background, backgroundRectangle, null, BACKGROUND_COLOR, 0, BACKGROUND_ORIGIN, SpriteEffects.None, 0);
            upButtonGraphic.Draw(gameTime);
            downButtonGraphic.Draw(gameTime);
            leftButtonGraphic.Draw(gameTime);
            rightButtonGraphic.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
