using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Univ.lib
{
    public class Command : ICommand
    {

        private Action Action;

        public event EventHandler CanExecuteChanged;

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
