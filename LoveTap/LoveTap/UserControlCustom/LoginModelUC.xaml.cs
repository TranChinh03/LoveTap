using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace LoveTap.UserControlCustom
{
    /// <summary>
    /// Interaction logic for LoginModelUC.xaml
    /// </summary>
    public partial class LoginModelUC : UserControl
    {
        public LoginModelUC()
        {
            InitializeComponent();
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btSignIn_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeWd = new HomeScreen();
            homeWd.Show();
           
            //MainWindow mainWd = new MainWindow();
            //mainWd.Show();
            Window.GetWindow(this).Close();
        }
    }
}
