using MiUtil;
using MiGui;

namespace Micycle
{
    class MicycleInputHandler : MiInputHandler
    {
        public MicycleInputHandler(Micycle game)
            : base(game)
        {
            oldState = MicycleController.GetState();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            MicycleControllerState newState = MicycleController.GetState();

            if (oldState.IsReleased(MicycleControls.UP) && newState.IsPressed(MicycleControls.UP))
            {
                Focused.Upped();
            }

            if (oldState.IsReleased(MicycleControls.DOWN) && newState.IsPressed(MicycleControls.DOWN))
            {
                System.Console.WriteLine("Downed");
                Focused.Downed();
            }

            if (oldState.IsReleased(MicycleControls.LEFT) && newState.IsPressed(MicycleControls.LEFT))
            {
                Focused.Lefted();
            }

            if (oldState.IsReleased(MicycleControls.RIGHT) && newState.IsPressed(MicycleControls.RIGHT))
            {
                Focused.Righted();
            }

            if (oldState.IsReleased(MicycleControls.A) && newState.IsPressed(MicycleControls.A))
            {
                Focused.Pressed();
            }

            if (oldState.IsReleased(MicycleControls.B) && newState.IsPressed(MicycleControls.B))
            {
                Focused.Cancelled();
            }

            oldState = newState;
        }
    }
}
