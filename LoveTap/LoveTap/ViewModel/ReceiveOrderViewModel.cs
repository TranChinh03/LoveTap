using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Service;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    public class ReceiveOrderViewModel : BaseViewModel
    {
        public static HOADON OrderSelected { get; set; }


        public ICommand navAddReceive { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public ReceiveOrderViewModel(NavigationStore navigationStore)
        {
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            navAddReceive = new NavigationCommand<AddReceiveViewModel>(navigationStore, () => new AddReceiveViewModel(navigationStore));
            navDetail = new NavigationCommand<ReceiveDetailViewModel>(navigationStore, () => new ReceiveDetailViewModel(navigationStore));
            Detail = new RelayCommand<ReceiveOrdViewUC>((p) => { return p.ReceiveList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }
        void _DetailCs(ReceiveOrdViewUC p)
        {
            OrderSelected = (HOADON)p.ReceiveList.SelectedItem;
        }
    }
}
