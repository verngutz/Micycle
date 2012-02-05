using Microsoft.Xna.Framework;

namespace Micycle
{
    abstract class MiScreen : DrawableGameComponent
    {
        public MiScreen(Micycle game) : base(game) 
        {
            Enabled = false;
            Visible = false;
        }

        //public abstract void RespondTo(MiCommand command);
    }
}
