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
        Image newImage = Image.FromFile("laser.jpg");
        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawEllipse(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 50;            
        }
        
    }
}
