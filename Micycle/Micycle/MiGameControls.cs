using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MiUtil;

namespace Micycle
{
    public class MiGameControls
    {
        public static int SIZE = 6;
        public static MiControl LEFT;
        public static MiControl RIGHT;
        public static MiControl UP;
        public static MiControl DOWN;
        public static MiControl A;
        public static MiControl B;

        public enum Button
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            A,
            B
        }

        public MiGameControls() {
            LEFT = new MiControl((int)Button.LEFT);
            RIGHT = new MiControl((int)Button.RIGHT);
            UP = new MiControl((int)Button.UP);
            DOWN = new MiControl((int)Button.DOWN);
            A = new MiControl((int)Button.A);
            B = new MiControl((int)Button.B);
        }
    }
}
