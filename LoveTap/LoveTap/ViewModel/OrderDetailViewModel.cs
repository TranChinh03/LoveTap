using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace LoveTap.ViewModel
{
    internal class OrderDetailViewModel:BaseViewModel
    {
        public ICommand GetIdButton { get; set; }
        string Name;
        public ICommand SwitchTab { get; set; }
        public OrderDetailViewModel()
        {
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<OrdersDetailUC>((p) => true, (p) => switchtab(p));
        }
        void switchtab(OrdersDetailUC p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
                case 2:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
            }
        }

    }
}
