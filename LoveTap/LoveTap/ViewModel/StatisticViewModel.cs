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
    public class StatisticViewModel:BaseViewModel
    {
        public ICommand LoadedStatisticView { get; set; }
        public ICommand selectionChaged { get; set; }
        public string _selectedType;
        public string selectedType { get => _selectedType; set { _selectedType = value; OnPropertyChanged(); } }
        public string _NameofCard1;
        public string NameofCard1 { get => _NameofCard1; set { _NameofCard1 = value; OnPropertyChanged(); } }
        public string _NameofCard2;
        public string NameofCard2 { get => _NameofCard2; set { _NameofCard2 = value; OnPropertyChanged(); } }
        public string _NameofCard3;
        public string NameofCard3 { get => _NameofCard3; set { _NameofCard3 = value; OnPropertyChanged(); } }
        public string _NameofCard4;
        public string NameofCard4 { get => _NameofCard4; set { _NameofCard4 = value; OnPropertyChanged(); } }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> _OrderList;
        public ObservableCollection<HOADON> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }

        public string[] BranchIDList { get; set; } = new string[DataProvider.Ins.DB.CHINHANHs.Count()];
        public string[] YearList { get; set; } = new string[DataProvider.Ins.DB.HOADONs.Count()];

        public StatisticViewModel(NavigationStore navigationStore)
        {
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            OrderList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);

            for (int i = 0; i<DataProvider.Ins.DB.CHINHANHs.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }
            LoadedStatisticView = new RelayCommand<StatisticViewUC>((p) => { return  true; }, (p) => _selectedBranch(p));

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
                    break;
            }
        }
    }


}
