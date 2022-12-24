using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class OrdersViewModel : BaseViewModel
    {
        public static HOADON OrderSelected { get; set; }


        public ICommand navAddOrder { get; set; }

        public ICommand navDetail { get; set; }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public OrdersViewModel(NavigationStore navigationStore)
        {
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            navAddOrder = new NavigationCommand<AddOrdersViewModel>(navigationStore, () => new AddOrdersViewModel(navigationStore));
            navDetail = new NavigationCommand<OrderDetailViewModel>(navigationStore, () => new OrderDetailViewModel(navigationStore));
        }
        void _DetailCs(OrdersWindow p)
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
            p.OrderDetail.Visibility= Visibility.Visible;
            //listKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            //paramater.ListViewKH.ItemsSource = listKH;
            //paramater.ListViewKH.SelectedItem = null;
            OrderSelected = (HOADON)p.OrderList.SelectedItem;
        }
    }
}
