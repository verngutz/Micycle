using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiUtil;

namespace Micycle
{
    public class Mice : MiDrawableComponent
    {
        public static int speed = 20;

        private Texture2D image;
        string filename;
        Vector2 position;
        Vector2 dimension;
        Node path;

        public Boolean IsDone { get; set; }

        public Mice(MiGame game, Node path, int x, int y, int w, int h, string filename)
            : base(game)
        {
            this.filename = filename;
            this.path = path;
            position = new Vector2(x, y);
            dimension = new Vector2(w, h);
            IsDone = false;
        }

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 unit = (path.position - this.position) / Vector2.Distance(path.position, position);
            Vector2 futureDest = this.position + unit * speed;
            if (Vector2.Distance(futureDest, path.position) < speed)
            {
                this.position = path.position;
                if (path.next != null) path = path.next;
                else IsDone = true;
                
            }
            else
            {
                this.position = futureDest;
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            image = Game.Content.Load<Texture2D>(filename);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.Game.SpriteBatch.Draw(image, new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y), Color.White);

        }
    }

    
}
