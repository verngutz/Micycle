using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;
using MiGui;

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

        public MiGameScreen(Micycle game)
            : base(game)
        {
            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, 200, 50, 1, 0, 0, 0);
            cursor.Visible = false;
            cursor.Enabled = false;

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

            //
            // Action Events
            //
            Cancelled += delegate
            {
                Game.EventQueue.AddEvent(new MiEvent(showInGameMenu), 0);
            };
        }

        private void showInGameMenu()
        {
            InGameMenu.Enabled = true;
            InGameMenu.Visible = true;
            Game.ToUpdate.Push(InGameMenu);
            Game.ToDraw.Push(InGameMenu);
            Game.InputHandler.Focused = InGameMenu;
            InGameMenu.EntrySequence();
        }

        public override void LoadContent()
        {
            factory.AddTexture(Game.Content.Load<Texture2D>("Factory"), 0);
            school.AddTexture(Game.Content.Load<Texture2D>("School"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("City"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("RnD"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (factory.Enabled)
                factory.Update(gameTime);

            if (school.Enabled)
                school.Update(gameTime);

            if (city.Enabled)
                city.Update(gameTime);

            if (rnd.Enabled)
                rnd.Update(gameTime);

            if (cursor.Enabled)
                cursor.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (factory.Visible)
                factory.Draw(gameTime);

            if (school.Visible)
                school.Draw(gameTime);

            if (city.Visible)
                city.Draw(gameTime);

            if (rnd.Visible)
                rnd.Draw(gameTime);

            if (cursor.Visible)
                cursor.Draw(gameTime);
        }
    }
}
