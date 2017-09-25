using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    
    class Room:Entity
    {
        #region ROOM INFOS
        public bool isCleared
        {
            get; set;
        } = true;

        public bool isStarted
        {
            get; set;
        } = false;

        public bool isEmpty
        {
            get; set;
        } = false;

        int[,] level;

        public int roomID { get; set; }

        public Door[] doors = new Door[4];

        int cH = 0;
        public Entity[] obstacles = new Entity[30];

        #endregion

        #region OTHER FIELDS
        int STARTX;
        int STARTY;
        #endregion

        #region MAP EDGE FIELDS / COLLIDERS
        public static BoxCollider topC { get; set; }
        public static BoxCollider downC { get; set; }
        public static BoxCollider leftC { get; set; }
        public static BoxCollider rightC { get; set; }
        Image bg;
        #endregion

        #region TILEMAP FORMAT
        public static Rectangle topLeft = new Rectangle(0, 0, 80, 80);
        public static Rectangle top = new Rectangle(80, 0, 80, 80);
        public static Rectangle topRight = new Rectangle(160, 0, 80, 80);

        public static Rectangle left = new Rectangle(0, 80, 80, 80);
        public static Rectangle empty = new Rectangle(80, 80, 80, 80);
        public static Rectangle right = new Rectangle(160, 80, 80, 80);

        public static Rectangle bottomLeft = new Rectangle(0, 160, 80, 80);
        public static Rectangle bottom = new Rectangle(80, 160, 80, 80);
        public static Rectangle bottomRight = new Rectangle(160, 160, 80, 80);
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Główna funkcja startująca pokój
        /// </summary>
        public Room()
        {
            // przesuniecie
            STARTX = Game.Instance.HalfWidth - 1280 / 2;
            STARTY = Game.Instance.HalfHeight - 720 / 2;

            // pozycjonowanie koliderów
            topC = new BoxCollider(1280, 80, GameHandler.kolider.granica);
            downC = new BoxCollider(1280, 80, GameHandler.kolider.granica);
            topC.SetPosition(STARTX, STARTY - 63);
            downC.SetPosition(STARTX, STARTY + 634);
            // ciag dalszy
            leftC = new BoxCollider(80, 720, GameHandler.kolider.granica);
            rightC = new BoxCollider(80, 720, GameHandler.kolider.granica);
            leftC.SetPosition(STARTX - 15, STARTY);
            rightC.SetPosition(STARTX + 1212, STARTY);
        }



        /// <summary>
        /// GENERUJE DRZWI 
        /// </summary>
        /// <param name="u">Cel drzwi górnych</param>
        /// <param name="d">Cel drzwi lewych</param>
        /// <param name="t">Cel drzwi dolnych</param>
        /// <param name="q">Cel drzwi prawych</param>
        public void GenerateDoors(Room u, Room d, Room t, Room q)
        {
            doors[0] = new Door(0, this,u);
            doors[1] = new Door(1, this,d);
            doors[2] = new Door(2, this,t);
            doors[3] = new Door(3, this,q);
            GameHandler.gameScene.Add(doors[0]);
            GameHandler.gameScene.Add(doors[1]);
            GameHandler.gameScene.Add(doors[2]);
            GameHandler.gameScene.Add(doors[3]);

            if (isStarted)
            {
                SwitchRoom(true);
            }
            else
            {
                SwitchRoom(false);
            }
        }
       
        /// <summary>
        /// POKAZ/SCHOWAJ pokój
        /// </summary>
        /// <param name="show">false/true pokaż pokój</param>
        public void SwitchRoom(bool show)
        {
            // ukrywanie rzeczy po zmianie poziomu
            if (!show)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.doors[i].Paused();
                    this.doors[i].Visible = false;
                }
                if (cH > 0)
                {
                    for (int i = 0; i < cH; i++)
                    {
                        this.obstacles[i].Paused();
                        this.obstacles[i].Visible = false;
                        this.obstacles[i].Collidable = false;
                    }
                }
                this.Visible = false;
                this.bg.Visible = false;
                this.Paused();
            }
            // pokazywanie poziomu
            else if (show)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.doors[i].Resumed();
                    this.doors[i].Visible = true;
                }
                if (cH > 0)
                {
                    for (int i = 0; i < cH; i++)
                    {
                        this.obstacles[i].Resumed();
                        this.obstacles[i].Visible = true;
                        this.obstacles[i].Collidable = true;
                    }
                }
                this.Visible = true;
                this.bg.Visible = true;
                this.Resumed();
            }

            if (!GameHandler.FirstStart)
                GameHandler.pl.canUseDoors = false;
        }

        /// <summary>
        /// Generowanie poziomu
        /// </summary>
        /// <param name="lvl">ArrayINT z poziomem</param>
        public void GenerateLevel(int[,] lvl)
        {
            this.level = lvl;
            
            // losowanie tła/podlogi
            int randomki = Rand.Int(1, 9);
            if(randomki == 1 || randomki == 2 || randomki ==3)
            {
                bg = new Image("../../Assets/bg_t1.png");
            }
            else if (randomki == 4 || randomki == 5 || randomki == 6)
            {
                bg = new Image("../../Assets/bg_t2.png");
            }
            else if (randomki == 7 || randomki == 8 || randomki == 9)
            {
                bg = new Image("../../Assets/bg_t3.png");
            }
            bg.ScaledHeight = 560;
            bg.ScaledWidth = 1120;
            bg.SetPosition(STARTX + 80, STARTY + 80);
            AddGraphic(bg);

            //// dodawanie koliderów krawedzi mapy
            AddColliders(
                topC, 
                downC,
                leftC,
                rightC
                );

            // rysowanie obramówki mapy
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Image tile = new Image("../../Assets/tilemap_2.png");
                    if (level[x, y] == 0)
                    {

                        //tile.ClippingRegion = empty;
                        tile.TextureRegion = empty;

                    }

                    if (level[x, y] == 1)
                    {

                        //tile.ClippingRegion = topLeft;
                        tile.TextureRegion = topLeft;

                    }

                    if (level[x, y] == 2)
                    {

                        //tile.ClippingRegion = top;
                        tile.TextureRegion = top;

                    }

                    if (level[x, y] == 3)
                    {

                        //tile.ClippingRegion = topRight;
                        tile.TextureRegion = topRight;

                    }
                    if (level[x, y] == 4)
                    {

                        //tile.ClippingRegion = right;
                        tile.TextureRegion = right;
                    }
                    if (level[x, y] == 5)
                    {

                        //tile.ClippingRegion = bottomRight;
                        tile.TextureRegion = bottomRight;
                    }
                    if (level[x, y] == 6)
                    {

                        //tile.ClippingRegion = bottom;
                        tile.TextureRegion = bottom;
                    }
                    if (level[x, y] == 7)
                    {

                        //tile.ClippingRegion = bottomLeft;
                        tile.TextureRegion = bottomLeft;
                    }
                    if (level[x, y] == 8)
                    {

                        //tile.ClippingRegion = left;
                        tile.TextureRegion = left;


                    }
                    if (level[x, y] == 60)
                    {

                        //tile.ClippingRegion = left;
                        tile.TextureRegion = empty;
                        AddObstacle(new Stone(STARTX + y * 80, STARTY + x * 80, this));

                    }

                    //tile.AtlasRegion();
                    tile.Relative = false;
                    tile.ClippingRegion = topLeft;
                    tile.SetPosition(STARTX + y * 80, STARTY + x * 80);
                    tile.Smooth = true;
                    AddGraphic(tile);
                }
            }
        }
        /// <summary>
        /// Dodaje nowa przeszkode do poziomu
        /// </summary>
        /// <param name="e">Entity obiektu</param>
        public void AddObstacle(Entity e)
        {
            obstacles[cH] = e;
            GameHandler.gameScene.Add(obstacles[cH]);
            cH++;
        }
        #endregion

        // renderowowanie koliderow /debug
        public override void Render()
        {
            base.Render();
            if (GameHandler.DEBUGMODE)
            {
                topC.Render();
                leftC.Render();
                downC.Render();
                rightC.Render();
            }
        }

    }
}
