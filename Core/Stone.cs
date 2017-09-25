using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Stone:Entity
    {
        Image tex = new Image("../../Assets/stone_2.png");
        Image shadow = Image.CreateCircle(10, new Color(0, 0, 0, 0.3f));
        Room currentRoom;
        public Stone(int x, int y, Room rum):base(x,y)
        {
            currentRoom = rum;
            Layer = -102;
            shadow.Y += 65;
            shadow.X -= 10;
            shadow.ScaledHeight = 20;
            shadow.ScaledWidth = 100;

            AddGraphic(shadow);
            AddGraphic(tex);
            
            AddCollider(new BoxCollider(40, 20, GameHandler.kolider.kamien));
            Collider.X += 20;
        }
        public override void Update()
        {
            base.Update();
            if (Overlap(this.X, this.Y, GameHandler.kolider.wybuch) && currentRoom.roomID == GameHandler.pl.playerRoom)
            {
                //Bomb bomba = (Bomb)Overlapped;
                // if (bomba.TIMER == 0)
                    RemoveSelf();
            }
        }
        public override void Render()
        {
            base.Render();
            if(GameHandler.DEBUGMODE)
                Collider.Render();
        }
    }
}
