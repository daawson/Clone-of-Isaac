using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class SoundHandler
    {
        public static Sound bulletShot { get; private set; } = new Sound("../../Assets/shot_start.wav");
        public static Sound bulletHit { get; private set; } = new Sound("../../Assets/shot_hit.wav");
        public static Sound bombEx { get; private set; } = new Sound("../../Assets/bomb.wav");
        public static Sound teleportSound { get; private set; } = new Sound("../../Assets/teleport.wav");

        public static Music mainTrack1 { get; private set; } = new Music("../../Assets/soundtrack_1.wav");
        public static Music mainTrack2 { get; private set; } = new Music("../../Assets/st_2.wav");
        public static Music mainTrack3 { get; private set; } = new Music("../../Assets/st_3.wav");
        //public static Sound bulletShot { get; private set; } = new Sound("../../Assets/shot.wav");

        public SoundHandler()
        {
            int randomek = Rand.Int(1, 3);
            if(randomek == 1)
            {
                mainTrack1.Play();
              
                 
            }
            if (randomek == 2)
            {
                mainTrack2.Play();

            }
            if (randomek == 3)
            {
                mainTrack3.Play();

            }

            bulletShot.Volume = 0.1f;
            bulletHit.Volume = 0.2f;
            bombEx.Volume = 0.3f;
            //teleportSound.Volume = 0.1f;
            Sound.GlobalVolume = 0.5f;
            Music.GlobalVolume = 0.6f;
        }
    }
}
