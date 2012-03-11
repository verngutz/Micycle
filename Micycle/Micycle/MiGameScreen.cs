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

#if DEBUG
using FarseerPhysics.DebugViews;
using FarseerPhysics.SamplesFramework;
#endif

using MiUtil;

namespace Micycle
{
    class MiGameScreen : MiScreen
    {
        #region Coordinates Constants

        private const int CENTER_X = 600;
        private const int CENTER_Y = 450;
        private const int RADIUS = 350;

        private const int SCHOOL_WIDTH = 900;
        private const int SCHOOL_HEIGHT = 900;
        private const float SCHOOL_SCALE = 0.1f;
        private const int SCHOOL_X = CENTER_X;
        private const int SCHOOL_Y = CENTER_Y;
        private const float SCHOOL_ORIGIN_X = SCHOOL_WIDTH / 2;
        private const float SCHOOL_ORIGIN_Y = SCHOOL_HEIGHT / 2;
        private const float SCHOOL_NORTH_EXIT_X = SCHOOL_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float SCHOOL_NORTH_EXIT_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float SCHOOL_NORTH_ENTRANCE_X = SCHOOL_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float SCHOOL_NORTH_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float SCHOOL_WEST_EXIT_X = SCHOOL_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float SCHOOL_WEST_EXIT_Y = SCHOOL_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float SCHOOL_WEST_ENTRANCE_X = SCHOOL_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float SCHOOL_WEST_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float SCHOOL_EAST_EXIT_X = SCHOOL_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float SCHOOL_EAST_EXIT_Y = SCHOOL_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float SCHOOL_EAST_ENTRANCE_X = SCHOOL_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float SCHOOL_EAST_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;

        private const int CITY_WIDTH = 900;
        private const int CITY_HEIGHT = 900;
        private const float CITY_SCALE = 0.1f;
        private const int CITY_X = CENTER_X;
        private const int CITY_Y = CENTER_Y - 300;
        private const float CITY_ORIGIN_X = CITY_WIDTH / 2;
        private const float CITY_ORIGIN_Y = CITY_HEIGHT / 2;
        private const float CITY_SOUTH_EXIT_X = CITY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float CITY_SOUTH_EXIT_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float CITY_SOUTH_ENTRANCE_X = CITY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float CITY_SOUTH_ENTRANCE_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float CITY_WEST_EXIT_X = CITY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_WEST_EXIT_Y = CITY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float CITY_WEST_ENTRANCE_X = CITY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_WEST_ENTRANCE_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float CITY_EAST_EXIT_X = CITY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_EAST_EXIT_Y = CITY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float CITY_EAST_ENTRANCE_X = CITY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_EAST_ENTRANCE_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;

        private const int RND_WIDTH = 900;
        private const int RND_HEIGHT = 900;
        private const float RND_SCALE = 0.1f;
        private const int RND_X = CENTER_X + 300;
        private const int RND_Y = CENTER_Y + 225;
        private const float RND_ORIGIN_X = RND_WIDTH / 2;
        private const float RND_ORIGIN_Y = RND_HEIGHT / 2;
        private const float RND_EAST_EXIT_X = RND_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2 + 40;
        private const float RND_EAST_EXIT_Y = RND_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float RND_EAST_ENTRANCE_X = RND_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2 + 40;
        private const float RND_EAST_ENTRANCE_Y = RND_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float RND_WEST_EXIT_X = RND_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float RND_WEST_EXIT_Y = RND_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float RND_WEST_ENTRANCE_X = RND_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float RND_WEST_ENTRANCE_Y = RND_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float RND_SOUTH_EXIT_X = RND_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float RND_SOUTH_EXIT_Y = RND_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float RND_SOUTH_ENTRANCE_X = RND_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float RND_SOUTH_ENTRANCE_Y = RND_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        
        private const int FACTORY_WIDTH = 900;
        private const int FACTORY_HEIGHT = 900;
        private const float FACTORY_SCALE = 0.1f;
        private const int FACTORY_X = CENTER_X - 300;
        private const int FACTORY_Y = CENTER_Y + 225;
        private const float FACTORY_ORIGIN_X = FACTORY_WIDTH / 2;
        private const float FACTORY_ORIGIN_Y = FACTORY_HEIGHT / 2;
        private const float FACTORY_WEST_EXIT_X = FACTORY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2 - 40;
        private const float FACTORY_WEST_EXIT_Y = FACTORY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float FACTORY_WEST_ENTRANCE_X = FACTORY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2 - 40;
        private const float FACTORY_WEST_ENTRANCE_Y = FACTORY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float FACTORY_EAST_EXIT_X = FACTORY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float FACTORY_EAST_EXIT_Y = FACTORY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float FACTORY_EAST_ENTRANCE_X = FACTORY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float FACTORY_EAST_ENTRANCE_Y = FACTORY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 4;
        private const float FACTORY_SOUTH_EXIT_X = FACTORY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float FACTORY_SOUTH_EXIT_Y = FACTORY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float FACTORY_SOUTH_ENTRANCE_X = FACTORY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 4;
        private const float FACTORY_SOUTH_ENTRANCE_Y = FACTORY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;

