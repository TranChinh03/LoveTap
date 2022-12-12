using LoveTap.Model;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    public class HomeProfileEditViewModel : BaseViewModel
    {
        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

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


        public ICommand EditCommand { get; set; }

        public HomeProfileEditViewModel()
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FullName)|| string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Branch) || string.IsNullOrEmpty(CoefficientsSalary) || string.IsNullOrEmpty(BasicPay) || string.IsNullOrEmpty(Role))
                    return false;

                //var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                //if (displayList == null || displayList.Count() != 0)
                //    return false;

                return true;

            }, (p) =>
            {
                var employee = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).SingleOrDefault();
                employee.MANV = ID;
                employee.HOTEN = FullName;
                employee.SDT = PhoneNumber;
                employee.EMAIL = Email;
                employee.NTNS = DateTime.Parse(Birthday);
                employee.HESOLUONG = float.Parse(CoefficientsSalary);
                employee.LUONGCB = float.Parse(BasicPay);
                if (Role == "Staff" || Role == "staff")
                    employee.VAITRO = Convert.ToBoolean(0);
                else
                    employee.VAITRO = Convert.ToBoolean(1);


                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}

