using Library.Stores;
using Library.ViewModels;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Services;
using System.Windows.Navigation;

namespace Library.Commands
{
    public class NavigateCommand: CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public override void Execute(object parameter)
        {
            
            _navigationStore.CurrentViewModel = _createViewModel();
        }

    }
}
