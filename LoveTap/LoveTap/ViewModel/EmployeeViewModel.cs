using LoveTap.Model;
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
    public class EmployeeViewModel : BaseViewModel
    {

        public static NHANVIEN EmployeeSelected { get; set; }
        public ICommand GetIdButton { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand Detail { get; set; }
        string Name;

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private string _Role;
        
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }    
        //public NHANVIEN VAITRO { get => _VAITRO; set { _VAITRO = value;
        //        if (VAITRO != null)
        //            VITRI = "Admin";
        //        else
        //            VITRI = "Staff";
        //    } }

        //private string _VITRI;
        //public string VITRI { get => _VITRI; set { _VITRI = value;} }
        public EmployeeViewModel()
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            string role =  EmployeeList[0].VAITRO.ToString();
            if (role == "True")
                Role = "Admin";
            else
                Role = "Staff";
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<EmployeeWindow>((p) => true, (p) => switchtab(p));
            Detail = new RelayCommand<EmployeeWindow>((p) => { return p.EmployeeList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }

        void switchtab(EmployeeWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.AddEmployee.Visibility=Visibility.Visible;
                        break;
                    }
            }
        }

        void _DetailCs(EmployeeWindow p)
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
            p.EmployeeDetail.Visibility= Visibility.Visible;
            //listKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            //paramater.ListViewKH.ItemsSource = listKH;
            //paramater.ListViewKH.SelectedItem = null;
            EmployeeSelected = (NHANVIEN)p.EmployeeList.SelectedItem;
        }

    }
}
