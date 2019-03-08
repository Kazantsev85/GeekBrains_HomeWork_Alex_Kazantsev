using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Workers
{
    //класс описывает сотрудников
    class Employee
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Surname { get; set; }

        public Employee(string department, string name, string surname, string occupation, int age, int salary)
        {
            Department = department;
            Name = name;
            Occupation = occupation;
            Age = age;
            Salary = salary;
            Surname = surname;
            
        }
        public void AddToDataBase()//метод вызывается при создании нового сотрудника через текстовые поля и записывает данные в текстовый файл
        {
            StreamWriter WorkersList = new StreamWriter("Employee.txt", true);
            WorkersList.WriteLine("{0:G} {1:G} {2:G} {3:G} {4:N} {5:N}\n", Department, Name, Surname, Occupation, Age, Salary);
            WorkersList.Close();

        }
    }
    
}
