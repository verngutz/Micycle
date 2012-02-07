using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MiUtil
{
    /// <summary>
    /// MiAnimation defines an animation sequence as a queue of textures. 
    /// The textures are drawn one by one using a queue, with each texture staying on the queue for a certain amount of time.
    /// On top of that, the position, scaling, and rotation of the textures can be changed over time by manipulating the sequence's movement curves.
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
        public Curve RotationPointXOverTime { get; set; }
        public Curve RotationPointYOverTime { get; set; }

        public ulong Time { get; set; }

        private Vector2 position;
        public Vector2 Position { get { return position; } }

        private Vector2 rotationPoint;
        private float scale;
        private float rotation;

        public MiAnimatingComponent(MiGame game) : this(game, 0, 0, 1, 0, 0, 0) { }

        public MiAnimatingComponent(MiGame game, float default_x, float default_y) : this(game, default_x, default_y, 1, 0, 0, 0) { }

        public MiAnimatingComponent(MiGame game, float default_x, float default_y, float default_scale, float default_rotate, float rotation_x, float rotation_y)
            : base(game)
        {
            spriteQueue = new Queue<KeyValuePair<Texture2D, int>>();
            spriteQueueTimer = 0;

            position = new Vector2(default_x, default_y);
            scale = default_scale;
            rotation = default_rotate;
            rotationPoint = new Vector2(rotation_x, rotation_y);

            XPositionOverTime = new Curve();
            XPositionOverTime.Keys.Add(new CurveKey(0, default_x));

            YPositionOverTime = new Curve();
            YPositionOverTime.Keys.Add(new CurveKey(0, default_y));

            ScalingOverTime = new Curve();
            ScalingOverTime.Keys.Add(new CurveKey(0, default_scale));

            RotationOverTime = new Curve();
            RotationOverTime.Keys.Add(new CurveKey(0, default_rotate));

            RotationPointXOverTime = new Curve();
            RotationPointXOverTime.Keys.Add(new CurveKey(0, rotation_x));

            RotationPointYOverTime = new Curve();
            RotationPointYOverTime.Keys.Add(new CurveKey(0, rotation_y));
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
            Time++;
            spriteQueueTimer++;
            if (SpriteQueueEnabled && spriteQueueTimer > spriteQueue.Peek().Value)
            {
                spriteQueueTimer = 0;
                if (SpriteQueueLoop)
                    spriteQueue.Enqueue(spriteQueue.Dequeue());
                else if (spriteQueue.Count > 1)
                    spriteQueue.Dequeue();
            }

            position.X = XPositionOverTime.Evaluate(Time);
            position.Y = YPositionOverTime.Evaluate(Time);
            scale = ScalingOverTime.Evaluate(Time);
            rotation = RotationOverTime.Evaluate(Time);
            rotationPoint.X = RotationPointXOverTime.Evaluate(Time);
            rotationPoint.Y = RotationPointYOverTime.Evaluate(Time);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(spriteQueue.Peek().Key, position, null, Color.White, rotation, rotationPoint, scale, SpriteEffects.None, 0);
        }
    }
}
