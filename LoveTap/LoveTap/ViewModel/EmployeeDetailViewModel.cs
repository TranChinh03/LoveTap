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
    public class EmployeeDetailViewModel:BaseViewModel
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

        public ICommand LoadedEmployeeDetailUC { get; set; }
        public ICommand GetIdButton { get; set; }
        string Name;
        public ICommand SwitchTab { get; set; }
        public EmployeeDetailViewModel()
        {
            LoadedEmployeeDetailUC = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                NHANVIEN temp = EmployeeViewModel.EmployeeSelected;

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

            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<EmployeeDetailUC>((p) => true, (p) => switchtab(p));
        }
        void switchtab(EmployeeDetailUC p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.EditEmployee.Visibility=Visibility.Visible;
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
