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
        public static event Action<string> CreateAsteroid; //Событие оповещает о создании астероида
        public static event Action<string> RegenerateAsteroid; //Событие оповещает о обновлении астероида
        
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            CreateAsteroid?.Invoke($"{DateTime.Now}: Астероид создан."); //Вызов события создания астероида в конструкторе
            Power = 1;
        }      
        
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {            

               Pos.X = Pos.X + Dir.X;

               if (Pos.X < 0)
                {
                Regeneration();                
                }
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
            RegenerateAsteroid?.Invoke($"{DateTime.Now}: Астероид обновлен.");
        }
        public void AsteroidShootedDown()
        /// метод создан для разделения записи в журнале. Поскольку метод Regeneration используется для обновления астероидов,
        /// как после ухода за экран и уничтожения так и после столкновения с кораблем
        {
            Regeneration();
            RegenerateAsteroid?.Invoke($"{DateTime.Now}: Астероид уничтожен."); // Вызов события уничтожения астероида
        }
        public void ShipShootedDown()
        /// метод создан для разделения записи в журнале. Поскольку метод Regeneration используется для обновления астероидов,
        /// как после ухода за экран и уничтожения так и после столкновения с кораблем 
        {
            Regeneration();
            RegenerateAsteroid?.Invoke($"{DateTime.Now}: Корабль уничтожен."); // Вызов события уничтожения корабля
        }


    }
}
