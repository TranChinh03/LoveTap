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
using System.Windows.Shapes;

namespace LoveTap
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public Visibility visibilitiBG(object sender, SelectionChangedEventArgs e)
        {
            string vis;
            return BackgroundOpacity.Visibility;
        }

            public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee.Visibility=Visibility.Visible;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DetailOfE.Visibility=Visibility.Visible;
        }

    }
}
