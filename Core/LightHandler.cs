using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class LightHandler:Entity
    {

        Surface final;

        bool requestClear;
        int reqTimer = 5;
        bool TESTMODE = false;

        //Light mysza = new Light(new Vector2(0, 0), new Vector2(1, 1), Color.Blue);

        struct Light
        {
            public Image usedImage { get; set; }
            public Image s1 { get; }
            public Image s2 { get; }
            public Vector2 pos { get; set; }
            public float scale { get; }
            public Color color { get; }
            public int room { get; }

            public Light(Vector2 npos, float nscale, Color ncolor, int nroom)
            {
                this.pos = npos;
                this.scale = nscale;
                this.color = ncolor;
                this.room = nroom;

                this.s1 = new Image("../../Assets/light_1.png");
                this.s1.Scale = nscale;
                this.s1.SetPosition(npos.X, npos.Y);
                this.s1.CenterOrigin();
                this.s1.Blend = BlendMode.Add;
                this.s1.Color = ncolor;

                this.s2 = new Image("../../Assets/light_1.png");
                this.s2.SetPosition(npos.X, npos.Y);
                this.s2.Scale = nscale + 0.02f;
                this.s2.CenterOrigin();
                this.s2.Blend = BlendMode.Add;
                this.s2.Color = ncolor;


                this.usedImage = s1;
                this.usedImage.SetPosition(npos.X, npos.Y);
                this.usedImage.CenterOrigin();
                this.usedImage.Blend = BlendMode.Add;
                this.usedImage.Color = ncolor;

            }
        };

        Light[] lights = new Light[99];
        int cLights = 0;

        public LightHandler()
        {
            final = new Surface(Game.Instance.Width, Game.Instance.Height, new Color(0.5f, 0.5f, 0.5f));
            final.AutoClear = false;
            final.Blend = BlendMode.Multiply;      
            AddGraphic(final);
            Layer = -111;
            
        }
        public override void UpdateLast()
        {
            base.UpdateLast();
            reqTimer--;
            if (reqTimer <= 0)
            {
                requestClear = true;
                reqTimer = 1;
            }
            if (requestClear)

                DrawLights();

            if (Input.Instance.KeyPressed(Key.E))
            {
                requestClear = true;
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.MouseButtonPressed(MouseButton.Left) && TESTMODE)
            {
                AddLight(Input.Instance.MouseScreenX, Input.Instance.MouseScreenY, 1.2f, Color.Random, GameHandler.pl.playerRoom);
            }


            if (TESTMODE)
            {
                this.Visible = true;
            }else
            {
                this.Visible = false;
            }

            if (Input.KeyPressed(Key.F2))
            {
                if (TESTMODE)
                {
                    TESTMODE = false;
                }
                else
                {
                    TESTMODE = true;
                }
            }

            
          
        }

        public void AddLight(float x, float y, float scale, Color color, int room)
        {
            lights[cLights] = new Light(new Vector2(x,y), scale, color, room);
            lights[cLights].usedImage = lights[cLights].s1;
            final.Draw(lights[cLights].usedImage);
            cLights++;

        }
        void DrawLights()
        {
            final.Clear();
            
            for (int i = 0; i < cLights; i++)
            {
                if (GameHandler.pl.playerRoom == lights[i].room)
                {
                    lights[i].usedImage = ((lights[i].usedImage == lights[i].s1) ? lights[i].s2 : lights[i].s1);
                    final.Draw(lights[i].usedImage);
                }
            }
            requestClear = false;
        }
    }
}