        #endregion

        #region Loose Paths

        private const int QUADRANT_RADIUS = 15;
        private static readonly MiMousePath CITY_TO_SCHOOL = new MiMousePath
        (
            CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y, 
            SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y,
            SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS,
            CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y, 
            CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y
        );

        private static readonly MiMousePath CITY_TO_FACTORY = new MiMousePath
        (
            CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y, 
            FACTORY_WEST_ENTRANCE_X, FACTORY_WEST_ENTRANCE_Y,
            FACTORY_X - QUADRANT_RADIUS, FACTORY_Y - QUADRANT_RADIUS,
            CITY_X - QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS, 
            FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y, 
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y
        );

        private static readonly MiMousePath CITY_TO_RND = new MiMousePath
        (
            CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y, 
            RND_EAST_ENTRANCE_X, RND_EAST_ENTRANCE_Y,
            RND_X + QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS,
            CITY_X + QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS, 
            RND_EAST_EXIT_X, RND_EAST_EXIT_Y, 
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y
        );

        private static readonly MiMousePath SCHOOL_TO_CITY = new MiMousePath
        (
            SCHOOL_X - QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS, 
            SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y, 
            CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y,
            CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS,
            SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS, 
            CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y, 
            SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y
        );

        private static readonly MiMousePath SCHOOL_TO_FACTORY = new MiMousePath
        (
            SCHOOL_X - QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS, 
            SCHOOL_WEST_EXIT_X, SCHOOL_WEST_EXIT_Y, 
            FACTORY_EAST_ENTRANCE_X, FACTORY_EAST_ENTRANCE_Y,
            FACTORY_X + QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS,
            CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y, 
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y
        );

        private static readonly MiMousePath SCHOOL_TO_RND = new MiMousePath
        (
            SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS, 
            SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y, 
            RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y,
            RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS,
            CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS, 
            RND_EAST_EXIT_X, RND_EAST_EXIT_Y, 
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y
        );

        private static readonly MiMousePath RND_TO_CITY = new MiMousePath
        (
            RND_X + QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS, 
            RND_EAST_EXIT_X, RND_EAST_EXIT_Y, 
            CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y,
            CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS,
            RND_X + QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS, 
            CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y, 
            RND_EAST_ENTRANCE_X, RND_EAST_ENTRANCE_Y
        );

        private static readonly MiMousePath RND_TO_SCHOOL = new MiMousePath
        (
            RND_X - QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS, 
            RND_WEST_EXIT_X, RND_WEST_EXIT_Y, 
            SCHOOL_EAST_ENTRANCE_X, SCHOOL_EAST_ENTRANCE_Y,
            SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS,
            RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS,
            SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y, 
            RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y
        );

        private static readonly MiMousePath RND_TO_FACTORY = new MiMousePath
        (
            RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS, 
            RND_SOUTH_EXIT_X, RND_SOUTH_EXIT_Y, 
            FACTORY_SOUTH_ENTRANCE_X, FACTORY_SOUTH_ENTRANCE_Y,
            FACTORY_X + QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS,
            RND_X + QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS, 
            FACTORY_SOUTH_EXIT_X, FACTORY_SOUTH_EXIT_Y, 
            RND_SOUTH_ENTRANCE_X, RND_SOUTH_ENTRANCE_Y
        );

