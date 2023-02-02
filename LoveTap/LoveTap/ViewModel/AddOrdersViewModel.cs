using LoveTap.Model;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    public class AddOrdersViewModel : BaseViewModel
    {
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        //public ICommand LoadedAddOrder { get; set; }
        public ICommand IdChanged { get; set; }
        public ICommand gNameChanged { get; set; }
        public ICommand phoneChanged { get; set; }
        public ICommand nameChanged { get; set; }
        // public ICommand updateListView { get; set; }

        private int _OrderID;
        public int OrderID { get => _OrderID; set { _OrderID = value; OnPropertyChanged(); } }
        private int _eID;
        public int eID { get => _eID; set { _eID = value; OnPropertyChanged(); } }

        private string _Date;
        public string Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }

        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }

        private int _GoodID;
        public int GoodID { get => _GoodID; set { _GoodID = value; OnPropertyChanged(); } }

        private string _GoodName;
        public string GoodName { get => _GoodName; set { _GoodName = value; OnPropertyChanged(); } }


        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }



        private double _SubTotal;
        public double SubTotal { get => _SubTotal; set { _SubTotal = value; OnPropertyChanged(); } }


        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }


        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        //private DateTime _Now;
        //public DateTime Now { get => _Now; set { _Now = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        private ObservableCollection<DANHMUC> _TypeList;
        public ObservableCollection<DANHMUC> TypeList { get => _TypeList; set { _TypeList = value; OnPropertyChanged(); } }

        public int[] GoodIDList { get; set; } = new int[DataProvider.Ins.DB.SANPHAMs.Count()];
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.SANPHAMs.Count()];

        public string[] GoodNameList { get; set; } = new string[DataProvider.Ins.DB.SANPHAMs.Count()];

        public struct DetailList
        {
            public int MASP { get; set; }
            public string TENSP { get; set; }
            public string DANHMUC { get; set; }
            public double GIA { get; set; }
            public int SOLUONG { get; set; }
        }

        private List<CTHD> _MyOrderDetailList = new List<CTHD>();
        public List<CTHD> MyOrderDetailList { get { return _MyOrderDetailList; } set { _MyOrderDetailList = value; OnPropertyChanged(); } }

        private List<DetailList> _MyListView = new List<DetailList>();
        public List<DetailList> MyListView { get => _MyListView; set { _MyListView = value; OnPropertyChanged(); } }
        public AddOrdersViewModel(NavigationStore navigationStore)
        {
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            TypeList = new ObservableCollection<DANHMUC>(DataProvider.Ins.DB.DANHMUCs);

            eID = MainViewModel.ID;
            for (int i = 0; i < EmployeeList.Count; i++)
                if (EmployeeList[i].MANV == eID)
                {
                    EmployeeName = EmployeeList[i].HOTEN;
                    Branch = (int)EmployeeList[i].MACN;
                }

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

            if (GoodID.ToString() != "")
            {
                foreach (SANPHAM sp in ProductList)
                {
                    if (sp.MASP == GoodID)
                        GoodName = sp.TEN;
                }
            }

            AddCommand = new RelayCommand<CreateOrderUC>((p) =>
            {
                if ((string.IsNullOrEmpty(GoodID.ToString())&& string.IsNullOrEmpty(GoodName)) || int.Parse(Amount) <= 0)
                    return false;
                var orderDetailList = DataProvider.Ins.DB.CTHDs.Where(x => x.MAHD == OrderID && x.MASP == GoodID);
                if (orderDetailList == null || orderDetailList.Count() != 0)
                    return false;
                return true;
            }, (p) => _addListView(p)

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
            //        var cthd = new CTHD();
            //        cthd.MASP = GoodID;
            //        cthd.SOLUONG = int.Parse(Amount);
            //        MyOrderDetailList.Add(cthd);
            //        foreach (SANPHAM sp in ProductList)
            //        {
            //            if (sp.MASP == GoodID)
            //                SubTotal += (double)(cthd.SOLUONG * sp.GIA);
            //        }
            //    }
            //}    
            //

            //var cthd = new CTHD();
            //cthd.MASP = GoodID;
            //cthd.SOLUONG = int.Parse(Amount);
            //bool flag = false;
            //foreach (CTHD ct in MyOrderDetailList)
            //{
            //    if (ct.MASP == cthd.MASP)
            //    {
            //        ct.SOLUONG += cthd.SOLUONG;
            //        flag = true;
            //    }
            //}
            //if (flag == false)
            //    MyOrderDetailList.Add(cthd);
            //foreach (SANPHAM sp in ProductList)
            //{
            //    if (sp.MASP == GoodID)
            //        SubTotal += (double)(cthd.SOLUONG * sp.GIA);
            //}


            );


            DoneCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(EmployeeName))
                    return false;
                var orderList = DataProvider.Ins.DB.HOADONs.Where(x => x.MAHD == OrderID);
                if (orderList == null || orderList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var hd = new HOADON();
                //hd.MAHD = OrderID;
                hd.NGMUA = DateTime.Parse(Date);
                hd.TONGTIEN = 0;

                hd.MANV = eID;

                //hd.SDT = Phone;

                foreach (KHACHHANG kh in CustomerList)
                    if (kh.SDT == Phone)
                    {
                        hd.MAKH = kh.MAKH;
                        //kh.DOANHSO += SubTotal;
                    }
                hd.MACN = Branch;
                hd.DELETED = false;
                DataProvider.Ins.DB.HOADONs.Add(hd);
                DataProvider.Ins.DB.SaveChanges();
                foreach (CTHD cthd in MyOrderDetailList)
                {
                    cthd.MAHD = hd.MAHD;
                    DataProvider.Ins.DB.CTHDs.Add(cthd);
                    DataProvider.Ins.DB.SaveChanges();
                    hd.TONGTIEN = SubTotal;
                }

            });


            navBack = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navDone = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));

            IdChanged = new RelayCommand<ComboBox>((p) => { return true; }, p => _changeIDValue(p));
            gNameChanged = new RelayCommand<ComboBox>((p) => { return true; }, p => _changeNameGood(p));

            phoneChanged = new RelayCommand<TextBox>((p) => { return true; }, p => _changePhoneCustomer(p));
            nameChanged = new RelayCommand<TextBox>((p) => { return true; }, p => _changeNameCustomer(p));
            //updateListView = new RelayCommand<OrderViewUC>((p) => { return true; }, p => _updateListView(p));
        }

        //void _updateListView (OrderViewUC p)
        //{
        //    p.OrderList.Items.Refresh();
        //}

        void _addListView(CreateOrderUC p)
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
            p.idGood.SelectedValue = "";
            p.goodName.Text = "";

            var cthd = new CTHD();
            cthd.MASP = GoodID;
            cthd.SOLUONG = int.Parse(Amount);
            bool flag = false;
            foreach (CTHD ct in MyOrderDetailList)
            {
                if (ct.MASP == cthd.MASP)
                {
                    ct.SOLUONG += cthd.SOLUONG;
                    flag = true;
                }
            }
            if (flag == false)
                MyOrderDetailList.Add(cthd);
            foreach (SANPHAM sp in ProductList)
            {
                if (sp.MASP == GoodID)
                    SubTotal += (double)(cthd.SOLUONG * sp.GIA);
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
        void _changePhoneCustomer(TextBox p)
        {
            for (int i = 0; i< DataProvider.Ins.DB.KHACHHANGs.Count(); i++)
            {
                if (CustomerList[i].SDT == p.Text)
                {
                    CustomerName = CustomerList[i].HOTEN;
                    return;
                }
                else CustomerName = "";
            }
        }
        void _changeNameCustomer(TextBox p)
        {
            for (int i = 0; i< DataProvider.Ins.DB.KHACHHANGs.Count(); i++)
            {
                if (CustomerList[i].HOTEN == p.Text)
                {
                    Phone = CustomerList[i].SDT;
                    return;
                }
                //else Phone = "";
            }
        }
    }
}
