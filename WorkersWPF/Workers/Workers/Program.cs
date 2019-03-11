using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Workers
{
    class Program : INotifyPropertyChanged
    {
        private bool flag;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool Flag
        {
            get { return this.flag; }
            set
            {
                this.flag = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.flag)));
            }
        }

        public ObservableCollection<Department> Dep = new ObservableCollection<Department>(); //Основная коллекция департаментов
        
        public List<string> deplist = new List<string>(); // коллекция строк, соэдается из строк текстового файла департаментов   

        public void DepClearDataBase()
        {            
            for(int j = 0; j < Dep.Count; j++)
            {                                            
                    Dep.RemoveAt(j);                                   
            }
        }

        public void DepGetFromDataBase()
        {
            StreamReader DepartmentsList = new StreamReader("Departments.txt");
            
            string F = DepartmentsList.ReadToEnd(); // считывает файл департаментов в string
            string[] deplist = F.Split(new[] { Environment.NewLine }, StringSplitOptions.None); //разбивает файл департаментов в массив строк         
                               
            for (var i = 0; i < deplist.Length - 1; i++) // разбивает строки deplist на два элемента сепаратором и заполняет основную коллекцию департаментов  
            {
                string[] L = deplist[i].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                Dep.Add(new Department(L[0], L[1]));
            }
        }
        public void DepAddToDataBase(Department dep) //метод вызывается при создании нового отдела через текстовые поля и записывает данные в текстовый файл
        {
            StreamWriter DepartmentsList = new StreamWriter("Departments.txt", true);
            
            bool Flag = false;

            for(var i = 0; i<Dep.Count;i++)
            {
                if (Dep[i].Name == dep.Name)
                {
                    Flag = true;
                    break;
                }
                else Flag = false;
            }
            if(Flag == false)
            {
                DepartmentsList.WriteLine("{0:G},{1:G} \n", dep.Name, dep.Location);
                DepartmentsList.Close();
                Dep.Add(dep);
            }
        }
    }
}

