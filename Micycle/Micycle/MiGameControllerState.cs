using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using MiUtil;
namespace Micycle
{
    public class MiGameControllerState: MiControllerState
    {
        private MiGameControls gameControls;
        private ButtonState[] controlStates;

        public MiGameControllerState copy()
        {
            ButtonState[] states = new ButtonState[MiGameControls.SIZE];
            for (int i = 0; i < MiGameControls.SIZE; i++)
            {
                states[i] = controlStates[i];
            }

            MiGameControllerState toRet = new MiGameControllerState();
            toRet.setState(states);
            return toRet;
        }

        public void setState(ButtonState[] b)
        {
            controlStates = b;
        }
        public MiGameControllerState()
        {
            gameControls = new MiGameControls();
            controlStates = new ButtonState[MiGameControls.SIZE];

            for (int i = 0; i < MiGameControls.SIZE; i++)
            {
                controlStates[i] = ButtonState.Released;
            }
        }

        public override Microsoft.Xna.Framework.Input.ButtonState this[MiControl control]
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
