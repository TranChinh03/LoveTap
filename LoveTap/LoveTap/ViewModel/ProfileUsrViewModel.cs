using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class ProfileUsrViewModel : BaseViewModel
    {
        public int UserID { get; set; }
        public ICommand NavChangePw { get; set; }
        public ICommand NavEditUsr { get; }

        private ObservableCollection<NHANVIEN> _User;
        public ObservableCollection<NHANVIEN> User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _DOB;
        public Nullable<System.DateTime> DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private string _CoefficientsSalary;
        public string CoefficientsSalary { get => _CoefficientsSalary; set { _CoefficientsSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }
        private string _ImgPath;
        public string ImgPath { get => _ImgPath; set { _ImgPath = value; OnPropertyChanged(); } }

        private string _NumberOfOrder;
        public string OrdersCount { get => _NumberOfOrder; set { _NumberOfOrder = value; OnPropertyChanged(); } }

        string Name;
        public ProfileUsrViewModel(NavigationStore navigationStore)
        {
            UserID = MainViewModel.ID;
            User = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == UserID));
            if (User != null && User.Count > 0)
            {
                FullName = User[0].HOTEN;
                DOB = User[0].NTNS;
                PhoneNumber = User[0].SDT;
                Email = User[0].EMAIL;
                Address = User[0].DIACHI;
                ID = User[0].MANV;
                Branch = (int)User[0].MACN;
                CoefficientsSalary = User[0].HESOLUONG.ToString();
                BasicPay = User[0].LUONGCB.ToString();
                string role = User[0].VAITRO.ToString();
                if (role == "True")
                    Role = "Admin";
                else
                    Role = "Staff";
                ImgPath = User[0].IMG_PATH;
                OrdersCount = DataProvider.Ins.DB.HOADONs.Where(x => x.MANV == UserID).Count().ToString();
            }
            ;

            NavEditUsr = new NavigationCommand<UsrPro5EditViewModel>(navigationStore, () => new UsrPro5EditViewModel(navigationStore));
            NavChangePw = new NavigationCommand<CreatePwViewModel>(navigationStore, () => new CreatePwViewModel(navigationStore));
        }


    }
}
