using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListT
{
   class Program
    {
        public static List<int> Numbers = new List<int>();
        static void Main(string[] args)
        {
            var rnd = new Random();
            for(int i = 0; i < 100; i++)
            {
                Numbers.Add(rnd.Next(0,100));
            }
            int [] Mass = new int[Numbers.Count];
            foreach (var v in Numbers)
            {
                Mass[v]++;
                Console.WriteLine(v + " ");              

            }
            Console.ReadKey();
        }
    }
}


    

