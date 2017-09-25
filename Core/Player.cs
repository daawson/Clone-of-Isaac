using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Player : Entity
    {
        #region FIELDS
        public static Axis controlAxis { get; set; }
        public static int centrumKontroli { get; set; } = 0;

        public PHealth HEALTHCOMP { get; set; }
        enum pAnim
        {
            down, top, left, right, idle
        }

        public enum pShoot
        {
            down, top, left, right, idle
        }

        public bool canUseDoors { get; set; } = true;
        int doorTimer = 0;
        int doorTimerMax = 40;

        public int playerRoom { get; set; } = 1;

        Spritemap<pAnim> pAssets = new Spritemap<pAnim>("../../Assets/main_test.png", 80, 80);
        public Spritemap<pShoot> pSAssets { get; set; } = new Spritemap<pShoot>("../../Assets/head_test.png", 80, 80);
        public bool isMoving { get; set; } = false;
        #endregion

        #region METHODS
        public override void Render()
        {
            base.Render();
            if(GameHandler.DEBUGMODE)
                Collider.Render();
        }

        /// <summary>
        /// tworzy gracza w centrum ekranu
        /// </summary>
        public Player():base(Game.Instance.HalfWidth, Game.Instance.HalfHeight)
        {
            int STARTX = Game.Instance.HalfWidth - 1280 / 2;
            int STARTY = Game.Instance.HalfHeight - 720 / 2;

            Image shadow = Image.CreateCircle(20, new Color(0, 0, 0, 0.9f));
            shadow.ScaledWidth = 50;
            shadow.ScaledHeight = 20;
            shadow.Y = 30;
            shadow.X = -25;
            AddGraphic(shadow);

            initSpriteMap();
            SetPosition(Game.Instance.HalfWidth, Game.Instance.HalfHeight);
            BoxCollider pCollider = new BoxCollider(80, 80, GameHandler.kolider.gracz);
            pCollider.CenterOrigin();
            AddCollider(pCollider);
            controlAxis = Axis.CreateWASD();
            HEALTHCOMP = new PHealth(this);
            AddComponents(
                controlAxis,
                new PMovement(this),
                new PShooting(this),
                HEALTHCOMP
                );

            Console.WriteLine("Gracz załadowany!");
            AddGraphic(pAssets);
            AddGraphic(pSAssets);


            pAssets.CenterOrigin();
            pSAssets.CenterOrigin();

            pAssets.Smooth = true;
            pSAssets.Smooth = true;

            pAssets.Scale = 1.1f;
            pSAssets.Scale = 1.1f;
            Layer = -104;
            
        }

        void initSpriteMap()
        {
            pAssets.Add(pAnim.top, "0,1,2", "8");
            pAssets.Add(pAnim.left, "3,4,5", "8");
            pAssets.Add(pAnim.down, "6,7,8", "8");
            pAssets.Add(pAnim.right, "9,10,11", "8");
            pAssets.Add(pAnim.idle, "0", "4");
            Console.WriteLine("SpriteMap CHODZENIA załadoway!");
            pAssets.Play(pAnim.idle);

            pSAssets.Add(pShoot.down, "0,1,0", "8").NoRepeat();
            pSAssets.Add(pShoot.top, "4,5,4", "8").NoRepeat();
            pSAssets.Add(pShoot.left, "2,3,2", "8").NoRepeat();
            pSAssets.Add(pShoot.right, "6,7,6", "8").NoRepeat();
            pSAssets.Add(pShoot.idle, "0", "1");
            pSAssets.Play(pShoot.idle);
            Console.WriteLine("SpriteMap STRZELANIA załadoway!");
        }
        public override void Update()
        {
            base.Update();

            if (canUseDoors == false)
            {
                doorTimer--;
                if (doorTimer <= 0)
                {
                    canUseDoors = true;
                    doorTimer = doorTimerMax;
                }
            }


            if (Input.KeyPressed(Key.W))
            {
                pAssets.Play(pAnim.top);
                centrumKontroli = 1;
                //pSAssets.Play(pShoot.top);
            }

            if (Input.KeyPressed(Key.E))
            {
                GameHandler.gameScene.Add(new Bomb((int)X, (int)Y, playerRoom));
            }

            else if (Input.KeyPressed(Key.S))
            {
                pAssets.Play(pAnim.down);
                centrumKontroli = 2;
                //pSAssets.Play(pShoot.down);
            }

            else if(Input.KeyPressed(Key.A))
            {
                pAssets.Play(pAnim.left);
                centrumKontroli = 3;
                //pSAssets.Play(pShoot.left);
            }

            else if (Input.KeyPressed(Key.D))
            {
                pAssets.Play(pAnim.right);
                centrumKontroli = 4;
            }

            else if (Input.KeyReleased(Key.Any))
            {
                pAssets.Play(pAnim.idle);
                centrumKontroli = 0;
            }

            if (Input.KeyDown(Key.Any))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
                centrumKontroli = 0;
            }
            
        }
        public Axis GetAxis()
        {
            Console.WriteLine("Oś załadowana!");
            return controlAxis;
            
        }

        #endregion

    }

    class PMovement : BasicMovement
    {
        public PMovement(Player p) : base(450 , 450, 150)
        {
            Axis = p.GetAxis();
            Collider = p.Collider;
            AddCollision(GameHandler.kolider.granica);
            AddCollision(GameHandler.kolider.kamien);
            OnMove += new Action(czekMuw);
        }
        void czekMuw()
        {
            //Console.WriteLine("MOVE");
        }
    }
}
