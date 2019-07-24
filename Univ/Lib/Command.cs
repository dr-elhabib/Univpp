using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Univ
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action Action;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action();
        }
         public Command(Action Action)
        {
            this.Action = Action;
        }

    }
}
