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
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    public class EmployeeDetailViewModel:BaseViewModel
    {
        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _DOB;
        public Nullable<System.DateTime> DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _CoefficientSalary;
        public string CoefficientSalary { get => _CoefficientSalary; set { _CoefficientSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        public ICommand LoadedEmployeeDetailUC { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand navEdit { get; set; }
        public ICommand navBack { get; set; }
        public EmployeeDetailViewModel(NavigationStore navigationStore)
        {
            LoadedEmployeeDetailUC = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                NHANVIEN temp = EmployeeViewModel.CurrentSelected;

                ID = temp.MANV;
                EmployeeName = temp.HOTEN;
                if (temp.VAITRO == true)
                    Position = "Admin";
                else
                    Position = "Staff";
                DOB = temp.NTNS;
                PhoneNumber = temp.SDT;
                Address = temp.DIACHI;
                CoefficientSalary = temp.HESOLUONG.ToString();
                BasicPay = temp.LUONGCB.ToString();
                Branch = (int)temp.MACN;
                Email = temp.EMAIL;
            });

            DeleteCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                var employee = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).SingleOrDefault();
                employee.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });

            navBack = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
            navEdit = new NavigationCommand<EditEmployeeViewModel>(navigationStore, () => new EditEmployeeViewModel(navigationStore));
        }
    }
}
