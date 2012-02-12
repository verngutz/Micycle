using Microsoft.Xna.Framework.Input;

using MiUtil;

namespace Micycle
{
    class MicycleControllerState: MiControllerState
    {
        private ButtonState[] controlStates;

        public MicycleControllerState()
        {
            controlStates = new ButtonState[MicycleControls.SIZE];
        }

        public override ButtonState this[MiControl control]
        {
            get { return controlStates[control]; }
            set { controlStates[control] = value; }
        }

        public override bool IsReleased(MiControl control)
        {
            if (controlStates[control] == ButtonState.Released) return true;
            return false;
        }

        public override bool IsPressed(MiControl control)
        {
            return !IsReleased(control);
        }
    }
}
