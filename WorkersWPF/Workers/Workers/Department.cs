using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Workers
{
    //класс описывает различные отделы. Принимает два парамера, Наименование и расположение
    class Department
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Department( string name, string location)
        {
            Name = name;
            Location = location;
        }

        public static List<Department> DepList = new List<Department>();
        public static List<string> deplist = new List<string>();

        public void AddToDataBase() //метод вызывается при создании нового отдела через текстовые поля и записывает данные в текстовый файл
        {
            
            StreamWriter DepartmentsList = new StreamWriter("Departments.txt", true);
            DepartmentsList.WriteLine("{0:G},{1:G} \n", Name, Location);

            DepartmentsList.Close();
        }
        public void GetFromDataBase()
        {
            StreamReader DepartmentsList = new StreamReader("Departments.txt");
            //Read the first line of text
            //char delimiter = ',';
            //int numberOfCols = 2;

            //var lines = File
            //    .ReadLines("Departments.txt")
            //    .Where(l => l.Split(delimiter).Count() == numberOfCols);
            int i = 0;
            while (DepartmentsList != null)
            {                
                deplist[i] = DepartmentsList.ReadLine();
                i++;                               
            }
                

            //Continue to read until you reach end of file
            //while (line != null)
            //{
            //    //write the lie to console window
            //    Console.WriteLine(line);
            //    //Read the next line
            //    line = sr.ReadLine();
            //}

            //close the file
            //sr.Close();
            //Console.ReadLine();

        }



    }
       
    
}
