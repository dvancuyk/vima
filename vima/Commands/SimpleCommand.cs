using System;
using System.Windows.Input;

namespace vima.Commands
{
    public class SimpleCommand : ICommand
    {
        #region : Methods :

        public Predicate<object> CanExecuteDelegate { get; set; } 
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}
