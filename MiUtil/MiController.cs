using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    public abstract class MiController<T> : MiGameComponent where T : MiControllerState, new()
    {
        protected T controllerState;

        public MiController(MiGame game) : base(game)
        {
            controllerState = new T();
        }

        /*public T GetState()
        {
            return controllerState;
        }*/
    }
}
