using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class SplashScreenObjects
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        public SplashScreenObjects(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public virtual void Draw()
        {
            SplashScreen.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public virtual void Update()
        {

            Pos.X = Pos.X + Dir.X;

            if (Pos.X < 0) Pos.X = 400 + Size.Width;
            
        }
    }
}
