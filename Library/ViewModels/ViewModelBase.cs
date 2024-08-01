using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        // This is a function to raise this event for each viewmodel which inherit from this
        protected void OnPropretyChanged(string propretyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propretyName));
        }
    }
}
