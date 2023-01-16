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
        public static Receive ReceiveSelected { get; set; }



        public ICommand navAddReceive { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }


        private ObservableCollection<PHIEUNHAP> _ReceiveList;
        public ObservableCollection<PHIEUNHAP> ReceiveList { get => _ReceiveList; set { _ReceiveList = value; } }


        private ObservableCollection<NHACUNGCAP> _SuplierList;
        public ObservableCollection<NHACUNGCAP> SuplierList { get => _SuplierList; set { _SuplierList = value; } }

        public struct Receive
        {
            public int ID { get; set; }

            public int MANCC { get; set; }

            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Date { get; set; }
            public double Total { get; set; }
            public int MANV { get; set; }
        }

        private List<Receive> _MyReceiveList = new List<Receive>();
        public List<Receive> MyReceiveList { get => _MyReceiveList; set { _MyReceiveList = value; } }
        public ReceiveOrderViewModel(NavigationStore navigationStore)
        {
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            SuplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs);

            navAddReceive = new NavigationCommand<AddReceiveViewModel>(navigationStore, () => new AddReceiveViewModel(navigationStore));
            navDetail = new NavigationCommand<ReceiveDetailViewModel>(navigationStore, () => new ReceiveDetailViewModel(navigationStore));
            Detail = new RelayCommand<ReceiveOrdViewUC>((p) => { return p.ReceiveList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));

            Receive phieunhap = new Receive();

            foreach (PHIEUNHAP pn in ReceiveList.Where(x => x.DELETED == false))
            {
                phieunhap.ID = pn.MAPN;
                phieunhap.MANCC = (int)pn.MANCC;
                phieunhap.Date = (DateTime)pn.NGNHAP;
                phieunhap.Total = (double)pn.TONGTIEN;
                phieunhap.MANV = (int)pn.MANV;
                foreach (NHACUNGCAP ncc in SuplierList)
                {
                    if (ncc.MANCC == phieunhap.MANCC)
                    {
                        phieunhap.Name = ncc.TEN;
                        phieunhap.Email = ncc.EMAIL;
                    }
                }
                MyReceiveList.Add(phieunhap);
            }
        }
        void _DetailCs(ReceiveOrdViewUC p)
        {
            ReceiveSelected = (Receive)p.ReceiveList.SelectedItem;

        }
    }
}
