using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Door : Entity
    {
        int STARTX, STARTY;

        Room targetRoom;
        Room currentRoom;
        int roomDir;

        #region TILEFORMAT
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


        public override void Render()
        {
            base.Render();
            if (Collider != null && GameHandler.DEBUGMODE)
                Collider.Render();
        }

        public Door(int dir, Room _cur, Room _tar)
        {
            STARTX = Game.Instance.HalfWidth - 1280 / 2;
            STARTY = Game.Instance.HalfHeight - 720 / 2;
            Layer = -101;
            targetRoom = _tar;
            currentRoom = _cur;
            roomDir = dir;

            
            
            if (_tar.isEmpty) { Image.CreateRectangle(); }
            else {
                if (dir == 0)
                {
                    Image tile = new Image("../../Assets/teleport_0.png");
                    Image shadow = Image.CreateCircle(80, new Color(0, 0, 0, 0.2f));
                    shadow.ScaledHeight = 10;
                    shadow.ScaledWidth = 80;
                    tile.ScaledHeight = 80;
                    tile.ScaledWidth = 80;
                    //tile.CenterOrigin();
                    //tile.TextureRegion = top;
                    //tile.ClippingRegion = topLeft;
                   
                    tile.SetPosition(STARTX + 7 * 80 + 40, STARTY + 0 * 80);
                    shadow.SetPosition(tile.X, tile.Y + 80);
                    AddCollider(new BoxCollider(40, 80, GameHandler.kolider.drzwi));
                    Collider.SetPosition(STARTX + 7 * 80 + 60, STARTY + 0 * 80-60);
                    tile.Smooth = true;
                    shadow.Smooth = true;
                    AddGraphic(shadow);
                    AddGraphic(tile);
                }
                else if (dir == 1)
                {
                    Image tile = new Image("../../Assets/teleport_0.png");
                    Image shadow = Image.CreateCircle(80, new Color(0, 0, 0, 0.2f));
                    shadow.ScaledHeight = 80;
                    shadow.ScaledWidth = 10;
                    tile.ScaledHeight = 80;
                    tile.ScaledWidth = 80;
                    tile.Angle = 90;
                    //tile.CenterOriginZero();
                    //tile.CenterOrigin();
                    //tile.TextureRegion = left;
                    //tile.ClippingRegion = topLeft;
                    tile.SetPosition(STARTX + 0 * 80, STARTY + 5 * 80);
                    shadow.SetPosition(tile.X+80, tile.Y-80);
                    AddCollider(new BoxCollider(81, 40, GameHandler.kolider.drzwi));
                    Collider.SetPosition(STARTX + 0 * 80, STARTY + 4 * 80+20);
                    tile.Smooth = true;
                    shadow.Smooth = true;
                    AddGraphic(shadow);
                    AddGraphic(tile);
                }
                else if (dir == 2)
                {
                    Image tile = new Image("../../Assets/teleport_0.png");
                    tile.ScaledHeight = 80;
                    tile.ScaledWidth = 80;
                    tile.Angle = 180;
                    //tile.CenterOrigin();
                    //tile.TextureRegion = bottom;
                    //tile.ClippingRegion = topLeft;
                    tile.SetPosition(STARTX + 8 * 80 + 40, STARTY + 9 * 80);
                    AddCollider(new BoxCollider(40, 80, GameHandler.kolider.drzwi));
                    Collider.SetPosition(STARTX + 7 * 80 + 60, STARTY + 8 * 80-10);
                    tile.Smooth = true;
                    AddGraphic(tile);
                }
                else if (dir == 3)
                {
                    Image tile = new Image("../../Assets/teleport_0.png");
                    Image shadow = Image.CreateCircle(80, new Color(0, 0, 0, 0.2f));
                    shadow.ScaledHeight = 80;
                    shadow.ScaledWidth = 10;
                    tile.ScaledHeight = 80;
                    tile.ScaledWidth = 80;
                    tile.Angle = -90;
                    //tile.CenterOriginZero();
                    //tile.CenterOrigin();
                    //tile.TextureRegion = right;
                    //tile.ClippingRegion = topLeft;
                    tile.SetPosition(STARTX + 16 * 80, STARTY + 4 * 80);
                    shadow.SetPosition(tile.X-90, tile.Y);
                    AddCollider(new BoxCollider(80, 40, GameHandler.kolider.drzwi));
                    Collider.SetPosition(STARTX + 15 * 80, STARTY + 4 * 80+20);
                    tile.Smooth = true;
                    shadow.Smooth = true;
                    AddGraphic(shadow);
                    AddGraphic(tile);
                }
            }
        }
        public override void Update()
        {
            base.Update();
            if (Collider != null)
            {
                if (Overlap(this.X, this.Y, GameHandler.pl) && GameHandler.pl.canUseDoors && currentRoom.roomID == GameHandler.pl.playerRoom)
                {
                    if (!currentRoom.isCleared) { }
                    else
                    {
                        Player pl = GameHandler.pl;
                        if (roomDir == 0 && Input.Instance.KeyDown(Key.W))
                        {
                            //Console.WriteLine(targetRoom.doors[2].X + " " + targetRoom.doors[2].Y);
                            currentRoom.SwitchRoom(false);
                            targetRoom.SwitchRoom(true);
                            pl.X = targetRoom.doors[2].Collider.X+20;
                            pl.Y = targetRoom.doors[2].Collider.Y - 81;
                            pl.playerRoom = targetRoom.roomID;
                            SoundHandler.teleportSound.Play();
                        }

                        else if(roomDir == 1 && Input.Instance.KeyDown(Key.A))
                        {
                            //Console.WriteLine(targetRoom.doors[2].X + " " + targetRoom.doors[2].Y);
                            currentRoom.SwitchRoom(false);
                            targetRoom.SwitchRoom(true);
                            pl.X = targetRoom.doors[3].Collider.X - 41;
                            pl.Y = targetRoom.doors[3].Collider.Y + 20;
                            pl.playerRoom = targetRoom.roomID;
                            SoundHandler.teleportSound.Play();
                        }

                        else if(roomDir == 2 && Input.Instance.KeyDown(Key.S))
                        {
                            //Console.WriteLine(targetRoom.doors[2].X + " " + targetRoom.doors[2].Y);
                            currentRoom.SwitchRoom(false);
                            targetRoom.SwitchRoom(true);
                            pl.X = targetRoom.doors[0].Collider.X + 20;
                            pl.Y = targetRoom.doors[0].Collider.Y + 160;
                            pl.playerRoom = targetRoom.roomID;
                            SoundHandler.teleportSound.Play();
                        }

                        else if (roomDir == 3 && Input.Instance.KeyDown(Key.D))
                        {
                            //Console.WriteLine(targetRoom.doors[2].X + " " + targetRoom.doors[2].Y);
                            currentRoom.SwitchRoom(false);
                            targetRoom.SwitchRoom(true);
                            pl.X = targetRoom.doors[1].Collider.X + 121;
                            pl.Y = targetRoom.doors[1].Collider.Y + 20;
                            pl.playerRoom = targetRoom.roomID;
                            SoundHandler.teleportSound.Play();
                        }
                        
                    }
                }
            }
        }
    }
}
