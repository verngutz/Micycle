using Microsoft.Xna.Framework;

using MiUtil;

namespace MiGui
{
    public abstract class MiScreen : MiDrawableGameComponent
    {
        public MiScreen(MiGame game)
            : base(game)
        {
            Enabled = false;
            Visible = false;
        }
    }
}
