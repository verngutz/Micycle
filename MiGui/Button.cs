using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MiGui
{
    public delegate void ActionEvent ( Button button );
    public class Button
    {
        Button Up { set; get; }
        Button Down { set; get; }
        Button Left { set; get; }
        Button Right { set; get; }

        public event ActionEvent Pressed;
        protected virtual void OnPressed(EventArgs e)
        {
            if (Pressed != null)
                Pressed(this);
        }

        
    }
}
