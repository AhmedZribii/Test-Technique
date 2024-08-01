using Library.Commands;
using Library.Context;
using Library.Models;
using Library.Services;
using Library.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class ListBooksViewModel:ViewModelBase
    {
        private ObservableCollection<Books> _books;
        private Books _selectedBook;
        private NavigationStore _navigationStore;
        
        private NavigationStore navigationStore;
        private Func<AddBookViewModel> createAddBookViewModel;

        public ObservableCollection<Books> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropretyChanged(nameof(Books));
            }
        }

        public Books SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropretyChanged(nameof(SelectedBook));
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand UpdateBookCommand { get; }

        public ListBooksViewModel(NavigationStore navigationStore,Func<AddBookViewModel> createViewModel)
        {
            LoadBooks();
            DeleteBookCommand = new RelayCommand(DeleteBook, CanUpdateOrDelete);
            UpdateBookCommand = new RelayCommand(UpdateBook, CanUpdateOrDelete);
            AddBookCommand = new NavigateCommand(navigationStore ,createViewModel);
        }

       
        private void LoadBooks()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    Books = new ObservableCollection<Books>(context.Books.Include(t => t.Author).ToList());
                }
                catch(Exception ex)
                {

                }
                

            }
        }


        
        private void DeleteBook()
        {
            if (SelectedBook != null)
            {
                using (var context = new MyDbContext())
                {
                    var book = context.Books.Find(SelectedBook.Id);
                    if (book != null)
                    {
                        context.Books.Remove(book);
                        context.SaveChanges();
                        Books.Remove(SelectedBook);
                        SelectedBook = null;
                    }
                }
            }
        }

        private void UpdateBook()
        {
            if (SelectedBook != null)
            {
                using (var context = new MyDbContext())
                {
                    var book = context.Books.Find(SelectedBook.Id);
                    if (book != null)
                    {
                        book.Title = SelectedBook.Title;
                        book.Author = SelectedBook.Author;
                        book.ISBN = SelectedBook.ISBN;
                        book.PubYear = SelectedBook.PubYear;
                        context.SaveChanges();
                    }
                }
            }
        }

        private bool CanUpdateOrDelete()
        {
            return SelectedBook != null;
        }


    }
}
