using Microsoft.Xna.Framework;

namespace MiUtil
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
