using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
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
    public class StatisticViewModel : BaseViewModel
    {
        public ICommand LoadedStatisticView { get; set; }
        public ICommand selectionChaged { get; set; }
        public string _selectedType;
        public string selectedType { get => _selectedType; set { _selectedType = value; OnPropertyChanged(); } }
        public string _NameofCard1;
        public string NameofCard1 { get => _NameofCard1; set { _NameofCard1 = value; OnPropertyChanged(); } }
        public string _TextOfCard1;
        public string TextOfCard1 { get => _TextOfCard1; set { _TextOfCard1 = value; OnPropertyChanged(); } }
        public string _NameofCard2;
        public string NameofCard2 { get => _NameofCard2; set { _NameofCard2 = value; OnPropertyChanged(); } }
        public string _TextOfCard2;
        public string TextOfCard2 { get => _TextOfCard2; set { _TextOfCard2 = value; OnPropertyChanged(); } }

        public string _NameofCard3;
        public string NameofCard3 { get => _NameofCard3; set { _NameofCard3 = value; OnPropertyChanged(); } }
        public string _TextOfCard31;
        public string TextOfCard31 { get => _TextOfCard31; set { _TextOfCard31 = value; OnPropertyChanged(); } }
        public string _TextOfCard32;
        public string TextOfCard32 { get => _TextOfCard32; set { _TextOfCard32 = value; OnPropertyChanged(); } }

        public string _NameofCard4;
        public string NameofCard4 { get => _NameofCard4; set { _NameofCard4 = value; OnPropertyChanged(); } }
        public string _TextOfCard41;
        public string TextOfCard41 { get => _TextOfCard41; set { _TextOfCard41 = value; OnPropertyChanged(); } }
        public string _TextOfCard42;
        public string TextOfCard42 { get => _TextOfCard42; set { _TextOfCard42 = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> _OrderList;
        public ObservableCollection<HOADON> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }
        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }
        private ObservableCollection<SANPHAM> _GoodList;
        public ObservableCollection<SANPHAM> GoodList { get => _GoodList; set { _GoodList = value; OnPropertyChanged(); } }

        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];
        public string[] YearList { get; set; } = new string[DataProvider.Ins.DB.HOADONs.Count()];

        public StatisticViewModel(NavigationStore navigationStore)
        {
            NameofCard1 = "TỔNG SẢN PHẨM BÁN RA";
            NameofCard2 = "TỔNG SẢN PHẨM NHẬP KHO";
            NameofCard3 = "SẢN PHẨM BÁN RA NHIỀU NHẤT";
            NameofCard4 = "THÁNG CÓ SẢN PHẨM BÁN RA NHIỀU NHẤT";

            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            OrderList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            GoodList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);

            for (int i = 0; i<DataProvider.Ins.DB.CHINHANHs.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            LoadedStatisticView = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _selectedBranch(p));

            YearList[0] = OrderList[0].NGMUA.Value.Year.ToString();
            int j = 0;
            for (int i = 1; i<DataProvider.Ins.DB.HOADONs.Count(); i++)
            {
                if (OrderList[i].NGMUA.Value.Year.ToString() != YearList[j])
                {
                    YearList[i] = OrderList[i].NGMUA.Value.Year.ToString();
                    j++;
                }
                LoadedStatisticView = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _selectedBranch(p));
            }
            selectionChaged = new RelayCommand<ComboBox>((p) => { return true; }, (p) => _selectedChaged(p));
            changeValue(0);
        }
        private string _branchText;
        public string branchText
        {
            get { return _branchText; }
            set
            {
                _branchText = value;
                OnPropertyChanged("branchText");
                OnPropertyChanged("MyFilterList");
            }
        }
        public void _selectedBranch(StatisticViewUC p)
        {
            p.cbbType.SelectedIndex = 0;
            p.cbbBranch.SelectedIndex = 0;
            p.cbbYear.SelectedIndex = 0;
        }
        public void _selectedChaged(ComboBox p)
        {
            switch (p.SelectedIndex)
            {
                case 0:
                    NameofCard1 = "TỔNG SẢN PHẨM BÁN RA";
                    NameofCard2 = "TỔNG SẢN PHẨM NHẬP KHO";
                    NameofCard3 = "SẢN PHẨM BÁN RA NHIỀU NHẤT";
                    NameofCard4 = "THÁNG CÓ SẢN PHẨM BÁN RA NHIỀU NHẤT";
                    changeValue(p.SelectedIndex);
                    break;
                case 1:
                    NameofCard1 = "TỔNG ĐƠN HÀNG BÁN RA";
                    NameofCard2 = "TỔNG ĐƠN HÀNG NHẬP VÀO";
                    NameofCard3 = "ĐƠN HÀNG CÓ TRỊ GIÁ NHIỀU NHẤT";
                    NameofCard4 = "THÁNG CÓ SỐ LƯỢNG ĐƠN HÀNG NHIỀU NHẤT";
                    changeValue(p.SelectedIndex);

                    break;
                case 2:
                    NameofCard1 = "TỔNG SỐ LƯỢNG KHÁCH HÀNG";
                    NameofCard2 = "SỐ LƯỢNG KHÁCH HÀNG MỚI";
                    NameofCard3 = "KHÁCH HÀNG CÓ DOANH SỐ CAO NHẤT";
                    NameofCard4 = "THÁNG CÓ NHIỀU KHÁCH HÀNG NHẤT";
                    changeValue(p.SelectedIndex);

                    break;
            }
        }

        void changeValue(int n)
        {
            //int card1 = 0, card2 = 0, card32 = 0, card41 = 0, card42 = 0; string card31 = "";
            //int[] maxMonth = new int[14]; maxMonth[12] = 0; maxMonth[13]= 0;
            //int[] maxGood = new int[DataProvider.Ins.DB.SANPHAMs.Count()+2]; maxGood[DataProvider.Ins.DB.SANPHAMs.Count()] = 0;

            //switch (n)
            //{
            //    case 0:
            //        for (int i = 0; i<DataProvider.Ins.DB.HOADONs.Count(); i++)
            //        {
            //            if (OrderList[i].LOAIHD == true)
            //            {
            //                for (int j = 0; j<DataProvider.Ins.DB.CTHDs.Count(); j++)
            //                    if (OrderDetailList[j].MAHD == OrderList[i].MAHD)
            //                        card1+= (int)OrderDetailList[i].SOLUONG;
            //            }
            //            else for (int j = 0; j<DataProvider.Ins.DB.CTHDs.Count(); j++)
            //                    if (OrderDetailList[j].MAHD == OrderList[i].MAHD)
            //                        card2+= (int)OrderDetailList[i].SOLUONG;
            //        }

            //        for (int i = 0; i<DataProvider.Ins.DB.SANPHAMs.Count(); i++)
            //        {
            //            int tong = 0;
            //            for (int j = 0; j<DataProvider.Ins.DB.HOADONs.Count(); j++)
            //                if (OrderList[j].LOAIHD == true)
            //                    for (int k = 0; k<DataProvider.Ins.DB.CTHDs.Count(); k++)
            //                        if (GoodList[i].MASP == OrderDetailList[k].MASP && OrderDetailList[k].MAHD == OrderList[j].MAHD)
            //                            tong++;
            //            maxGood[i] = tong;
            //        }


            //        for (int i = 0; i<DataProvider.Ins.DB.SANPHAMs.Count(); i++)
            //        {
            //            if (maxGood[i] > maxGood[DataProvider.Ins.DB.SANPHAMs.Count()])
            //            {
            //                maxGood[DataProvider.Ins.DB.SANPHAMs.Count()] = maxGood[i];
            //                maxGood[DataProvider.Ins.DB.SANPHAMs.Count()+1] = i;
            //            }
            //        }

            //        for (int i = 0; i<DataProvider.Ins.DB.SANPHAMs.Count(); i++)
            //        {
            //            if (i == maxGood[DataProvider.Ins.DB.SANPHAMs.Count()+1])
            //            {
            //                card31 = GoodList[i].TEN;
            //                break;
            //            }
            //        }

            //        card32 = maxGood[DataProvider.Ins.DB.SANPHAMs.Count()];

            //        for (int i = 0; i < 12; i++)
            //        {
            //            int tong = 0;
            //            for (int j = 0; j<DataProvider.Ins.DB.HOADONs.Count(); j++)
            //                if (i== (int)OrderList[j].NGMUA.Value.Month)
            //                    tong++;
            //            maxMonth[i] = tong;
            //        }


            //        for (int i = 0; i < 12; i++)
            //            if (maxMonth[i] > maxMonth[12])
            //            {
            //                maxMonth[12] = maxMonth[i];
            //                maxMonth[13] = i;
            //            }
            //        card41 = maxMonth[13];
            //        card42 = maxMonth[12];
            //        break;
            //    case 1:
            //        for (int i = 0; i<DataProvider.Ins.DB.HOADONs.Count(); i++)
            //        {
            //            if (OrderList[i].LOAIHD != true)
            //            {

            //            }
            //        }
            //        break;
            //    case 2:
            //        for (int i = 0; i<DataProvider.Ins.DB.HOADONs.Count(); i++)
            //        {
            //            if (OrderList[i].LOAIHD == true)
            //            {
            //                for (int j = 0; j<DataProvider.Ins.DB.CTHDs.Count(); j++)
            //                    if (OrderDetailList[j].MAHD == OrderList[i].MAHD)
            //                        card1+= (int)OrderDetailList[i].SOLUONG;
            //            }
            //        }
            //        break;

            //}
            //TextOfCard1 = card1.ToString();
            //TextOfCard2 = card2.ToString();
            //TextOfCard31 = card31;
            //TextOfCard32 = card32.ToString();
            //TextOfCard41 = "THÁNG " +  card41.ToString();
            //TextOfCard42 = card42.ToString();
        }
    }


}
