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

        private List<Receive> _ReceiveDateList = new List<Receive>();
        public List<Receive> ReceiveDateList { get => _ReceiveDateList; set { _ReceiveDateList = value; OnPropertyChanged(); } }


        private List<Receive> _ReceiveSaleList = new List<Receive>();
        public List<Receive> ReceiveSaleList { get => _ReceiveSaleList; set { _ReceiveSaleList = value; OnPropertyChanged(); } }

        public IEnumerable<Receive> MyFilterList
        {
            get
            {
                if (searchText == null && sortText == null)
                    return MyReceiveList;
                else if (sortText != null && findText == "Name")
                {
                    if (sortText == "Date")
                        return ReceiveDateList.Where(x => (x.Name.ToUpper().Contains(searchText.ToUpper())));
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Total")
                        return ReceiveSaleList.Where(x => (x.Name.ToUpper().Contains(searchText.ToUpper())));
                }
                else if (sortText != null && findText == "Email")
                {
                    if (sortText == "Date")
                        return ReceiveDateList.Where(x => (x.Email.ToUpper().Contains(searchText.ToUpper())));
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Total")
                        return ReceiveSaleList.Where(x => (x.Email.ToUpper().Contains(searchText.ToUpper())));
                }
                //else if (sortText != null && findText == "Address")
                //{
                //    if (sortText == "Date of Birth")
                //        return CustomerDOBList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //    else if (sortText == "Registation Date")
                //        return CustomerRDList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //    else if (sortText == "Sale")
                //        return CustomerSaleList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //}
                else if (sortText != null && findText == null)
                {
                    if (sortText == "Date")
                        return ReceiveDateList;
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList;
                    else if (sortText == "Total")
                        return ReceiveSaleList;
                }
                else if (sortText == null && findText != null)
                {
                    if (findText == "Name")
                        return MyReceiveList.Where(x => (x.Name.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Email")
                        return MyReceiveList.Where(x => (x.Email.ToUpper().Contains(searchText.ToUpper())));
                    //else if (findText == "Address")
                    //    return MyCustomerList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                }
                return MyReceiveList;
                //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));

            }
        }

        void SwapInt(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void SwapDouble(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }

        void SwapString(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        void SwapDateTime(ref DateTime a, ref DateTime b)
        {
            DateTime temp = a;
            a = b;
            b = temp;
        }
        void Swap(ref Receive a, ref Receive b)
        {
            int mahda = a.ID;
            int mahdb = b.ID;
            int mancca = a.MANCC;
            int manccb  = b.MANCC;
            string tena = a.Name;
            string tenb = b.Name;
            string emaila = a.Email;
            string emailb = b.Email;
            DateTime datea = (DateTime)a.Date;
            DateTime dateb = (DateTime)b.Date;
            double salea = (double)a.Total;
            double saleb = (double)b.Total;
            int manva = a.MANV;
            int manvb = b.MANV;
            SwapInt(ref mahda, ref mahdb);
            SwapInt(ref mancca, ref manccb);
            SwapInt(ref manva, ref manvb);
            SwapString(ref tena, ref tenb);
            SwapString(ref emaila, ref emailb);
            SwapDateTime(ref datea, ref dateb);
            SwapDouble(ref salea, ref saleb);
            a.ID = mahda;
            a.MANCC = mancca;
            a.MANV = manva;
            a.Name = tena;
            a.Email = emaila;
            a.Date = datea;
            a.Total = salea;
            b.ID = mahdb;
            b.MANCC = manccb;
            b.MANV = manvb;
            b.Name = tenb;
            b.Email = emailb;
            b.Date = dateb;
            b.Total = saleb;
        }

        public List<Receive> SapGiamDate(List<Receive> list)
        {
            Receive a, b;
            List<Receive> list1 = new List<Receive>();
            foreach (Receive hd in list)
            {
                list1.Add(hd);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].Date < list[j].Date)
                    {
                        a = list1[i];
                        b = list1[j];
                        Swap(ref a, ref b);
                        list1[i] = a;
                        list1[j] = b;
                    }

                }
            return list1;
        }

        //public List<KHACHHANG> SapGiamRD(List<KHACHHANG> list)
        //{
        //    KHACHHANG a, b;
        //    List<KHACHHANG> list1 = new List<KHACHHANG>();
        //    foreach (KHACHHANG kh in list)
        //    {
        //        list1.Add(kh);
        //    }

        //    for (int i = 0; i < list1.Count() - 1; i++)
        //        for (int j = i + 1; j < list1.Count(); j++)
        //        {
        //            if (list1[i].NGDK < list[j].NGDK)
        //            {
        //                a = list1[i];
        //                b = list1[j];
        //                Swap(ref a, ref b);
        //                list1[i] = a;
        //                list1[j] = b;
        //            }

        //        }
        //    return list1;
        //}

        public List<Receive> SapGiamSale(List<Receive> list)
        {
            Receive a, b;
            List<Receive> list1 = new List<Receive>();
            foreach (Receive hd in list)
            {
                list1.Add(hd);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].Total < list[j].Total)
                    {
                        a = list1[i];
                        b = list1[j];
                        Swap(ref a, ref b);
                        list1[i] = a;
                        list1[j] = b;
                    }

                }
            return list1;
        }



        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("searchText");
                OnPropertyChanged("MyFilterList");
            }
        }

        private string _findText;
        public string findText
        {
            get { return _findText; }
            set
            {
                _findText = value;
                OnPropertyChanged("findText");
                OnPropertyChanged("MyFilterList");
            }
        }

        private string _sortText;
        public string sortText
        {
            get { return _sortText; }
            set
            {
                _sortText = value;
                OnPropertyChanged("sortText");
                OnPropertyChanged("MyFilterList");
            }
        }

        private string _branchText;
        public string branchText
        {
            get { return _branchText; }
            set
            {
                _branchText = value;
                OnPropertyChanged("branchText");
                OnPropertyChanged("MyFilterList");
            }
        }


        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];


        public ReceiveOrderViewModel(NavigationStore navigationStore)
        {
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.DELETED == false));
            SuplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.DELETED == false));

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

            ReceiveSaleList = SapGiamSale(MyReceiveList);
            ReceiveDateList = SapGiamDate(MyReceiveList);
        }
        void _DetailCs(ReceiveOrdViewUC p)
        {
            ReceiveSelected = (Receive)p.ReceiveList.SelectedItem;

        }
    }
}
