using Library.Commands;
using Library.Services;
using Library.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class NavigationViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<AddBookViewModel> createViewModelBooks;
        private readonly Func<AddAuthorViewModel> createViewModelAuthor;
        

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand NavigateBooksCommand { get; }
        public ICommand NavigateAuthorsCommand { get; }
        public ICommand NavigateLoansCommand { get; }

        public NavigationViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateBooksCommand = new RelayCommand(NavigateBooks);
            NavigateAuthorsCommand = new RelayCommand(NavigateAuthors);
            NavigateLoansCommand = new RelayCommand(NavigateLoans);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropretyChanged(nameof(CurrentViewModel));
        }

        private void NavigateBooks()
        {
            //_navigationStore.CurrentViewModel = new ListBooksViewModel(_navigationStore, createViewModelBooks,);
        }

        private void NavigateAuthors()
        {
            //_navigationStore.CurrentViewModel = new ListOfAuthorsViewModel(_navigationStore, createViewModelAuthor,);
        }

        private void NavigateLoans()
        {
           // _navigationStore.CurrentViewModel = new LoanListViewModel(_navigationService);
        }
    }
}
