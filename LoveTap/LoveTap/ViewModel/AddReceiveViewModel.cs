using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static LoveTap.ViewModel.AddOrdersViewModel;

namespace LoveTap.ViewModel
{
    public class AddReceiveViewModel : BaseViewModel
    {
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        public ICommand LoadedAddReceive { get; set; }
        public ICommand IdChanged { get; set; }
        public ICommand gNameChanged { get; set; }
        public ICommand nameSupplierChanged { get; set; }

        private int _ReceiveID;
        public int ReceiveID { get => _ReceiveID; set { _ReceiveID = value; OnPropertyChanged(); } }

        private string _Date;
        public string Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }

        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }

        private int _GoodID;
        public int GoodID { get => _GoodID; set { _GoodID = value; OnPropertyChanged(); } }
        private int _eID;
        public int eID { get => _eID; set { _eID = value; OnPropertyChanged(); } }

        private string _GoodName;
        public string GoodName { get => _GoodName; set { _GoodName = value; OnPropertyChanged(); } }


        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }



        private double _SubTotal;
        public double SubTotal { get => _SubTotal; set { _SubTotal = value; OnPropertyChanged(); } }


        private string _SupplierName;
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }


        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        //private DateTime _Now;
        //public DateTime Now { get => _Now; set { _Now = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUNHAP> _ReceiveList;
        public ObservableCollection<PHIEUNHAP> ReceiveList { get => _ReceiveList; set { _ReceiveList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTPN> _ReceiveDetailList;
        public ObservableCollection<CTPN> ReceiveDetailList { get => _ReceiveDetailList; set { _ReceiveDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private ObservableCollection<NHACUNGCAP> _SupplierList;
        public ObservableCollection<NHACUNGCAP> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        private ObservableCollection<DANHMUC> _TypeList;
        public ObservableCollection<DANHMUC> TypeList { get => _TypeList; set { _TypeList = value; OnPropertyChanged(); } }

        public int[] GoodIDList { get; set; } = new int[DataProvider.Ins.DB.SANPHAMs.Count()];
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.SANPHAMs.Count()];

        public string[] GoodNameList { get; set; } = new string[DataProvider.Ins.DB.SANPHAMs.Count()];

        public string[] SupplierNameList { get; set; } = new string[DataProvider.Ins.DB.NHACUNGCAPs.Count()];

        //public struct Detail
        //{
        //    public string ten { get; set; }
        //    public string sl { get; set; }
        //}

        private List<CTPN> _MyRecieveDetailList = new List<CTPN>();
        public List<CTPN> MyRecieveDetailList { get { return _MyRecieveDetailList; } set { _MyRecieveDetailList = value; OnPropertyChanged(); } }
        private List<DetailList> _MyListView = new List<DetailList>();
        public List<DetailList> MyListView { get => _MyListView; set { _MyListView = value; OnPropertyChanged(); } }

        public AddReceiveViewModel(NavigationStore navigationStore)
        {

            //LoadedAddReceive = new RelayCommand<UserControl>((p) => true, (p) =>
            //{
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            ReceiveDetailList = new ObservableCollection<CTPN>(DataProvider.Ins.DB.CTPNs);
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            TypeList = new ObservableCollection<DANHMUC>(DataProvider.Ins.DB.DANHMUCs);

            Amount = "0";
            SubTotal = 0;
            Date = DateTime.Now.ToString();
            for (int i = 0; i < ProductList.Count; i++)
            {
                GoodIDList[i] = ProductList[i].MASP;
                GoodNameList[i] = ProductList[i].TEN;
            }

            for (int i = 0; i < BranchList.Count; i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            for (int i = 0; i < SupplierList.Count; i++)
            {
                SupplierNameList[i] = SupplierList[i].TEN;
            }

            if (GoodID.ToString() != "")
            {
                foreach (SANPHAM sp in ProductList)
                {
                    if (sp.MASP == GoodID)
                        GoodName = sp.TEN;
                }
            }

            eID = MainViewModel.ID;

            for (int i = 0; i < EmployeeList.Count; i++)
                if (EmployeeList[i].MANV == eID)
                {
                    EmployeeName = EmployeeList[i].HOTEN;
                    Branch = (int)EmployeeList[i].MACN;
                }
            //}
            //);


            AddCommand = new RelayCommand<AddReceiveViewUC>((p) =>
            {
                if ((string.IsNullOrEmpty(GoodID.ToString()) && string.IsNullOrEmpty(GoodName)) || int.Parse(Amount) <= 0)
                    return false;
                var receiveDetailList = DataProvider.Ins.DB.CTPNs.Where(x => x.MAPN == ReceiveID && x.MASP == GoodID);
                if (receiveDetailList == null || receiveDetailList.Count() != 0)
                    return false;
                return true;
            }, (p) => _addListView(p)
            //{
            //foreach(CTHD ct in MyOrderDetailList)
            //{
            //    if (ct.MASP == GoodID)
            //    {
            //        ct.SOLUONG += int.Parse(Amount);
            //        foreach (SANPHAM sp in ProductList)
            //        {
            //            if (sp.MASP == GoodID)
            //                SubTotal += (double)(int.Parse(Amount) * sp.GIA);
            //        }
            //    }
            //    else
            //    {
            //        var ctpn = new CTHD();
            //        ctpn.MASP = GoodID;
            //        ctpn.SOLUONG = int.Parse(Amount);
            //        MyOrderDetailList.Add(ctpn);
            //        foreach (SANPHAM sp in ProductList)
            //        {
            //            if (sp.MASP == GoodID)
            //                SubTotal += (double)(ctpn.SOLUONG * sp.GIA);
            //        }
            //    }
            //}    
            //var ctpn = new CTPN();
            //ctpn.MASP = GoodID;
            //ctpn.SOLUONG = int.Parse(Amount);
            //bool flag = false;
            //foreach (CTPN ct in MyRecieveDetailList)
            //{
            //    if (ct.MASP == ctpn.MASP)
            //    {
            //        ct.SOLUONG += ctpn.SOLUONG;
            //        flag = true;
            //    }
            //}
            //if (flag == false)
            //    MyRecieveDetailList.Add(ctpn);
            //foreach (SANPHAM sp in ProductList)
            //{
            //    if (sp.MASP == GoodID)
            //        SubTotal += (double)(ctpn.SOLUONG * sp.GIA);
            //}



            //}
            );


            DoneCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(EmployeeName))
                    return false;
                var receiveList = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MAPN == ReceiveID);
                if (receiveList == null || receiveList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var pn = new PHIEUNHAP();
                //hd.MAHD = OrderID;
                pn.NGNHAP = DateTime.Parse(Date);
                pn.TONGTIEN = 0;

                pn.MANV = eID;

                //hd.SDT = Phone;

                foreach (NHACUNGCAP ncc in SupplierList)
                    if (ncc.TEN == SupplierName)
                    {
                        pn.MANCC = ncc.MANCC;
                        //kh.DOANHSO += SubTotal;
                    }
                pn.DELETED = false;
                DataProvider.Ins.DB.PHIEUNHAPs.Add(pn);
                DataProvider.Ins.DB.SaveChanges();
                foreach (CTPN ctpn in MyRecieveDetailList)
                {
                    ctpn.MAPN = pn.MAPN;
                    DataProvider.Ins.DB.CTPNs.Add(ctpn);
                    DataProvider.Ins.DB.SaveChanges();
                    pn.TONGTIEN = SubTotal;
                }

            });

            navBack = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));
            navDone = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));

            IdChanged = new RelayCommand<ComboBox>((p) => { return true; }, p => _changeIDValue(p));
            gNameChanged = new RelayCommand<ComboBox>((p) => { return true; }, p => _changeNameGood(p));

            nameSupplierChanged = new RelayCommand<ComboBox>((p) => { return true; }, p => _changeNameSupplier(p));

        }
        void _addListView(AddReceiveViewUC p)
        {
            int flag1 = 0;
            for (int i = 0; i < MyListView.Count(); i++)
                if (GoodID == MyListView[i].MASP)
                {
                    flag1 = 1;
                    DetailList tmp = MyListView[i];
                    tmp.SOLUONG += int.Parse(Amount);
                    MyListView[i] = tmp;
                }
            if (flag1 == 0)
            {
                DetailList dtl = new DetailList();
                dtl.MASP = GoodID;
                dtl.TENSP = GoodName;
                dtl.SOLUONG = Int32.Parse(p.AmountTb.Text);
                for (int i = 0; i < DataProvider.Ins.DB.SANPHAMs.Count(); i++)
                    if (GoodID == ProductList[i].MASP)
                    {
                        dtl.GIA = (double)ProductList[i].GIA;
                        for (int j = 0; j < DataProvider.Ins.DB.DANHMUCs.Count(); j++)
                            if (ProductList[i].MADM == TypeList[j].MADM)
                                dtl.DANHMUC = TypeList[j].TENDM;
                    }

                MyListView.Add(dtl);
            }
            p.listGood.Items.Refresh();
            p.idGood.SelectedValue = string.Empty;
            p.goodName.SelectedValue = string.Empty;

            var ctpn = new CTPN();
            ctpn.MASP = GoodID;
            ctpn.SOLUONG = int.Parse(Amount);
            bool flag = false;
            foreach (CTPN ct in MyRecieveDetailList)
            {
                if (ct.MASP == ctpn.MASP)
                {
                    ct.SOLUONG += ctpn.SOLUONG;
                    flag = true;
                }
            }
            if (flag == false)
                MyRecieveDetailList.Add(ctpn);
            foreach (SANPHAM sp in ProductList)
            {
                if (sp.MASP == GoodID)
                    SubTotal += (double)(ctpn.SOLUONG * sp.GIA);
            }
            Amount = "0";

        }

        void _changeIDValue(ComboBox p)
        {
            if (p.SelectedIndex >= 0)
                for (int i = 0; i< DataProvider.Ins.DB.SANPHAMs.Count(); i++)
                    if (ProductList[i].MASP == (int)p.SelectedValue)
                        GoodName = ProductList[i].TEN;
        }
        void _changeNameGood(ComboBox p)
        {
            if (p.SelectedIndex >= 0)
                for (int i = 0; i< DataProvider.Ins.DB.SANPHAMs.Count(); i++)
                    if (ProductList[i].TEN == (string)p.SelectedValue)
                        GoodID = ProductList[i].MASP;
        }
        void _changeNameSupplier(ComboBox p)
        {
            if (p.SelectedIndex >= 0)
                for (int i = 0; i< DataProvider.Ins.DB.NHACUNGCAPs.Count(); i++)
                {
                    if (SupplierList[i].TEN == p.SelectedValue.ToString())
                    {
                        Email = SupplierList[i].EMAIL;
                        Address = SupplierList[i].DIACHI;
                    }
                }
        }

    }
}
