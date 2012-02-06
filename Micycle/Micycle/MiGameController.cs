using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MiUtil;
namespace Micycle
{
    public class MiGameController : MiController<MiGameControllerState>
    {
        public MiGameController(MiGame game) : base(game)
        {
        }

        public  MiGameControllerState GetState() {

            return this.controllerState.copy();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            if (gamePadState.IsConnected)
            {
                this.controllerState[MiGameControls.A] = gamePadState.Buttons.A;
                this.controllerState[MiGameControls.B] = gamePadState.Buttons.B;
                this.controllerState[MiGameControls.LEFT] = gamePadState.DPad.Left;
                this.controllerState[MiGameControls.RIGHT] = gamePadState.DPad.Right;
                this.controllerState[MiGameControls.UP] = gamePadState.DPad.Up;
                this.controllerState[MiGameControls.DOWN] = gamePadState.DPad.Down;

                Vector2 ts_direction = gamePadState.ThumbSticks.Left;
                if (ts_direction.X > 0) this.controllerState[MiGameControls.RIGHT] = ButtonState.Pressed;
                if (ts_direction.X < 0) this.controllerState[MiGameControls.LEFT] = ButtonState.Pressed;
                if (ts_direction.Y > 0) this.controllerState[MiGameControls.UP] = ButtonState.Pressed;
                if (ts_direction.Y < 0) this.controllerState[MiGameControls.DOWN] = ButtonState.Pressed;

            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.A))
                    this.controllerState[MiGameControls.A] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.A] = ButtonState.Released;

                if (keyboardState.IsKeyDown(Keys.B))
                    this.controllerState[MiGameControls.B] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.B] = ButtonState.Released;

                if (keyboardState.IsKeyDown(Keys.Left))
                    this.controllerState[MiGameControls.LEFT] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.LEFT] = ButtonState.Released;

                if (keyboardState.IsKeyDown(Keys.Right))
                    this.controllerState[MiGameControls.RIGHT] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.RIGHT] = ButtonState.Released;

                if (keyboardState.IsKeyDown(Keys.Up))
                    this.controllerState[MiGameControls.UP] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.UP] = ButtonState.Released;

                if (keyboardState.IsKeyDown(Keys.Down))
                    this.controllerState[MiGameControls.DOWN] = ButtonState.Pressed;
                else
                    this.controllerState[MiGameControls.DOWN] = ButtonState.Released;
            }
        }
    }
}
