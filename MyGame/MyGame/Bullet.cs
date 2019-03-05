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
        double X; // Переменная принимает хначение X координат пули
        public double Xx // свойство передает координату пули Х в класс Game
        {
            get
            {
                return X;
            }
        }
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        Image newImage = Image.FromFile("laser.jpg");
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            X = Pos.X;
            Pos.X = Pos.X + 15;            
        }
        
    }
}
