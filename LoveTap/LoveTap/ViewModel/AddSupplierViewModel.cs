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
    public class AddSupplierViewModel: BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public ICommand AddCommand { get; set; }
        private string _SupplierName;
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }

        private int _EmployeeID;
        public int EmployeeID { get => _EmployeeID; set { _EmployeeID = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
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
        public AddSupplierViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<SupplierViewModel>(navigationStore, () => new SupplierViewModel(navigationStore));
            navBack = new NavigationCommand<SupplierViewModel>(navigationStore, () => new SupplierViewModel(navigationStore));
            //BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            //EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            //for (int i = 0; i < EmployeeList.Count; i++)
            //{
            //    EmployeeIDList[i] = EmployeeList[i].MANV;
            //}

            //for (int i = 0; i < BranchList.Count; i++)
            //{
            //    BranchIDList[i] = BranchList[i].MACN;
            //}
            //RegistDate = DateTime.Now;
            AddCommand = new RelayCommand<object>((p) =>
            {
                //RegistDate = DateTime.Now;
                if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address))
                    return false;
                return true;
            }, (p) =>
            {
                var supplier = new NHACUNGCAP();
                supplier.TEN = SupplierName;
                supplier.SDT = Phone;
                supplier.EMAIL = Email;
                supplier.DIACHI = Address;
                supplier.DELETED = false;
                DataProvider.Ins.DB.NHACUNGCAPs.Add(supplier);
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
