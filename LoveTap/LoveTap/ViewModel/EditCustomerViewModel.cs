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
    internal class EditCustomerViewModel : BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public ICommand EditCommand { get; set; }
        private string _textType;
        public string textType { get => _textType; set { _textType = value; OnPropertyChanged(); } }
        private string _bgType;
        public string bgType { get => _bgType; set { _bgType = value; OnPropertyChanged(); } }
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
        public ICommand LoadedEditCustomerDetail { get; set; }
        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        public string[] BranchIDList { get; set; } = new string[DataProvider.Ins.DB.CHINHANHs.Count()];


        public EditCustomerViewModel(NavigationStore navigationStore)
        {
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for (int i = 0; i < DataProvider.Ins.DB.CHINHANHs.Count(); i++)

            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            LoadedEditCustomerDetail = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                KHACHHANG x = CustomerViewModel.CurrentSelected;
                if (x != null)
                {
                    CusName = x.HOTEN;
                    Phone = x.SDT;
                    DOB = x.NGSINH;
                    Address = x.DIACHI;
                    RegistDate = x.NGDK;
                    Sale = x.DOANHSO;
                    Type = "Đồng";
                    if (Sale > 2000000 && Sale <= 5000000)
                    {
                        Type = "Bronz";
                        bgType = "#eec170";
                        textType = "#E2711D";
                    }

                    else if (Sale <= 10000000)
                    {
                        Type = "Silver";
                        bgType = "#dee2e6";
                        textType = "#4f5d75";
                    }

                    else if (Sale > 10000000)
                    {
                        Type = "Gold";
                        bgType = "#fff2b2";
                        textType = "#ffaa00";
                    }
                }

            })
            {

            };
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(CusName) || string.IsNullOrEmpty(Phone) || DOB==null  || string.IsNullOrEmpty(Address) || RegistDate==null)
                    return false;

                //var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                //if (displayList == null || displayList.Count() != 0)
                //    return false;

                return true;

            }, (p) =>
            {
                var customer = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.SDT == Phone).SingleOrDefault();
                customer.HOTEN = CusName;
                //customer.SDT = Phone;
                //foreach (KHACHHANG kh in CustomerList)
                //{
                //    if (kh.SDT == Phone)
                //        customer.SDT = kh.SDT;
                //}
                customer.NGSINH = DOB;
                customer.DIACHI = Address;
                customer.NGDK = RegistDate;
                customer.DOANHSO = Sale;
                Type = "Đồng";
                if (Sale > 2000000 && Sale <= 5000000)
                    Type = "Bạc";
                else if (Sale <= 10000000)
                    Type = "Vàng";
                else if (Sale > 10000000)
                    Type = "Kim cương";

                //DataProvider.Ins.DB.Entry(customer).State = System.Data.Entity.EntityState.Detached;
                DataProvider.Ins.DB.SaveChanges();

            });

            navDone = new NavigationCommand<CustomerDetailViewModel>(navigationStore, () => new CustomerDetailViewModel(navigationStore));
            navBack = new NavigationCommand<CustomerDetailViewModel>(navigationStore, () => new CustomerDetailViewModel(navigationStore));
        }
    }
}
