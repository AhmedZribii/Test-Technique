using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Commands
{
    public class RelayCommand: ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        
        private Action action;
        private Action actions;

        public Func<bool> CanUpdateOrDelete { get; }

        public RelayCommand(Action<object> execute,  Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            
            
        }

        public RelayCommand(Action actions)
        {
            this.action = actions;
            
            
        }

        public RelayCommand(Action actions, Func<bool> canUpdateOrDelete) : this(actions)
        {
            CanUpdateOrDelete = canUpdateOrDelete;
            
        }

        
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
