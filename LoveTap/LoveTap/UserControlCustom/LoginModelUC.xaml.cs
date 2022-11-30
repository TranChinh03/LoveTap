using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
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

        private void signinBtn_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection sqlCon = new SqlConnection(@"Data Source=local; Intial Catalog =LOGINDB; Intergrated Security=True;");
            //try
            //{ 
            //    if(sqlCon.State == System.Data.ConnectionState.Closed)
            //        sqlCon.Open();
            //    string query = "SELECT COUNT(1) FROM LOGIN WHERE USERNAME= = @USERNAME AND UPASSWORD = @PASSWORD";
            //    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            //    sqlCmd.CommandType = System.Data.CommandType.Text;
            //    sqlCmd.Parameters.AddWithValue("@USERNAME", usernameTxb.Text);
            //    sqlCmd.Parameters.AddWithValue("@PASSWORD", passwordTbx.Password);
            //    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            //    if (count == 1)
            //    {
            //        SuccessModelUC successModelUC = new SuccessModelUC();
            //        successModelUC.Visibility = Visibility.Visible;
            //        this.Visibility = Visibility.Collapsed;
            //    }
            //    else
            //    {
            //        FailedModelUC failedModelUC = new FailedModelUC();
            //        failedModelUC.Visibility = Visibility.Visible;
            //        this.Visibility= Visibility.Collapsed;  
            //    }
                    
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{ 
            //    sqlCon.Close(); 
            //}
        }
    }
}