        private static readonly MiMousePath FACTORY_TO_CITY = new MiMousePath
        (
            FACTORY_X - QUADRANT_RADIUS, FACTORY_Y - QUADRANT_RADIUS, 
            FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y, 
            CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y,
            CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS,
            FACTORY_X - QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS, 
            CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y, 
            FACTORY_WEST_ENTRANCE_X, FACTORY_WEST_ENTRANCE_Y
        );

        #endregion

        #region Mouse Fields and Constants

        private const int MOUSE_R = 25;
        private static readonly Vector2 MOUSE_ORIGIN = new Vector2(MOUSE_R, MOUSE_R);
        private const float MOUSE_SCALE = 0.25f;
        private const int MOUSE_UNCROWDEDNESS = 2;
        private const float MOUSE_MASS = 2;
        private const float MOUSE_FRICTION = 0.5f;
        private const ushort MOUSE_MOVETIME = 20;
        private const int DESTINATION_REACHED_LAXITY = 8;
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

        #region Collision Map

        private const int COLLISION_TEXTURE_WIDTH = 300;
        private const int COLLISION_TEXTURE_HEIGHT = 300;
        private const float COLLISION_TEXTURE_SCALE = 0.25f;
        private static readonly Vector2 COLLISION_TEXTURE_ORIGIN = new Vector2(COLLISION_TEXTURE_WIDTH / 2, COLLISION_TEXTURE_HEIGHT / 2);
        private const int COLLISION_TEXTURE_KINDS = 9;
        private static readonly string[] COLLISION_TEXTURE_KIND_NAMES = 
        { 
            "RoadCollision\\BLOCKED", 
            "RoadCollision\\BUILDING_IN", 
            "RoadCollision\\STRAIGHT",
            "RoadCollision\\STRAIGHT",
            "RoadCollision\\CORNER", 
            "RoadCollision\\CORNER", 
            "RoadCollision\\CORNER", 
            "RoadCollision\\CORNER", 
            "RoadCollision\\BUILDING_CORNER", 
        };
        private Texture2D[] collisionTextures;

        private const int COLLISION_MAP_WIDTH = 13;
        private const int COLLISION_MAP_HEIGHT = 11;
        private const float COLLISION_MAP_X_OFFSET = 2;
        private const float COLLISION_MAP_Y_OFFSET = 1;
        private const int O = 9;
        private const int M = 0;
        private const int X = 1;
        private const int I = 2;
        private const int _ = 3;
        private const int r = 4;
        private const int L = 5;
        private const int J = 6;
        private const int T = 7;
        private static readonly int[][] COLLISION_BODIES_TEXTURE_KIND_MAP =
        {
            new int[] { O, M, M, M, M, M, M, M, M, M, M, M, O },
            new int[] { M, r, _, _, _, _, X, _, _, _, _, T, M },
            new int[] { M, I, M, M, M, M, I, M, M, M, M, I, M },
            new int[] { M, I, M, O, O, M, I, M, O, O, M, I, M },
            new int[] { M, I, M, M, M, M, I, M, M, M, M, I, M },
            new int[] { M, I, M, r, _, _, X, _, _, T, M, I, M },
            new int[] { M, I, M, I, M, M, M, M, M, I, M, I, M },
            new int[] { M, I, M, I, M, O, O, O, M, I, M, I, M },
            new int[] { M, L, X, J, M, M, M, M, M, L, X, J, M },
            new int[] { O, M, L, _, _, _, _, _, _, _, J, M, O },
            new int[] { O, O, M, M, M, M, M, M, M, M, M, O, O }
        };
        private static readonly Dictionary<int, float> COLLISION_BODIES_ROTATION_MAP = new Dictionary<int,float>()
        {
            { O, 0 },
            { M, 0 },
            { X, 0 },
            { I, (float)Math.PI * 1/2 },
            { _, 0 },
            { r, 0 },
            { L, (float)Math.PI * 3/2 },
            { J, (float)Math.PI },
            { T, (float)Math.PI * 1/2 }
        };

