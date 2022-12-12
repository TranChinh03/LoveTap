using LoveTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        //public bool IsLoaded = false;
        //if (!IsLoaded)
        //{
        //    //HomeScreen home = new HomeScreen();
        //    //home.ShowDialog();
        //    //HomePersonal homePersonal = new HomePersonal();
        //    //homePersonal.ShowDialog();
        //    //HomeProfileEdit homeProfileEdit = new HomeProfileEdit();
        //    //homeProfileEdit.ShowDialog();
        //    //LoginWindow loginWindow = new LoginWindow();
        //    //loginWindow.ShowDialog();

        //    //GoodsWindow goodsWindow = new GoodsWindow();
        //    //goodsWindow.ShowDialog();

        //    EmployeeWindow employeeWindow = new EmployeeWindow();
        //    employeeWindow.ShowDialog();

        //    //GoodsWindow goodsWindow = new GoodsWindow();   
        //    //goodsWindow.ShowDialog();
        //    IsLoaded = true;
        //}

        //    public ICommand CloseLogin { get; set; }
        //public ICommand MinimizeLogin { get; set; }
        //public ICommand MoveWindow { get; set; }




        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public ICommand TenDangNhap_Loaded { get; set; }
        public ICommand Quyen_Loaded { get; set; }
        public ICommand LogOutCommand { get; set; }
        //private NGUOIDUNG _User;
        //public NGUOIDUNG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }
        public string Name;
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public ICommand Loadwd { get; set; }
        //public MainViewModel()
        //{
        //    Loadwd = new RelayCommand<MainWindow>((p) => true, (p) => _Loadwd(p));
        //    CloseLogin = new RelayCommand<MainWindow>((p) => true, (p) => Close());
        //    MinimizeLogin = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
        //    MoveWindow = new RelayCommand<MainWindow>((p) => true, (p) => moveWindow(p));
        //    GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
        //    SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
        //    TenDangNhap_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadTenND(p));
        //    Quyen_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadQuyen(p));
        //    LogOutCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => LogOut(p));
        //}
        //void _Loadwd(MainWindow p)
        //{
        //    if (LoginViewModel.IsLogin)
        //    {
        //        string a = Const.TenDangNhap;
        //        User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.USERNAME == a).FirstOrDefault();
        //        Const.ND = User;
        //        SetQuanLy = User.QTV ? Visibility.Visible : Visibility.Collapsed;
        //        Const.Admin = User.QTV;
        //        Ava = User.AVA;
        //        LoadTenND(p);
        //    }
        //}

        public bool IsLoaded = false;
        public ICommand LoadedHomeCommand { get; set; }
        public MainViewModel()
        {

            LoadedHomeCommand = new RelayCommand<Window>((p) => { return true; },(p)=> {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM=loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            });

            //LoadedHomeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HomeWindow homeWindow = new HomeWindow(); homeWindow.ShowDialog(); });
        }
    }
}
