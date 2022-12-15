using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using LoveTap.Model;

namespace LoveTap.ViewModel
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        private string _CusName;
        public string CusName { get => _CusName; set { _CusName = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _DOB;
        public Nullable<System.DateTime> DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }
        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _RegistDate;
        public Nullable<System.DateTime> RegistDate { get => _RegistDate; set { _RegistDate = value; OnPropertyChanged(); } }
        private Nullable<double> _Sale;
        public Nullable<double> Sale { get => _Sale; set { _Sale = value; OnPropertyChanged(); } }
        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public ICommand GetIdButton { get; set; }
        string Name;
        public ICommand SwitchTab { get; set; }
        public ICommand LoadedDetailCustomer { get; set; }
        public CustomerDetailViewModel() {
            LoadedDetailCustomer = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                KHACHHANG x = CustomerViewModel.CurrentSelected;
                if (x != null)
                {
                    CusName = x.HOTEN;
                    Phone = x.SDT;
                    DOB = x.NGSINH;
                    Address = x.DIACHI;
                    RegistDate = x.NGDK;
                    Sale = x.DOANHSO;
                    Type = "Đồng";
                    if (Sale > 2000000 && Sale <= 5000000)
                        Type = "Bạc";
                    else if (Sale <= 10000000)
                        Type = "Vàng";
                    else if (Sale > 10000000)
                        Type = "Kim cương";
                }

            });
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<CustomerDetailUC>((p) => true, (p) => switchtab(p));
        }

        void switchtab(CustomerDetailUC p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.EditCustomerUC.Visibility=Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
                case 3:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
            }
        }
    }
}