        private List<Texture2D> collisionBodiesTextures;
        private List<Vector2> collisionBodiesPositions;
        private List<float> collisionBodiesRotations;
        private List<Body> collisionBodies;

        private const int ROAD_TEXTURE_KINDS = 10;
        private static readonly string[] ROAD_TEXTURE_KIND_NAMES = 
        { 
            "RoadImage\\FLOOR_TILE", 
            "RoadImage\\BUILDING_IN", 
            "RoadImage\\STRAIGHT",
            "RoadImage\\STRAIGHT",
            "RoadImage\\CORNER", 
            "RoadImage\\CORNER", 
            "RoadImage\\CORNER", 
            "RoadImage\\CORNER", 
            "RoadImage\\BUILDING_CORNER",
            "RoadImage\\GRASS_TILE"
        };
        private Texture2D[] roadTextures;
        private List<Texture2D> roadTexturesToDraw;
        private List<Vector2> roadTexturePositions;
        private List<float> roadTextureRotations;

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

        private const float BACKGROUND_SCALE = 4;
        private Texture2D background;

        private MiAnimatingComponent cursor;
        private MicycleGameSystem system;
        private MiScriptEngine inGameScripts;
        private World world;

#if DEBUG
        private DebugViewXNA debugView;
        private Camera2D camera;
#endif

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
            foreach (Body mouse in mice)
                world.RemoveBody(mouse);
            mice.Clear();
            inGameScripts = new MiScriptEngine(Game);
            ActiveButton = schoolButton;
            system.Reset();
            system.Enabled = true;
        }

