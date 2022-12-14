using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    internal class UsrPro5EditViewModel:BaseViewModel
    {
        public ICommand GetIdButton { get; set; }

        public ICommand Todo { get; set; }

        string Name;
        public UsrPro5EditViewModel() {
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            Todo = new RelayCommand<HomeProfileEdit>((p) => true, (p) => ToDo(p));

        }

        void ToDo(HomeProfileEdit p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.Close();
                        break;
                    }
                case 2:
                    {
                        p.Close();
                        break;
                    }
            }
        }
    }
}
