using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    abstract class BaseEmployee
    {
        public double Fee { get; set; }

        public BaseEmployee (double fee)
        {
            Fee = fee;
        }        
        public abstract double Salary();
        public abstract void Print();
    }
}
