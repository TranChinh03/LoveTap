using LoveTap.Service;
using LoveTap.Stores;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveTap.Commands
{
    public class ChangeOrdCommand : CommandBase
    {
        private readonly OrdersViewModel _navigationStore;
        private readonly string _str;
        private readonly ParameterNavigationService<string, OrderDetailViewModel> _navigationService;

        public ChangeOrdCommand(OrdersViewModel viewModel, string str, ParameterNavigationService<string, OrderDetailViewModel> navigationService)
        {
            _navigationStore=viewModel;
            _navigationService=navigationService;
            _str=str;
        }

        public override void Execute(object parameter)
        {
            string textTest = _str;
            _navigationService.Navigate(textTest);
        }
    }
}
