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

        public static int Width { get; set; }
        public static int Height { get; set; }
                       
        static Game()
        {
        }
        
        private static Bullet bullet;
        private static Moon moon;
        private static Planet planet;        
        
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
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
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
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject objects in asteroids)
                objects.Draw();
            Moon M = moon ;
            M.Draw();
            Planet P = planet;
            P.Draw();
            Bullet B = bullet;
            B.Draw();

            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            
            Moon M = moon;
            M.Update();
            Planet P = planet;
            P.Update();
            Bullet B = bullet;
            B.Update();

            foreach (BaseObject objects in asteroids)
            {
                objects.Update();
                if (objects.Collision(bullet)) // проверка наличия пересечения астероида со снарядом
                {
                    System.Media.SystemSounds.Hand.Play();
                    bullet.Regeneration();//регенерация снаряда в левой стороне экрана
                    objects.Regeneration();//регенерация астероида в правой стороне экрана
                }
            }         
        }

        public static BaseObject[] _objs;
        private static Asteroid[] asteroids;

        public static void Load()
        {
            _objs = new BaseObject[30];
            
            planet = new Planet(new Point(600, 40), new Point(-1, 0), new Size(20, 20));
            moon = new Moon(new Point(600, 100), new Point(-1, 0), new Size(40, 40));          
            bullet = new Bullet (new Point(0, 200), new Point(5, 0), new Size(4, 1));

            asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                asteroids[i] = new Asteroid(new Point(1000,rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }

        }      

    }
}
