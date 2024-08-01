using Library.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationstore; 
        public ViewModelBase CurrentViewModel => _navigationstore.CurrentViewModel;

        

        public MainViewModel(NavigationStore navigationStore) 
        {
            _navigationstore = navigationStore;
            _navigationstore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropretyChanged(nameof(CurrentViewModel));
        }
    }
}
