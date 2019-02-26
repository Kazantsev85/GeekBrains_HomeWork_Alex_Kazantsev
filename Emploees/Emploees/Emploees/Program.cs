using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            Employees();
        }
        public static BaseEmployee[] Empl;
        
        public static void Employees()
        {
            Empl = new BaseEmployee[20];

            var Rand = new Random();

            for (var i = 0; i < Empl.Length/2; i++)

            {
                double MonthRate = Rand.Next(100000, 150000);
                Empl[i] = new FixedRateEmp(MonthRate);
            }
            for (var i = Empl.Length / 2; i < Empl.Length; i++)
            {
                double HourRate = Rand.Next(1000, 1500);
                Empl[i] = new HourRateEmp(HourRate);
            }
            Print();
        }
        public static void Print()
        {
            foreach (BaseEmployee empl in Empl)
                empl.Print();
            Console.ReadKey();
        }
    }
}
