using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace MyGame
{
    //public delegate void Log();
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }
                       
        static Game()
        {
        }
        
        public static Bullet bullet;
        private static Moon moon;
        private static Planet planet;
        public static Ship ship;
        private static Health health;
        

        private static Timer timer = new Timer { Interval = 100 }; // таймер вынесен из метода Init        

        public static void Init (Form form)
        {
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
            if (bullet == null && e.KeyCode == Keys.ControlKey) bullet = new Bullet(new Point(ship.Rect.X+70, ship.Rect.Y), new Point(5, 0), new Size(4, 1));
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
            bullet?.Draw();
            health.Draw();
            ship?.Draw();

            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject objects in asteroids)
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
            bullet?.Update();                      
            health.Update();
            ship?.Update();

            for (var i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] == null) continue;
                asteroids[i].Update();
                if (bullet != null && bullet.Collision(asteroids[i]))
                {
                    ship?.HitsUpdate();                    
                    System.Media.SystemSounds.Hand.Play();
                    asteroids[i].Regeneration();
                    
                    bullet = null;
                    continue;
                }
                if (!ship.Collision(asteroids[i])) continue;                
                ship?.EnergyLow();
                System.Media.SystemSounds.Asterisk.Play();
                asteroids[i].Regeneration(); // регенерирует новый астероид после столкновения с кораблем
                if (ship.Energy <= 0) ship?.Die();
            }
            if (health!=null && ship.Collision(health) && ship.Energy < 3)
            {
                ship?.EnergyUp();
                health.Regeneration();
            }
        }

        public static BaseObject[] _objs;
        private static Asteroid[] asteroids;

        public static void Load()
        {
            _objs = new BaseObject[30];
            
            planet = new Planet(new Point(600, 40), new Point(-1, 0), new Size(20, 20));
            moon = new Moon(new Point(600, 100), new Point(-1, 0), new Size(40, 40));         
            ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(70, 40));            

            asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(15, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                asteroids[i] = new Asteroid(new Point(1000,rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
            int rd = rnd.Next(5, 50);
            health = new Health(new Point(1000, rnd.Next(0,Game.Height)), new Point(-rd/5, rd), new Size(40, 40));
        }
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

    }
}
