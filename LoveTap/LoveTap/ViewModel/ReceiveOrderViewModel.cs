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
        public static PHIEUNHAP ReceiveSelected { get; set; }
        public ICommand navAddReceive { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<PHIEUNHAP> _ReceivesList;
        public ObservableCollection<PHIEUNHAP> ReceivesList { get => _ReceivesList; set { _ReceivesList = value; } }
        public ReceiveOrderViewModel(NavigationStore navigationStore)
        {
            ReceivesList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            navAddReceive = new NavigationCommand<AddReceiveViewModel>(navigationStore, () => new AddReceiveViewModel(navigationStore));
            Detail = new RelayCommand<ReceiveOrdViewUC>((p) => { return p.ReceiveList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
            navDetail = new NavigationCommand<ReceiveDetailViewModel>(navigationStore, () => new ReceiveDetailViewModel(navigationStore));
        }
        void _DetailCs(ReceiveOrdViewUC p)
        {
            ReceiveSelected =(PHIEUNHAP)p.ReceiveList.SelectedItem;
        }
    }
}
