using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class HourRateEmp : BaseEmployee
    {
        protected double S { get; set; }        
        public HourRateEmp(double fee) : base(fee)
        {
            S = fee;
        }
        public override double Salary()
        {
            double Sal = 20.8*8*S;
            return Sal;
        }
        public override void Print()
        {
            double Sal = Salary();
            Console.WriteLine("Почасовая оплата: {0:f}\n", Sal);
        }
    }
}
