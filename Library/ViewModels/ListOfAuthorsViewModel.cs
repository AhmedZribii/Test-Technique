using Library.Commands;
using Library.Context;
using Library.Models;
using Library.Services;
using Library.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class ListOfAuthorsViewModel:ViewModelBase
    {
        private ObservableCollection<Authors> _authors;
        private Authors _selectedAuthor;
       
        private NavigationStore navigationStore;
        private Func<AddAuthorViewModel> createAddAuthorViewModel;

        public ObservableCollection<Authors> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropretyChanged(nameof(Authors));
            }
        }

        public Authors SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropretyChanged(nameof(SelectedAuthor));
            }
        }

        public ICommand AddAuthor {  get;}
        public ICommand DeleteAuthorCommand { get; }
        public ICommand UpdateAuthorCommand { get; }

        public ListOfAuthorsViewModel(NavigationStore navigationStore,Func<AddAuthorViewModel>createViewModel)
        {
            LoadAuthors();
            DeleteAuthorCommand = new RelayCommand(DeleteAuthor, CanUpdateOrDelete);
            UpdateAuthorCommand = new RelayCommand(UpdateAuthor, CanUpdateOrDelete);
            AddAuthor = new NavigateCommand(navigationStore,createViewModel);
        }

        
        

        private void LoadAuthors()
        {
            using (var context = new MyDbContext())
            {
                Authors = new ObservableCollection<Authors>(context.Authors.ToList());
            }
        }

        private void DeleteAuthor()
        {
            if (SelectedAuthor != null)
            {
                using (var context = new MyDbContext())
                {
                    var author = context.Authors.Find(SelectedAuthor.Id);
                    if (author != null)
                    {
                        context.Authors.Remove(author);
                        context.SaveChanges();
                        Authors.Remove(SelectedAuthor);
                        SelectedAuthor = null;
                    }
                }
            }
        }

        private void UpdateAuthor()
        {
            if (SelectedAuthor != null)
            {
                using (var context = new MyDbContext())
                {
                    var author = context.Authors.Find(SelectedAuthor.Id);
                    if (author != null)
                    {
                        author.FirstName = SelectedAuthor.FirstName;
                        author.LastName = SelectedAuthor.LastName;
                        author.DateOfBirth = SelectedAuthor.DateOfBirth;
                        context.SaveChanges();
                    }
                }
            }
        }

        private bool CanUpdateOrDelete()
        {
            return SelectedAuthor != null;
        }
    }
}
