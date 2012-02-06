using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiUtil
{
    public abstract class MiController<T> where T : MiControllerState, new()
    {
        protected static T controllerState = new T();
        public static T GetState() { throw new NotImplementedException();  }
    }
}
