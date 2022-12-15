using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    internal class CreatePwViewModel:BaseViewModel
    {
        public ICommand changePw { get; set; }
        public ICommand HiddenBG { get; set; }

        public CreatePwViewModel()
        {
            changePw = new RelayCommand<CreatePasswordUC>((p) => true, (p) => changePass(p));
        }
        void changePass(CreatePasswordUC p)
        {
            p.Visibility = Visibility.Collapsed;
        }

    }
}
