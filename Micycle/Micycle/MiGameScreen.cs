using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        public MiInGameMenu InGameMenu { get; set; }

        private MiAnimatingComponent factory;
        private MiAnimatingComponent school;
        private MiAnimatingComponent city;
        private MiAnimatingComponent rnd;

        private MiAnimatingComponent cursor;

        private Texture2D mouseImage;
        private Dictionary<int, MiAnimatingComponent> mice;

        private MicycleGameSystem system;

        public MiGameScreen(Micycle game) : base(game) 
        {
            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 200, 50, 1, 0, 0, 0);

            //
            // Factory
            //
            factory = new MiAnimatingComponent(game, 100, 400, 0.5f, 0, 0, 0);

            //
            // School
            //
            school = new MiAnimatingComponent(game, 400, 300, 0.5f, 0, 0, 0);

            //
            // City
            //
            city = new MiAnimatingComponent(game, 400, 50, 0.5f, 0, 0, 0);

            //
            // Rnd
            //
            rnd = new MiAnimatingComponent(game, 700, 400, 0.5f, 0, 0, 0);
        }

        public override void Initialize()
        {
            //
            // Mice
            //
            mice = new Dictionary<int, MiAnimatingComponent>();
            Game.ScriptEngine.ExecuteScript(new MiScript(TestSendMouse));

            //
            // System
            //
            system = new MicycleGameSystem(Game as Micycle);
        }

        private IEnumerator<int> TestSendMouse()
        {
            MiAnimatingComponent mouse = new MiAnimatingComponent(Game, 400, 50);
            mouse.AddTexture(mouseImage, 0);
            mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.Time + 200, 300));
            mouse.MoveEnabled = true;
            mice.Add(1, mouse);
            yield return 200;
            mice.Remove(1);
        }

        public override IEnumerator<int> Pressed()
        {
            return base.Pressed();
        }

        public override IEnumerator<int> Cancelled()
        {
            Game.ToUpdate.Push(InGameMenu);
            Game.ToDraw.Push(InGameMenu);
            return InGameMenu.EntrySequence();
        }

        public override IEnumerator<int> Upped()
        {
            return base.Upped();
        }

        public override IEnumerator<int> Downed()
        {
            return base.Downed();
        }

        public override IEnumerator<int> Lefted()
        {
            return base.Lefted();
        }

        public override IEnumerator<int> Righted()
        {
            return base.Righted();
        }

        public override void LoadContent()
        {
            factory.AddTexture(Game.Content.Load<Texture2D>("Factory"), 0);
            school.AddTexture(Game.Content.Load<Texture2D>("School"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("City"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("RnD"), 0);
            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);

            mouseImage = Game.Content.Load<Texture2D>("mice");
        }

        public override void Update(GameTime gameTime)
        {
            factory.Update(gameTime);
            school.Update(gameTime);
            city.Update(gameTime);
            rnd.Update(gameTime);
            cursor.Update(gameTime);

            foreach (MiAnimatingComponent mouse in mice.Values)
                mouse.Update(gameTime);

            system.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            factory.Draw(gameTime);
            school.Draw(gameTime);
            city.Draw(gameTime);
            rnd.Draw(gameTime);
            cursor.Draw(gameTime);

            foreach (MiAnimatingComponent mouse in mice.Values)
                mouse.Draw(gameTime);
        }
    }
}
