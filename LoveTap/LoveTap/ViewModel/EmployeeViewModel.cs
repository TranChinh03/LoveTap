using LoveTap.Model;
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
        public EmployeeViewModel()
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            string role =  EmployeeList[0].VAITRO.ToString();
            if (role == "True")
                Role = "Admin";
            else
                Role = "Staff";
        }
    }
}
