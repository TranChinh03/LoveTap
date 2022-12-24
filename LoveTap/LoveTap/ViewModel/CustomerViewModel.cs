using LoveTap.Model;
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
    public class CustomerViewModel: BaseViewModel
    {
        public ICommand GetIdButton { get; set; }

        public ICommand SwitchTab { get; set; }
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
        



        public CustomerViewModel()
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for(int i =0; i<BranchIDList.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<CustomerWindow>((p) => true, (p) => switchtab(p));
            Detail = new RelayCommand<CustomerWindow>((p) => { return p.CustomerList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));

        }
        void switchtab(CustomerWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.AddCustomer.Visibility=Visibility.Visible;
                        break;
                    }
            }
        }

        void _DetailCs(CustomerWindow p)
        {
            //CODE CỦA ANH KHÔI Á. ĐỂ LẠI MÍ PÀ CÓ XÀI GÌ XÀI

            //DetailCustomerView detailCustomerView = new DetailCustomerView();
            //KHACHHANG temp = (KHACHHANG)paramater.ListViewKH.SelectedItem;
            //detailCustomerView.MaKH.Text = temp.MAKH;
            //detailCustomerView.TenKH.Text = temp.HOTEN;
            //detailCustomerView.SDT.Text = temp.SDT;
            //detailCustomerView.GT.Text = temp.GIOITINH;
            //detailCustomerView.DC.Text = temp.DCHI;
            //int doanhso = 0;
            //foreach (HOADON a in DataProvider.Ins.DB.HOADONs)
            //{
            //    if (a.MAKH == temp.MAKH)
            //        doanhso += a.TRIGIA;
            //}
            //detailCustomerView.DS.Text = String.Format("{0:0,0}", doanhso) + " VND"; ;
            //string hang = "Đồng";
            //if (doanhso > 2000000 && doanhso <= 5000000)
            //    hang = "Bạc";
            //else if (doanhso > 5000000 && doanhso <= 10000000)
            //    hang = "Vàng";
            //else if (doanhso>10000000)
            //    hang = "Kim cương";
            //detailCustomerView.Rank.Text = hang;
            //detailCustomerView.ShowDialog();
            p.DetailCustomer.Visibility= Visibility.Visible;
            //listKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            //paramater.ListViewKH.ItemsSource = listKH;
            //paramater.ListViewKH.SelectedItem = null;
            CurrentSelected = (KHACHHANG)p.CustomerList.SelectedItem;
        }
    }
}
