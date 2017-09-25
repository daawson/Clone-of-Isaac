using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Bomb:Entity
    {
        #region FIELDS
        public int TIMER
        {
            get; set;
        } = 75;

        bool forceRemove = false;
        int bombRoom;
        #endregion

        #region METHODS
        /// <summary>
        /// Tworzy bombę.
        /// </summary>
        /// <param name="x">Pozycja X</param>
        /// <param name="y">Pozycja Y</param>
        /// <param name="rum">Pokój w którym ma być stworzona</param>
        public Bomb(int x, int y, int rum) : base(x, y)
        {
            Image img = new Image("../../Assets/bomb.png");

            // cień
            Image shadow = Image.CreateCircle(50, new Color(0, 0, 0, 0.2f));
            shadow.ScaledWidth = 50;
            shadow.ScaledHeight = 10;
            shadow.Y += 24;
            shadow.CenterOrigin();

            img.CenterOrigin();

            this.bombRoom = rum;

            AddGraphic(shadow);
            AddGraphic(img);
        }

        public override void Removed()
        {
            base.Removed();
            if (forceRemove) { } // jeśli gracz zmieni pokój zanim bomba wybuchnie to nic sie nie stanie.
            else {
                GameHandler.gameScene.Add(new Particle(X, Y, "../../Assets/8f.png", 64, 64)
                {
                    Layer = -106,
                    FrameCount = 8,
                    LifeSpan = 50f,
                    LockScaleRatio = true,
                    ScaleX = 3f

                }); // dodaje fx wybuchu
                SoundHandler.bombEx.Play();
            }
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Update()
        {
            base.Update();
            TIMER--;

            if(bombRoom != GameHandler.pl.playerRoom) // nic sie nie dzieje
            {
                RemoveSelf();
                forceRemove = true;
            }
            if (TIMER == 0) // boom
            {
                GameHandler.gameScene.Add(new Explosion(this.X, this.Y));
                RemoveSelf();
            }
        }
        #endregion
    }
}
