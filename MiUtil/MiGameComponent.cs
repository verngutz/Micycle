using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiGameComponent : GameComponent
    {
        public new MiGame Game { get; set; }
        public MiGameComponent(MiGame game)
            : base(game)
        {
            Game = game;
        }
    }
}
