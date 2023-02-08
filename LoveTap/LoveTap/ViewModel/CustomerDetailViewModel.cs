using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.Commands;
using System.Collections.ObjectModel;

namespace LoveTap.ViewModel
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        private string _textType;
        public string textType { get => _textType; set { _textType = value; OnPropertyChanged(); } }
        private string _bgType;
        public string bgType { get => _bgType; set { _bgType = value; OnPropertyChanged(); } }
        private string _bgProgress;
        public string bgProgress { get => _bgProgress; set { _bgProgress = value; OnPropertyChanged(); } }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

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

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }
        public ICommand navEdit { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand navBack { get; set; }
        public ICommand LoadedDetailCustomer { get; set; }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }
        public CustomerDetailViewModel(NavigationStore navigationStore) {
            LoadedDetailCustomer = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                CustomerViewModel.Customer temp = CustomerViewModel.CurrentSelected;
                CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
                foreach(KHACHHANG kh in CustomerList)
                {
                    if (temp.CustomerID == kh.MAKH)
                    {
                        ID = kh.MAKH;
                        CusName = kh.HOTEN;
                        Phone = kh.SDT;
                        DOB = kh.NGSINH;
                        Address = kh.DIACHI;
                        RegistDate = kh.NGDK;
                        Sale = kh.DOANHSO;
                        Branch = (int)kh.MACN;
                        Type = "Đồng";
                        if (Sale <= 5000000)
                        {
                            Type = "Bronz";
                            bgType = "#eec170";
                            textType = "#E2711D";
                            bgProgress = "#e2711d";
                        }

                        else if (Sale > 5000000 && Sale <= 10000000)
                        {
                            Type = "Silver";
                            bgType = "#dee2e6";
                            textType = "#4f5d75";
                            textType = "#4f5d75";
                            bgProgress = "#adb5bd";
                        }

                        else if (Sale > 10000000)
                        {
                            Type = "Gold";
                            bgType = "#fff2b2";
                            textType = "#ffaa00";
                            bgProgress="#ffe97f";
                        }
                    }
                }

            });


            DeleteCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                var customer = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == ID).SingleOrDefault();
                customer.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });
            navBack = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navEdit = new NavigationCommand<EditCustomerViewModel>(navigationStore, () => new EditCustomerViewModel(navigationStore));
        }


    }
}
