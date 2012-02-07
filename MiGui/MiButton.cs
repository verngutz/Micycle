using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace MiGui
{
    public class MiButton : MiAnimatingComponent
    {
        public MiEvent Pressed;

        public MiButtonState State { set; get; }
        private Queue<KeyValuePair<Texture2D, int>> unhoveredSpriteQueue;
        private Queue<KeyValuePair<Texture2D, int>> hoveredSpriteQueue;
        private Queue<KeyValuePair<Texture2D, int>> depressedSpriteQueue;

        public MiButton(MiGame game) : base(game) { }

        public MiButton(MiGame game, float default_x, float default_y) : base(game, default_x, default_y) { }

        public MiButton(MiGame game, float default_x, float default_y, float default_scale, float default_rotate, float rotation_x, float rotation_y)
            : base(game, default_x, default_y, default_scale, default_rotate, rotation_x, rotation_y) { }

        /**
        public override void AddTexture(Texture2D texture, int time)
        {
            throw new NotImplementedException();
        }
         */
    }
}
