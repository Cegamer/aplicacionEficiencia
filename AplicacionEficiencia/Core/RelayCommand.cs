using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AplicacionEficiencia.Core
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _handler;

        public RelayCommand(Action<object> handler)
        {
            _handler = handler;
        }

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) => _handler(parameter);
    }
}
