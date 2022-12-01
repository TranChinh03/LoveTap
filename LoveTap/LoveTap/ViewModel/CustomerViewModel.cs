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
    public class CustomerViewModel
    {
        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value;} }

        public CustomerViewModel()
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        }
    }
}
