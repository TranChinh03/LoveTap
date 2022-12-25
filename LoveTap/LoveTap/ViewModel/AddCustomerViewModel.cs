using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{

    internal class AddCustomerViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public ICommand AddCommand { get; set; }
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
        public AddCustomerViewModel(NavigationStore navigationStore) {
            navDone = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navBack = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(CusName) || string.IsNullOrEmpty(Phone) || DOB == null || string.IsNullOrEmpty(Address) || RegistDate == null)
                    return false;
                return true;
            }, (p) =>
            {
                var customer = new KHACHHANG() { HOTEN = CusName, SDT = Phone, NGSINH = DOB, DIACHI = Address, NGDK = RegistDate, DOANHSO = Sale, DELETED = false};
                DataProvider.Ins.DB.KHACHHANGs.Add(customer);
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
