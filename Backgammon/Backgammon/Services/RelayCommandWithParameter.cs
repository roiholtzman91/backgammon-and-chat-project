using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Backgammon.Services
{
    class RelayCommandWithParameter<T> : ICommand
    {
        Action<T> _act;
        public RelayCommandWithParameter(Action<T> act)
        {
            _act = act;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _act((T)parameter);
        }
    }    
}
