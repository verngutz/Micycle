using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

using MiUtil;

namespace Micycle
{
    class MiDialogScreen : MiScreen
    {
        private const int CORNER_SIZE = 162;
        private const int SIDE_SIZE = 8;

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

        private Texture2D background;

        private Texture2D panelUpperLeftCorner;
        private Texture2D panelOtherCorner;
        private Texture2D panelSide;
        private Texture2D panelFill;

        private Rectangle panelUpperLeftRect;
        private Rectangle panelUpperRightRect;
        private Rectangle panelLowerLeftRect;
        private Rectangle panelLowerRightRect;
        private List<Rectangle> panelLeftSideRect;
        private List<Rectangle> panelRightSideRect;
        private List<Rectangle> panelTopSideRect;
        private List<Rectangle> panelBottomSideRect;
        private Rectangle panelFillRect;

        private static Vector2 PANEL_CORNER_ORIGIN = new Vector2(163, 163);
        private static Vector2 PANEL_SIDE_ORIGIN = new Vector2(162, 8.5f);
        private static Vector2 PANEL_FILL_ORIGIN = new Vector2(112, 112);

        private MiAnimatingComponent cursor;
        protected MiAnimatingComponent Cursor { get { return cursor; } }

        private const int CURSOR_WIDTH = 40;
        private const int CURSOR_HEIGHT = 40;
        private const int CURSOR_ORIGIN_X = CURSOR_WIDTH / 2;
        private const int CURSOR_ORIGIN_Y = CURSOR_HEIGHT / 2;
        private const float CURSOR_ROTATE = (float)Math.PI * 3 / 4;

        private SpriteFont menuFont;
        protected SpriteFont MenuFont { get { return menuFont; } }

        private SoundEffect appearSound;
        protected SoundEffectInstance appearSoundInstance;
        private const float APPEAR_SOUND_VOLUME = 0.8f;

        public MiDialogScreen(Micycle game, int width, int height)
            : base(game)
        {
            //
            // Panel
            //

            /**
            panelUpperLeftCorner = new MiAnimatingComponent(game, MiResolution.Center.X - width / 2, MiResolution.Center.Y - height / 2, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelUpperRightCorner = new MiAnimatingComponent(game, MiResolution.Center.X + width / 2, MiResolution.Center.Y - height / 2, PANEL_SCALE, (float)Math.PI*3/2, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelLowerLeftCorner = new MiAnimatingComponent(game, MiResolution.Center.X - width / 2, MiResolution.Center.Y + height / 2, PANEL_SCALE, (float)Math.PI/2, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);
            panelLowerRightCorner = new MiAnimatingComponent(game, MiResolution.Center.X + width / 2, MiResolution.Center.Y + height / 2, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_CORNER_ORIGIN);

            panelLeftSide = new MiAnimatingComponent[(int)Math.Ceiling((height - PANEL_CORNER_SIZE * PANEL_SCALE)/(PANEL_SIDE_SIZE * PANEL_SCALE))];
            for (int i = 0; i < panelLeftSide.Length; i++)
                panelLeftSide[i] = new MiAnimatingComponent(game, MiResolution.Center.X - width / 2 + 1, MiResolution.Center.Y - height / 2 + PANEL_CORNER_ORIGIN * PANEL_SCALE + i * PANEL_SIDE_SIZE * PANEL_SCALE, PANEL_SCALE, 0, PANEL_CORNER_ORIGIN, PANEL_SIDE_HALFSIZE);

            panelRightSide = new MiAnimatingComponent[(int)Math.Ceiling((height - PANEL_CORNER_SIZE * PANEL_SCALE) / (PANEL_SIDE_SIZE * PANEL_SCALE))];
            for (int i = 0; i < panelLeftSide.Length; i++)
                panelRightSide[i] = new MiAnimatingComponent(game, MiResolution.Center.X + width / 2 - 1, MiResolution.Center.Y - height / 2 + PANEL_CORNER_ORIGIN * PANEL_SCALE + i * PANEL_SIDE_SIZE * PANEL_SCALE, PANEL_SCALE, (float)Math.PI, PANEL_CORNER_ORIGIN, PANEL_SIDE_HALFSIZE);
             */
            panelUpperLeftRect = new Rectangle((int)(MiResolution.Center.X - width / 2), (int)(MiResolution.Center.Y - height / 2), CORNER_SIZE, CORNER_SIZE);
            panelUpperRightRect = new Rectangle((int)(MiResolution.Center.X + width / 2), (int)(MiResolution.Center.Y - height / 2), CORNER_SIZE, CORNER_SIZE);
            panelLowerLeftRect = new Rectangle((int)(MiResolution.Center.X - width / 2), (int)(MiResolution.Center.Y + height / 2), CORNER_SIZE, CORNER_SIZE);
            panelLowerRightRect = new Rectangle((int)(MiResolution.Center.X + width / 2), (int)(MiResolution.Center.Y + height / 2), CORNER_SIZE, CORNER_SIZE);
            panelLeftSideRect = new List<Rectangle>();
            for (int i = 0; i <= Math.Ceiling((float)(height - CORNER_SIZE) / SIDE_SIZE); i++)
                panelLeftSideRect.Add(new Rectangle((int)(MiResolution.Center.X - width / 2), (int)(MiResolution.Center.Y - height / 2) + i * SIDE_SIZE + CORNER_SIZE / 2, CORNER_SIZE, SIDE_SIZE));
            panelRightSideRect = new List<Rectangle>();
            for (int i = 0; i <= Math.Ceiling((float)(height - CORNER_SIZE) / SIDE_SIZE); i++)
                panelRightSideRect.Add(new Rectangle((int)(MiResolution.Center.X + width / 2), (int)(MiResolution.Center.Y - height / 2) + i * SIDE_SIZE + CORNER_SIZE / 2, CORNER_SIZE, SIDE_SIZE));
            panelTopSideRect = new List<Rectangle>();
            for (int i = 0; i <= Math.Ceiling((float)(width - CORNER_SIZE) / SIDE_SIZE); i++)
                panelTopSideRect.Add(new Rectangle((int)(MiResolution.Center.X - width / 2) + i * SIDE_SIZE + CORNER_SIZE / 2, (int)(MiResolution.Center.Y - height / 2), CORNER_SIZE, SIDE_SIZE));
            panelBottomSideRect = new List<Rectangle>();
            for (int i = 0; i <= Math.Ceiling((float)(width - CORNER_SIZE) / SIDE_SIZE); i++)
                panelBottomSideRect.Add(new Rectangle((int)(MiResolution.Center.X - width / 2) + i * SIDE_SIZE + CORNER_SIZE / 2, (int)(MiResolution.Center.Y + height / 2), CORNER_SIZE, SIDE_SIZE));
            panelFillRect = new Rectangle((int)(MiResolution.Center.X), (int)(MiResolution.Center.Y), width, height);

            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 0, 0, 1, CURSOR_ROTATE, CURSOR_ORIGIN_X, CURSOR_ORIGIN_Y);
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
                appearSoundInstance.Play();
                ActiveButton.Pressed();
                exitSequenceMutex = false;
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
            background = Game.Content.Load<Texture2D>("BlackOut");

