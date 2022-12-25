using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class GoodsViewModel : BaseViewModel
    {
        public ICommand navDetail { get; set; }
        public ICommand navAddGood { get; set; }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }
        private string _CusName;
        public string CusName { get => _CusName; set { _CusName = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _DOB;
        public Nullable<System.DateTime> DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }
        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _RegistDate;
        public Nullable<System.DateTime> RegistDate { get => _RegistDate; set { _RegistDate = value; OnPropertyChanged(); } }
        private Nullable<double> _Sale;
        public Nullable<double> Sale { get => _Sale; set { _Sale = value; OnPropertyChanged(); } }
        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }

        //private List<SANPHAM> _MyFilteredList = new List<SANPHAM>();
        //public List<SANPHAM> MyFilterList { get => _MyFilteredList; set { _MyFilteredList = value; } }
        public IEnumerable<SANPHAM> MyFilterList
        {
            get
            {
                if (searchText == null)
                    return ProductList;
                else
                {
                    if(sortText == "Product Name")
                        return ProductList.Where(x => (x.TEN.ToUpper().Contains(searchText.ToUpper())));
                    else if(sortText == "Product ID")
                        return ProductList.Where(x => (x.MASP.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Category")
                        return ProductList.Where(x => (x.MADM.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Details")
                        return ProductList.Where(x => (x.CHITIET.ToUpper().Contains(searchText.ToUpper())));
                    else
                        return ProductList.Where(x => x.GIA >= double.Parse(searchText));
                }
                    //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));
                    
            }
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

        public GoodsViewModel(NavigationStore navigationStore)

        {
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            navAddGood = new NavigationCommand<AddGoodViewModel>(navigationStore, () => new AddGoodViewModel(navigationStore));
            navDetail = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
        }

    }
}
