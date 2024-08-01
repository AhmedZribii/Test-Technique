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

namespace Library.ViewModels
{
    public class AddAuthorViewModel:ViewModelBase
    {
        private Authors _newAuthor;
       
        private NavigationStore navigationStore;
        private Func<ListOfAuthorsViewModel> createListOfAuthorsViewModel;

        public Authors NewAuthor
        {
            get => _newAuthor;
            set
            {
                _newAuthor = value;
                OnPropretyChanged(nameof(NewAuthor));
            }
        }
        public ICommand AuthorList { get; }
        public ICommand AddAuthorCommand { get; }

        public AddAuthorViewModel(NavigationStore navigationStore, Func<ListOfAuthorsViewModel>createViewModel )
        {
            NewAuthor = new Authors();
            AddAuthorCommand = new RelayCommand(AddAuthor);
            AuthorList = new NavigateCommand(navigationStore, createViewModel);
        }

        
       

        private void AddAuthor()
        {
            using (var context = new MyDbContext())
            {
                var author = new Authors
                {
                    FirstName = NewAuthor.FirstName,
                    LastName = NewAuthor.LastName,
                    DateOfBirth = NewAuthor.DateOfBirth
                };

                context.Authors.Add(author);
                context.SaveChanges();
            }

            NewAuthor = new Authors();
            OnPropretyChanged(nameof(NewAuthor));
        }

    }
}
