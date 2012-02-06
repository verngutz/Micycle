using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiScreen : MiDrawableComponent
    {
        public MiScreen(MiGame game)
            : base(game)
        {
            Enabled = false;
            Visible = false;
        }
    }
}
