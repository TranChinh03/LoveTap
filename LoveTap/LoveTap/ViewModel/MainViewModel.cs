﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LoveTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public bool IsLoaded = false;
        public MainViewModel()
        {
            if (!IsLoaded)
            {
                //Home home = new Home();
                //home.ShowDialog();
                //HomePersonal homePersonal = new HomePersonal();
                //homePersonal.ShowDialog();
                //HomeProfileEdit homeProfileEdit = new HomeProfileEdit();
                //homeProfileEdit.ShowDialog();
                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.ShowDialog();

                //GoodsWindow goodsWindow = new GoodsWindow();
                //goodsWindow.ShowDialog();

                //EmployeeWindow employeeWindow = new EmployeeWindow();
                //employeeWindow.ShowDialog();

                GoodsWindow goodsWindow = new GoodsWindow();
                goodsWindow.ShowDialog();
                IsLoaded = true;

                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.ShowDialog();
                //IsLoaded = true;

                //MessageBox.Show(DataProvider.Ins.DB.SANPHAMs.First().TEN);
            }

           
        }
    }
}
