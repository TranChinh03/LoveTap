using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoveTap.UserControlCustom
{
    /// <summary>
    /// Interaction logic for MenuBarUC.xaml
    /// </summary>
    public partial class MenuBarUC : UserControl
    {
       
        public MenuBarUC()
        {
            InitializeComponent();
        }

        private void HomeCheck()
        {
            homeTbl.Foreground = Brushes.White;
            homeImg1.Visibility = Visibility.Hidden;
            homeImg2.Visibility = Visibility.Visible;
        }

        private void HomeUncheck()
        {
            homeTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            homeImg1.Visibility = Visibility.Visible;
            homeImg2.Visibility = Visibility.Hidden;
        }

        private void GoodsCheck()
        {
            goodsTbl.Foreground = Brushes.White;
            goodsImg1.Visibility = Visibility.Hidden;
            goodsImg2.Visibility = Visibility.Visible;
        }

        private void GoodsUncheck()
        {
            goodsTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            goodsImg1.Visibility = Visibility.Visible;
            goodsImg2.Visibility = Visibility.Hidden;
        }

        private void OrdersCheck()
        {
            ordersTbl.Foreground = Brushes.White;
            ordersImg1.Visibility = Visibility.Hidden;
            ordersImg2.Visibility = Visibility.Visible;
        }

        private void OrdersUncheck()
        {
            ordersTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            ordersImg1.Visibility = Visibility.Visible;
            ordersImg2.Visibility = Visibility.Hidden;
        }
        private void CustomerCheck()
        {
            customerTbl.Foreground = Brushes.White;
            customerImg1.Visibility = Visibility.Hidden;
            customerImg2.Visibility = Visibility.Visible;
        }

        private void CustomerUncheck()
        {
            customerTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            customerImg1.Visibility = Visibility.Visible;
            customerImg2.Visibility = Visibility.Hidden;
        }
        private void StatisticCheck()
        {
            statisticTbl.Foreground = Brushes.White;
            statisticImg1.Visibility = Visibility.Hidden;
            statisticImg2.Visibility = Visibility.Visible;
        }

        private void StatisticUncheck()
        {
            statisticTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            statisticImg1.Visibility = Visibility.Visible;
            statisticImg2.Visibility = Visibility.Hidden;
        }

        private void EmployeeCheck()
        {
            employeeTbl.Foreground = Brushes.White;
            employeeImg1.Visibility = Visibility.Hidden;
            employeeImg2.Visibility = Visibility.Visible;
        }
        private void EmployeeUncheck()
        {
            employeeTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
            employeeImg1.Visibility = Visibility.Visible;
            employeeImg2.Visibility = Visibility.Hidden;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeCheck();
            GoodsUncheck();
            OrdersUncheck();
            CustomerUncheck();
            StatisticUncheck();
            EmployeeUncheck();
        }

        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            HomeUncheck();
            GoodsCheck();
            OrdersUncheck();
            CustomerUncheck();
            StatisticUncheck();
            EmployeeUncheck();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            HomeUncheck();
            GoodsUncheck();
            OrdersCheck();
            CustomerUncheck();
            StatisticUncheck();
            EmployeeUncheck();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            HomeUncheck();
            GoodsUncheck();
            OrdersUncheck();
            CustomerCheck();
            StatisticUncheck();
            EmployeeUncheck();
        }

        private void Statistic_Click(object sender, RoutedEventArgs e)
        {
            HomeUncheck();
            GoodsUncheck();
            OrdersUncheck();
            CustomerUncheck();
            StatisticCheck();
            EmployeeUncheck();
        }
        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            HomeUncheck();
            GoodsUncheck();
            OrdersUncheck();
            CustomerUncheck();
            StatisticUncheck();
            EmployeeCheck();
        }
    }
}
