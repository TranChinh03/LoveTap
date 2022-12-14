using LoveTap.Model;
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
        public string UserID { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand LoadedProfileUsr { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand changePw { get; set; }
        public ICommand HiddenBG { get; set; }

        private ObservableCollection<NHANVIEN> _User;
        public ObservableCollection<NHANVIEN> User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }

        private string _Birthday;
        public string Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _ID;
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Branch;
        public string Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private string _CoefficientsSalary;
        public string CoefficientsSalary { get => _CoefficientsSalary; set { _CoefficientsSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        string Name;
        public ProfileUsrViewModel()
        {
            LoadedProfileUsr = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                UserID = MainViewModel.ID;
                User = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NVID == UserID));
                if (User != null && User.Count > 0)
                {
                    FullName = User[0].HOTEN;
                    Birthday = User[0].NTNS.ToString();
                    PhoneNumber = User[0].SDT;
                    Email = User[0].EMAIL;
                    Address = User[0].DIACHI;
                    ID = User[0].MANV;
                    Branch = User[0].MACN;
                    CoefficientsSalary = User[0].HESOLUONG.ToString();
                    BasicPay = User[0].LUONGCB.ToString();
                    string role = User[0].VAITRO.ToString();
                    if (role == "True")
                        Role = "Admin";
                    else
                        Role = "Staff";
                }
            });

            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<HomePersonal>((p) => true, (p) => switchtab(p));
            changePw = new RelayCommand<CreatePasswordUC>((p) => true, (p) => changePass(p));
            HiddenBG = new RelayCommand<Grid>((p) => true, (p) => hiddenBG(p));
        }
        void switchtab(HomePersonal p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.changePW.Visibility=Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        HomeProfileEdit hpEditWd = new HomeProfileEdit();
                        hpEditWd.ShowDialog();
                        break;
                    }

            }
        }

        void changePass(CreatePasswordUC p)
        {
            p.Visibility = Visibility.Collapsed;
        }

        void hiddenBG(Grid p)
        {
            p.Visibility= Visibility.Collapsed;
        }
    }
}
