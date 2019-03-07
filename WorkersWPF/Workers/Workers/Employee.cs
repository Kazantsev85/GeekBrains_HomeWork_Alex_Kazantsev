using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Employee
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public Employee(string department, string name, string occupation, int age, int salary)
        {
            Department = department;
            Name = name;
            Occupation = occupation;
            Age = age;
            Salary = salary;
        }
    }
}
