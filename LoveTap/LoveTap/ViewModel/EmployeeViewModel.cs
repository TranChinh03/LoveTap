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
using System.Windows.Markup;
using System.Windows.Navigation;
using System.Xml.Linq;
using static LoveTap.ViewModel.CustomerViewModel;

namespace LoveTap.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {

        public static NHANVIEN EmployeeSelected { get; set; }
        public ICommand navAddEmployee { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }
        public static NHANVIEN CurrentSelected { get; set; }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }


        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }


        //public struct Employee
        //{
        //    public string id { get; set; }

        //    public string ten { get; set; }
        //    public string sdt { get; set; }
        //    public string email { get; set; }
        //    public string chinhanh { get; set; }
        //    public string vitri { get; set; }
        //}

        private List<NHANVIEN> _MyEmployeeList= new List<NHANVIEN>();
        public List<NHANVIEN> MyEmployeeList { get => _MyEmployeeList; set { _MyEmployeeList = value; OnPropertyChanged(); } }

        public IEnumerable<NHANVIEN> MyFilterList
        {
            get
            {
                if (searchText == null && branchText == null)
                    return MyEmployeeList;
                else if ((searchText == null && branchText != null)||(searchText != null && branchText != null && findText == null))
                    return MyEmployeeList.Where(x => x.MACN == branchText);
                else if(searchText != null && branchText == null)
                {
                    if(findText == "Employee ID")
                        return MyEmployeeList.Where(x => (x.MANV.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Name")
                        return MyEmployeeList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Phone")
                        return MyEmployeeList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Email")
                        return MyEmployeeList.Where(x => (x.EMAIL.ToUpper().Contains(searchText.ToUpper())));
                    else if (findText == "Position")
                    {
                        if (searchText.ToUpper() == "STAFF")
                            return MyEmployeeList.Where(x => (x.VAITRO == false));
                        else if (searchText.ToUpper() == "STAFF")
                            return MyEmployeeList.Where(x => (x.VAITRO == true));
                        else
                            return MyEmployeeList;
                    }
                }
                else if(searchText!= null && branchText != null)
                {
                    if (findText == "Employee ID")
                        return (MyEmployeeList.Where(x => (x.MANV.ToUpper().Contains(searchText.ToUpper())))).Where(y => y.MACN == branchText);
                    else if (findText == "Name")
                        return (MyEmployeeList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())))).Where(y => y.MACN == branchText);
                    else if (findText == "Phone")
                        return (MyEmployeeList.Where(x => (x.SDT.ToUpper().Contains(searchText.ToUpper())))).Where(y => y.MACN == branchText);
                    else if (findText == "Email")
                        return (MyEmployeeList.Where(x => (x.EMAIL.ToUpper().Contains(searchText.ToUpper())))).Where(y => y.MACN == branchText);
                    else if (findText == "Position")
                    {
                        if (searchText.ToUpper() == "STAFF")
                            return MyEmployeeList.Where(x => (x.VAITRO == false)).Where(y=> y.MACN == branchText);
                        else if (searchText.ToUpper() == "STAFF")
                            return MyEmployeeList.Where(x => (x.VAITRO == true)).Where(y=> y.MACN == branchText);
                        else
                            return MyEmployeeList.Where(y=> y.MACN == branchText);
                    }
                }
                return MyEmployeeList;
                //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));

            }
        }

        void SwapString(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
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

        private string _sortText;
        public string sortText
        {
            get { return _sortText; }
            set
            {
                _sortText = value;
                OnPropertyChanged("sortText");
                OnPropertyChanged("MyFilterList");
            }
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

        public string[] BranchIDList { get; set; } = new string[DataProvider.Ins.DB.CHINHANHs.Count()];

        public EmployeeViewModel(NavigationStore navigationStore)
        {
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);

            for(int i =0; i<DataProvider.Ins.DB.CHINHANHs.Count(); i++)
            {
                BranchIDList[i] = BranchList[i].MACN;
            }

            foreach(NHANVIEN nv in EmployeeList)
            {
                //Employee temp = new Employee();
                //temp.ten = nv.HOTEN;
                //temp.id = nv.MANV;
                //temp.sdt = nv.SDT;
                //temp.email = nv.EMAIL;
                //temp.chinhanh = nv.MACN;
                //if (nv.VAITRO == true)
                //    temp.vitri = "Admin";
                //else
                //    temp.vitri = "Staff";
                //MyEmployeeList.Add(temp);
                MyEmployeeList.Add(nv);
            }

            navAddEmployee = new NavigationCommand<AddEmployeeViewModel>(navigationStore, () => new AddEmployeeViewModel(navigationStore));
            navDetail = new NavigationCommand<EmployeeDetailViewModel>(navigationStore, () => new EmployeeDetailViewModel(navigationStore));
            Detail = new RelayCommand<EmployeeViewUC>((p) => { return p.EmployeeList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }
        void _DetailCs(EmployeeViewUC p)
        {
            CurrentSelected =(NHANVIEN)p.EmployeeList.SelectedItem;
        }

    }
}
