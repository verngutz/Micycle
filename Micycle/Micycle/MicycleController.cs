using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using MiUtil;

namespace Micycle
{
    class MicycleController : MiController<MicycleControllerState>
    {
        new public static MicycleControllerState GetState()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            controllerState = new MicycleControllerState();
            if (gamePadState.IsConnected)
            {
                controllerState[MicycleControls.A] = gamePadState.Buttons.A;
                controllerState[MicycleControls.B] = gamePadState.Buttons.B;
                controllerState[MicycleControls.LEFT] = gamePadState.DPad.Left;
                controllerState[MicycleControls.RIGHT] = gamePadState.DPad.Right;
                controllerState[MicycleControls.UP] = gamePadState.DPad.Up;
                controllerState[MicycleControls.DOWN] = gamePadState.DPad.Down;

                Vector2 ts_direction = gamePadState.ThumbSticks.Left;
                if (ts_direction.X > 0) controllerState[MicycleControls.RIGHT] = ButtonState.Pressed;
                if (ts_direction.X < 0) controllerState[MicycleControls.LEFT] = ButtonState.Pressed;
                if (ts_direction.Y > 0) controllerState[MicycleControls.UP] = ButtonState.Pressed;
                if (ts_direction.Y < 0) controllerState[MicycleControls.DOWN] = ButtonState.Pressed;

            }
            else
            {
                controllerState[MicycleControls.A] = (ButtonState)keyboardState[Keys.Space];
                controllerState[MicycleControls.B] = (ButtonState)keyboardState[Keys.LeftShift];
                controllerState[MicycleControls.LEFT] = (ButtonState)keyboardState[Keys.Left];
                controllerState[MicycleControls.RIGHT] = (ButtonState)keyboardState[Keys.Right];
                controllerState[MicycleControls.UP] = (ButtonState)keyboardState[Keys.Up];
                controllerState[MicycleControls.DOWN] = (ButtonState)keyboardState[Keys.Down];
            }
            return controllerState;
        }
    }
}
