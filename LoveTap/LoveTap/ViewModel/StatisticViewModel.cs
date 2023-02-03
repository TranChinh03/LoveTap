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
        public static int cbbTypeSlted = 0;
        public int _cbbTypeIndex;
        public int cbbTypeIndex { get => _cbbTypeIndex; set { _cbbTypeIndex = value; OnPropertyChanged(); } }
        public int _cbbBranchValue;
        public int cbbBranchValue { get => _cbbBranchValue; set { _cbbBranchValue = value; OnPropertyChanged(); } }
        public int _cbbBranchIndex;
        public int cbbBranchIndex { get => _cbbBranchIndex; set { _cbbBranchIndex = value; OnPropertyChanged(); } }
        public string _cbbYearValue;
        public string cbbYearValue { get => _cbbYearValue; set { _cbbYearValue = value; OnPropertyChanged(); } }
        public int _cbbYearIndex;
        public int cbbYearIndex { get => _cbbYearIndex; set { _cbbYearIndex = value; OnPropertyChanged(); } }
        public ICommand LoadedStatisticView { get; set; }
        public ICommand cbb1Changed { get; set; }
        public ICommand cbb2Changed { get; set; }
        public ICommand cbb3Changed { get; set; }
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
        public string _iconType1;
        public string iconType1 { get => _iconType1; set { _iconType1 = value; OnPropertyChanged(); } }
        public string _iconType2;
        public string iconType2 { get => _iconType2; set { _iconType2 = value; OnPropertyChanged(); } }
        public string _typeText;
        public string typeText { get => _typeText; set { _typeText = value; OnPropertyChanged(); } }
        public string _typeText2;
        public string typeText2 { get => _typeText2; set { _typeText2 = value; OnPropertyChanged(); } }
        public string _colorType;
        public string colorType { get => _colorType; set { _colorType = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> _OrderList;
        public ObservableCollection<HOADON> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }
        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUNHAP> _ReceiveList;
        public ObservableCollection<PHIEUNHAP> ReceiveList { get => _ReceiveList; set { _ReceiveList = value; OnPropertyChanged(); } }
        private ObservableCollection<CTPN> _ReceiveDetailList;
        public ObservableCollection<CTPN> ReceiveDetailList { get => _ReceiveDetailList; set { _ReceiveDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _GoodList;
        public ObservableCollection<SANPHAM> GoodList { get => _GoodList; set { _GoodList = value; OnPropertyChanged(); } }
        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];
        public string[] YearList { get; set; } = new string[DataProvider.Ins.DB.HOADONs.Count()];

        public StatisticViewModel(NavigationStore navigationStore)
        {
            //NameofCard1 = "TỔNG SẢN PHẨM BÁN RA";
            //NameofCard2 = "TỔNG SẢN PHẨM NHẬP KHO";
            //NameofCard3 = "SẢN PHẨM BÁN RA NHIỀU NHẤT";
            //NameofCard4 = "THÁNG CÓ SẢN PHẨM BÁN RA NHIỀU NHẤT";
            iconType2 = "CalendarMonth";
            typeText2= "M";

            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            OrderList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            ReceiveDetailList = new ObservableCollection<CTPN>(DataProvider.Ins.DB.CTPNs);
            GoodList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

            for (int i = 0; i<DataProvider.Ins.DB.CHINHANHs.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            //LoadedStatisticView = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _selectedBranch(p));

            YearList[0] = OrderList[0].NGMUA.Value.Year.ToString();
            int j = 0;
            for (int i = 1; i<DataProvider.Ins.DB.HOADONs.Count(); i++)
            {
                if (OrderList[i].NGMUA.Value.Year.ToString() != YearList[j])
                {
                    YearList[j+1] = OrderList[i].NGMUA.Value.Year.ToString();
                    j++;
                }
            }
            int flag = 0;
            for (int i = 0; i<DataProvider.Ins.DB.PHIEUNHAPs.Count(); i++)
            {
                for (int k = 1; k<YearList.Count(); k++)
                    if (ReceiveList[i].NGNHAP.Value.Year.ToString() == YearList[k])
                    {
                        flag = 1;
                    }
                if (flag == 0)
                {
                    YearList[j+1] = ReceiveList[i].NGNHAP.Value.Year.ToString();
                    j++;
                }
            }
            cbb1Changed = new RelayCommand<ComboBox>((p) => { return true; }, (p) => _cbbTypeChanged(p.SelectedIndex));
            cbb2Changed = new RelayCommand<ComboBox>((p) => { return true; }, (p) => _cbbBranchChanged(p));
            cbb3Changed = new RelayCommand<ComboBox>((p) => { return true; }, (p) => _cbbYearChanged(p));
            //LoadedStatisticView = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => Initialized(p));

            cbbTypeIndex = cbbTypeSlted;
            cbbTypeSlted = 0;
            cbbBranchIndex= 0;
            cbbYearIndex= 0;

            cbbBranchValue= BranchIDList[0];
            cbbYearValue = YearList[0];
            _cbbTypeChanged(cbbTypeIndex);
        }
        //private string _branchText;
        //public string branchText
        //{
        //    get { return _branchText; }
        //    set
        //    {
        //        _branchText = value;
        //        OnPropertyChanged("branchText");
        //        OnPropertyChanged("MyFilterList");
        //    }
        //}
        //public void Initialized(StatisticViewUC p)
        //{
        //    //p.cbbType.SelectedIndex = cbbTypeIndex;
        //    //cbbTypeIndex=0;
        //    //p.cbbBranch.SelectedIndex = 0;
        //    //p.cbbYear.SelectedIndex = 0;

        //    //_cbbTypeChanged(p.cbbType.SelectedIndex);     
        //}
        public void _cbbTypeChanged(int i)
        {
            cbbTypeIndex = i;
            switch (i)
            {
                case 0:
                    NameofCard1 = "TỔNG SẢN PHẨM BÁN RA";
                    NameofCard2 = "TỔNG SẢN PHẨM NHẬP KHO";
                    NameofCard3 = "SẢN PHẨM BÁN RA NHIỀU NHẤT";
                    NameofCard4 = "THÁNG CÓ SẢN PHẨM BÁN RA NHIỀU NHẤT";
                    iconType1 = "CartCheck";
                    typeText = "G";
                    colorType = "LightGreen";
                    changeValue(i);
                    break;
                case 1:
                    NameofCard1 = "TỔNG ĐƠN HÀNG BÁN RA";
                    NameofCard2 = "TỔNG ĐƠN HÀNG NHẬP VÀO";
                    NameofCard3 = "ĐƠN HÀNG CÓ TRỊ GIÁ CAO NHẤT";
                    NameofCard4 = "THÁNG CÓ SỐ LƯỢNG ĐƠN HÀNG NHIỀU NHẤT";
                    iconType1 = "ReceiptTextCheck";
                    typeText = "O";
                    colorType = "CornflowerBlue";
                    changeValue(i);

                    break;
                case 2:
                    NameofCard1 = "TỔNG SỐ LƯỢNG KHÁCH HÀNG";
                    NameofCard2 = "SỐ LƯỢNG KHÁCH HÀNG MỚI";
                    NameofCard3 = "KHÁCH HÀNG CÓ DOANH SỐ CAO NHẤT";
                    NameofCard4 = "THÁNG CÓ NHIỀU KHÁCH HÀNG NHẤT";
                    iconType1 = "AccountMultiple";
                    typeText = "C";
                    colorType ="Orange";
                    changeValue(i);
                    break;
            }
        }
        void _cbbBranchChanged(ComboBox p)
        {
            if (p.SelectedIndex<0) return;
            cbbBranchIndex = p.SelectedIndex;
            cbbBranchValue = (int)p.SelectedValue;
            changeValue(cbbTypeIndex);
        }
        void _cbbYearChanged(ComboBox p)
        {
            if (p.SelectedIndex<0) return;
            cbbYearIndex = p.SelectedIndex;
            cbbYearValue = p.SelectedValue.ToString();
            changeValue(cbbTypeIndex);
        }
        void changeValue(int n)
        {
            int gListlength = DataProvider.Ins.DB.SANPHAMs.Count();
            int oListlength = DataProvider.Ins.DB.HOADONs.Count();
            int oDetailListlength = DataProvider.Ins.DB.CTHDs.Count();
            int rListlength = DataProvider.Ins.DB.PHIEUNHAPs.Count();
            int rDetailListlength = DataProvider.Ins.DB.CTPNs.Count();
            int cListlength = DataProvider.Ins.DB.KHACHHANGs.Count();
            int card1 = 0, card2 = 0, card41 = 0, card42 = 0; string card31 = "", card32 = "";
            int[] maxMonth = new int[14]; maxMonth[12] = 0; maxMonth[13]= 0;
            int[] maxGood = new int[gListlength+2]; maxGood[gListlength] = 0;

            switch (n) // n = cbbTypeIndex
            {
                case 0:
                    //TONG SP BAN RA
                    for (int i = 0; i<oListlength; i++)
                    {
                        if (OrderList[i].MACN == cbbBranchValue && OrderList[i].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[i].DELETED == false)
                            for (int j = 0; j<oDetailListlength; j++)
                                if (OrderDetailList[j].MAHD == OrderList[i].MAHD)
                                    card1+= (int)OrderDetailList[i].SOLUONG; //card1=tong sp ban ra
                    }

                    //TONG SP NHAP KHO
                    for (int i = 0; i<rListlength; i++)
                    {
                        if (ReceiveList[i].MACN == cbbBranchValue && ReceiveList[i].NGNHAP.Value.Year.ToString() == cbbYearValue && ReceiveList[i].DELETED == false)
                            for (int j = 0; j<rDetailListlength; j++)
                                if (ReceiveDetailList[j].MAPN == ReceiveList[i].MAPN)
                                    card2+= (int)ReceiveDetailList[i].SOLUONG; //card2=tong sp nhap kho
                    }

                    //SP BAN RA NHIEU NHAT
                    for (int i = 0; i<gListlength; i++)
                    {
                        int tong = 0;
                        for (int j = 0; j<oListlength; j++)
                            if (OrderList[j].MACN == cbbBranchValue && OrderList[j].NGMUA.Value.Year.ToString()== cbbYearValue && OrderList[i].DELETED==false)
                                for (int k = 0; k<oDetailListlength; k++)
                                    if (GoodList[i].MASP == OrderDetailList[k].MASP && OrderDetailList[k].MAHD == OrderList[j].MAHD)
                                        tong++;
                        maxGood[i] = tong; //so luong tung sp ban duoc
                    }


                    for (int i = 0; i<gListlength; i++)
                    {
                        if (maxGood[i] > maxGood[gListlength])
                        {
                            maxGood[gListlength] = maxGood[i]; //maxGood[DataProvider.Ins.DB.SANPHAMs.Count()] = sl sp ban duoc nhieu nhat
                            card31 = GoodList[i].TEN;
                        }
                    }

                    card32 = maxGood[gListlength].ToString();

                    //THANG BAN DUOC NHIEU SP NHAT
                    for (int i = 0; i < 12; i++)
                    {
                        int tong = 0;
                        for (int j = 0; j<oListlength; j++)
                            if (i== (int)OrderList[j].NGMUA.Value.Month && OrderList[j].MACN ==  cbbBranchValue && OrderList[j].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[j].DELETED==false)
                                for (int k = 0; k<oDetailListlength; k++)
                                    if (OrderDetailList[k].MAHD == OrderList[j].MAHD)
                                        tong += (int)OrderDetailList[k].SOLUONG;
                        maxMonth[i] = tong;
                    }

                    for (int i = 0; i < 12; i++)
                        if (maxMonth[i] > maxMonth[12])
                        {
                            maxMonth[12] = maxMonth[i];
                            maxMonth[13] = i;
                        }
                    card41 = maxMonth[13];
                    card42 = maxMonth[12];
                    break;

                case 1:
                    //TONG DON HANG
                    for (int i = 0; i<oListlength; i++)
                    {
                        if (OrderList[i].MACN == cbbBranchValue && OrderList[i].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[i].DELETED == false)
                            card1++; //card1=tong don hang ban ra
                    }

                    //TONG PHIEU NHAP
                    for (int i = 0; i<rListlength; i++)
                    {
                        if (ReceiveList[i].MACN == cbbBranchValue && ReceiveList[i].NGNHAP.Value.Year.ToString() == cbbYearValue && ReceiveList[i].DELETED == false)
                            card2++; //card2=tong phieu nhap
                    }

                    //DON HANG TRI GIA CAO NHAT
                    double max = 0;
                    for (int i = 0; i<oListlength; i++)
                    {
                        if (OrderList[i].MACN == cbbBranchValue && OrderList[i].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[i].TONGTIEN > max && OrderList[i].DELETED==false)
                        {
                            max = (double)OrderList[i].TONGTIEN;
                            card31 = "Order " + OrderList[i].MAHD.ToString();
                        }
                    }
                    card32 = max.ToString();

                    //THANG CO NHIEU DON HANG NHAT
                    for (int i = 0; i < 12; i++)
                    {
                        int tong = 0;
                        for (int j = 0; j<oListlength; j++)
                            if (i== (int)OrderList[j].NGMUA.Value.Month && OrderList[j].MACN ==  cbbBranchValue && OrderList[j].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[i].DELETED==false)
                                tong++;
                        maxMonth[i] = tong;
                    }
                    for (int i = 0; i < 12; i++)
                        if (maxMonth[i] > maxMonth[12])
                        {
                            maxMonth[12] = maxMonth[i];
                            maxMonth[13] = i;
                        }
                    card41 = maxMonth[13];
                    card42 = maxMonth[12];
                    break;
                case 2:
                    //TONG SO KHACH HANG
                    int[] Customer = new int[DataProvider.Ins.DB.KHACHHANGs.Count()];
                    for (int i = 0; i<oListlength; i++)
                    {
                        if (OrderList[i].MACN == cbbBranchValue && OrderList[i].NGMUA.Value.Year.ToString() == cbbYearValue&& OrderList[i].DELETED==false)
                            Customer[(int)OrderList[i].MAKH] = 1;
                    }

                    for (int i = 0; i<cListlength; i++)
                    {
                        if (Customer[i] == 1)
                            card1++;
                    }

                    //SO LUONG KH MOI
                    for (int i = 0; i<cListlength; i++)
                    {
                        if (CustomerList[i].MACN == cbbBranchValue && CustomerList[i].NGDK.Value.Year.ToString() == cbbYearValue && CustomerList[i].DELETED == false)
                            card2++;
                    }

                    //KHACH HANG CO DOANH SO CAO NHAT
                    max = 0;
                    for (int i = 0; i<cListlength; i++)
                    {
                        if (CustomerList[i].MACN == cbbBranchValue && CustomerList[i].DOANHSO > max && CustomerList[i].DELETED == false )
                        {
                            max = (double)CustomerList[i].DOANHSO;
                            card31 = CustomerList[i].HOTEN;
                        };
                    }
                    card32 = max.ToString();

                    //THANG NHIEU KHACH HANG NHAT
                    for (int i = 0; i < cListlength; i++)
                        Customer[i] = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        int tong = 0;
                        for (int j = 0; j<oListlength; j++)
                            if (i== (int)OrderList[j].NGMUA.Value.Month && OrderList[j].MACN ==  cbbBranchValue && OrderList[j].NGMUA.Value.Year.ToString() == cbbYearValue && OrderList[i].DELETED==false)
                                Customer[(int)OrderList[j].MAKH] = 1;
                        for (int j = 0; j<cListlength; j++)
                        {
                            if (Customer[j] == 1)
                                tong++;
                        }
                        maxMonth[i] = tong;
                    }

                    for (int i = 0; i < 12; i++)
                        if (maxMonth[i] > maxMonth[12])
                        {
                            maxMonth[12] = maxMonth[i];
                            maxMonth[13] = i;
                        }
                    card41 = maxMonth[13];
                    card42 = maxMonth[12];
                    break;

            }
            TextOfCard1 = card1.ToString();
            TextOfCard2 = card2.ToString();
            TextOfCard31 = card31;
            TextOfCard32 = card32.ToString();
            TextOfCard41 = "THÁNG " +  card41.ToString();
            TextOfCard42 = card42.ToString();
        }
    }


}
