using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    /// <summary>
    /// Initializes a new instance of the MiControllerState class
    /// </summary>
    public abstract class MiControllerState
    {
        public abstract ButtonState this[MiControl control] { get; set; }
        public abstract bool IsPressed(MiControl control);
        public abstract bool IsReleased(MiControl control);
    }
}
