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

namespace LoveTap
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ControlBarUC_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RenderSignIn(object sender, RoutedEventArgs e)
        {
            SignUpModel.Visibility = Visibility.Collapsed;
            SignUpText.Visibility = Visibility.Collapsed;
            CreatePasswordModel.Visibility = Visibility.Collapsed;
            CreatePasswordText.Visibility = Visibility.Collapsed;
            LoginModel.Visibility = Visibility.Visible;
            //LoginText.Visibility = Visibility.Visible;
        }
        private void RenderSignUp(object sender, RoutedEventArgs e)
        {
            LoginModel.Visibility = Visibility.Collapsed;
            //LoginText.Visibility = Visibility.Collapsed;
            CreatePasswordModel.Visibility = Visibility.Collapsed;
            CreatePasswordText.Visibility = Visibility.Collapsed;
            SignUpModel.Visibility = Visibility.Visible;
            SignUpText.Visibility = Visibility.Visible;
        }
        private void RenderCreatePassword(object sender, RoutedEventArgs e)
        {
            SignUpModel.Visibility = Visibility.Collapsed;
            SignUpText.Visibility = Visibility.Collapsed;
            LoginModel.Visibility = Visibility.Collapsed;
            //LoginText.Visibility = Visibility.Collapsed;
            CreatePasswordModel.Visibility = Visibility.Visible;
            CreatePasswordText.Visibility = Visibility.Visible;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginModel_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
