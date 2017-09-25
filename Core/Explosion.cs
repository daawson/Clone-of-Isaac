using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Explosion:Entity
    {
        public Explosion(float x, float y):base(x, y)
        {
            //AddGraphics(Image.CreateRectangle());
            AddCollider(new CircleCollider(160, GameHandler.kolider.wybuch));
            Collider.CenterOrigin();
            LifeSpan = 20f;
            
        }
        public override void Render()
        {
            base.Render();
            if (GameHandler.DEBUGMODE && Collider != null)
                Collider.Render();
        }
        public override void Update()
        {
            base.Update();

            if (Overlap(this.X, this.Y, GameHandler.kolider.gracz))
            {
                GameHandler.pl.HEALTHCOMP.Damaged(50);
            }
        }
    }
}
