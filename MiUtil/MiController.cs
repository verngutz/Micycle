using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    public abstract class MiController<T> : MiComponent where T : MiControllerState, new()
    {
        protected MiControllerState controllerState;

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
