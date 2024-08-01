using Library.Services;
using Library.Stores;
using Library.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<AddBookViewModel> _createViewModel;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel= CreateListBooksViewModel();
            

            var mainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            mainWindow.Show();
            base.OnStartup(e);
        }

        private ListBooksViewModel CreateListBooksViewModel()
        {
            return new ListBooksViewModel(_navigationStore, CreateAddBookViewModel);
        }
        private AddBookViewModel CreateAddBookViewModel()
        {
            return new AddBookViewModel(_navigationStore, CreateListBooksViewModel);
        }
        private AddAuthorViewModel CreateAddAuthorViewModel()
        {
            return new AddAuthorViewModel(_navigationStore, CreateListOfAuthorsViewModel);
        }
        private ListOfAuthorsViewModel CreateListOfAuthorsViewModel()
        {
            return new ListOfAuthorsViewModel(_navigationStore, CreateAddAuthorViewModel);
        }
    }

    
}


