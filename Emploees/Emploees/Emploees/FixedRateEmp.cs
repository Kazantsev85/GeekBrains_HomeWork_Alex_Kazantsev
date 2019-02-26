using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class FixedRateEmp : BaseEmployee
    {

        protected double S { get; set; }
        

        public FixedRateEmp(double fee):base(fee)
        {
            S = fee;
        }
        public override double Salary()
        {
            double Sal = S;
            return Sal;
        }
        public override void Print()
        {
            double Sal = Salary();
            Console.WriteLine("Ежемесячная оплата: {0:f}\n",Sal);
        }
    }
}
