using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LoveTap.Stores;
using LoveTap.Commands;
using LoveTap.Model;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Policy;
using System.Data;
using System.Windows.Media;


namespace LoveTap.ViewModel
{
    public class EditSupplierViewModel: BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public ICommand EditCommand { get; set; }
        private string _textType;
        public string textType { get => _textType; set { _textType = value; OnPropertyChanged(); } }
        private string _bgType;
        public string bgType { get => _bgType; set { _bgType = value; OnPropertyChanged(); } }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _SupplierName;
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
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
        public ICommand LoadedEditSupplierDetail { get; set; }
        private ObservableCollection<NHACUNGCAP> _SupplierList;
        public ObservableCollection<NHACUNGCAP> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged(); } }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];


        public EditSupplierViewModel(NavigationStore navigationStore)
        {
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for (int i = 0; i < DataProvider.Ins.DB.CHINHANHs.Count(); i++)

            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.DELETED == false));
            LoadedEditSupplierDetail = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                SupplierViewModel.Supplier temp = SupplierViewModel.CurrentSelected;
                ID = temp.SupplierID;
                foreach (NHACUNGCAP ncc in SupplierList)
                {
                    if (temp.SupplierID == ncc.MANCC)
                    {
                        SupplierName = ncc.TEN;
                        Phone = ncc.SDT;
                        Email = ncc.EMAIL;
                        Address = ncc.DIACHI;
                       
                    }
                }
            })
            {

            };
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address) || RegistDate == null)
                    return false;

                //var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                //if (displayList == null || displayList.Count() != 0)
                //    return false;

                return true;

            }, (p) =>
            {
                var supplier = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MANCC == ID).SingleOrDefault();
                supplier.TEN = SupplierName;
                //customer.SDT = Phone;
                //foreach (KHACHHANG kh in CustomerList)
                //{
                //    if (kh.SDT == Phone)
                //        customer.SDT = kh.SDT;
                //}
                supplier.SDT = Phone;
                supplier.DIACHI = Address;
                supplier.EMAIL = Email;

                //DataProvider.Ins.DB.Entry(customer).State = System.Data.Entity.EntityState.Detached;
                DataProvider.Ins.DB.SaveChanges();

            });

            navDone = new NavigationCommand<SupplierDetailViewModel>(navigationStore, () => new SupplierDetailViewModel(navigationStore));
            navBack = new NavigationCommand<SupplierDetailViewModel>(navigationStore, () => new SupplierDetailViewModel(navigationStore));
        }
    }
}
