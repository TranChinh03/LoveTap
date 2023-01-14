using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
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
    public class UsrPro5EditViewModel:BaseViewModel
    {
        public string UserID { get; set; }

        public ICommand LoadedHomeProfileEdit { get; set; }
        public ICommand NavBack2Pro5 { get; set; }
        public ICommand GetIdButton { get; set; }

        public ICommand Todo { get; set; }

        string Name;
        private ObservableCollection<NHANVIEN> _User;
        public ObservableCollection<NHANVIEN> User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }

        private string _Birthday;
        public string Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _ID;
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Branch;
        public string Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }
        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }

        private string _CoefficientsSalary;
        public string CoefficientsSalary { get => _CoefficientsSalary; set { _CoefficientsSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }


        public ICommand EditCommand { get; set; }
        public string[] BranchIDList { get; set; } = new string[DataProvider.Ins.DB.CHINHANHs.Count()];


        public UsrPro5EditViewModel(NavigationStore navigationStore) {
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);
            for (int i = 0; i < DataProvider.Ins.DB.CHINHANHs.Count(); i++)

            {
                BranchIDList[i] = BranchList[i].MACN;
            }


            LoadedHomeProfileEdit = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                UserID = MainViewModel.ID;
                User = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NVID == UserID));
                if (User != null && User.Count > 0)
                {
                    FullName = User[0].HOTEN;
                    Birthday = User[0].NTNS.ToString();
                    PhoneNumber = User[0].SDT;
                    Email = User[0].EMAIL;
                    Address = User[0].DIACHI;
                    ID = User[0].MANV;
                    Branch = User[0].MACN;
                    CoefficientsSalary = User[0].HESOLUONG.ToString();
                    BasicPay = User[0].LUONGCB.ToString();
                    string role = User[0].VAITRO.ToString();
                    if (role == "1")
                        Role = "Admin";
                    else
                        Role = "Staff";
                }
                
            });

            NavBack2Pro5 = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));


            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            //Todo = new RelayCommand<HomeProfileEdit>((p) => true, (p) => ToDo(p));
            


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(PhoneNumber) || 
                string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address) || 
                string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Branch) || string.IsNullOrEmpty(CoefficientsSalary) || 
                string.IsNullOrEmpty(BasicPay) || string.IsNullOrEmpty(Role))
                    return false;

                //var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                //if (displayList == null || displayList.Count() != 0)
                //    return false;

                return true;

            }, (p) =>
            {
                var employee = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).SingleOrDefault();
                employee.MANV = ID;
                employee.HOTEN = FullName;
                employee.SDT = PhoneNumber;
                employee.EMAIL = Email;
                employee.NTNS = DateTime.Parse(Birthday);
                employee.DIACHI = Address;
                employee.MACN = Branch;
                employee.HESOLUONG = float.Parse(CoefficientsSalary);
                employee.LUONGCB = float.Parse(BasicPay);
                if (Role == "Staff" || Role == "staff")
                    employee.VAITRO = Convert.ToBoolean(0);
                else
                    employee.VAITRO = Convert.ToBoolean(1);


                DataProvider.Ins.DB.SaveChanges();

            });
        }

        //void ToDo(HomeProfileEdit p)
        //{
        //    int index = int.Parse(Name);
        //    switch (index)
        //    {
        //        case 1:
        //            {
        //                p.Close();
        //                break;
        //            }
        //        case 2:
        //            {
        //                p.Close();
        //                break;
        //            }
        //    }
        //}
    }
}
