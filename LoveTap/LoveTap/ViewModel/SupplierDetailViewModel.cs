using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    public class SupplierDetailViewModel: BaseViewModel
    {
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
        public ICommand LoadedDetailSupplier { get; set; }

        private ObservableCollection<NHACUNGCAP> _SupplierList;
        public ObservableCollection<NHACUNGCAP> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged(); } }
        public SupplierDetailViewModel(NavigationStore navigationStore)
        {
            LoadedDetailSupplier = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                SupplierViewModel.Supplier temp = SupplierViewModel.CurrentSelected;
                SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.DELETED == false));
                foreach (NHACUNGCAP ncc in SupplierList)
                {
                    if (temp.SupplierID == ncc.MANCC)
                    {
                        ID = ncc.MANCC;
                        SupplierName = ncc.TEN;
                        Phone = ncc.SDT;
                        Address = ncc.DIACHI;
                    }
                }

            });


            DeleteCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                var supplier = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MANCC == ID).SingleOrDefault();
                supplier.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });
            navBack = new NavigationCommand<SupplierViewModel>(navigationStore, () => new SupplierViewModel(navigationStore));
            navEdit = new NavigationCommand<EditSupplierViewModel>(navigationStore, () => new EditSupplierViewModel(navigationStore));
        }

    }
}
