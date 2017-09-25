using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Bullet:Entity
    {
        int speed = 7;
        float startY;
        int distance = 400;
        int lifespan = 40;
        Vector2 _target;
        CircleCollider cc;
        Color BulletColor = Color.Blue;
        int room;

        int dir;

        bool forceRemove = false;


        enum bAnim
        {
            inAir, destruct
        }
        Spritemap<bAnim> bAssets = new Spritemap<bAnim>("../../Assets/bullet_test.png", 80, 80);
        Image shadow;

        float deltaX, deltaY;
        float angle, rad;
        
        public Bullet(int dir)
        {
            bAssets.CenterOrigin();
            Layer = -102;
            bAssets.Scale = 0.6f;
            bAssets.Add(bAnim.inAir, "0", "1");
            bAssets.Play(bAnim.inAir);
            this.dir = dir;
            if (dir == 0)
            {
                this._target = new Vector2(GameHandler.pl.X, GameHandler.pl.Y - distance);
                if(Player.centrumKontroli == 3)
                {
                    this._target.X -= 90;
                }
                else if (Player.centrumKontroli == 4)
                {
                    this._target.X += 90;
                }
            }
            else if (dir == 1)
            {
                this._target = new Vector2(GameHandler.pl.X, GameHandler.pl.Y + distance);
                if (Player.centrumKontroli == 3)
                {
                    this._target.X -= 90;
                }
                else if (Player.centrumKontroli == 4)
                {
                    this._target.X += 90;
                }
            }
            else if (dir == 2)
            {
                this._target = new Vector2(GameHandler.pl.X - distance, GameHandler.pl.Y);
                if (Player.centrumKontroli == 1)
                {
                    this._target.Y -= 90;
                }
                else if (Player.centrumKontroli == 2)
                {
                    this._target.Y += 90;
                }
            }
            else if (dir == 3)
            {
                this._target = new Vector2(GameHandler.pl.X + distance, GameHandler.pl.Y);
                if (Player.centrumKontroli == 1)
                {
                    this._target.Y -= 90;
                }
                else if (Player.centrumKontroli == 2)
                {
                    this._target.Y += 90;
                }
            }

            this.startY = this._target.Y;
            SetPosition(GameHandler.pl.X, GameHandler.pl.Y);
            bAssets.Color = BulletColor;
            AddGraphic(bAssets);
            shadow = Image.CreateCircle(10, new Color(0, 0, 0, 0.2f));
            shadow.CenterOrigin();
            shadow.Y = 30;
            AddGraphic(shadow);
            cc = new CircleCollider(5, GameHandler.kolider.gracz);
            AddCollider(cc);
            Collider.CenterOrigin();
            

            LifeSpan = lifespan;

            deltaX = this._target.X - this.X;
            deltaY = this._target.Y - this.Y;

            rad = (float)Math.Atan2(deltaY, deltaX);
            angle = (float)MathHelper.ToDegrees(rad);
        }
        public override void Added()
        {
            this.room = GameHandler.pl.playerRoom;
            base.Added();
            SoundHandler.bulletShot.Play();
        }
        public override void Render()
        {
            base.Render();
            if(GameHandler.DEBUGMODE)
                Collider.Render();
        }
        public override void Removed()
        {
            base.Removed();
            if (!forceRemove) {
                int randomki = Rand.Int(4, 7);
                for (int i = 0; i < randomki; i++)
                {
                    GameHandler.gameScene.Add(new AfterBullet(this.X + (Rand.Int(-2, 2) * 5), this.Y + (Rand.Int(-2, 2) * 5), "../../Assets/particle.png", 240, 240, BulletColor, this.dir));
                    
                }
                SoundHandler.bulletHit.Play();
            }
        }
        public override void Update()
        {
            base.Update();
            this.X = this.X + speed * (float)Math.Cos(rad);
            this.Y = this.Y + speed * (float)Math.Sin(rad);

            if(Overlap(this.X-+speed, this.Y-+speed, GameHandler.kolider.granica) || Overlap(this.X - +speed, this.Y - +speed, GameHandler.kolider.kamien))
            {
                RemoveSelf();
            }

            if(this.room != GameHandler.pl.playerRoom)
            {
                forceRemove = true;
                RemoveSelf();
            }
        }
    }
    class AfterBullet : Particle
    {
        public AfterBullet(float x, float y, string texture, int width, int height, Color c, int _dir) :base(x, y, texture, width, height)
        {
            this.Layer = -112;
            this.Color = c;
            this.Alpha = 0.9f;
            this.FrameCount = 1;
            this.LifeSpan = 20f;
            this.SpeedY = Rand.Float(0.1f,1f);
            this.LockScaleRatio = true;
            this.ScaleX = Rand.Float(0.1f, 0.3f);
            this.FinalScaleX = 0f;
            this.FinalAlpha = 0.1f;

            if(_dir == 2)
            {
                this.SpeedX = -1;
            }
            else if(_dir == 3)
            {
                this.SpeedX = 1;
            }
        }
    }
}
