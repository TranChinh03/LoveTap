using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LoveTap.Stores;
using LoveTap.Commands;
using LoveTap.Model;
using System.Collections.ObjectModel;
using static LoveTap.ViewModel.OrderDetailViewModel;

namespace LoveTap.ViewModel
{
    internal class AddEmployeeViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }

        public ICommand AddCommand { get; set; }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _CoefficientSalary;
        public string CoefficientSalary { get => _CoefficientSalary; set { _CoefficientSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        public AddEmployeeViewModel(NavigationStore navigationStore)
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for (int i = 0; i < DataProvider.Ins.DB.CHINHANHs.Count(); i++)

            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            AddCommand = new RelayCommand<object>((p) =>
            {
                if ( string.IsNullOrEmpty(EmployeeName) || string.IsNullOrEmpty(Position) ||
                string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(CoefficientSalary) || 
                string.IsNullOrEmpty(BasicPay) || string.IsNullOrEmpty(Branch.ToString())||Birthday == null)
                    return false;
                var employee = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID);
                if (employee == null || employee.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var nhanvien = new NHANVIEN();
                nhanvien.HOTEN = EmployeeName;
                if (Position.ToUpper() == "STAFF")
                    nhanvien.VAITRO = false;
                else
                    nhanvien.VAITRO = true;
                nhanvien.SDT = PhoneNumber;
                nhanvien.DIACHI = Address;
                nhanvien.HESOLUONG =double.Parse(CoefficientSalary);
                nhanvien.LUONGCB = double.Parse(BasicPay);
                nhanvien.MACN = Branch;
                nhanvien.NTNS = Birthday;
                nhanvien.EMAIL= Email;
                nhanvien.DELETED = false;
                nhanvien.AVA = "../img/Person.jpg";

                DataProvider.Ins.DB.NHANVIENs.Add(nhanvien);
                DataProvider.Ins.DB.SaveChanges();

                EmployeeList.Add(nhanvien);
            });

            navDone = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
            navBack = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));

        }
    }
}
