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
    public class SupplierViewModel: BaseViewModel
    {
        public ICommand navAddSupplier { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        string Name;
        public static Supplier CurrentSelected { get; set; }

        private ObservableCollection<NHACUNGCAP> _SupplierList;
        public ObservableCollection<NHACUNGCAP> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged(); } }

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

        public struct Supplier
        {
            public int SupplierID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            //public DateTime DOB { get; set; }
            //public double Total { get; set; }

        }

        private List<Supplier> _MySupplierList = new List<Supplier>();
        public List<Supplier> MySupplierList { get => _MySupplierList; set { _MySupplierList = value; OnPropertyChanged(); } }

       
        public IEnumerable<Supplier> MyFilterList
        {
            get
            {
                if (searchText == null)
                    return MySupplierList;
                else if (findText == "Name" && searchText!=null)
                {
                    return MySupplierList.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()));
                }
                else if (findText == "Email" && searchText!=null)
                {
                    return MySupplierList.Where(x => x.Email.ToUpper().Contains(searchText.ToUpper()));
                }
                return MySupplierList;
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
        void Swap(ref Supplier a, ref Supplier b)
        {
            int mancca = a.SupplierID;
            int manccb = b.SupplierID;
            //string sdta = a.Phone;
            //string sdtb = b.Phone;
            string tena = a.Name;
            string tenb = b.Name;
            string emaila = a.Email;
            string emailb = b.Email;
            //DateTime doba = (DateTime)a.DOB;
            //DateTime dobb = (DateTime)b.DOB;
            //double salea = (double)a.Total;
            //double saleb = (double)b.Total;
            SwapInt(ref mancca, ref manccb);
            SwapString(ref tena, ref tenb);
            SwapString(ref emaila, ref emailb);
            //SwapDateTime(ref doba, ref dobb);
            //SwapDouble(ref salea, ref saleb);
            a.SupplierID = mancca;
            b.SupplierID = manccb;
            a.Name = tena;
            b.Name = tenb;
            a.Email = emaila;
            b.Email = emailb;
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

        //private string _sortText;
        //public string sortText
        //{
        //    get { return _sortText; }
        //    set
        //    {
        //        _sortText = value;
        //        OnPropertyChanged("sortText");
        //        OnPropertyChanged("MyFilterList");
        //    }
        //}

        //private string _branchText;
        //public string branchText
        //{
        //    get { return _branchText; }
        //    set
        //    {
        //        _branchText = value;
        //        OnPropertyChanged("branchText");
        //        OnPropertyChanged("MyFilterList");
        //    }
        //}


        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];




        public SupplierViewModel(NavigationStore navigationStore)
        {
            SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.DELETED == false));
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);

            for (int i = 0; i < BranchIDList.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            Supplier temp = new Supplier();

            foreach (NHACUNGCAP ncc in SupplierList)
            {
               
                temp.Name = ncc.TEN;
                temp.SupplierID = ncc.MANCC;
                temp.Email = ncc.EMAIL;
                MySupplierList.Add(temp);
            }



            navAddSupplier = new NavigationCommand<AddSupplierViewModel>(navigationStore, () => new AddSupplierViewModel(navigationStore));
            navDetail = new NavigationCommand<SupplierDetailViewModel>(navigationStore, () => new SupplierDetailViewModel(navigationStore));
            Detail = new RelayCommand<SupplierViewUC>((p) => { return p.SupplierList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));


        }
        void _DetailCs(SupplierViewUC p)
        {
            CurrentSelected = (Supplier)p.SupplierList.SelectedItem;
        }
    }
}
