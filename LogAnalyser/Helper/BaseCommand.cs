using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LogAnalyser.Helper
{
    public class BaseCommand : ICommand
    {
        private readonly bool _canExecute;
        private readonly Action _execute;

        public BaseCommand(bool canExecute, Action execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
