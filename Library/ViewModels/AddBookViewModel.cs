using Library.Commands;
using Library.Context;
using Library.Models;
using Library.Services;
using Library.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Library.ViewModels
{
    public class AddBookViewModel:ViewModelBase
    {
        private Books _newBook;
        private NavigationService navigationService;
        private NavigationStore navigationStore;
        private Func<ListBooksViewModel> createListBooksViewModel;

        public Books NewBook
        {
            get => _newBook;
            set
            {
                _newBook = value;
                OnPropretyChanged(nameof(NewBook));
            }
        }
        public ICommand BookList {  get;}
        public ICommand AddBookCommand { get; }

        public AddBookViewModel(NavigationStore navigationStore,Func<ListBooksViewModel> createViewModel)
        {
            NewBook = new Books();
            AddBookCommand = new RelayCommand(AddBook);
            BookList = new NavigateCommand(navigationStore,createViewModel);

        }

        

        

        private void AddBook()
        {
            using (var context = new MyDbContext())
            {
                var book = new Books
                {
                    Title = NewBook.Title,
                    Author = NewBook.Author,
                    ISBN = NewBook.ISBN,
                    PubYear = NewBook.PubYear
                };

                context.Books.Add(book);

                context.SaveChanges();
            }

            NewBook = new Books();
            OnPropretyChanged(nameof(NewBook));
        }

    }
}
