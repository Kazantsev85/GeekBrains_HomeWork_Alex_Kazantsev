using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Workers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Нажатие кнопки NEW DEPARTMENT. Считывает данные из двух текст боксов и записывает их в текстовый файл Departments.txt
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = TextBox1.Text;
            string Location = TextBox2.Text;           
            
            Department Dep = new Department( Name, Location);
            Dep.AddToDataBase();           

        }
        // Нажатие кнопки NEW EMPLOYEE. Считывает данные из текст боксов и записывает их в текстовый файл Employee.txt
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string Name = TextBox3.Text;
            string Surname = TextBox4.Text;
            string Department = TextBox6.Text;
            string Occupation = TextBox5.Text;
            int Age = int.Parse(TextBox7.Text);
            int Salary = int.Parse(TextBox8.Text);

            Employee Emp = new Employee(Department, Name, Surname, Occupation, Age, Salary);
            Emp.AddToDataBase();            

        }

        public static List<string> deplist = new List<string>();
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                                                  

            StreamReader DepartmentsList = new StreamReader("Departments.txt");            

            while (!DepartmentsList.EndOfStream) 
            {
                deplist.Add(DepartmentsList.ReadLine());                
            }
            
            for (var i = 0; i < deplist.Count; i++)
            {
                TextBlock1.Text += deplist[i]+"\n";                                
            }           
        }
    }
}
