using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using LoveTap.Model;
using System.Security.Cryptography;
using System.Data.Entity;

namespace LoveTap.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; } = false;
        private string _UserName;
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); }}
        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; OnPropertyChanged(); } }
        public string ID { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {
            UserName = "";
            Password = "";
            LoginCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Object p)
        {
            if (p == null) return;
            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.Ins.DB.LOGINs.Where(x => x.USERNAME == UserName && x.USERPASS == passEncode).Count();

            if (accCount > 0) 
            {
                IsLogin = true;
                var user = DataProvider.Ins.DB.LOGINs.Where(x => x.USERNAME == UserName && x.USERPASS == passEncode).ToList();
                ID = user[0].ID;
                FrameworkElement window = GetWindowParent(p);
                var w = (window as Window);
                if (w != null)
                    w.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Wrong account or password");
            }
        }
        FrameworkElement GetWindowParent(Object u)
        {
            UserControl p = (u as UserControl); 
            FrameworkElement parent = p;
            while (parent.Parent != null)
                parent = parent.Parent as FrameworkElement;
            return parent;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