        private IEnumerator<ulong> SendMouse(MiMousePath path, MiSemaphoreSet sema)
        {
            Body mouseBody = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(MOUSE_R * MOUSE_SCALE), MOUSE_MASS, ConvertUnits.ToSimUnits(new Vector2(path.SourceX, path.SourceY) - MiResolution.Center));
            mouseBody.BodyType = BodyType.Dynamic;
            mouseBody.Friction = MOUSE_FRICTION;
            mouseBody.CreateFixture(new CircleShape(ConvertUnits.ToSimUnits(MOUSE_R * MOUSE_SCALE + MOUSE_UNCROWDEDNESS), MOUSE_MASS));
            mice.Add(mouseBody);
            Vector2 nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.SourceExitX, path.SourceExitY) - MiResolution.Center);
            while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
            {
                mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                yield return MOUSE_MOVETIME;
            }
            mouseBody.ResetDynamics();
            system.Signal(ref sema.HasReachedWaitQueueTail);
            nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.WaitQueueHeadX, path.WaitQueueHeadY) - MiResolution.Center);
            while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
            {
                mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                yield return MOUSE_MOVETIME;
            }
            mouseBody.ResetDynamics();
            system.Signal(ref sema.HasReachedWaitQueueHead);
            while (true)
            {
                if (system.Wait(ref sema.Accept))
                {
                    nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.AcceptDestX, path.AcceptDestY) - MiResolution.Center);
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
                    {
                        mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    break;
                }
                else if (system.Wait(ref sema.Reject))
                {
                    // mouseBody.IgnoreCollisionWith(backgroundBody);
                    nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.RejectWaitQueueTailX, path.RejectWaitQueueTailY) - MiResolution.Center);
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
                    {
                        mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    // mouseBody.RestoreCollisionWith(backgroundBody);
                    nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.RejectWaitQueueHeadX, path.RejectWaitQueueHeadY) - MiResolution.Center);
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
                    {
                        mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    yield return MOUSE_MOVETIME;
                    nextGoal = ConvertUnits.ToSimUnits(new Vector2(path.RejectDestX, path.RejectDestY) - MiResolution.Center);
                    while (Vector2.DistanceSquared(mouseBody.Position, nextGoal) > ConvertUnits.ToSimUnits(DESTINATION_REACHED_LAXITY))
                    {
                        mouseBody.LinearVelocity = ConvertUnits.ToSimUnits(MOUSE_MOVE_FORCE) * Vector2.Normalize(nextGoal - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    yield return MOUSE_MOVETIME;
                    break;
                }
                else yield return MOUSE_MOVETIME;
            }
            mice.Remove(mouseBody);
            world.RemoveBody(mouseBody);
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
                ActiveButton = schoolButton;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, SCHOOL_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, SCHOOL_Y));
                yield return 20;
                cursor.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Downed()
        {
            if (ActiveButton == schoolButton)
            {
                ActiveButton = factoryButton;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_Y));
                yield return 20;
                cursor.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Lefted()
        {
            if (ActiveButton == schoolButton || ActiveButton == rndButton)
            {
                ActiveButton = factoryButton;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, FACTORY_Y));
                yield return 20;
                cursor.MoveEnabled = false;
            }
        }

        public override IEnumerator<ulong> Righted()
        {
            if (ActiveButton == schoolButton || ActiveButton == factoryButton)
            {
                ActiveButton = rndButton;
                cursor.MoveEnabled = true;
                cursor.XPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, RND_X));
                cursor.YPositionOverTime.Keys.Add(new CurveKey(cursor.MoveTimer + 20, RND_Y));
                yield return 20;
                cursor.MoveEnabled = false;
            }
        }

        public override void LoadContent()
        {

#if DEBUG
            debugView = new DebugViewXNA(world);
            debugView.AppendFlags(DebugViewFlags.Shape);
            debugView.AppendFlags(DebugViewFlags.PolygonPoints);
            debugView.LoadContent(Game.GraphicsDevice, Game.Content);
            camera = new Camera2D(Game.GraphicsDevice);
#endif
            background = Game.Content.Load<Texture2D>("RoadImage\\GRASS_TILE");

            collisionTextures = new Texture2D[COLLISION_TEXTURE_KINDS];
            for (int i = 0; i < COLLISION_TEXTURE_KINDS; i++)
                collisionTextures[i] = Game.Content.Load<Texture2D>(COLLISION_TEXTURE_KIND_NAMES[i]);

            collisionBodiesTextures = new List<Texture2D>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O)
                        collisionBodiesTextures.Add(collisionTextures[COLLISION_BODIES_TEXTURE_KIND_MAP[j][i]]);

            collisionBodiesPositions = new List<Vector2>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O)
                        collisionBodiesPositions.Add(new Vector2((i + COLLISION_MAP_X_OFFSET) * COLLISION_TEXTURE_WIDTH * COLLISION_TEXTURE_SCALE, (j + COLLISION_MAP_Y_OFFSET) * COLLISION_TEXTURE_HEIGHT * COLLISION_TEXTURE_SCALE));

            collisionBodiesRotations = new List<float>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O)
                    {
                        float rotation;
                        COLLISION_BODIES_ROTATION_MAP.TryGetValue(COLLISION_BODIES_TEXTURE_KIND_MAP[j][i], out rotation);
                        collisionBodiesRotations.Add(rotation);
                    }

            collisionBodies = new List<Body>();
            for(int i = 0; i < collisionBodiesTextures.Count; i++)
            {
                Texture2D collisionTexture = collisionBodiesTextures[i];
                uint[] collisionData = new uint[collisionTexture.Width * collisionTexture.Height];
                collisionTexture.GetData(collisionData);
                Vertices collisionVertices = PolygonTools.CreatePolygon(collisionData, collisionTexture.Width, false);
                Vector2 centroid = new Vector2(-150, -150);
                collisionVertices.Translate(centroid);
                collisionVertices = SimplifyTools.ReduceByDistance(collisionVertices, 4);
                Vector2 scale = ConvertUnits.ToSimUnits(new Vector2(COLLISION_TEXTURE_SCALE, COLLISION_TEXTURE_SCALE));
                collisionVertices.Scale(ref scale);
                List<Vertices> decomposedVertices = BayazitDecomposer.ConvexPartition(collisionVertices);
                Body collisionBody = BodyFactory.CreateCompoundPolygon(world, decomposedVertices, 1, ConvertUnits.ToSimUnits(collisionBodiesPositions[i] - MiResolution.Center));
                collisionBody.BodyType = BodyType.Static;
                collisionBody.Enabled = true;
                foreach (Vertices v in decomposedVertices)
                    collisionBody.CreateFixture(new PolygonShape(v, 1));
                collisionBody.SetTransform(collisionBody.Position, collisionBodiesRotations[i]);
                collisionBodies.Add(collisionBody);
            }

            roadTextures = new Texture2D[ROAD_TEXTURE_KINDS];
            for (int i = 0; i < ROAD_TEXTURE_KINDS; i++)
                roadTextures[i] = Game.Content.Load<Texture2D>(ROAD_TEXTURE_KIND_NAMES[i]);

            roadTexturesToDraw = new List<Texture2D>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O && COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != M)
                        roadTexturesToDraw.Add(roadTextures[COLLISION_BODIES_TEXTURE_KIND_MAP[j][i]]);

            roadTexturePositions = new List<Vector2>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O && COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != M)
                        roadTexturePositions.Add(new Vector2((i + COLLISION_MAP_X_OFFSET) * COLLISION_TEXTURE_WIDTH * COLLISION_TEXTURE_SCALE, (j + COLLISION_MAP_Y_OFFSET) * COLLISION_TEXTURE_HEIGHT * COLLISION_TEXTURE_SCALE));

            roadTextureRotations = new List<float>();
            for (int i = 0; i < COLLISION_MAP_WIDTH; i++)
                for (int j = 0; j < COLLISION_MAP_HEIGHT; j++)
                    if (COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != O && COLLISION_BODIES_TEXTURE_KIND_MAP[j][i] != M)
                    {
                        float rotation;
                        COLLISION_BODIES_ROTATION_MAP.TryGetValue(COLLISION_BODIES_TEXTURE_KIND_MAP[j][i], out rotation);
                        roadTextureRotations.Add(rotation);
                    }

            school.AddTexture(Game.Content.Load<Texture2D>("Buildings\\SCHOOL"), 0);
            city.AddTexture(Game.Content.Load<Texture2D>("Buildings\\CITY"), 0);
            rnd.AddTexture(Game.Content.Load<Texture2D>("Buildings\\LAB"), 0);
            factory.AddTexture(Game.Content.Load<Texture2D>("Buildings\\FACTORY"), 0);

            cursor.AddTexture(Game.Content.Load<Texture2D>("buttonoutline"), 0);

            mouseImage = Game.Content.Load<Texture2D>("mice");

            inGameMenu.LoadContent();
            schoolMenu.LoadContent();
            factoryMenu.LoadContent();
            rndMenu.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
