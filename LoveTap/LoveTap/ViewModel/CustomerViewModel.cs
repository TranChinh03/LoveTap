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
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class CustomerViewModel:BaseViewModel
    {
        public ICommand GetIdButton { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; } }
        string Name;
        public static KHACHHANG CurrentSelected { get; set; }
        public CustomerViewModel(NavigationStore navigationStore)
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
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
