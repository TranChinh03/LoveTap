using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
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

        public IEnumerable<KHACHHANG> MyFilterList
        {
            get
            {
                if (searchText == null)
                    return CustomerList;
                else
                {
                    if (findText == "Phone")
                        return CustomerList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Name")
                        return CustomerList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Date of Birth")
                        return CustomerList.Where(x => (x.NGSINH.ToString().Contains(searchText.ToUpper())));
                    else if (findText == "Adress")
                        return CustomerList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Registation Date")
                        return CustomerList.Where(x => (x.NGDK.ToString().Contains(searchText.ToUpper())));
                    else
                        return CustomerList.Where(x => x.DOANHSO >= double.Parse(searchText));
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


        public string[] BranchIDList { get; set; } = new string[DataProvider.Ins.DB.CHINHANHs.Count()];
        



        public CustomerViewModel(NavigationStore navigationStore)
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for(int i =0; i<BranchIDList.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }

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
