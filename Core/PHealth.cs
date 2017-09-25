using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PHealth:Component
    {
        Player pl;
        float hapsy = 50;
        float mhapsy = 100;
        bool reqChange = false;
        int DAMAGETIMER = 0;
        Image bg = new Image("../../Assets/health_bar.png");
        Image hp = Image.CreateRectangle(225, 31, Color.Red);
        Entity hpHolder = new Entity();
        public PHealth(Player p)
        {
            this.pl = p;
            GameHandler.gameScene.Add(hpHolder);
            hpHolder.AddGraphic(bg);
            hp.SetPosition(80, 24);
            hpHolder.AddGraphic(hp);
            hpHolder.Layer = -112;
            UpdateHP();
        }
        public override void Update()
        {
            if (GameHandler.DEBUGMODE)
            {

                hpHolder.Visible = false;
            }
            else
            {
                hpHolder.Visible = true;
            }

            DAMAGETIMER--;
            /*
            if (Input.Instance.KeyPressed(Key.R))
            {
                Damaged(25);
            }
            if (Input.Instance.KeyPressed(Key.T))
            {
                Healed(25);
            }
            */
            if (DAMAGETIMER > 0)
            {
                pl.Graphic.Color = Color.Red;
            }
            else
            {
                pl.Graphic.Color = new Color(0,0,0,0.3f);
            }
            if (reqChange)
            {
                UpdateHP();
                reqChange = false;
            }
        }
        public void UpdateHP()
        {
            float width = (hapsy / mhapsy) * 225;
            hp.ScaledWidth = width;
        }
        /// <summary>
        /// Funkcja lecząca
        /// </summary>
        /// <param name="hp">ilość HP do dodania</param>
        public void Healed(int hp)
        {
            if(hapsy+hp > 100)
            {
                hapsy = 100;
            }
            else
            {
                hapsy += hp;
            }
            reqChange = true;
        }

        /// <summary>
        /// Funkcja zadająca obrazenia
        /// </summary>
        /// <param name="hp">Ilość HP do zadania</param>
        public void Damaged(int hp)
        {
            if (DAMAGETIMER <= 0)
            {
                if (hapsy - hp > 0)
                {
                    hapsy -= hp;
                }
                else
                {
                    hapsy = 100;
                    Console.WriteLine("DEAD");
                }
                DAMAGETIMER = 100;
                reqChange = true;
            }
        }
    }
}
