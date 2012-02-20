using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Micycle
{
    public class Node
    {
        public Node next;
        public Vector2 position;

        public Node(int x, int y)
        {
            position = new Vector2(x, y);
        }
    }
}
