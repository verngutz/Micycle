
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
namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        Texture2D factory;
        Texture2D school;
        Texture2D city;
        Texture2D rnd;
        public MiGameScreen(Micycle game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            factory = Game.Content.Load<Texture2D>("Factory");
            school = Game.Content.Load<Texture2D>("School");
            city = Game.Content.Load<Texture2D>("City");
            rnd = Game.Content.Load<Texture2D>("RnD");
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            
            this.sb.Draw(school, new Rectangle(250, 250, 100, 100), Color.White );
            this.sb.Draw(factory, new Rectangle(250, 250, 100, 100), Color.White);
            this.sb.Draw(city, new Rectangle(250, 250, 100, 100), Color.White);
            this.sb.Draw(rnd, new Rectangle(250, 250, 100, 100), Color.White); 
        }
    }
}
