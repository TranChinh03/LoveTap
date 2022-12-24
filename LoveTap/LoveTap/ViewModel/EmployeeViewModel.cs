using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {

        public static NHANVIEN EmployeeSelected { get; set; }
        public ICommand navAddEmployee { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }
        public static NHANVIEN CurrentSelected { get; set; }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private string _Role;
        
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }    
        //public NHANVIEN VAITRO { get => _VAITRO; set { _VAITRO = value;
        //        if (VAITRO != null)
        //            VITRI = "Admin";
        //        else
        //            VITRI = "Staff";
        //    } }

        //private string _VITRI;
        //public string VITRI { get => _VITRI; set { _VITRI = value;} }
        public EmployeeViewModel(NavigationStore navigationStore)
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            string role =  EmployeeList[0].VAITRO.ToString();
            if (role == "True")
                Role = "Admin";
            else
                Role = "Staff";
            navAddEmployee = new NavigationCommand<AddEmployeeViewModel>(navigationStore, () => new AddEmployeeViewModel(navigationStore));
            navDetail = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));
            Detail = new RelayCommand<EmployeeViewUC>((p) => { return p.EmployeeList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }
        void _DetailCs(EmployeeViewUC p)
        {
            CurrentSelected =(NHANVIEN)p.EmployeeList.SelectedItem;
        }

    }
}
