using LoveTap.Model;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveTap.ViewModel
{
    public class EmployeeViewModel
    {
        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value;} }

        private NHANVIEN _VAITRO;
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
        }
    }
}
