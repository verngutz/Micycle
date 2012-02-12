using System.Collections.Generic;

using Microsoft.Xna.Framework;

using MiUtil;

namespace MiGui
{
    public abstract class MiScreen : MiGameState
    {
        public MiButton ActiveButton { get; set; }

        public MiScreen(MiGame game)
            : base(game)
        {
            Enabled = false;
            Visible = false;
        }
    }
}
