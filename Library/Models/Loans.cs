using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Loans : ViewModelBase
    {
        private DateTime _loanDate;
        private DateTime _expectedReturnDate;
        private string _borrower;
        private Books _borrowedBook;
        private bool _isReturned;

        [Key]
        public int LoanId {  get; set; }    
        public DateTime LoanDate
        {
            get => _loanDate;
            set
            {
                _loanDate = value;
                OnPropretyChanged(nameof(LoanDate));
            }
        }

        public DateTime ExpectedReturnDate
        {
            get => _expectedReturnDate;
            set
            {
                _expectedReturnDate = value;
                OnPropretyChanged(nameof(ExpectedReturnDate));
            }
        }

        public string Borrower
        {
            get => _borrower;
            set
            {
                _borrower = value;
                OnPropretyChanged(nameof(Borrower));
            }
        }

        public Books BorrowedBook
        {
            get => _borrowedBook;
            set
            {
                _borrowedBook = value;
                OnPropretyChanged(nameof(BorrowedBook));
            }
        }

        public bool IsReturned
        {
            get => _isReturned;
            set
            {
                _isReturned = value;
                OnPropretyChanged(nameof(IsReturned));
            }
        }
    }
}
