using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    internal class CustomerDetailViewModel : BaseViewModel
    {
        public ICommand GetIdButton { get; set; }
        string Name;
        public ICommand SwitchTab { get; set; }
        public CustomerDetailViewModel() {
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<CustomerDetailUC>((p) => true, (p) => switchtab(p));
        }
        void switchtab(CustomerDetailUC p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.EditCustomerUC.Visibility=Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
                case 3:
                    {
                        p.Visibility=Visibility.Collapsed;
                        break;
                    }
            }
        }
    }
}
