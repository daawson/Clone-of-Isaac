using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Core
{
    class GUITest
    {
        public GUITest()
        {
            GameHandler.gameScene.Add(new GUI());
        }
    }
    class GUI : Entity
    {
        public Text FPS { get; set; }
        public Text Memorki { get; set; }
        public Text Rum { get; set; }

        public Process currentProces { get; set; }
        public GUI()
        {

            currentProces = Process.GetCurrentProcess();
            
            FPS = new Text("DataInit", 24);
            FPS.SetPosition(5, 10);
            FPS.Color = Color.White;

            Memorki = new Text("DataInit", 24);
            Memorki.SetPosition(5, 36);
            Memorki.Color = Color.White;

            Rum = new Text("DataInit", 24);
            Rum.SetPosition(5, 52);
            Rum.Color = Color.White;

            Console.WriteLine("GUITest załadowane!");

            AddGraphics(FPS, Memorki, Rum);

            Layer = -102;
        }

        public override void Update()
        {
            base.Update();
            UpdateText();
            if (Input.Instance.KeyPressed(Key.F1))
            {
                if (GameHandler.DEBUGMODE)
                {
                    GameHandler.DEBUGMODE = false;
                }
                else
                {
                    GameHandler.DEBUGMODE = true;
                }
            }

            if (GameHandler.DEBUGMODE)
            {
                FPS.Visible = true;
                Memorki.Visible = true;
                Rum.Visible = true;
            }
            else
            {
                FPS.Visible = false;
                Memorki.Visible = false;
                Rum.Visible = false;
            }
        }
        void UpdateText()
        {
            FPS.String = Game.Framerate.ToString() + " FPS";
            Memorki.String = GC.GetTotalMemory(true).ToString() + "Memory usage";
            Rum.String = GameHandler.pl.playerRoom + " Room";
        }

    }
}
