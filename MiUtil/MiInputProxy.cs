using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    public class MiInputProxy : MiGameComponent
    {
        MiControllerState controllerState;

        public MiInputProxy(MiGame game)
            : base(game)
        {
            controllerState = new MiControllerState();
        }

        public MiControllerState GetControllerState()
        {
            return controllerState;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
