using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PShooting:Component
    {
        Player p;

        bool isShooting = false;
        bool hasShot = false;
        int shotDir = 0;
        int shotTimer = 0; int shotTimerMax = 25;
        int turnTimer = 0; int turnTimerMax = 60;

        public PShooting(Player pl) : base()
        {
            p = pl;
        }

        public override void Update()
        {
            base.Update();

            shotTimer--;
            if (GameHandler.BULLETSHOT)
            {
                isShooting = true;
                if (shotTimer <= 0)
                {
                    GameHandler.BULLETSHOT = false;
                }
            }

            
            if (GameHandler.BULLETSHOT == false)
            {
                if (Input.Instance.KeyDown(Key.Up))
                {
                    p.pSAssets.Play(Player.pShoot.top);
                    GameHandler.gameScene.Add(new Bullet(0));
                    shotTimer = shotTimerMax;
                    GameHandler.BULLETSHOT = true;
                }
                else if (Input.Instance.KeyDown(Key.Down))
                {
                    p.pSAssets.Play(Player.pShoot.down);
                    GameHandler.gameScene.Add(new Bullet(1));
                    shotTimer = shotTimerMax;
                    GameHandler.BULLETSHOT = true;
                }
                else if (Input.Instance.KeyDown(Key.Left))
                {
                    p.pSAssets.Play(Player.pShoot.left);
                    GameHandler.gameScene.Add(new Bullet(2));
                    shotTimer = shotTimerMax;
                    GameHandler.BULLETSHOT = true;
                }
                else if (Input.Instance.KeyDown(Key.Right))
                {
                    p.pSAssets.Play(Player.pShoot.right);
                    GameHandler.gameScene.Add(new Bullet(3));
                    shotTimer = shotTimerMax;
                    GameHandler.BULLETSHOT = true;
                }
            }
            turnTimer--;
            if (turnTimer <=  0 && shotTimer <= 0)
            {
                turnTimer = turnTimerMax;
                p.pSAssets.Play(Player.pShoot.idle);
            }
        }
    }
}
