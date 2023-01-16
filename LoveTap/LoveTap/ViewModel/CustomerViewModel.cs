using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{

    public class CustomerViewModel:BaseViewModel
    {
        public ICommand navAddCustomer { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        string Name;
        public static KHACHHANG CurrentSelected { get; set; }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }

        //public struct Customer
        //{
        //    public string ten { get; set;}
        //    public string sdt { get; set; }
        //    public string dchi { get; set; }
        //    public DateTime dob { get; set; }
        //    public DateTime rd { get; set; }
        //    public double sale { get; set; }
        //}

        private List<KHACHHANG> _MyCustomerList = new List<KHACHHANG>();
        public List<KHACHHANG> MyCustomerList { get => _MyCustomerList; set { _MyCustomerList = value; OnPropertyChanged(); } }

        private List<KHACHHANG> _CustomerDOBList = new List<KHACHHANG>();
        public List<KHACHHANG> CustomerDOBList { get => _CustomerDOBList; set { _CustomerDOBList = value; OnPropertyChanged(); } }


        private List<KHACHHANG> _CustomerRDList = new List<KHACHHANG>();
        public List<KHACHHANG> CustomerRDList { get => _CustomerRDList; set { _CustomerRDList = value; OnPropertyChanged(); } }

        private List<KHACHHANG> _CustomerSaleList = new List<KHACHHANG>();
        public List<KHACHHANG> CustomerSaleList { get => _CustomerSaleList; set { _CustomerSaleList = value; OnPropertyChanged(); } }

        public IEnumerable<KHACHHANG> MyFilterList
        {
            get
            {
                if (searchText == null && sortText == null)
                    return MyCustomerList;
                else if (sortText != null && findText == "Name")
                {
                    if (sortText == "Date of Birth")
                        return CustomerDOBList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Registation Date")
                        return CustomerRDList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Sale")
                        return CustomerSaleList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                }
                else if (sortText != null && findText == "Phone")
                {
                    if (sortText == "Date of Birth")
                        return CustomerDOBList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Registation Date")
                        return CustomerRDList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Sale")
                        return CustomerSaleList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                }
                else if (sortText != null && findText == "Address")
                {
                    if (sortText == "Date of Birth")
                        return CustomerDOBList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Registation Date")
                        return CustomerRDList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Sale")
                        return CustomerSaleList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                }
                else if(sortText != null && findText==null)
                {
                    if (sortText == "Date of Birth")
                        return CustomerDOBList;
                    else if (sortText == "Registation Date")
                        return CustomerRDList;
                    else if (sortText == "Sale")
                        return CustomerSaleList;
                }
                else if(sortText == null && findText != null)
                {
                    if (findText == "Name")
                        return MyCustomerList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Phone")
                        return MyCustomerList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Address")
                        return MyCustomerList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                }    
                return MyCustomerList;
                //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));

            }
        }

        void SwapInt(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void SwapDouble (ref double a, ref double b)
        {
            double temp = a;
            a= b;
            b= temp;
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
        void Swap(ref KHACHHANG a, ref KHACHHANG b)
        {
            string sdta = a.SDT;
            string sdtb = b.SDT;
            string tena = a.HOTEN;
            string tenb = b.HOTEN;
            string dchia = a.DIACHI;
            string dchib = b.DIACHI;
            DateTime doba = (DateTime)a.NGSINH;
            DateTime dobb = (DateTime)b.NGSINH;
            DateTime rda = (DateTime)a.NGDK;
            DateTime rdb = (DateTime)b.NGDK;
            double salea = (double)a.DOANHSO;
            double saleb = (double)b.DOANHSO;
            int manva = (int)a.MANV;
            int manvb = (int)b.MANV;
            SwapString(ref sdta, ref sdtb);
            SwapString(ref tena, ref tenb);
            SwapString(ref dchia, ref dchib);
            SwapDateTime(ref doba, ref dobb);
            SwapDateTime(ref rda, ref rdb);
            SwapDouble(ref salea, ref saleb);
            SwapInt(ref manva, ref manvb);
            a.SDT = sdta;
            a.HOTEN = tena;
            a.DIACHI = dchia;
            a.NGSINH = doba;
            a.NGDK = rda;
            a.DOANHSO = salea;
            a.MANV = manva;
            b.SDT = sdtb;
            b.HOTEN = tenb;
            b.DIACHI = dchib;
            b.NGSINH = dobb;
            b.NGDK = rdb;
            b.DOANHSO = saleb;
            b.MANV = manvb;
        }

        public List<KHACHHANG> SapGiamDOB(List<KHACHHANG> list)
        {
            KHACHHANG a, b;
            List<KHACHHANG> list1 = new List<KHACHHANG>();
            foreach(KHACHHANG kh in list)
            {
                list1.Add(kh);
            }

            for(int i = 0; i < list1.Count()-1;i++)
                for(int j=i+1; j < list1.Count();j++)
                {
                    if (list1[i].NGSINH < list[j].NGSINH)
                    {
                        a = list1[i];
                        b = list1[j];
                        Swap(ref a, ref b);
                        list1[i]= a;
                        list1[j]= b;
                    }

                }
            return list1;
        }

        public List<KHACHHANG> SapGiamRD(List<KHACHHANG> list)
        {
            KHACHHANG a, b;
            List<KHACHHANG> list1 = new List<KHACHHANG>();
            foreach (KHACHHANG kh in list)
            {
                list1.Add(kh);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].NGDK < list[j].NGDK)
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

        public List<KHACHHANG> SapGiamSale(List<KHACHHANG> list)
        {
            KHACHHANG a, b;
            List<KHACHHANG> list1 = new List<KHACHHANG>();
            foreach (KHACHHANG kh in list)
            {
                list1.Add(kh);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].DOANHSO < list[j].DOANHSO)
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
        



        public CustomerViewModel(NavigationStore navigationStore)
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);

            for(int i =0; i<BranchIDList.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            
            foreach(KHACHHANG kh in CustomerList)
            {
                if (kh.DOANHSO == null)
                    kh.DOANHSO = 0;
                //Customer temp = new Customer();
                //temp.ten = kh.HOTEN;
                //temp.dchi = kh.DIACHI;
                //temp.sdt = kh.SDT;
                //temp.rd = (DateTime)kh.NGDK;
                //temp.dob = (DateTime)kh.NGSINH;
                //temp.sale = (double)kh.DOANHSO;
                MyCustomerList.Add(kh);
            }

            CustomerDOBList = SapGiamDOB(MyCustomerList);
            CustomerRDList= SapGiamRD(MyCustomerList);
            CustomerSaleList= SapGiamSale(MyCustomerList);


            navAddCustomer = new NavigationCommand<AddCustomerViewModel>(navigationStore, () => new AddCustomerViewModel(navigationStore));
            navDetail = new NavigationCommand<CustomerDetailViewModel>(navigationStore, () => new CustomerDetailViewModel(navigationStore));
            Detail = new RelayCommand<CustomerViewUC>((p) => { return p.CustomerList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));


        }
        void _DetailCs(CustomerViewUC p)
        {
            CurrentSelected =(KHACHHANG)p.CustomerList.SelectedItem;
        }

    }
}
