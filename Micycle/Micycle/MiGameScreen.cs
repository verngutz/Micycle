using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MiUtil;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        private const int CENTER_X = 400;
        private const int CENTER_Y = 350;
        private const int RADIUS = 250;

        private const int SCHOOL_WIDTH = 188;
        private const int SCHOOL_HEIGHT = 365;
        private const int SCHOOL_X = CENTER_X - (int)(SCHOOL_WIDTH * SCHOOL_SCALE / 2);
        private const int SCHOOL_Y = CENTER_Y - (int)(SCHOOL_HEIGHT * SCHOOL_SCALE / 2);
        private const float SCHOOL_SCALE = 0.5f;

        private const int CITY_WIDTH = 188;
        private const int CITY_HEIGHT = 365;
        private const float CITY_THETA = (float)(3 * Math.PI / 2);
        private static readonly int CITY_X = (int)(CENTER_X + RADIUS * Math.Cos(CITY_THETA) - (int)(CITY_WIDTH * SCHOOL_SCALE / 2));
        private static readonly int CITY_Y = (int)(CENTER_Y + RADIUS * Math.Sin(CITY_THETA) - (int)(CITY_HEIGHT * SCHOOL_SCALE / 2));
        private const float CITY_SCALE = 0.5f;

        private const int RND_WIDTH = 188;
        private const int RND_HEIGHT = 365;
        private const float RND_THETA = (float)(1 * Math.PI / 6);
        private static readonly int RND_X = (int)(CENTER_X + RADIUS * Math.Cos(RND_THETA) - (int)(RND_WIDTH * SCHOOL_SCALE / 2));
        private static readonly int RND_Y = (int)(CENTER_Y + RADIUS * Math.Sin(RND_THETA) - (int)(RND_HEIGHT * SCHOOL_SCALE / 2));
        private const float RND_SCALE = 0.5f;

        private const int FACTORY_WIDTH = 188;
        private const int FACTORY_HEIGHT = 365;
        private const float FACTORY_THETA = (float)(5 * Math.PI / 6);
        private static readonly int FACTORY_X = (int)(CENTER_X + RADIUS * Math.Cos(FACTORY_THETA) - (int)(FACTORY_WIDTH * SCHOOL_SCALE / 2));
        private static readonly int FACTORY_Y = (int)(CENTER_Y + RADIUS * Math.Sin(FACTORY_THETA) - (int)(FACTORY_HEIGHT * SCHOOL_SCALE / 2));
        private const float FACTORY_SCALE = 0.5f;

        public MiInGameMenu InGameMenu { get; set; }
        public MiFactoryMenu FactoryMenu { get; set; }
        public MiSchoolMenu SchoolMenu { get; set; }
        public MiRndMenu RndMenu { get; set; }

        private MiAnimatingComponent school;
        private MiAnimatingComponent city;
        private MiAnimatingComponent rnd;
        private MiAnimatingComponent factory;

        private MiAnimatingComponent cursor;

        private MiButton schoolButton;
        private MiButton cityButton;
        private MiButton rndButton;
        private MiButton factoryButton;

        private Texture2D mouseImage;
        private Dictionary<uint, MiAnimatingComponent> mice;
        private static uint mouseKey = 0;

        private MicycleGameSystem system;
        private MiScriptEngine inGameScripts;

        public MiGameScreen(Micycle game) : base(game) 
        {
            inGameScripts = new MiScriptEngine(game);            

            //
            // School
            //
            school = new MiAnimatingComponent(game, SCHOOL_X, SCHOOL_Y, SCHOOL_SCALE, 0, 0, 0);
            schoolButton = new MiButton();
            schoolButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(SchoolMenu); 
                    Game.ToDraw.Push(SchoolMenu);
                    return null;
                });

            //
            // City
            //
            city = new MiAnimatingComponent(game, CITY_X, CITY_Y, CITY_SCALE, 0, 0, 0);
            cityButton = new MiButton();

            //
            // Rnd
            //
            rnd = new MiAnimatingComponent(game, RND_X, RND_Y, RND_SCALE, 0, 0, 0);
            rndButton = new MiButton();
            rndButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(RndMenu);
                    Game.ToDraw.Push(RndMenu);
                    return null;
                });

            //
            // Factory
            //
            factory = new MiAnimatingComponent(game, FACTORY_X, FACTORY_Y, FACTORY_SCALE, 0, 0, 0);
            factoryButton = new MiButton();
            factoryButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(FactoryMenu);
                    Game.ToDraw.Push(FactoryMenu);
                    return null;
                });

            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, school.Position.X, school.Position.Y, 1, 0, 0, 0);
        }

        public override void Initialize()
        {
            //
            // Mice
            //
            mice = new Dictionary<uint, MiAnimatingComponent>();

            //
            // System
            //
            system = new MicycleGameSystem(Game as Micycle);
        }

        private IEnumerator<int> SendMouse(float source_x, float source_y, float dest_x, float dest_y, ushort time)
        {
            MiAnimatingComponent mouse = new MiAnimatingComponent(Game, source_x, source_y);
            mouse.AddTexture(mouseImage, 0);
            mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + time, dest_x));
            mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + time, dest_y));
            mouse.MoveEnabled = true;
            uint key = mouseKey++;
            mice.Add(key, mouse);
            yield return time;
            mice.Remove(key);
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
            school.AddTexture(Game.Content.Load<Texture2D>("School"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("City"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("RnD"), 0);
            factory.AddTexture(Game.Content.Load<Texture2D>("Factory"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);

            mouseImage = Game.Content.Load<Texture2D>("mice");
        }

        public override void Update(GameTime gameTime)
        {
            inGameScripts.Update(gameTime);

            foreach (MiAnimatingComponent mouse in mice.Values)
                mouse.Update(gameTime);

            school.Update(gameTime);
            city.Update(gameTime);
            rnd.Update(gameTime);
            factory.Update(gameTime);

            cursor.Update(gameTime);

            system.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (MiAnimatingComponent mouse in mice.Values)
                mouse.Draw(gameTime);

            school.Draw(gameTime);
            city.Draw(gameTime);
            rnd.Draw(gameTime);
            factory.Draw(gameTime);

            cursor.Draw(gameTime);
        }
    }
}
