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
    public class OrdersViewModel
    {
        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public OrdersViewModel()
        {
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
        }
    }
}
