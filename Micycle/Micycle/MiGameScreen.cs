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
        private const float SCHOOL_NORTH_EXIT_X = SCHOOL_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 3;
        private const float SCHOOL_NORTH_EXIT_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float SCHOOL_NORTH_ENTRANCE_X = SCHOOL_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 3;
        private const float SCHOOL_NORTH_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float SCHOOL_WEST_EXIT_X = SCHOOL_X - 3 * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float SCHOOL_WEST_EXIT_Y = SCHOOL_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float SCHOOL_WEST_ENTRANCE_X = SCHOOL_X - 3 * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float SCHOOL_WEST_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float SCHOOL_EAST_EXIT_X = SCHOOL_X + 3 * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float SCHOOL_EAST_EXIT_Y = SCHOOL_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float SCHOOL_EAST_ENTRANCE_X = SCHOOL_X + 2.5f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float SCHOOL_EAST_ENTRANCE_Y = SCHOOL_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;

        private const int CITY_WIDTH = 900;
        private const int CITY_HEIGHT = 900;
        private const float CITY_SCALE = 0.1f;
        private const int CITY_X = CENTER_X;
        private const int CITY_Y = CENTER_Y - 300;
        private const float CITY_ORIGIN_X = CITY_WIDTH / 2;
        private const float CITY_ORIGIN_Y = CITY_HEIGHT / 2;
        private const float CITY_SOUTH_EXIT_X = CITY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 3;
        private const float CITY_SOUTH_EXIT_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float CITY_SOUTH_ENTRANCE_X = CITY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 3;
        private const float CITY_SOUTH_ENTRANCE_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 2;
        private const float CITY_WEST_EXIT_X = CITY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_WEST_EXIT_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float CITY_WEST_ENTRANCE_X = CITY_X - 5.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float CITY_WEST_ENTRANCE_Y = CITY_Y - 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float CITY_EAST_EXIT_X = CITY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float CITY_EAST_EXIT_Y = CITY_Y + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float CITY_EAST_ENTRANCE_X = CITY_X + 5.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float CITY_EAST_ENTRANCE_Y = CITY_Y - 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;

        private const int RND_WIDTH = 900;
        private const int RND_HEIGHT = 900;
        private const float RND_SCALE = 0.1f;
        private const int RND_X = CENTER_X + 300;
        private const int RND_Y = CENTER_Y + 225;
        private const float RND_ORIGIN_X = RND_WIDTH / 2;
        private const float RND_ORIGIN_Y = RND_HEIGHT / 2;
        private const float RND_EAST_EXIT_X = RND_X + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_EAST_EXIT_Y = RND_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float RND_EAST_ENTRANCE_X = RND_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_EAST_ENTRANCE_Y = RND_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float RND_WEST_EXIT_X = RND_X - 0.5f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_WEST_EXIT_Y = RND_Y - 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float RND_WEST_ENTRANCE_X = RND_X - 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_WEST_ENTRANCE_Y = RND_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float RND_SOUTH_EXIT_X = RND_X - 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_SOUTH_EXIT_Y = RND_Y + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float RND_SOUTH_ENTRANCE_X = RND_X + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float RND_SOUTH_ENTRANCE_Y = RND_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        
        private const int FACTORY_WIDTH = 900;
        private const int FACTORY_HEIGHT = 900;
        private const float FACTORY_SCALE = 0.1f;
        private const int FACTORY_X = CENTER_X - 300;
        private const int FACTORY_Y = CENTER_Y + 225;
        private const float FACTORY_ORIGIN_X = FACTORY_WIDTH / 2;
        private const float FACTORY_ORIGIN_Y = FACTORY_HEIGHT / 2;
        private const float FACTORY_WEST_EXIT_X = FACTORY_X - 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float FACTORY_WEST_EXIT_Y = FACTORY_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float FACTORY_WEST_ENTRANCE_X = FACTORY_X - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float FACTORY_WEST_ENTRANCE_Y = FACTORY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float FACTORY_EAST_EXIT_X = FACTORY_X + COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH / 2;
        private const float FACTORY_EAST_EXIT_Y = FACTORY_Y - COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT / 3;
        private const float FACTORY_EAST_ENTRANCE_X = FACTORY_X + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float FACTORY_EAST_ENTRANCE_Y = FACTORY_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float FACTORY_SOUTH_EXIT_X = FACTORY_X - 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float FACTORY_SOUTH_EXIT_Y = FACTORY_Y + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;
        private const float FACTORY_SOUTH_ENTRANCE_X = FACTORY_X + 1.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_WIDTH;
        private const float FACTORY_SOUTH_ENTRANCE_Y = FACTORY_Y + 0.25f * COLLISION_TEXTURE_SCALE * COLLISION_TEXTURE_HEIGHT;

        #endregion

        #region Loose Paths

        private const int QUADRANT_RADIUS = 15;
        private static readonly MiMousePath CITY_TO_SCHOOL = new MiMousePath
        (
            new Vector2(CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS), 
            new Vector2(CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y), 
            new Vector2(SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y),
            new Vector2(SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS),
            new Vector2(SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y), 
            new Vector2(CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y),
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath CITY_TO_FACTORY = new MiMousePath
        (
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS), 
            new Vector2(CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y), 
            new Vector2(FACTORY_WEST_ENTRANCE_X, FACTORY_WEST_ENTRANCE_Y),
            new Vector2(FACTORY_X - QUADRANT_RADIUS, FACTORY_Y - QUADRANT_RADIUS),
            new Vector2(FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y), 
            new Vector2(CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y),
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS)
        );

        private static readonly MiMousePath CITY_TO_RND = new MiMousePath
        (
            new Vector2(CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS), 
            new Vector2(CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y), 
            new Vector2(RND_EAST_ENTRANCE_X, RND_EAST_ENTRANCE_Y),
            new Vector2(RND_X + QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS),
            new Vector2(RND_EAST_EXIT_X, RND_EAST_EXIT_Y), 
            new Vector2(CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y),
            new Vector2(CITY_X + QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS)
        );

        private static readonly MiMousePath SCHOOL_TO_CITY = new MiMousePath
        (
            new Vector2(SCHOOL_X - QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS), 
            new Vector2(SCHOOL_NORTH_EXIT_X, SCHOOL_NORTH_EXIT_Y), 
            new Vector2(CITY_SOUTH_ENTRANCE_X, CITY_SOUTH_ENTRANCE_Y),
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS),
            new Vector2(CITY_SOUTH_EXIT_X, CITY_SOUTH_EXIT_Y), 
            new Vector2(SCHOOL_NORTH_ENTRANCE_X, SCHOOL_NORTH_ENTRANCE_Y),
            new Vector2(SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath SCHOOL_TO_FACTORY = new MiMousePath
        (
            new Vector2(SCHOOL_X - QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS), 
            new Vector2(SCHOOL_WEST_EXIT_X, SCHOOL_WEST_EXIT_Y), 
            new Vector2(FACTORY_EAST_ENTRANCE_X, FACTORY_EAST_ENTRANCE_Y),
            new Vector2(FACTORY_X + QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS),
            new Vector2(FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y), 
            new Vector2(CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y),
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath SCHOOL_TO_RND = new MiMousePath
        (
            new Vector2(SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y + QUADRANT_RADIUS), 
            new Vector2(SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y), 
            new Vector2(RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y),
            new Vector2(RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS),
            new Vector2(RND_EAST_EXIT_X, RND_EAST_EXIT_Y), 
            new Vector2(CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y),
            new Vector2(CITY_X + QUADRANT_RADIUS, CITY_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath RND_TO_CITY = new MiMousePath
        (
            new Vector2(RND_X + QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS), 
            new Vector2(RND_EAST_EXIT_X, RND_EAST_EXIT_Y), 
            new Vector2(CITY_EAST_ENTRANCE_X, CITY_EAST_ENTRANCE_Y),
            new Vector2(CITY_X + QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS),
            new Vector2(CITY_EAST_EXIT_X, CITY_EAST_EXIT_Y), 
            new Vector2(RND_EAST_ENTRANCE_X, RND_EAST_ENTRANCE_Y),
            new Vector2(RND_X + QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS)
        );

        private static readonly MiMousePath RND_TO_SCHOOL = new MiMousePath
        (
            new Vector2(RND_X - QUADRANT_RADIUS, RND_Y - QUADRANT_RADIUS), 
            new Vector2(RND_WEST_EXIT_X, RND_WEST_EXIT_Y), 
            new Vector2(SCHOOL_EAST_ENTRANCE_X, SCHOOL_EAST_ENTRANCE_Y),
            new Vector2(SCHOOL_X + QUADRANT_RADIUS, SCHOOL_Y - QUADRANT_RADIUS),
            new Vector2(SCHOOL_EAST_EXIT_X, SCHOOL_EAST_EXIT_Y), 
            new Vector2(RND_WEST_ENTRANCE_X, RND_WEST_ENTRANCE_Y),
            new Vector2(RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath RND_TO_FACTORY = new MiMousePath
        (
            new Vector2(RND_X - QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS), 
            new Vector2(RND_SOUTH_EXIT_X, RND_SOUTH_EXIT_Y), 
            new Vector2(FACTORY_SOUTH_ENTRANCE_X, FACTORY_SOUTH_ENTRANCE_Y),
            new Vector2(FACTORY_X + QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS),
            new Vector2(FACTORY_SOUTH_EXIT_X, FACTORY_SOUTH_EXIT_Y), 
            new Vector2(RND_SOUTH_ENTRANCE_X, RND_SOUTH_ENTRANCE_Y),
            new Vector2(RND_X + QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS)
        );

        private static readonly MiMousePath FACTORY_TO_CITY = new MiMousePath
        (
            new Vector2(FACTORY_X - QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS), 
            new Vector2(FACTORY_WEST_EXIT_X, FACTORY_WEST_EXIT_Y), 
            new Vector2(CITY_WEST_ENTRANCE_X, CITY_WEST_ENTRANCE_Y),
            new Vector2(CITY_X - QUADRANT_RADIUS, CITY_Y - QUADRANT_RADIUS),
            new Vector2(CITY_WEST_EXIT_X, CITY_WEST_EXIT_Y), 
            new Vector2(FACTORY_WEST_ENTRANCE_X, FACTORY_WEST_ENTRANCE_Y),
            new Vector2(FACTORY_X - QUADRANT_RADIUS, FACTORY_Y - QUADRANT_RADIUS)
        );

        private static readonly MiMousePath FACTORY_TO_RND = new MiMousePath
        (
            new Vector2(FACTORY_X - QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS),
            new Vector2(FACTORY_SOUTH_EXIT_X, FACTORY_SOUTH_EXIT_Y),
            new Vector2(RND_SOUTH_ENTRANCE_X, RND_SOUTH_ENTRANCE_Y),
            new Vector2(RND_X + QUADRANT_RADIUS, RND_Y + QUADRANT_RADIUS),
            new Vector2(RND_SOUTH_EXIT_X, RND_SOUTH_EXIT_Y),
            new Vector2(FACTORY_SOUTH_ENTRANCE_X, FACTORY_SOUTH_ENTRANCE_Y),
            new Vector2(FACTORY_X + QUADRANT_RADIUS, FACTORY_Y + QUADRANT_RADIUS)
        );

        #endregion

        #region Mouse Fields and Constants

        private const float MOUSE_R = 25;
        private static readonly Vector2 MOUSE_ORIGIN = new Vector2(MOUSE_R / 2, MOUSE_R / 2);
        private const float MOUSE_SCALE = 0.25f;
        private const float MOUSE_UNCROWDEDNESS = 2;
        private const float MOUSE_MASS = 0.2f;
        private const ushort MOUSE_MOVETIME = 20;
        private const float DESTINATION_REACHED_LAXITY = 0.1f;
        private const float MOUSE_MOVE_FORCE = 0.5f;
        private const float MOUSE_SEND_WAIT_TIME = 60;
        private static CircleShape mouseFixture = new CircleShape(ConvertUnits.ToSimUnits(MOUSE_R * MOUSE_SCALE + MOUSE_UNCROWDEDNESS), MOUSE_MASS);
        private Texture2D mouseImage;
        private List<Body> mice;

        #endregion

        #region Neighboring Screens or Menus

        private MiInGameMenu inGameMenu;
        private MiGameOverTimeUp gameOverTimeUpScreen;
        private MiGameOverMoneyUp gameOverMoneyUpScreen;
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

        #region Player Game Resource Stats

        private const int TOP_PADDING = 5;
        private const int LEFT_PADDING = 5;
        private const int RIGHT_PADDING = 5;
        private const int BAR_THICKNESS = 30;

        private Texture2D horizontalBarBackgroundTexture;
        private Texture2D horizontalBarTexture;
        private Texture2D verticalBarBackgroundTexture;
        private Texture2D verticalBarTexture;

        private MiAnimatingComponent cashIcon;
        private const int CASH_ICON_WIDTH = 66;
        private const int CASH_ICON_HEIGHT = 66;
        private const float CASH_ICON_SCALE = 0.5f;
        private Rectangle cashBarFull;
        private Rectangle cashBar;
        private Color cashBarColor;

        private MiAnimatingComponent techPointsIcon;
        private const int TECH_POINTS_ICON_WIDTH = 66;
        private const int TECH_POINTS_ICON_HEIGHT = 66;
        private const float TECH_POINTS_ICON_SCALE = 0.5f;
        private Rectangle techPointsBarFull;
        private Rectangle techPointsBar;
        private Color techPointsBarColor;

        private SpriteFont statsFont;

        private MiAnimatingComponent populationIcon;
        private const int POPULATION_ICON_WIDTH = 66;
        private const int POPULATION_ICON_HEIGHT = 66;
        private const float POPULATION_ICON_SCALE = 0.5f;
        private Vector2 populationTextPosition;

        private MiAnimatingComponent timeLimitIcon;
        private const int TIME_LIMIT_ICON_WIDTH = 66;
        private const int TIME_LIMIT_ICON_HEIGHT = 66;
        private const float TIME_LIMIT_ICON_SCALE = 0.5f;
        private Vector2 timeLimitTextPosition;
        private double timeLeft;
        private bool gameRunning;

        #endregion

        #region Player Game Goal Stats

        private const int BOTTOM_PADDING = 5;
        private const int GOAL_BAR_Y = 650;
        private const int GOAL_BAR_HEIGHT = 200;

        private MiAnimatingComponent economyIcon;
        private const int ECONOMY_ICON_WIDTH = 66;
        private const int ECONOMY_ICON_HEIGHT = 66;
        private const float ECONOMY_ICON_SCALE = 0.5f;
        private Rectangle economyBar;
        private Rectangle economyBarFull;
        private Color economyBarColor;

        private MiAnimatingComponent technologyIcon;
        private const int TECHNOLOGY_ICON_WIDTH = 66;
        private const int TECHNOLOGY_ICON_HEIGHT = 66;
        private const float TECHNOLOGY_ICON_SCALE = 0.5f;
        private Rectangle technologyBar;
        private Rectangle technologyBarFull;
        private Color technologyBarColor;

        private MiAnimatingComponent employmentIcon;
        private const int EMPLOYMENT_ICON_WIDTH = 66;
        private const int EMPLOYMENT_ICON_HEIGHT = 66;
        private const float EMPLOYMENT_ICON_SCALE = 0.5f;
        private Rectangle employmentBar;
        private Rectangle employmentBarFull;
        private Color employmentBarColor;

        private MiAnimatingComponent educationIcon;
        private const int EDUCATION_ICON_WIDTH = 66;
        private const int EDUCATION_ICON_HEIGHT = 66;
        private const float EDUCATION_ICON_SCALE = 0.5f;
        private Rectangle educationBar;
        private Rectangle educationBarFull;
        private Color educationBarColor;

        #endregion

        private const float BACKGROUND_SCALE = 4f;
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
            gameOverTimeUpScreen = new MiGameOverTimeUp(game, system);
            gameOverMoneyUpScreen = new MiGameOverMoneyUp(game, system);
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

            #region Player Game Stats Initialization

            float barWidth = (MiResolution.VirtualWidth - (LEFT_PADDING + RIGHT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + POPULATION_ICON_WIDTH * POPULATION_ICON_SCALE + TIME_LIMIT_ICON_WIDTH * TIME_LIMIT_ICON_SCALE)) / 4;
            cashIcon = new MiAnimatingComponent(game, LEFT_PADDING, TOP_PADDING, CASH_ICON_SCALE, 0, 0, 0);
            cashBarFull = new Rectangle((int)(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE), TOP_PADDING, (int)barWidth, BAR_THICKNESS);
            cashBar = new Rectangle((int)(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE), TOP_PADDING, 0, BAR_THICKNESS);
            cashBarColor = Color.Turquoise;

            techPointsIcon = new MiAnimatingComponent(game, LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + barWidth, TOP_PADDING, TECH_POINTS_ICON_SCALE, 0, 0, 0);
            techPointsBarFull = new Rectangle((int)(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + barWidth), TOP_PADDING, (int)barWidth, BAR_THICKNESS);
            techPointsBar = new Rectangle((int)(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + barWidth), TOP_PADDING, 0, BAR_THICKNESS);
            techPointsBarColor = Color.LightSalmon;

            populationIcon = new MiAnimatingComponent(game, LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + 2 * barWidth, TOP_PADDING, POPULATION_ICON_SCALE, 0, 0, 0);
            populationTextPosition = new Vector2(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + POPULATION_ICON_WIDTH * POPULATION_ICON_SCALE + 2 * barWidth, TOP_PADDING);

            timeLimitIcon = new MiAnimatingComponent(game, LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + POPULATION_ICON_WIDTH * POPULATION_ICON_SCALE + 3 * barWidth, TOP_PADDING, TIME_LIMIT_ICON_SCALE, 0, 0, 0);
            timeLimitTextPosition = new Vector2(LEFT_PADDING + CASH_ICON_WIDTH * CASH_ICON_SCALE + TECH_POINTS_ICON_WIDTH * TECH_POINTS_ICON_SCALE + POPULATION_ICON_WIDTH * POPULATION_ICON_SCALE + TIME_LIMIT_ICON_WIDTH * TIME_LIMIT_ICON_SCALE + 3 * barWidth, TOP_PADDING);
            timeLeft = MicycleGameSystem.TIME_LIMIT;
            gameRunning = true;

            #endregion

            #region Player Game Goal Stats Initialization

            economyIcon = new MiAnimatingComponent(game, LEFT_PADDING, MiResolution.VirtualHeight - BOTTOM_PADDING - ECONOMY_ICON_HEIGHT * ECONOMY_ICON_SCALE, ECONOMY_ICON_SCALE, 0, 0, 0);
            economyBarFull = new Rectangle(LEFT_PADDING, GOAL_BAR_Y, BAR_THICKNESS, GOAL_BAR_HEIGHT);
            economyBar = new Rectangle(LEFT_PADDING, GOAL_BAR_Y + GOAL_BAR_HEIGHT, BAR_THICKNESS, 0);
            economyBarColor = Color.SlateBlue;
            technologyIcon = new MiAnimatingComponent(game, LEFT_PADDING + ECONOMY_ICON_WIDTH * ECONOMY_ICON_SCALE, MiResolution.VirtualHeight - BOTTOM_PADDING - TECHNOLOGY_ICON_HEIGHT * TECHNOLOGY_ICON_SCALE, TECHNOLOGY_ICON_SCALE, 0, 0, 0);
            technologyBarFull = new Rectangle((int)(LEFT_PADDING + ECONOMY_ICON_WIDTH * ECONOMY_ICON_SCALE), GOAL_BAR_Y, BAR_THICKNESS, GOAL_BAR_HEIGHT);
            technologyBar = new Rectangle((int)(LEFT_PADDING + ECONOMY_ICON_WIDTH * ECONOMY_ICON_SCALE), GOAL_BAR_Y + GOAL_BAR_HEIGHT, BAR_THICKNESS, 0);
            technologyBarColor = Color.Goldenrod;
            employmentIcon = new MiAnimatingComponent(game, MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE - EMPLOYMENT_ICON_WIDTH * EMPLOYMENT_ICON_SCALE, MiResolution.VirtualHeight - BOTTOM_PADDING - EMPLOYMENT_ICON_HEIGHT * EMPLOYMENT_ICON_SCALE, EMPLOYMENT_ICON_SCALE, 0, 0, 0);
            employmentBarFull = new Rectangle((int)(MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE - EMPLOYMENT_ICON_WIDTH * EMPLOYMENT_ICON_SCALE), GOAL_BAR_Y, BAR_THICKNESS, GOAL_BAR_HEIGHT);
            employmentBar = new Rectangle((int)(MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE - EMPLOYMENT_ICON_WIDTH * EMPLOYMENT_ICON_SCALE), GOAL_BAR_Y + GOAL_BAR_HEIGHT, BAR_THICKNESS, 0);
            employmentBarColor = Color.MediumPurple;
            educationIcon = new MiAnimatingComponent(game, MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE, MiResolution.VirtualHeight - BOTTOM_PADDING - EDUCATION_ICON_HEIGHT * EDUCATION_ICON_SCALE, EDUCATION_ICON_SCALE, 0, 0, 0);
            educationBarFull = new Rectangle((int)(MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE), GOAL_BAR_Y, BAR_THICKNESS, GOAL_BAR_HEIGHT);
            educationBar = new Rectangle((int)(MiResolution.VirtualWidth - RIGHT_PADDING - EDUCATION_ICON_WIDTH * EDUCATION_ICON_SCALE), GOAL_BAR_Y + GOAL_BAR_HEIGHT, BAR_THICKNESS, 0);
            educationBarColor = Color.Sienna;

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
            sema.SendUnlocked = false;
            Body mouseBody = BodyFactory.CreateBody(world, path.Source);
            mouseBody.BodyType = BodyType.Dynamic;
            mouseBody.CreateFixture(mouseFixture);
            mice.Add(mouseBody);
            while (Vector2.DistanceSquared(mouseBody.Position, path.AcceptWaitQueueTail) > DESTINATION_REACHED_LAXITY)
            {
                mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.AcceptWaitQueueTail - mouseBody.Position);
                yield return MOUSE_MOVETIME;
            }
            mouseBody.ResetDynamics();
            int yieldTime = 0;
            system.Signal(ref sema.HasReachedWaitQueueTail);
            while (Vector2.DistanceSquared(mouseBody.Position, path.AcceptWaitQueueHead) > DESTINATION_REACHED_LAXITY)
            {
                mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.AcceptWaitQueueHead - mouseBody.Position);
                if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                    sema.SendUnlocked = true;
                yield return MOUSE_MOVETIME;
            }
            mouseBody.ResetDynamics();
            system.Signal(ref sema.HasReachedWaitQueueHead);
            while (true)
            {
                if (system.Wait(ref sema.Accept))
                {
                    while (Vector2.DistanceSquared(mouseBody.Position, path.AcceptDest) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.AcceptDest - mouseBody.Position);
                        if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                            sema.SendUnlocked = true;
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    break;
                }
                else if (system.Wait(ref sema.Reject))
                {
                    while (Vector2.DistanceSquared(mouseBody.Position, path.AcceptDest) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.AcceptDest - mouseBody.Position);
                        if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                            sema.SendUnlocked = true;
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    mouseBody.SetTransform(path.RejectWaitQueueTail, 0);
                    /**
                    while (Vector2.DistanceSquared(mouseBody.Position, path.RejectWaitQueueTail) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.RejectWaitQueueTail - mouseBody.Position);
                        yield return MOUSE_MOVETIME;
                    }
                     * */
                    while (Vector2.DistanceSquared(mouseBody.Position, path.RejectWaitQueueHead) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.RejectWaitQueueHead - mouseBody.Position);
                        if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                            sema.SendUnlocked = true;
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    while (Vector2.DistanceSquared(mouseBody.Position, path.RejectDest) > DESTINATION_REACHED_LAXITY)
                    {
                        mouseBody.LinearVelocity = MOUSE_MOVE_FORCE * Vector2.Normalize(path.RejectDest - mouseBody.Position);
                        if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                            sema.SendUnlocked = true;
                        yield return MOUSE_MOVETIME;
                    }
                    mouseBody.ResetDynamics();
                    break;
                }
                else
                {
                    if ((yieldTime += MOUSE_MOVETIME) > MOUSE_SEND_WAIT_TIME)
                        sema.SendUnlocked = true;
                    yield return MOUSE_MOVETIME;
                }
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

        private IEnumerator<ulong> GameOverTimeUp()
        {
            yield return 1;
            system.Enabled = false;
            Game.ToUpdate.Push(gameOverTimeUpScreen);
            Game.ToDraw.AddLast(gameOverTimeUpScreen);
            gameOverTimeUpScreen.CalculateScore();
            IEnumerator<ulong> entry = gameOverTimeUpScreen.EntrySequence();
            do
            {
                yield return entry.Current;
            }
            while (entry.MoveNext());
        }

        private IEnumerator<ulong> GameOverMoneyUp()
        {
            yield return 1;
            system.Enabled = false;
            Game.ToUpdate.Push(gameOverMoneyUpScreen);
            Game.ToDraw.AddLast(gameOverMoneyUpScreen);
            gameOverMoneyUpScreen.CalculateScore();
            IEnumerator<ulong> entry = gameOverMoneyUpScreen.EntrySequence();
            do
            {
                yield return entry.Current;
            }
            while (entry.MoveNext());
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

            horizontalBarBackgroundTexture = Game.Content.Load<Texture2D>("horizontalBar");
            horizontalBarTexture = Game.Content.Load<Texture2D>("horizontalBar");
            verticalBarBackgroundTexture = Game.Content.Load<Texture2D>("verticalBar");
            verticalBarTexture = Game.Content.Load<Texture2D>("verticalBar");

            cashIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            techPointsIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            populationIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            timeLimitIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            economyIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            technologyIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            employmentIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);
            educationIcon.AddTexture(Game.Content.Load<Texture2D>("mice"), 0);

            statsFont = Game.Content.Load<SpriteFont>("Fonts\\Default");

            inGameMenu.LoadContent();
            gameOverTimeUpScreen.LoadContent();
            gameOverMoneyUpScreen.LoadContent();
            schoolMenu.LoadContent();
            factoryMenu.LoadContent();
            rndMenu.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
#if DEBUG
            camera.Update(gameTime);
#endif

            cursor.Update(gameTime);

            if (system.Enabled)
            {
                system.Update(gameTime);
                world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
                inGameScripts.Update(gameTime);

                if (system.CityToSchool.SendUnlocked && system.Wait(ref system.CityToSchool.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_SCHOOL, system.CityToSchool);
                        }));

                if (system.CityToFactory.SendUnlocked && system.Wait(ref system.CityToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_FACTORY, system.CityToFactory);
                        }));

                if (system.CityToRnd.SendUnlocked && system.Wait(ref system.CityToRnd.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(CITY_TO_RND, system.CityToRnd);
                        }));

                if (system.SchoolToCity.SendUnlocked && system.Wait(ref system.SchoolToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_CITY, system.SchoolToCity);
                        }));

                if (system.SchoolToFactory.SendUnlocked && system.Wait(ref system.SchoolToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                          delegate
                          {
                              return SendMouse(SCHOOL_TO_FACTORY, system.SchoolToFactory);
                          }));

                if (system.SchoolToRnd.SendUnlocked && system.Wait(ref system.SchoolToRnd.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(SCHOOL_TO_RND, system.SchoolToRnd);
                        }));

                if (system.FactoryToCity.SendUnlocked && system.Wait(ref system.FactoryToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(FACTORY_TO_CITY, system.FactoryToCity);
                        }));

                if (system.RndToCity.SendUnlocked && system.Wait(ref system.RndToCity.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_CITY, system.RndToCity);
                        }));

                if (system.RndToSchool.SendUnlocked && system.Wait(ref system.RndToSchool.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_SCHOOL, system.RndToSchool);
                        }));

                if (system.RndToFactory.SendUnlocked && system.Wait(ref system.RndToFactory.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(RND_TO_FACTORY, system.RndToFactory);
                        }));

                if(system.FactoryToRnd.SendUnlocked && system.Wait(ref system.FactoryToRnd.SendFromAToB))
                    inGameScripts.ExecuteScript(new MiScript(
                        delegate
                        {
                            return SendMouse(FACTORY_TO_RND, system.FactoryToRnd);
                        }));

                techPointsBar.Width = (int)(techPointsBarFull.Width * system.GetTechPoints());
                if (system.GetCash() < 0)
                {
                    gameRunning = false;
                    Game.ScriptEngine.ExecuteScript(new MiScript(GameOverMoneyUp));
                }
                else
                {
                    cashBar.Width = (int)(cashBarFull.Width * system.GetCash());
                }
                timeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timeLeft <= 0)
                {
                    timeLeft = 0;
                    if (gameRunning)
                    {
                        gameRunning = false;
                        Game.ScriptEngine.ExecuteScript(new MiScript(GameOverTimeUp));
                    }
                }
                economyBar.Height = (int)(economyBarFull.Height * system.EconomyGoalProgress);
                economyBar.Y = GOAL_BAR_Y + GOAL_BAR_HEIGHT - economyBar.Height;
                technologyBar.Height = (int)(technologyBarFull.Height * system.TechnologyGoalProgress);
                technologyBar.Y = GOAL_BAR_Y + GOAL_BAR_HEIGHT - technologyBar.Height;
                employmentBar.Height = (int)(employmentBarFull.Height * system.EmploymentGoalProgress);
                employmentBar.Y = GOAL_BAR_Y + GOAL_BAR_HEIGHT - employmentBar.Height;
                educationBar.Height = (int)(educationBarFull.Height * system.EducationGoalProgress);
                educationBar.Y = GOAL_BAR_Y + GOAL_BAR_HEIGHT - educationBar.Height;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(background, MiResolution.Center, null, Color.White, 0, COLLISION_TEXTURE_ORIGIN, BACKGROUND_SCALE, SpriteEffects.None, 0);

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

            cashIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(horizontalBarBackgroundTexture, cashBarFull, Color.White);
            Game.SpriteBatch.Draw(horizontalBarTexture, cashBar, cashBarColor);
            techPointsIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(horizontalBarBackgroundTexture, techPointsBarFull, Color.White);
            Game.SpriteBatch.Draw(horizontalBarTexture, techPointsBar, techPointsBarColor);
            populationIcon.Draw(gameTime);
            Game.SpriteBatch.DrawString(statsFont, system.GetTotalPopulation().ToString(), populationTextPosition, Color.White);
            timeLimitIcon.Draw(gameTime);
            Game.SpriteBatch.DrawString(statsFont, (ushort)(timeLeft / 60) + ":" + ((ushort)(timeLeft % 60)).ToString().PadLeft(2, '0'), timeLimitTextPosition, Color.White);

            economyIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(verticalBarBackgroundTexture, economyBarFull, Color.White);
            Game.SpriteBatch.Draw(verticalBarTexture, economyBar, economyBarColor);
            technologyIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(verticalBarBackgroundTexture, technologyBarFull, Color.White);
            Game.SpriteBatch.Draw(verticalBarTexture, technologyBar, technologyBarColor);
            employmentIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(verticalBarBackgroundTexture, employmentBarFull, Color.White);
            Game.SpriteBatch.Draw(verticalBarTexture, employmentBar, employmentBarColor);
            educationIcon.Draw(gameTime);
            Game.SpriteBatch.Draw(verticalBarBackgroundTexture, educationBarFull, Color.White);
            Game.SpriteBatch.Draw(verticalBarTexture, educationBar, educationBarColor);

#if DEBUG
            Game.SpriteBatch.DrawString(statsFont, system.printStats(), new Vector2(0, 50), Color.Black);
            //Matrix projection = camera.SimProjection * MiResolution.GetTransformationMatrix();
            //Matrix view = camera.SimView;

            //debugView.RenderDebugData(ref projection, ref view);
#endif
        }
    }
}
