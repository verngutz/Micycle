using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    /// <summary>
    /// Initializes a new instance of the MiControllerState class
    /// </summary>
    public abstract class MiControllerState
    {
        public abstract KeyState this[MiControl control] { get; }
        public abstract MiControl[] GetPressedControls();
        public abstract bool IsUp(MiControl control);
        public abstract bool IsDown(MiControl control);
    }
}
