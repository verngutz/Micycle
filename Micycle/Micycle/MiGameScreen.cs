using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;

using MiUtil;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        #region Coordinates Constants

        private const int CENTER_X = 400;
        private const int CENTER_Y = 300;
        private const int RADIUS = 250;

        private const int SCHOOL_WIDTH = 188;
        private const int SCHOOL_HEIGHT = 365;
        private const float SCHOOL_SCALE = 0.5f;
        private const int SCHOOL_X = CENTER_X;
        private const int SCHOOL_Y = CENTER_Y;
        private const float SCHOOL_ORIGIN_X = SCHOOL_WIDTH / 2;
        private const float SCHOOL_ORIGIN_Y = SCHOOL_HEIGHT / 2;
        private const float SCHOOL_NORTH_EXIT_X = SCHOOL_X - SCHOOL_WIDTH / 4;
        private const float SCHOOL_NORTH_EXIT_Y = SCHOOL_Y - SCHOOL_HEIGHT / 2;
        private const float SCHOOL_NORTH_ENTRANCE_X = SCHOOL_X + SCHOOL_WIDTH / 4;
        private const float SCHOOL_NORTH_ENTRANCE_Y = SCHOOL_Y - SCHOOL_HEIGHT / 2;
        private const float SCHOOL_WEST_EXIT_X = SCHOOL_X - SCHOOL_WIDTH / 2;
        private const float SCHOOL_WEST_EXIT_Y = SCHOOL_Y + SCHOOL_HEIGHT / 4;
        private const float SCHOOL_WEST_ENTRANCE_X = SCHOOL_X - SCHOOL_WIDTH / 2;
        private const float SCHOOL_WEST_ENTRANCE_Y = SCHOOL_Y - SCHOOL_HEIGHT / 4;
        private const float SCHOOL_EAST_EXIT_X = SCHOOL_X + SCHOOL_WIDTH / 2;
        private const float SCHOOL_EAST_EXIT_Y = SCHOOL_Y + SCHOOL_HEIGHT / 4;
        private const float SCHOOL_EAST_ENTRANCE_X = SCHOOL_X + SCHOOL_WIDTH / 2;
        private const float SCHOOL_EAST_ENTRANCE_Y = SCHOOL_Y - SCHOOL_HEIGHT / 4;

        private const int CITY_WIDTH = 188;
        private const int CITY_HEIGHT = 365;
        private const float CITY_SCALE = 0.5f;
        private const double CITY_THETA = 3 * Math.PI / 2;
        private static readonly float CITY_X = (float)(CENTER_X + RADIUS * Math.Cos(CITY_THETA));
        private static readonly float CITY_Y = (float)(CENTER_Y + RADIUS * Math.Sin(CITY_THETA));
        private static readonly float CITY_ORIGIN_X = CITY_WIDTH / 2;
        private static readonly float CITY_ORIGIN_Y = CITY_HEIGHT / 2;
        private static readonly float CITY_SOUTH_EXIT_X = CITY_X + CITY_WIDTH / 4;
        private static readonly float CITY_SOUTH_EXIT_Y = CITY_Y + CITY_HEIGHT / 2;
        private static readonly float CITY_SOUTH_ENTRANCE_X = CITY_X - CITY_WIDTH / 4;
        private static readonly float CITY_SOUTH_ENTRANCE_Y = CITY_Y + CITY_HEIGHT / 2;
        private static readonly float CITY_WEST_EXIT_X = CITY_X - CITY_WIDTH / 2;
        private static readonly float CITY_WEST_EXIT_Y = CITY_Y + CITY_HEIGHT / 4;
        private static readonly float CITY_WEST_ENTRANCE_X = CITY_X - CITY_WIDTH / 2;
        private static readonly float CITY_WEST_ENTRANCE_Y = CITY_Y - CITY_HEIGHT / 4;
        private static readonly float CITY_EAST_EXIT_X = CITY_X + CITY_WIDTH / 2;
        private static readonly float CITY_EAST_EXIT_Y = CITY_Y + CITY_HEIGHT / 4;
        private static readonly float CITY_EAST_ENTRANCE_X = CITY_X + CITY_WIDTH / 2;
        private static readonly float CITY_EAST_ENTRANCE_Y = CITY_Y - CITY_HEIGHT / 4;

        private const int RND_WIDTH = 188;
        private const int RND_HEIGHT = 365;
        private const float RND_SCALE = 0.5f;
        private const double RND_THETA = 1 * Math.PI / 6;
        private static readonly float RND_X = (float)(CENTER_X + RADIUS * Math.Cos(RND_THETA));
        private static readonly float RND_Y = (float)(CENTER_Y + RADIUS * Math.Sin(RND_THETA));
        private static readonly float RND_ORIGIN_X = RND_WIDTH / 2;
        private static readonly float RND_ORIGIN_Y = RND_HEIGHT / 2; 
        private static readonly float RND_NORTH_EXIT_X = RND_X - RND_WIDTH / 4;
        private static readonly float RND_NORTH_EXIT_Y = RND_Y - RND_HEIGHT / 2;
        private static readonly float RND_NORTH_ENTRANCE_X = RND_X + RND_WIDTH / 4;
        private static readonly float RND_NORTH_ENTRANCE_Y = RND_Y - RND_HEIGHT / 2;
        private static readonly float RND_WEST_EXIT_X = RND_X - RND_WIDTH / 2;
        private static readonly float RND_WEST_EXIT_Y = RND_Y + RND_HEIGHT / 4;
        private static readonly float RND_WEST_ENTRANCE_X = RND_X - RND_WIDTH / 2;
        private static readonly float RND_WEST_ENTRANCE_Y = RND_Y - RND_HEIGHT / 4;
        
        private const int FACTORY_WIDTH = 188;
        private const int FACTORY_HEIGHT = 365;
        private const float FACTORY_SCALE = 0.5f;
        private const double FACTORY_THETA = 5 * Math.PI / 6;
        private static readonly float FACTORY_X = (float)(CENTER_X + RADIUS * Math.Cos(FACTORY_THETA));
        private static readonly float FACTORY_Y = (float)(CENTER_Y + RADIUS * Math.Sin(FACTORY_THETA));
        private static readonly float FACTORY_ORIGIN_X = FACTORY_WIDTH / 2;
        private static readonly float FACTORY_ORIGIN_Y = FACTORY_HEIGHT / 2;
        private static readonly float FACTORY_NORTH_EXIT_X = FACTORY_X - FACTORY_WIDTH / 4;
        private static readonly float FACTORY_NORTH_EXIT_Y = FACTORY_Y - FACTORY_HEIGHT / 2;
        private static readonly float FACTORY_NORTH_ENTRANCE_X = FACTORY_X + FACTORY_WIDTH / 4;
        private static readonly float FACTORY_NORTH_ENTRANCE_Y = FACTORY_Y - FACTORY_HEIGHT / 2;
        private static readonly float FACTORY_EAST_EXIT_X = FACTORY_X + FACTORY_WIDTH / 2;
        private static readonly float FACTORY_EAST_EXIT_Y = FACTORY_Y + FACTORY_HEIGHT / 4;
        private static readonly float FACTORY_EAST_ENTRANCE_X = FACTORY_X + FACTORY_WIDTH / 2;
        private static readonly float FACTORY_EAST_ENTRANCE_Y = FACTORY_Y - FACTORY_HEIGHT / 4;

        #endregion

        #region Loose Paths

        private static readonly MiMousePath CITY_TO_SCHOOL = new MiMousePath(CITY_X + 10, CITY_Y, CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y, SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y, SCHOOL_X + 10, SCHOOL_Y + 10, CITY_X, CITY_Y, SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y, CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y);
        private static readonly MiMousePath CITY_TO_FACTORY = new MiMousePath(CITY_X, CITY_Y, CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y, FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y, FACTORY_X, FACTORY_Y, CITY_X, CITY_Y, FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y, CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y);
        private static readonly MiMousePath CITY_TO_RND = new MiMousePath(CITY_X, CITY_Y, CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y, RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y, RND_X, RND_Y, CITY_X, CITY_Y, RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y, CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y);
        private static readonly MiMousePath SCHOOL_TO_CITY = new MiMousePath(SCHOOL_X - 10, SCHOOL_Y - 10, SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y, CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y, CITY_X - 10, CITY_Y - 10, SCHOOL_X, SCHOOL_Y, CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y, SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y);
        private static readonly MiMousePath SCHOOL_TO_FACTORY = new MiMousePath(SCHOOL_X, SCHOOL_Y, SCHOOL_WEST_EXIT_X, SCHOOL_WEST_EXIT_Y, FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y, FACTORY_X, FACTORY_Y, CITY_X, CITY_Y, FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y, CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y);
        private static readonly MiMousePath SCHOOL_TO_RND = new MiMousePath(SCHOOL_X, SCHOOL_Y, SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y, RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y, RND_X, RND_Y, CITY_X, CITY_Y, RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y, CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y);
        private static readonly MiMousePath RND_TO_CITY = new MiMousePath(RND_X, RND_Y, RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y, CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y, CITY_X, CITY_Y, RND_X, RND_Y, CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y, RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y);
        private static readonly MiMousePath RND_TO_SCHOOL = new MiMousePath(RND_X, RND_Y, RND_NORTH_EXIT_X, RND_NORTH_EXIT_Y, SCHOOL_EAST_ENTRANCE_X, SCHOOL_EAST_ENTRANCE_Y, SCHOOL_X, SCHOOL_Y, RND_X, RND_Y, SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y, RND_NORTH_ENTRANCE_X, RND_NORTH_ENTRANCE_Y);
        private static readonly MiMousePath RND_TO_FACTORY = new MiMousePath(RND_X, RND_Y, RND_WEST_EXIT_X, RND_WEST_EXIT_Y, FACTORY_EAST_ENTRANCE_X, FACTORY_EAST_ENTRANCE_Y, FACTORY_X, FACTORY_Y, RND_X, RND_Y, FACTORY_EAST_EXIT_X, FACTORY_EAST_EXIT_Y, RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y);
        private static readonly MiMousePath FACTORY_TO_CITY = new MiMousePath(FACTORY_X, FACTORY_Y, FACTORY_NORTH_EXIT_X, FACTORY_NORTH_EXIT_Y, CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y, CITY_X, CITY_Y, FACTORY_X, FACTORY_Y, CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y, FACTORY_NORTH_ENTRANCE_X, FACTORY_NORTH_ENTRANCE_Y);

        #endregion

        #region Mouse Fields and Constants

        private const int MOUSE_R = 25;
        private static readonly Vector2 MOUSE_ORIGIN = new Vector2(MOUSE_R, MOUSE_R);
        private const float MOUSE_SCALE = 0.25f;
        private const float MOUSE_MASS = 2;
        private const float MOUSE_FRICTION = 0.5f;
        private const ushort MOUSE_MOVETIME = 20;
        private const int DESTINATION_REACHED_LAXITY = 625;
        private const int MOUSE_MOVE_FORCE = 50;
        private Texture2D mouseImage;
        private List<Body> mice;

        #endregion

        #region Neighboring Screens or Menus

        private MiInGameMenu inGameMenu;
        private MiFactoryMenu factoryMenu;
        private MiSchoolMenu schoolMenu;
        private MiRndMenu rndMenu;

        #endregion

        #region Background

        private Texture2D background;
        private Body backgroundBody;

        #endregion

        #region Building Button-Graphics Pairs

        private MiAnimatingComponent school;
        private MiAnimatingComponent city;
        private MiAnimatingComponent rnd;
        private MiAnimatingComponent factory;


        private MiButton schoolButton;
        private MiButton cityButton;
        private MiButton rndButton;
        private MiButton factoryButton;

        #endregion

        private MiAnimatingComponent cursor;
        private MicycleGameSystem system;
        private MiScriptEngine inGameScripts;
        private World world;

        public MiGameScreen(Micycle game) : base(game)
        {
            cursor = new MiAnimatingComponent(game, SCHOOL_X, SCHOOL_Y, 0.94f, 0, 50, 37.5f);
            system = new MicycleGameSystem(game);
            inGameMenu = new MiInGameMenu(game, system);
            mice = new List<Body>();
            inGameScripts = new MiScriptEngine(game);
            world = new World(Vector2.Zero);

            #region City Graphics Initialization

            city = new MiAnimatingComponent(game, CITY_X, CITY_Y, CITY_SCALE, 0, CITY_ORIGIN_X, CITY_ORIGIN_Y);
            cityButton = new MiButton();

            #endregion

            #region School Button-Graphics-Menu Initialization

            school = new MiAnimatingComponent(game, SCHOOL_X, SCHOOL_Y, SCHOOL_SCALE, 0, SCHOOL_ORIGIN_X, SCHOOL_ORIGIN_Y);
            schoolButton = new MiButton();
            schoolButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(schoolMenu); 
                    Game.ToDraw.AddLast(schoolMenu);
                    return null;
                });
            schoolMenu = new MiSchoolMenu(game, SCHOOL_X, SCHOOL_Y, system, inGameMenu);
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

            #endregion

            #region Rnd Button-Graphics-Menu Initialization

            rnd = new MiAnimatingComponent(game, RND_X, RND_Y, RND_SCALE, 0, RND_ORIGIN_X, RND_ORIGIN_Y);
            rndButton = new MiButton();
            rndButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(rndMenu);
                    Game.ToDraw.AddLast(rndMenu);
                    return null;
                });
            rndMenu = new MiRndMenu(game, RND_X, RND_Y, system, inGameMenu);
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

            #endregion

            #region Factory-Graphics-Menu Initialization

            factory = new MiAnimatingComponent(game, FACTORY_X, FACTORY_Y, FACTORY_SCALE, 0, FACTORY_ORIGIN_X, FACTORY_ORIGIN_Y);
            factoryButton = new MiButton();
            factoryButton.Pressed += new MiScript(
                delegate
                {
                    Game.ToUpdate.Push(factoryMenu);
                    Game.ToDraw.AddLast(factoryMenu);
                    return null;
                });
            factoryMenu = new MiFactoryMenu(game, FACTORY_X, FACTORY_Y, system, inGameMenu);
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

            #endregion

            ActiveButton = schoolButton;
        }

        public void Reset()
        {
            mice.Clear();
            ActiveButton = schoolButton;
            system.Reset();
            system.Enabled = true;
        }

        private IEnumerator<ulong> SendMouse(MiMousePath path, MiSemaphoreSet sema)
        {
            Body mouseBody = BodyFactory.CreateCircle(world, MOUSE_R * MOUSE_SCALE, MOUSE_MASS, new Vector2(path.SourceX, path.SourceY) - MiResolution.Center);
            mouseBody.BodyType = BodyType.Dynamic;
            mouseBody.Friction = MOUSE_FRICTION;
            mouseBody.CreateFixture(new CircleShape(MOUSE_R * MOUSE_SCALE, MOUSE_MASS));
            mice.Add(mouseBody);
            Vector2 nextGoal = new Vector2(path.SourceExitX, path.SourceExitY) - MiResolution.Center;
            while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > DESTINATION_REACHED_LAXITY)
            {
                mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(nextGoal - mouseBody.Position);
                yield return MOUSE_MOVETIME;
            }
            mouseBody.ResetDynamics();
            system.Signal(ref sema.HasReachedB);
            while (true)
            {
                if (system.Wait(ref sema.AcceptIntoB))
                {
                    nextGoal = new Vector2(path.AcceptDestX, path.AcceptDestY) - MiResolution.Center;
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    break;
                }
                else if (system.Wait(ref sema.RejectFromB))
                {
                    System.Console.WriteLine("Rejected");
                    mouseBody.IgnoreCollisionWith(backgroundBody);
                    nextGoal = new Vector2(path.RejectWaitQueueTailX, path.RejectWaitQueueTailY) - MiResolution.Center;
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    mouseBody.RestoreCollisionWith(backgroundBody);
                    nextGoal = new Vector2(path.RejectWaitQueueHeadX, path.RejectWaitQueueHeadY) - MiResolution.Center;
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    yield return MOUSE_MOVETIME;
                    nextGoal = new Vector2(path.RejectDestX, path.RejectDestY) - MiResolution.Center;
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    yield return MOUSE_MOVETIME;
                    break;
                }
                else yield return MOUSE_MOVETIME;
            }
            world.RemoveBody(mouseBody);
            mice.Remove(mouseBody);
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
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, SCHOOL_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, SCHOOL_Y));
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
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_Y));
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
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_Y));
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
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, RND_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, RND_Y));
                yield return 20;
                cursor.MoveEnabled = false;
                ActiveButton = rndButton;
            }
        }

        public override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("gamebackground");
            uint[] backgroundData = new uint[background.Width * background.Height];
            background.GetData(backgroundData);
            Vertices backgroundVertices = PolygonTools.CreatePolygon(backgroundData, background.Width, false);
            /**
            Vector2 backgroundCentroid = -backgroundVertices.GetCentroid();
            backgroundVertices.Translate(ref backgroundCentroid);
             */
            backgroundVertices = SimplifyTools.ReduceByDistance(backgroundVertices, 4);
            List<Vertices> convexBackgroundVertices = BayazitDecomposer.ConvexPartition(backgroundVertices);
            backgroundBody = BodyFactory.CreateCompoundPolygon(world, convexBackgroundVertices, 1, -MiResolution.Center);
            backgroundBody.BodyType = BodyType.Static;
            backgroundBody.Enabled = true;
            foreach (Vertices v in convexBackgroundVertices)
                backgroundBody.CreateFixture(new PolygonShape(v, 1));

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
                system.Update(gameTime);
                inGameScripts.Update(gameTime);

                if (system.Wait(ref system.CityToSchool.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_SCHOOL, system.CityToSchool);
                        }));

                if (system.Wait(ref system.CityToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_FACTORY, system.CityToFactory);
                        }));

                if (system.Wait(ref system.CityToRnd.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_RND, system.CityToRnd);
                        }));

                if (system.Wait(ref system.SchoolToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_CITY, system.SchoolToCity);
                        }));

                if (system.Wait(ref system.SchoolToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                          delegate
                          {
                              return SendMouse(SCHOOL_TO_FACTORY, system.SchoolToFactory);
                          }));

                if (system.Wait(ref system.SchoolToRnd.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_RND, system.SchoolToRnd);
                        }));

                if (system.Wait(ref system.FactoryToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(FACTORY_TO_CITY, system.FactoryToCity);
                        }));

                if (system.Wait(ref system.RndToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_CITY, system.RndToCity);
                        }));

                if (system.Wait(ref system.RndToSchool.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_SCHOOL, system.RndToSchool);
                        }));

                if (system.Wait(ref system.RndToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_FACTORY, system.RndToFactory);
                        }));

                world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, (1f / 30f)));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            school.Draw(gameTime);
            city.Draw(gameTime);
            rnd.Draw(gameTime);
            factory.Draw(gameTime);

            Game.SpriteBatch.Draw(background, backgroundBody.Position + 2 * MiResolution.Center, null, Color.White, backgroundBody.Rotation, MiResolution.Center, 1, SpriteEffects.None, 0);
            foreach (Body mouse in mice)
                Game.SpriteBatch.Draw(mouseImage, mouse.Position + MiResolution.Center, null, Color.White, mouse.Rotation, MOUSE_ORIGIN, MOUSE_SCALE, SpriteEffects.None, 0);
             
            cursor.Draw(gameTime);
            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), system.printStats(), Vector2.Zero, Color.Black);
        }
    }
}