            panelUpperLeftCorner = Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER00");
            panelOtherCorner = Game.Content.Load<Texture2D>("InGameMenu\\PANEL_CORNER01");
            panelSide = Game.Content.Load<Texture2D>("InGameMenu\\panel_side");
            panelFill = Game.Content.Load<Texture2D>("InGameMenu\\panel_fill");

            menuFont = Game.Content.Load<SpriteFont>("Fonts\\Default");

            cursor.AddTexture(Game.Content.Load<Texture2D>("InGameMenu\\button_corner"), 0);

            appearSound = Game.Content.Load<SoundEffect>("SFX\\dialogAppear");
            appearSoundInstance = appearSound.CreateInstance();
            appearSoundInstance.Volume = APPEAR_SOUND_VOLUME;
        }

        public override void Update(GameTime gameTime)
        {
            cursor.Update(gameTime);
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
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);

            Game.SpriteBatch.Draw(panelUpperLeftCorner, panelUpperLeftRect, null, Color.White, 0, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelOtherCorner, panelUpperRightRect, null, Color.White, (float)Math.PI * 3 / 2, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelOtherCorner, panelLowerLeftRect, null, Color.White, (float)Math.PI / 2, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelUpperLeftCorner, panelLowerRightRect, null, Color.White, (float)Math.PI, PANEL_CORNER_ORIGIN, SpriteEffects.None, 0);
            foreach (Rectangle rect in panelTopSideRect)
                Game.SpriteBatch.Draw(panelSide, rect, null, Color.White, (float)Math.PI / 2, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            foreach (Rectangle rect in panelBottomSideRect)
                Game.SpriteBatch.Draw(panelSide, rect, null, Color.White, (float)Math.PI * 3 / 2, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            foreach (Rectangle rect in panelLeftSideRect)
                Game.SpriteBatch.Draw(panelSide, rect, null, Color.White, 0, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            foreach (Rectangle rect in panelRightSideRect)
                Game.SpriteBatch.Draw(panelSide, rect, null, Color.White, (float)Math.PI, PANEL_SIDE_ORIGIN, SpriteEffects.None, 0);
            Game.SpriteBatch.Draw(panelFill, panelFillRect, null, Color.White, 0, PANEL_FILL_ORIGIN, SpriteEffects.None, 0);

            cursor.Draw(gameTime);
        }
    }
}
