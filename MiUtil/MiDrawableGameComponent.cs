using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiDrawableGameComponent : DrawableGameComponent
    {
        public new MiGame Game { get; set; }
        public MiDrawableGameComponent(MiGame game)
            : base(game)
        {
            Game = game;
        }
    }
}
