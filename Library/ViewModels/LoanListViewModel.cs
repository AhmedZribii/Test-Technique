using Library.Commands;
using Library.Context;
using Library.Migrations;
using Library.Models;
using Library.Services;
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
    public class LoanListViewModel: ViewModelBase
    {
        private ObservableCollection<Loans> _loans;
        private Loans _selectedLoan;
        private string _searchQuery;
        private bool _searchByBorrower;
        private bool _searchByBook;

        public ObservableCollection<Loans> Loans
        {
            get => _loans;
            set
            {
                _loans = value;
                OnPropretyChanged(nameof(Loans));
            }
        }

        public Loans SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _selectedLoan = value;
                OnPropretyChanged(nameof(SelectedLoan));
            }
        }


        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropretyChanged(nameof(SearchQuery));
            }
        }

        public bool SearchByBorrower
        {
            get => _searchByBorrower;
            set
            {
                _searchByBorrower = value;
                OnPropretyChanged(nameof(SearchByBorrower));
            }
        }

        public bool SearchByBook
        {
            get => _searchByBook;
            set
            {
                _searchByBook = value;
                OnPropretyChanged(nameof(SearchByBook));
            }
        }

        public ICommand DeleteLoanCommand { get; }
        public ICommand UpdateLoanCommand { get; }
        public ICommand MarkAsReturnedCommand { get; }
        public ICommand SearchLoanCommand { get; }

        public LoanListViewModel()
        {
            LoadLoans();
            DeleteLoanCommand = new RelayCommand(DeleteLoan, CanUpdateOrDelete);
            UpdateLoanCommand = new RelayCommand(UpdateLoan, CanUpdateOrDelete);
            MarkAsReturnedCommand = new RelayCommand(MarkAsReturned, CanUpdateOrDelete);
            SearchLoanCommand = new RelayCommand(SearchLoan);
        }

        private void LoadLoans()
        {
            using (var context = new MyDbContext())
            {
                Loans = new ObservableCollection<Loans>(context.Loans.Include(l => l.BorrowedBook).ThenInclude(b => b.Author).ToList());
            }
        }

        private void DeleteLoan()
        {
            if (SelectedLoan != null)
            {
                using (var context = new MyDbContext())
                {
                    var loan = context.Loans.Find(SelectedLoan.LoanId);
                    if (loan != null)
                    {
                        context.Loans.Remove(loan);
                        context.SaveChanges();
                        Loans.Remove(SelectedLoan);
                        SelectedLoan = null;
                    }
                }
            }
        }

        private void UpdateLoan()
        {
            if (SelectedLoan != null)
            {
                using (var context = new MyDbContext())
                {
                    var loan = context.Loans.Find(SelectedLoan.LoanId);
                    if (loan != null)
                    {
                        loan.LoanDate = SelectedLoan.LoanDate;
                        loan.ExpectedReturnDate = SelectedLoan.ExpectedReturnDate;
                        loan.Borrower = SelectedLoan.Borrower;
                        loan.BorrowedBook = SelectedLoan.BorrowedBook;
                        loan.IsReturned = SelectedLoan.IsReturned;
                        context.SaveChanges();
                    }
                }
            }
        }

        private void MarkAsReturned()
        {
            if (SelectedLoan != null)
            {
                using (var context = new MyDbContext())
                {
                    var loan = context.Loans.Find(SelectedLoan.LoanId);
                    if (loan != null)
                    {
                        loan.IsReturned = true;
                        context.SaveChanges();
                        SelectedLoan.IsReturned = true;
                    }
                }
            }
        }

        private void SearchLoan()
        {
            using (var context = new MyDbContext())
            {
                IQueryable<Loans> query = context.Loans.Include(l => l.BorrowedBook).ThenInclude(b => b.Author);

                if (SearchByBorrower && !string.IsNullOrEmpty(SearchQuery))
                {
                    query = query.Where(l => l.Borrower.Contains(SearchQuery));
                }

                if (SearchByBook && !string.IsNullOrEmpty(SearchQuery))
                {
                    query = query.Where(l => l.BorrowedBook.Title.Contains(SearchQuery));
                }

                Loans = new ObservableCollection<Loans>(query.ToList());
            }

        }

        private bool CanUpdateOrDelete()
        {
            return SelectedLoan != null;
        }
    }
}
