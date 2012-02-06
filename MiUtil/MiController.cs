using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    public abstract class MiController<T> : MiGameComponent where T : MiControllerState, new()
    {
        MiControllerState controllerState;

        public MiController(MiGame game) : base(game)
        {
            controllerState = new T();
        }

        public MiControllerState GetState()
        {
            return controllerState;
        }
    }
}
