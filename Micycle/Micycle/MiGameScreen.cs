﻿using System;
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
        private const float SCHOOL_SCALE = 0.5f;
        private const int SCHOOL_CENTER_X = CENTER_X;
        private const int SCHOOL_CENTER_Y = CENTER_Y;
        private const float SCHOOL_X = SCHOOL_CENTER_X - SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 2;

        private const int CITY_WIDTH = 188;
        private const int CITY_HEIGHT = 365;
        private const float CITY_SCALE = 0.5f;
        private const double CITY_THETA = 3 * Math.PI / 2;
        private static readonly float CITY_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(CITY_THETA));
        private static readonly float CITY_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(CITY_THETA));
        private static readonly float CITY_X = CITY_CENTER_X - CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_Y = CITY_CENTER_Y - CITY_HEIGHT * CITY_SCALE / 2;

        private const int RND_WIDTH = 188;
        private const int RND_HEIGHT = 365;
        private const float RND_SCALE = 0.5f;
        private const double RND_THETA = 1 * Math.PI / 6;
        private static readonly float RND_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(RND_THETA));
        private static readonly float RND_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(RND_THETA));
        private static readonly float RND_X = RND_CENTER_X - RND_WIDTH * RND_SCALE / 2;
        private static readonly float RND_Y = RND_CENTER_Y - RND_HEIGHT * RND_SCALE / 2;
        
        private const int FACTORY_WIDTH = 188;
        private const int FACTORY_HEIGHT = 365;
        private const float FACTORY_SCALE = 0.5f;
        private const double FACTORY_THETA = 5 * Math.PI / 6;
        private static readonly float FACTORY_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(FACTORY_THETA));
        private static readonly float FACTORY_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(FACTORY_THETA));
        private static readonly float FACTORY_X = FACTORY_CENTER_X - FACTORY_WIDTH * FACTORY_SCALE / 2;
        private static readonly float FACTORY_Y = FACTORY_CENTER_Y - FACTORY_HEIGHT * FACTORY_SCALE / 2;

        private const int MOUSE_WIDTH = 50;
        private const int MOUSE_HEIGHT = 50;
        private const float MOUSE_SCALE = 0.5f;
        private const float MOUSE_X = - MOUSE_SCALE * MOUSE_WIDTH / 2;
        private const float MOUSE_Y = - MOUSE_SCALE * MOUSE_HEIGHT / 2;
        private const ushort MOUSE_MOVETIME = 200;

        private MiInGameMenu inGameMenu;
        private MiFactoryMenu factoryMenu;
        private MiSchoolMenu schoolMenu;
        private MiRndMenu rndMenu;

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
        private static uint mouseKey = 0;
        private Dictionary<uint, MiAnimatingComponent> mice;

        private MicycleGameSystem system;

        public MiGameScreen(Micycle game) : base(game) 
        {
            inGameMenu = new MiInGameMenu(game);

            //
            // City
            //
            city = new MiAnimatingComponent(game, CITY_X, CITY_Y, CITY_SCALE, 0, 0, 0);
            cityButton = new MiButton();

            //
            // School
            //
            school = new MiAnimatingComponent(game, SCHOOL_X, SCHOOL_Y, SCHOOL_SCALE, 0, 0, 0);
            schoolButton = new MiButton();
            schoolButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(schoolMenu); 
                    Game.ToDraw.AddLast(schoolMenu);
                    return null;
                });
            schoolMenu = new MiSchoolMenu(game, SCHOOL_CENTER_X, SCHOOL_CENTER_Y);
            schoolMenu.UpButton.Pressed += new MiScript(
                delegate
                {
                    system.SchoolUpButtonAction();
                    return null;
                });
            schoolMenu.DownButton.Pressed += new MiScript(
                delegate
                {
                    system.SchoolDownButtonAction();
                    return null;
                });
            schoolMenu.LeftButton.Pressed += new MiScript(
                delegate
                {
                    system.SchoolLeftButtonAction();
                    return null;
                });
            schoolMenu.RightButton.Pressed += new MiScript(
                delegate
                {
                    system.SchoolRightButtonAction();
                    return null;
                });

            //
            // Rnd
            //
            rnd = new MiAnimatingComponent(game, RND_X, RND_Y, RND_SCALE, 0, 0, 0);
            rndButton = new MiButton();
            rndButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(rndMenu);
                    Game.ToDraw.AddLast(rndMenu);
                    return null;
                });
            rndMenu = new MiRndMenu(game, RND_CENTER_X, RND_CENTER_Y);
            rndMenu.UpButton.Pressed += new MiScript(
                delegate
                {
                    system.RndUpButtonAction();
                    return null;
                });
            rndMenu.DownButton.Pressed += new MiScript(
                delegate
                {
                    system.RndDownButtonAction();
                    return null;
                });
            rndMenu.LeftButton.Pressed += new MiScript(
                delegate
                {
                    system.RndLeftButtonAction();
                    return null;
                });
            rndMenu.RightButton.Pressed += new MiScript(
                delegate
                {
                    system.RndRightButtonAction();
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
                    Game.ToUpdate.Push(factoryMenu);
                    Game.ToDraw.AddLast(factoryMenu);
                    return null;
                });
            factoryMenu = new MiFactoryMenu(game, FACTORY_CENTER_X, FACTORY_CENTER_Y);
            factoryMenu.UpButton.Pressed += new MiScript(
                delegate
                {
                    system.FactoryUpButtonAction();
                    return null;
                });
            factoryMenu.DownButton.Pressed += new MiScript(
                delegate
                {
                    system.FactoryDownButtonAction();
                    return null;
                });
            factoryMenu.LeftButton.Pressed += new MiScript(
                delegate
                {
                    system.FactoryLeftButtonAction();
                    return null;
                });
            factoryMenu.RightButton.Pressed += new MiScript(
                delegate
                {
                    system.FactoryRightButtonAction();
                    return null;
                });

            //
            // Cursor
            //
            cursor = new MiAnimatingComponent(game, school.Position.X, school.Position.Y, 1, 0, 0, 0);
            ActiveButton = schoolButton;

            //
            // Mice
            //
            mice = new Dictionary<uint, MiAnimatingComponent>();

            //
            // Game System
            //
            system = new MicycleGameSystem(game);
        }

        private IEnumerator<int> SendMouse(float source_x, float source_y, float dest_x, float dest_y)
        {
            MiAnimatingComponent mouse = new MiAnimatingComponent(Game, source_x + MOUSE_X, source_y + MOUSE_Y, MOUSE_SCALE, 0, 0, 0);
            mouse.AddTexture(mouseImage, 0);
            mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, dest_x + MOUSE_X));
            mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, dest_y + MOUSE_Y));
            mouse.MoveEnabled = true;
            uint key = mouseKey++;
            mice.Add(key, mouse);
            yield return MOUSE_MOVETIME;
            mice.Remove(key);
        }

        public override IEnumerator<int> Pressed()
        {
            ActiveButton.Pressed();
            yield return 0;
        }

        public override IEnumerator<int> Cancelled()
        {
            Game.ToUpdate.Push(inGameMenu);
            Game.ToDraw.AddLast(inGameMenu);
            return inGameMenu.EntrySequence();
        }

        public override IEnumerator<int> Upped()
        {
            if (ActiveButton == factoryButton || ActiveButton == rndButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, school.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, school.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = schoolButton;
            }
        }

        public override IEnumerator<int> Downed()
        {
            if (ActiveButton == schoolButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, factory.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, factory.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = factoryButton;
            }
        }

        public override IEnumerator<int> Lefted()
        {
            if (ActiveButton == schoolButton || ActiveButton == rndButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, factory.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, factory.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = factoryButton;
            }
        }

        public override IEnumerator<int> Righted()
        {
            if (ActiveButton == schoolButton || ActiveButton == factoryButton)
            {
                ActiveButton = null;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rnd.Position.X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, rnd.Position.Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = rndButton;
            }
        }

        public override void LoadContent()
        {
            school.AddTexture(Game.Content.Load<Texture2D>("School"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("City"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("RnD"), 0);
            factory.AddTexture(Game.Content.Load<Texture2D>("Factory"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);

            mouseImage = Game.Content.Load<Texture2D>("mice");

            inGameMenu.LoadContent();
            schoolMenu.LoadContent();
            factoryMenu.LoadContent();
            rndMenu.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MiAnimatingComponent mouse in mice.Values)
                mouse.Update(gameTime);

            school.Update(gameTime);
            city.Update(gameTime);
            rnd.Update(gameTime);
            factory.Update(gameTime);

            cursor.Update(gameTime);

            system.Update(gameTime);

            if (system.Wait(ref system.SendMouseFromCityToSchool))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(CITY_CENTER_X, CITY_CENTER_Y, SCHOOL_CENTER_X, SCHOOL_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromCityToFactory))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(CITY_CENTER_X, CITY_CENTER_Y, FACTORY_CENTER_X, FACTORY_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromCityToRnd))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(CITY_CENTER_X, CITY_CENTER_Y, RND_CENTER_X, RND_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromSchoolToCity))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(SCHOOL_CENTER_X, SCHOOL_CENTER_Y, CITY_CENTER_X, CITY_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromSchoolToFactory)) 
                Game.ScriptEngine.ExecuteScript(new MiScript(
                      delegate
                      {
                          return SendMouse(SCHOOL_CENTER_X, SCHOOL_CENTER_Y, FACTORY_CENTER_X, FACTORY_CENTER_Y);
                      }));

            if (system.Wait(ref system.SendMouseFromSchoolToRnd))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(SCHOOL_CENTER_X, SCHOOL_CENTER_Y, RND_CENTER_X, RND_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromFactoryToCity))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(FACTORY_CENTER_X, FACTORY_CENTER_Y, CITY_CENTER_X, CITY_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendMouseFromRndToCity))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(RND_CENTER_X, RND_CENTER_Y, CITY_CENTER_X, CITY_CENTER_Y);
                    }));

            if (system.Wait(ref system.SendRobotFromRndToFactory))
                Game.ScriptEngine.ExecuteScript(new MiScript(
                    delegate
                    {
                        return SendMouse(RND_CENTER_X, RND_CENTER_Y, FACTORY_CENTER_X, FACTORY_CENTER_Y);
                    }));
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
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), system.printStats(), Vector2.Zero, Color.Black);
        }
    }
}
