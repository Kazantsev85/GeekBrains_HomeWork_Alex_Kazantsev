using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    public partial class SplashScreen : Form
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;        

        public SplashScreen()
        {
            InitializeComponent();
        }
        
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Refrash();
        }
        public static void Draw()
        {            
            Buffer.Graphics.Clear(Color.Black);
            foreach (SplashScreenObjects obj in objs)
                obj.Draw();
            Buffer.Render();
        }
        public static void Refrash()
        {
            foreach (SplashScreenObjects obj in objs)
                obj.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.NewGame();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = CreateGraphics();


            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Loader();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        static SplashScreenObjects[] objs;
        public static void Loader()
        {
            objs = new SplashScreenObjects[30];
            
            for (int i = 0; i < objs.Length; i++)
                objs[i] = new SplashScreenObjects(new Point(20*i, 20*i), new Point(-1, 0), new Size(6, 6));
            
        }
    }
}
