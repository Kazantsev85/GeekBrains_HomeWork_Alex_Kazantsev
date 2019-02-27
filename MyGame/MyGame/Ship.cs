using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class Ship: BaseObject // Класс описывает космический корабль
    {
        private int energy = 3;
        public int Energy => energy;

        private int hits = 0;
        public int Hits => hits;

        public void EnergyLow()
        {
            energy--;
        }
        public void EnergyUp()
        {
            energy++;
        }
        public void HitsUpdate()
        {
            hits++;
        }


        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) // конструктор
        {

        }

        Image newImage = Image.FromFile("Ship.jpg");
        public static event Message MessageDie; // событие для гибели корабля

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X;
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}
