using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace MyGame
{   
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int AstLength = 3; // определяет изначальное количество астероидов в коллекции
        public static int Width { get; set; }
        public static int Height { get; set; }        

        static Game()
        {
        }

        
        public static List<Bullet> bullets = new List<Bullet>();// cоздаем коллекцию пуль
        
        public static List<Asteroid> asteroidsList = new List<Asteroid>(); // создаем коллекцию астероидов
        

        private static Moon moon;
        private static Planet planet;
        public static Ship ship;
        private static Health health;
        

        private static Timer timer = new Timer { Interval = 100 }; // таймер вынесен из метода Init             

        public static void Init (Form form)
        {
            Asteroid.CreateAsteroid += s => Console.WriteLine(s); 
            //Asteroid.AsteroodCollision += s => Console.WriteLine(s);
            Asteroid.RegenerateAsteroid += s => Console.WriteLine(s);
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            
            if (Width < 0 || Width > 1000) // параметры исключения
            {
                Exeption();                
            }
            else if (Height < 0 || Height > 1000)
            {
                Exeption();                
            }

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish; //подписка на событие гибели корабля            
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {            
            // добавляем новую пулю в коллекцию при выстреле
            if (e.KeyCode == Keys.ControlKey) bullets.Add ( new Bullet(new Point(ship.Rect.X+70, ship.Rect.Y), new Point(5, 0), new Size(10, 3)));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
        }
        private static void Exeption() // метод обработки исключения
        {
            throw new ArgumentOutOfRangeException("Dimentions is out of Range");            
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {               
            Buffer.Graphics.Clear(Color.Black);

            moon.Draw();
            planet.Draw();           
            health.Draw();
            ship?.Draw();
            foreach (Bullet b in bullets)
                b.Draw();
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject objects in asteroidsList)
                objects?.Draw();         
            
            if (ship != null)
                Buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("Hits:" + ship.Hits, SystemFonts.DefaultFont, Brushes.White, 100, 0);
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();            
            
            moon.Update();            
            planet.Update();                                
            health.Update();
            ship?.Update();

            foreach (BaseObject objects in asteroidsList)
                objects?.Update();
            foreach (Bullet b in bullets)            
                b.Update();
            
            for (var i = 0; i < asteroidsList.Count; i++)
            {                
                if (asteroidsList[i] == null) continue;
                asteroidsList[i].Update();

                if (ship.Collision(asteroidsList[i])) // столкновение корабля с астероидом
                {
                    ship?.EnergyLow();
                    System.Media.SystemSounds.Asterisk.Play();
                    asteroidsList[i].ShipShootedDown(); // регенерирует новый астероид после столкновения с кораблем и уменьшает количество энергии корабля
                    if (ship.Energy <= 0) ship?.Die();
                }

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (bullets[j].Collision(asteroidsList[i]))
                    {
                        
                        
                        ship?.HitsUpdate();
                        System.Media.SystemSounds.Hand.Play();
                        asteroidsList[i].AsteroidShootedDown();// увеличивает счетчик сбитых астероидов

                        asteroidsList.RemoveAt(i);//удаляет сбитый астероид из коллекции
                        i--;

                        if (asteroidsList.Count == 0) //Если все астероиды сбиты, увеличивает число астероидов на 1 и заново заполняет коллекцию астероидов
                        {
                            AstLength++;
                            AsteroidLoad(AstLength);
                        }

                        bullets.RemoveAt(j);
                        j--;

                        continue;
                    }
                    if (bullets[j].Xx > Width) // Получает координату Х пули и удаляет ее из коллекции если она покидает экран.
                    {
                        bullets.RemoveAt(j);
                        j--;
                        continue;
                    }
                }                    
            }

            if (health!=null && ship.Collision(health) && ship.Energy < 3)
            {
                ship?.EnergyUp();
                health.Regeneration();
            }           
        }

        public static BaseObject[] _objs;
        //private static Asteroid[] asteroids;

        public static void Load()
        {
            _objs = new BaseObject[30];
            
            planet = new Planet(new Point(600, 40), new Point(-1, 0), new Size(20, 20));
            moon = new Moon(new Point(600, 100), new Point(-1, 0), new Size(40, 40));         
            ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(70, 40));

            
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(15, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            // Загрузка астероидов реализована в отдельном методе AsteroidLoad.
            AsteroidLoad(AstLength);
            int rd = rnd.Next(5, 50);
            health = new Health(new Point(1000, rnd.Next(0,Game.Height)), new Point(-rd/5, rd), new Size(40, 40));
        }

        public static void AsteroidLoad(int Count)
        {
            var rnd = new Random();
            for (var i = 0; i < Count; i++)
            {
                int r = rnd.Next(5, 50);
                asteroidsList.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r)));
            }
        }

        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

    }
}
