using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiDrawableComponent : DrawableGameComponent
    {
        public new MiGame Game { get; set; }
        public MiDrawableComponent(MiGame game)
            : base(game)
        {
            Game = game;
        }

        public new virtual void LoadContent()
        {
            base.LoadContent();
        }
    }
}
