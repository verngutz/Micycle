using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MiUtil;

namespace Micycle
{
    class MicycleControls
    {
        public static int SIZE = 7;
        public static MiControl LEFT = new MiControl((int)Button.LEFT);
        public static MiControl RIGHT = new MiControl((int)Button.RIGHT);
        public static MiControl UP = new MiControl((int)Button.UP);
        public static MiControl DOWN = new MiControl((int)Button.DOWN);
        public static MiControl A = new MiControl((int)Button.A);
        public static MiControl B = new MiControl((int)Button.B);
        public static MiControl START = new MiControl((int)Button.START);

        private enum Button
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            A,
            B,
            START
        }
    }
}
