using System;

using Microsoft.Xna.Framework;

using MiUtil;

namespace Micycle
{
    class MicycleInputHandler : MiInputHandler
    {
        private const int HOLD_INTERVAL = 30;
        private int holdCount;
        private int holdATimer;

        public MicycleInputHandler(Micycle game)
            : base(game)
        {
            holdCount = 1;
            holdATimer = 0;
            oldState = MicycleController.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            MicycleControllerState newState = MicycleController.GetState();
            if (oldState.IsReleased(MicycleControls.UP) && newState.IsPressed(MicycleControls.UP))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Upped);
            }

            if (oldState.IsReleased(MicycleControls.DOWN) && newState.IsPressed(MicycleControls.DOWN))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Downed);
            }

            if (oldState.IsReleased(MicycleControls.LEFT) && newState.IsPressed(MicycleControls.LEFT))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Lefted);
            }

            if (oldState.IsReleased(MicycleControls.RIGHT) && newState.IsPressed(MicycleControls.RIGHT))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Righted);
            }

            if (oldState.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Pressed);
            }

            if (Focused is MiBuildingMenu)
            {
                if (oldState.IsPressed(MicycleControls.A) && newState.IsPressed(MicycleControls.A))
                {
                    holdATimer++;
                    if (holdATimer > HOLD_INTERVAL / holdCount)
                    {
                        holdCount++;
                        holdATimer = 0;
                        Game.ScriptEngine.ExecuteScript(Focused.Pressed);
                    }
                }

                if (newState.IsReleased(MicycleControls.A))
                {
                    holdCount = 1;
                    holdATimer = 0;
                }
            }

            if (oldState.IsReleased(MicycleControls.B) && newState.IsPressed(MicycleControls.B))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Cancelled);
            }

            if (oldState.IsReleased(MicycleControls.START) && newState.IsPressed(MicycleControls.START))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Escaped);
            }

            oldState = newState;
        }
    }
}
