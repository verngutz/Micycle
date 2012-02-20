using Microsoft.Xna.Framework;

using MiUtil;

namespace Micycle
{
    class MicycleInputHandler : MiInputHandler
    {
        public MicycleInputHandler(Micycle game)
            : base(game)
        {
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

            if (oldState.IsReleased(MicycleControls.B) && newState.IsPressed(MicycleControls.B))
            {
                Game.ScriptEngine.ExecuteScript(Focused.Cancelled);
            }

            oldState = newState;
        }
    }
}
