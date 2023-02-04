using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace LoveTap.ViewModel
{
    internal class CreatePwViewModel : BaseViewModel
    {
        public ICommand navCancel { get; set; }
        public ICommand changePwCmd { get; set; }

        public CreatePwViewModel(NavigationStore navigationStore)
        {
            changePwCmd =  new RelayCommand<CreatePasswordUC>((p) =>
            {
                if (string.IsNullOrEmpty(p.newPw.Password) || string.IsNullOrEmpty(p.confirmPw.Password))
                    return false;
                return true; ;
            }, (p) =>
            {
                ChangePw(p);
            });
            navCancel = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
        }

        void ChangePw(CreatePasswordUC p)
        {
            if (p.newPw.Password != p.confirmPw.Password)
            {
                p.pwNote.Text= "Passwords do NOT match!";
                p.pwNote.Visibility= Visibility.Visible;
                p.newPw.BorderBrush = Brushes.OrangeRed;
                p.confirmPw.BorderBrush = Brushes.OrangeRed;
                p.pwNote.Foreground= Brushes.OrangeRed;
            }
            else
            {

                int ID;
                ID = MainViewModel.ID;
                var account = DataProvider.Ins.DB.LOGINs.Where(x => x.ID == ID).SingleOrDefault();
                account.USERPASS= MD5Hash(Base64Encode(p.newPw.Password));
                DataProvider.Ins.DB.SaveChanges();

                p.pwNote.Visibility= Visibility.Visible;
                p.pwNote.Text= "Password changed successfully!";
                p.pwNote.Foreground = Brushes.Green;
                p.newPw.BorderBrush = Brushes.LightBlue;
                p.confirmPw.BorderBrush = Brushes.LightBlue;

            }
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

