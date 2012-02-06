using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiMenuScreen : MiScreen
    {
        Texture2D background;
        MiAnimating newGameButtonGraphics;
        MiAnimating quitGameButtonGraphics;
        MiEventQueue eventQueue;

        public MiMenuScreen(MiGame game)
            : base(game)
        {
            newGameButtonGraphics = new MiAnimating(game, -100, 350, 1, 0, 0, 0);
            quitGameButtonGraphics = new MiAnimating(game, -100, 460, 1, 0, 0, 0);

            newGameButtonGraphics.SpriteQueueEnabled = false;
            quitGameButtonGraphics.SpriteQueueEnabled = false;

            eventQueue = new MiEventQueue();
            newGameButtonGraphics.XPositionOverTime.Keys.Add(new CurveKey(100, 300));
            quitGameButtonGraphics.XPositionOverTime.Keys.Add(new CurveKey(100, 400));
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuScreen");
            newGameButtonGraphics.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            quitGameButtonGraphics.AddTexture(Game.Content.Load<Texture2D>("button"), 0);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            newGameButtonGraphics.Update(gameTime);
            quitGameButtonGraphics.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(background, MiResolution.BoundingRectangle, Color.White);
            newGameButtonGraphics.Draw(gameTime);
            quitGameButtonGraphics.Draw(gameTime);
            base.Draw(gameTime);
            Game.SpriteBatch.End();
        }
    }
}
