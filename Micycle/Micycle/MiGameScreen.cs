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
        private const float SCHOOL_SCALE = 0.5f;
        private const int SCHOOL_CENTER_X = CENTER_X;
        private const int SCHOOL_CENTER_Y = CENTER_Y;
        private const float SCHOOL_X = SCHOOL_CENTER_X - SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 2;
        private const float SCHOOL_NORTH_EXIT_X = SCHOOL_CENTER_X - SCHOOL_WIDTH * SCHOOL_SCALE / 4;
        private const float SCHOOL_NORTH_EXIT_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 2;
        private const float SCHOOL_NORTH_ENTRANCE_X = SCHOOL_CENTER_X + SCHOOL_WIDTH * SCHOOL_SCALE / 4;
        private const float SCHOOL_NORTH_ENTRANCE_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 2;
        private const float SCHOOL_WEST_EXIT_X = SCHOOL_CENTER_X - SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_WEST_EXIT_Y = SCHOOL_CENTER_Y + SCHOOL_HEIGHT * SCHOOL_SCALE / 4;
        private const float SCHOOL_WEST_ENTRANCE_X = SCHOOL_CENTER_X - SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_WEST_ENTRANCE_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 4;
        private const float SCHOOL_EAST_EXIT_X = SCHOOL_CENTER_X + SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_EAST_EXIT_Y = SCHOOL_CENTER_Y + SCHOOL_HEIGHT * SCHOOL_SCALE / 4;
        private const float SCHOOL_EAST_ENTRANCE_X = SCHOOL_CENTER_X + SCHOOL_WIDTH * SCHOOL_SCALE / 2;
        private const float SCHOOL_EAST_ENTRANCE_Y = SCHOOL_CENTER_Y - SCHOOL_HEIGHT * SCHOOL_SCALE / 4;

        private const int CITY_WIDTH = 188;
        private const int CITY_HEIGHT = 365;
        private const float CITY_SCALE = 0.5f;
        private const double CITY_THETA = 3 * Math.PI / 2;
        private static readonly float CITY_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(CITY_THETA));
        private static readonly float CITY_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(CITY_THETA));
        private static readonly float CITY_X = CITY_CENTER_X - CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_Y = CITY_CENTER_Y - CITY_HEIGHT * CITY_SCALE / 2;
        private static readonly float CITY_SOUTH_EXIT_X = CITY_CENTER_X + CITY_WIDTH * CITY_SCALE / 4;
        private static readonly float CITY_SOUTH_EXIT_Y = CITY_CENTER_Y + CITY_HEIGHT * CITY_SCALE / 2;
        private static readonly float CITY_SOUTH_ENTRANCE_X = CITY_CENTER_X - CITY_WIDTH * CITY_SCALE / 4;
        private static readonly float CITY_SOUTH_ENTRANCE_Y = CITY_CENTER_Y + CITY_HEIGHT * CITY_SCALE / 2;
        private static readonly float CITY_WEST_EXIT_X = CITY_CENTER_X - CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_WEST_EXIT_Y = CITY_CENTER_Y + CITY_HEIGHT * CITY_SCALE / 4;
        private static readonly float CITY_WEST_ENTRANCE_X = CITY_CENTER_X - CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_WEST_ENTRANCE_Y = CITY_CENTER_Y - CITY_HEIGHT * CITY_SCALE / 4;
        private static readonly float CITY_EAST_EXIT_X = CITY_CENTER_X + CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_EAST_EXIT_Y = CITY_CENTER_Y + CITY_HEIGHT * CITY_SCALE / 4;
        private static readonly float CITY_EAST_ENTRANCE_X = CITY_CENTER_X + CITY_WIDTH * CITY_SCALE / 2;
        private static readonly float CITY_EAST_ENTRANCE_Y = CITY_CENTER_Y - CITY_HEIGHT * CITY_SCALE / 4;

        private const int RND_WIDTH = 188;
        private const int RND_HEIGHT = 365;
        private const float RND_SCALE = 0.5f;
        private const double RND_THETA = 1 * Math.PI / 6;
        private static readonly float RND_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(RND_THETA));
        private static readonly float RND_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(RND_THETA));
        private static readonly float RND_X = RND_CENTER_X - RND_WIDTH * RND_SCALE / 2;
        private static readonly float RND_Y = RND_CENTER_Y - RND_HEIGHT * RND_SCALE / 2; 
        private static readonly float RND_NORTH_EXIT_X = RND_CENTER_X - RND_WIDTH * RND_SCALE / 4;
        private static readonly float RND_NORTH_EXIT_Y = RND_CENTER_Y - RND_HEIGHT * RND_SCALE / 2;
        private static readonly float RND_NORTH_ENTRANCE_X = RND_CENTER_X + RND_WIDTH * RND_SCALE / 4;
        private static readonly float RND_NORTH_ENTRANCE_Y = RND_CENTER_Y - RND_HEIGHT * RND_SCALE / 2;
        private static readonly float RND_WEST_EXIT_X = RND_CENTER_X - RND_WIDTH * RND_SCALE / 2;
        private static readonly float RND_WEST_EXIT_Y = RND_CENTER_Y + RND_HEIGHT * RND_SCALE / 4;
        private static readonly float RND_WEST_ENTRANCE_X = RND_CENTER_X - RND_WIDTH * RND_SCALE / 2;
        private static readonly float RND_WEST_ENTRANCE_Y = RND_CENTER_Y - RND_HEIGHT * RND_SCALE / 4;
        
        private const int FACTORY_WIDTH = 188;
        private const int FACTORY_HEIGHT = 365;
        private const float FACTORY_SCALE = 0.5f;
        private const double FACTORY_THETA = 5 * Math.PI / 6;
        private static readonly float FACTORY_CENTER_X = (float)(CENTER_X + RADIUS * Math.Cos(FACTORY_THETA));
        private static readonly float FACTORY_CENTER_Y = (float)(CENTER_Y + RADIUS * Math.Sin(FACTORY_THETA));
        private static readonly float FACTORY_X = FACTORY_CENTER_X - FACTORY_WIDTH * FACTORY_SCALE / 2;
        private static readonly float FACTORY_Y = FACTORY_CENTER_Y - FACTORY_HEIGHT * FACTORY_SCALE / 2;
        private static readonly float FACTORY_NORTH_EXIT_X = FACTORY_CENTER_X - FACTORY_WIDTH * FACTORY_SCALE / 4;
        private static readonly float FACTORY_NORTH_EXIT_Y = FACTORY_CENTER_Y - FACTORY_HEIGHT * FACTORY_SCALE / 2;
        private static readonly float FACTORY_NORTH_ENTRANCE_X = FACTORY_CENTER_X + FACTORY_WIDTH * FACTORY_SCALE / 4;
        private static readonly float FACTORY_NORTH_ENTRANCE_Y = FACTORY_CENTER_Y - FACTORY_HEIGHT * FACTORY_SCALE / 2;
        private static readonly float FACTORY_EAST_EXIT_X = FACTORY_CENTER_X + FACTORY_WIDTH * FACTORY_SCALE / 2;
        private static readonly float FACTORY_EAST_EXIT_Y = FACTORY_CENTER_Y + FACTORY_HEIGHT * FACTORY_SCALE / 4;
        private static readonly float FACTORY_EAST_ENTRANCE_X = FACTORY_CENTER_X + FACTORY_WIDTH * FACTORY_SCALE / 2;
        private static readonly float FACTORY_EAST_ENTRANCE_Y = FACTORY_CENTER_Y - FACTORY_HEIGHT * FACTORY_SCALE / 4;

        private static readonly MiMousePath CITY_TO_SCHOOL = new MiMousePath(
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y,
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y,
            CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y);

        private static readonly MiMousePath CITY_TO_FACTORY = new MiMousePath(
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y,
            FACTORY_CENTER_X, FACTORY_CENTER_Y,
            FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y,
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y);

        private static readonly MiMousePath CITY_TO_RND = new MiMousePath(
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y,
            RND_CENTER_X, RND_CENTER_Y,
            RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y,
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y);

        private static readonly MiMousePath SCHOOL_TO_CITY = new MiMousePath(
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y,
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y,
            SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y);

        private static readonly MiMousePath SCHOOL_TO_FACTORY = new MiMousePath(
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            SCHOOL_WEST_EXIT_X, SCHOOL_WEST_EXIT_Y,
            FACTORY_CENTER_X, FACTORY_CENTER_Y,
            FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y,
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y);

        private static readonly MiMousePath SCHOOL_TO_RND = new MiMousePath(
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y,
            RND_CENTER_X, RND_CENTER_Y,
            RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y,
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y);

        private static readonly MiMousePath RND_TO_CITY = new MiMousePath(
            RND_CENTER_X, RND_CENTER_Y,
            RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y,
            RND_CENTER_X, RND_CENTER_Y,
            CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y,
            RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y);

        private static readonly MiMousePath RND_TO_SCHOOL = new MiMousePath(
            RND_CENTER_X, RND_CENTER_Y,
            RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y,
            SCHOOL_CENTER_X, SCHOOL_CENTER_Y,
            SCHOOL_EAST_ENTRANCE_X, SCHOOL_EAST_ENTRANCE_Y,
            RND_CENTER_X, RND_CENTER_Y,
            SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y,
            RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y);

        private static readonly MiMousePath RND_TO_FACTORY = new MiMousePath(
            RND_CENTER_X, RND_CENTER_Y,
            RND_WEST_EXIT_X, RND_WEST_EXIT_Y,
            FACTORY_CENTER_X, FACTORY_CENTER_Y,
            FACTORY_EAST_ENTRANCE_X, FACTORY_EAST_ENTRANCE_Y,
            RND_CENTER_X, RND_CENTER_Y,
            FACTORY_EAST_EXIT_X, FACTORY_EAST_EXIT_Y,
            RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y);

        private static readonly MiMousePath FACTORY_TO_CITY = new MiMousePath(
            FACTORY_CENTER_X, FACTORY_CENTER_Y,
            FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y,
            CITY_CENTER_X, CITY_CENTER_Y,
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y,
            FACTORY_CENTER_X, FACTORY_CENTER_Y,
            CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y,
            FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y);

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
            //
            // Game System
            //
            system = new MicycleGameSystem(game);
            inGameMenu = new MiInGameMenu(game, system);

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
            schoolMenu = new MiSchoolMenu(game, SCHOOL_CENTER_X, SCHOOL_CENTER_Y, system, inGameMenu);
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
            rndMenu = new MiRndMenu(game, RND_CENTER_X, RND_CENTER_Y, system, inGameMenu);
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
            factoryMenu = new MiFactoryMenu(game, FACTORY_CENTER_X, FACTORY_CENTER_Y, system, inGameMenu);
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
        }

        public void Reset()
        {
            mice = new Dictionary<uint, MiAnimatingComponent>();
            ActiveButton = schoolButton;
            system.Reset();
        }

        private IEnumerator<ulong> SendMouse(MiMousePath path, MiSemaphoreSet sema)
        {
            MiAnimatingComponent mouse = new MiAnimatingComponent(Game, path.SourceX + MOUSE_X, path.SourceY + MOUSE_Y, MOUSE_SCALE, 0, 0, 0);
            mouse.AddTexture(mouseImage, 0);
            uint key = mouseKey++;
            mice.Add(key, mouse);
            mouse.MoveEnabled = true;
            mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.SourceExitX + MOUSE_X));
            mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.SourceExitY + MOUSE_Y));
            yield return MOUSE_MOVETIME;
            mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestAcceptEntranceX + MOUSE_X));
            mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestAcceptEntranceY + MOUSE_Y));
            yield return MOUSE_MOVETIME;
            system.Signal(ref sema.HasReachedB);
            mouse.MoveEnabled = false;
            while (true)
            {
                if (system.Wait(ref sema.AcceptIntoB))
                {
                    mouse.MoveEnabled = true;
                    mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestAcceptX + MOUSE_X));
                    mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestAcceptY + MOUSE_Y));
                    yield return MOUSE_MOVETIME;
                    break;
                }
                else if (system.Wait(ref sema.RejectFromB))
                {
                    mouse.MoveEnabled = true;
                    mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectExitX + MOUSE_X));
                    mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectExitY + MOUSE_Y));
                    yield return MOUSE_MOVETIME;
                    mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectEntranceX + MOUSE_X));
                    mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectEntranceY + MOUSE_Y));
                    yield return MOUSE_MOVETIME;
                    mouse.XPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectX + MOUSE_X));
                    mouse.YPositionOverTime.Keys.Add(new CurveKey(mouse.MoveTimer + MOUSE_MOVETIME, path.DestRejectY + MOUSE_Y));
                    yield return MOUSE_MOVETIME;
                    break;
                }
                else yield return MOUSE_MOVETIME;
            }
            mice.Remove(key);
        }

        public override IEnumerator<ulong> Pressed()
        {
            ActiveButton.Pressed();
            yield return 0;
        }

        public override IEnumerator<ulong> Escaped()
        {
            system.Enabled = false;
            Game.ToUpdate.Push(inGameMenu);
            Game.ToDraw.AddLast(inGameMenu);
            return inGameMenu.EntrySequence();
        }

        public override IEnumerator<ulong> Upped()
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

        public override IEnumerator<ulong> Downed()
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

        public override IEnumerator<ulong> Lefted()
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

        public override IEnumerator<ulong> Righted()
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
            school.Update(gameTime);
            city.Update(gameTime);
            rnd.Update(gameTime);
            factory.Update(gameTime);
            cursor.Update(gameTime);

            if (system.Enabled)
            {
                foreach (MiAnimatingComponent mouse in mice.Values)
                    mouse.Update(gameTime);

                system.Update(gameTime);

                if (system.Wait(ref system.CityToSchool.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_SCHOOL, system.CityToSchool);
                        }));

                if (system.Wait(ref system.CityToFactory.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_FACTORY, system.CityToFactory);
                        }));

                if (system.Wait(ref system.CityToRnd.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_RND, system.CityToRnd);
                        }));

                if (system.Wait(ref system.SchoolToCity.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_CITY, system.SchoolToCity);
                        }));

                if (system.Wait(ref system.SchoolToFactory.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                          delegate
                          {
                              return SendMouse(SCHOOL_TO_FACTORY, system.SchoolToFactory);
                          }));

                if (system.Wait(ref system.SchoolToRnd.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_RND, system.SchoolToRnd);
                        }));

                if (system.Wait(ref system.FactoryToCity.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(FACTORY_TO_CITY, system.FactoryToCity);
                        }));

                if (system.Wait(ref system.RndToCity.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_CITY, system.RndToCity);
                        }));

                if (system.Wait(ref system.RndToSchool.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_SCHOOL, system.RndToSchool);
                        }));

                if (system.Wait(ref system.RndToFactory.SendFromAToB))
                    Game.ScriptEngine.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_FACTORY, system.RndToFactory);
                        }));
            }
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
