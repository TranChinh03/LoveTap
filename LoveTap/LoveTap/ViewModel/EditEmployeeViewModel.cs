using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    internal class EditEmployeeViewModel:BaseViewModel
    {
        private string _ID;
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

        private string _Birthday;
        public string Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _CoefficientSalary;
        public string CoefficientSalary { get => _CoefficientSalary; set { _CoefficientSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private string _Branch;
        public string Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        public ICommand LoadedEditEmployeeUC { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }
        public EditEmployeeViewModel(NavigationStore navigationStore)
        {
            LoadedEditEmployeeUC = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                NHANVIEN temp = EmployeeViewModel.CurrentSelected;

                ID = temp.MANV;
                EmployeeName = temp.HOTEN;
                if (MainViewModel.IsAdmin == true)
                    Position = "Admin";
                else
                    Position = "Staff";
                Birthday = temp.NTNS.ToString();
                PhoneNumber = temp.SDT;
                Address = temp.DIACHI;
                CoefficientSalary = temp.HESOLUONG.ToString();
                BasicPay = temp.LUONGCB.ToString();
                Branch = temp.MACN;
            });
            navDone = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));
            navBack = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));

            EditCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(EmployeeName) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Branch) || string.IsNullOrEmpty(CoefficientSalary) || string.IsNullOrEmpty(BasicPay) || string.IsNullOrEmpty(Position))
                    return false;

                return true;
            }, (p) =>
            {
                var employee = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).SingleOrDefault();
                //employee.MANV = ID;
                employee.HOTEN = EmployeeName;
                employee.SDT = PhoneNumber;
                employee.NTNS = DateTime.Parse(Birthday);
                employee.DIACHI = Address;
                employee.HESOLUONG = float.Parse(CoefficientSalary);
                employee.LUONGCB = float.Parse(BasicPay);
                employee.MACN = Branch;
                if (Position == "Admin")
                    employee.VAITRO = Convert.ToBoolean(1);
                else
                    employee.VAITRO = Convert.ToBoolean(0);
                DataProvider.Ins.DB.SaveChanges();
            });
        }

       

    }
}
