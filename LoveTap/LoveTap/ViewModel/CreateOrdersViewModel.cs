using LoveTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveTap.ViewModel
{
    public class CreateOrdersViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public CreateOrdersViewModel()
        {
            
        }
    }
}
