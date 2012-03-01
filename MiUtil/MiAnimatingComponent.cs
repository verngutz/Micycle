using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MiUtil
{
    /// <summary>
    /// MiAnimatingComponent defines an animation sequence as a queue of textures. 
    /// The textures are drawn one by one using a queue, with each texture staying on the queue for a certain amount of time.
    /// On top of that, the position, scaling, and rotation of the textures can be changed over time 
    /// by manipulating the sequence's movement curves.
    /// </summary>
    public class MiAnimatingComponent : MiDrawableComponent
    {
        protected Queue<KeyValuePair<Texture2D, int>> spriteQueue;
        private int spriteQueueTimer;
        public bool SpriteQueueLoop { get; set; }
        public bool SpriteQueueEnabled { get; set; }

        public Curve XPositionOverTime { get; set; }
        public Curve YPositionOverTime { get; set; }
        public Curve ScalingOverTime { get; set; }
        public Curve RotationOverTime { get; set; }
        public Curve OriginXOverTime { get; set; }
        public Curve OriginYOverTime { get; set; }
        public Curve AlphaOverTime { get; set; }

        public bool MoveEnabled { get; set; }
        public bool ScaleEnabled { get; set; }
        public bool RotateEnabled { get; set; }
        public bool OriginChangeEnabled { get; set; }
        public bool AlphaChangeEnabled { get; set; }

        private ulong moveTimer;
        public ulong MoveTimer { get { return moveTimer; } }

        private ulong scaleTimer;
        public ulong ScaleTimer { get { return scaleTimer; } }

        private ulong rotateTimer;
        public ulong RotateTimer { get { return rotateTimer; } }

        private ulong originChangeTimer;
        public ulong OriginChangeTimer { get { return originChangeTimer; } }

        private ulong alphaChangeTimer;
        public ulong AlphaChangeTimer { get { return alphaChangeTimer; } }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 origin;
        private float scale;
        private float rotation;

        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public MiAnimatingComponent(MiGame game) : this(game, 0, 0, 1, 0, 0, 0) { }

        public MiAnimatingComponent(MiGame game, float default_x, float default_y) : this(game, default_x, default_y, 1, 0, 0, 0) { }

        public MiAnimatingComponent(MiGame game, float default_x, float default_y, float default_scale, float default_rotate, float origin_x, float origin_y)
            : this(game, default_x, default_y, default_scale, default_rotate, origin_x, origin_y, 255) { }

        public MiAnimatingComponent(MiGame game, float default_x, float default_y, float default_scale, float default_rotate, float origin_x, float origin_y, byte alpha)
            : base(game)
        {
            spriteQueue = new Queue<KeyValuePair<Texture2D, int>>();
            spriteQueueTimer = 0;

            moveTimer = 0;

            position = new Vector2(default_x, default_y);
            scale = default_scale;
            rotation = default_rotate;
            origin = new Vector2(origin_x, origin_y);

            XPositionOverTime = new Curve();
            XPositionOverTime.Keys.Add(new CurveKey(0, default_x));

            YPositionOverTime = new Curve();
            YPositionOverTime.Keys.Add(new CurveKey(0, default_y));

            ScalingOverTime = new Curve();
            ScalingOverTime.Keys.Add(new CurveKey(0, default_scale));

            RotationOverTime = new Curve();
            RotationOverTime.Keys.Add(new CurveKey(0, default_rotate));

            OriginXOverTime = new Curve();
            OriginXOverTime.Keys.Add(new CurveKey(0, origin_x));

            OriginYOverTime = new Curve();
            OriginYOverTime.Keys.Add(new CurveKey(0, origin_y));

            AlphaOverTime = new Curve();
            AlphaOverTime.Keys.Add(new CurveKey(0, alpha));
            color = Color.White;
            color.A = alpha;
        }

        /// <summary>
        /// Add a texture to this animation sequence.
        /// </summary>
        /// <param name="texture">The texture that will be drawn</param>
        /// <param name="time">The number of frames the texture will be drawn</param>
        public virtual void AddTexture(Texture2D texture, int time)
        {
            spriteQueue.Enqueue(new KeyValuePair<Texture2D, int>(texture, time));
        }

        public override void Update(GameTime gameTime)
        {
            if (MoveEnabled)
            {
                moveTimer++;
                position.X = XPositionOverTime.Evaluate(MoveTimer);
                position.Y = YPositionOverTime.Evaluate(MoveTimer);
            }

            if (ScaleEnabled)
            {
                scaleTimer++;
                scale = ScalingOverTime.Evaluate(ScaleTimer);
            }

            if (RotateEnabled)
            {
                rotateTimer++;
                rotation = RotationOverTime.Evaluate(RotateTimer);
            }

            if(OriginChangeEnabled)
            {
                originChangeTimer++;
                origin.X = OriginXOverTime.Evaluate(OriginChangeTimer);
                origin.Y = OriginYOverTime.Evaluate(OriginChangeTimer);
            }

            if (AlphaChangeEnabled)
            {
                alphaChangeTimer++;
                color.A = (byte)AlphaOverTime.Evaluate(AlphaChangeTimer);
            }

            if (SpriteQueueEnabled)
            {
                if (spriteQueueTimer++ > spriteQueue.Peek().Value)
                {
                    spriteQueueTimer = 0;
                    if (SpriteQueueLoop)
                        spriteQueue.Enqueue(spriteQueue.Dequeue());
                    else if (spriteQueue.Count > 1)
                        spriteQueue.Dequeue();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(spriteQueue.Peek().Key, position, null, Color, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
