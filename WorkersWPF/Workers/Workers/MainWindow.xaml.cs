using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        Program prog = new Program();
        ObservableCollection<string> items = new ObservableCollection<string>();// коллекция элементов для ListBox1. Имена департаментов
        //ObservableCollection<Department> items = new ObservableCollection<Department>(); // коллекция элементов для ListBox1. Имена департаментов
        public MainWindow()
        {           
            InitializeComponent();
            MainGrid.DataContext = prog;            
        }        
        // Нажатие кнопки NEW DEPARTMENT. Считывает данные из двух текст боксов, создает новый экземпляр департамена и добавляет его в основную коллекцию департаментов
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = TextBox1.Text;
            string Location = TextBox2.Text;

            Department Dep = new Department(Name, Location);
            prog.DepAddToDataBase(Dep);
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
            prog.DepClearDataBase();
            TextBlock1.Text = "";
            prog.DepGetFromDataBase();
            FillDepList();
            foreach (Department D in prog.Dep)
            {
                TextBlock1.Text += D.Name + " " + D.Location;
            }                   
        }
        void FillDepList()
        {
            for (int j = 0; j < items.Count; j++)
            {
                items.RemoveAt(j);
            }
            for (var i = 0; i < prog.Dep.Count; i++)
            {
                items.Add(prog.Dep[i].Name);
            }
            //items = prog.Dep;
            ListBox1.ItemsSource = items;

        }
    }
}
