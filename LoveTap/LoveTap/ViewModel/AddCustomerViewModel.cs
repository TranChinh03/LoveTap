using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private int _EmployeeID;
        public int EmployeeID { get => _EmployeeID; set { _EmployeeID = value; OnPropertyChanged(); } }
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

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }
        private string _Type;

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }
        public int[] EmployeeIDList { get; set; } = new int[DataProvider.Ins.DB.NHANVIENs.Count()];
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];
        public AddCustomerViewModel(NavigationStore navigationStore) {
            navDone = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navBack = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            for (int i = 0; i < EmployeeList.Count; i++)
            {
                EmployeeIDList[i] = EmployeeList[i].MANV;
            }

            for (int i = 0; i < BranchList.Count; i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            RegistDate = DateTime.Now;
            AddCommand = new RelayCommand<object>((p) =>
            {
                RegistDate = DateTime.Now;
                if (string.IsNullOrEmpty(CusName) || string.IsNullOrEmpty(Phone) || DOB == null || string.IsNullOrEmpty(Address))
                    return false;
                return true;
            }, (p) =>
            {
                var customer = new KHACHHANG();
                customer.HOTEN = CusName;
                customer.SDT = Phone;
                customer.NGSINH = DOB;
                customer.DIACHI= Address;
                customer.NGDK = RegistDate;
                customer.DOANHSO = 0;
                customer.DELETED = false;
                customer.MACN = Branch;
                customer.MANV = EmployeeID;
                DataProvider.Ins.DB.KHACHHANGs.Add(customer);
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
