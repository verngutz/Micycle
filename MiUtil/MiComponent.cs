using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiComponent : GameComponent
    {
        public new MiGame Game { get; set; }
        public MiComponent(MiGame game)
            : base(game)
        {
            Game = game;
        }
    }
}
