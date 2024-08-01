using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Books : ViewModelBase
    {
        private string _title;
        private string _author;
        private string _isbn;
        private string _pubyear;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropretyChanged(nameof(Title));
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropretyChanged(nameof(Author));
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropretyChanged(nameof(ISBN));
            }
        }
        public string PubYear
        {
            get => _pubyear;
            set
            {
                _pubyear = value;
                OnPropretyChanged(nameof(PubYear));
            }
        }


    }
}
