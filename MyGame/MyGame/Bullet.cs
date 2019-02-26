using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class Bullet: BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 3;            
            if (Pos.X > Game.Width) Pos.X = 0;
        }
        public override void Regeneration() // регенерация снаряда, вызывается после пересечения с астероидом 
        {
            Pos.X = 0;
        }
    }
}