#if DEBUG
            camera.Update(gameTime);
#endif

            school.Update(gameTime);
            city.Update(gameTime);
            rnd.Update(gameTime);
            factory.Update(gameTime);
            cursor.Update(gameTime);

            if (system.Enabled)
            {
                system.Update(gameTime);
                world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
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
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, BACKGROUND_SCALE, SpriteEffects.None, 0);

            for (int i = 0; i < roadTexturesToDraw.Count; i++)
                if (roadTexturesToDraw[i] != null)
                    Game.SpriteBatch.Draw(roadTexturesToDraw[i], roadTexturePositions[i], null, Color.White, roadTextureRotations[i], COLLISION_TEXTURE_ORIGIN, COLLISION_TEXTURE_SCALE, SpriteEffects.None, 0);

            foreach (Body mouse in mice)
                Game.SpriteBatch.Draw(mouseImage, ConvertUnits.ToDisplayUnits(mouse.Position) + MiResolution.Center, null, Color.White, mouse.Rotation, MOUSE_ORIGIN, MOUSE_SCALE, SpriteEffects.None, 0);

            school.Draw(gameTime);
            city.Draw(gameTime);
            rnd.Draw(gameTime);
            factory.Draw(gameTime);

            cursor.Draw(gameTime);

            Game.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>("Default"), system.printStats(), Vector2.Zero, Color.Black);

#if DEBUG
            //Matrix projection = camera.SimProjection * MiResolution.GetTransformationMatrix();
            //Matrix view = camera.SimView;

            //debugView.RenderDebugData(ref projection, ref view);
#endif
        }
    }
}
