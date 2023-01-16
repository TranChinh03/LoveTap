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
using System.Collections.ObjectModel;

namespace LoveTap.ViewModel
{
    internal class EditEmployeeViewModel:BaseViewModel
    {
        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private int _Position;
        public int Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

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
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        public ICommand LoadedEditEmployeeUC { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];

        public EditEmployeeViewModel(NavigationStore navigationStore)
        {
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for (int i = 0; i < DataProvider.Ins.DB.CHINHANHs.Count(); i++)

            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            LoadedEditEmployeeUC = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                NHANVIEN temp = EmployeeViewModel.CurrentSelected;

                ID = temp.MANV;
                EmployeeName = temp.HOTEN;
                if (temp.VAITRO == true)
                    Position = 1;
                else
                    Position = 0;
                Birthday = temp.NTNS.ToString();
                PhoneNumber = temp.SDT;
                Address = temp.DIACHI;
                CoefficientSalary = temp.HESOLUONG.ToString();
                BasicPay = temp.LUONGCB.ToString();
                Branch = (int)temp.MACN;
                Email = temp.EMAIL;
            });
            navDone = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));
            navBack = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));

            EditCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(EmployeeName) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Address)  || string.IsNullOrEmpty(Branch.ToString()) || string.IsNullOrEmpty(CoefficientSalary) || string.IsNullOrEmpty(BasicPay) )
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
                if (Position == 1)
                    employee.VAITRO = Convert.ToBoolean(1);
                else
                    employee.VAITRO = Convert.ToBoolean(0);
                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
