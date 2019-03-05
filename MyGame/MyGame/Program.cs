using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запуск формы меню
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);         
            Application.Run(new SplashScreen());    
            
        }
        static public void NewGame()
        {
            //запуск основной формы игры 
            Form form = new Form()
            {
                //Width = Screen.PrimaryScreen.Bounds.Width,
                //Height = Screen.PrimaryScreen.Bounds.Height

                Width = 1000,
                Height = 800
            };
            Game.Init(form);
            form.Show();
            Game.Draw();
            
        }
    }
}
