using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Core
{
    class GameHandler
    {
        #region PUBLCI FIELDS
        public static Scene gameScene { get; set; }
        public static Player pl { get; set; }
        public static LightHandler lightH { get; set; }
        public static SoundHandler soundH { get; set; }
        public static bool BULLETSHOT { get; set; }
        public static bool DEBUGMODE { get; set; } = false;

        public static bool FirstStart { get; set; } = true;
        
        public enum kolider
        {
            granica,gracz,drzwi, kamien, wybuch
        }

        #endregion

        #region METHODS
        public Scene StartScene()
        {
            gameScene = new Scene();
            gameScene.UseCameraBounds = true;
            Init();
            return gameScene;
        }
        void Init()
        {
            new Dungeon();

            lightH = gameScene.Add(new LightHandler());
            soundH = new SoundHandler();

            pl = gameScene.Add(new Player());
            new GUITest();
            
        }
        #endregion
    }
}
