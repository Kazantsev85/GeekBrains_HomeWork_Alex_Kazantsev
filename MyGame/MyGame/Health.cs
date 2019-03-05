using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class Health: BaseObject // Класс описывающий аптечки
    {
        public Health(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        Image newImage = Image.FromFile("Health.jpg");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Regeneration();
        }
        public override void Regeneration() // регенерация аптечки
        {
            var rnd = new Random();
            int r = rnd.Next(5, 50);

            Pos.X = Game.Width;
            Pos.Y = rnd.Next(0, Game.Height);
            Dir.X = -r / 5;
            Dir.Y = r;
            Size.Width = 40;
            Size.Height = 40;

            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);

        }
    }
}
