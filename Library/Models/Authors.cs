using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Authors : ViewModelBase
    {
        private string _firstname;
        private string _lastname;
        private DateTime _dateofbirth;
        

        public int Id { get; set; }

        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropretyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropretyChanged(nameof(LastName));
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateofbirth;
            set
            {
                _dateofbirth = value;
                OnPropretyChanged(nameof(DateOfBirth));
            }
        }
        
    }
}
