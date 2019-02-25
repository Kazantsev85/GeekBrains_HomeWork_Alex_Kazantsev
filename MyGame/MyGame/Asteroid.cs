using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {            

               Pos.X = Pos.X + Dir.X;

               if (Pos.X < 0) Regeneration();
        }
        public override void Regeneration() // регенерация астероида, вызывается после пересечения со снарядом и поле ухода за границу экрана
        {
            var rnd = new Random();        
            int r = rnd.Next(5, 50);             

            Pos.X = Game.Width;
            Pos.Y = rnd.Next(0, Game.Height);
            Dir.X = -r / 5;
            Dir.Y = r;
            Size.Width = r;
            Size.Height = r;

            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);

        }

    }
}
