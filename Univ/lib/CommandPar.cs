using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Univ.lib
{
  public  class CommandPar : ICommand
    {

        private Action<object>  Action;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.Action(parameter);
        }
         public CommandPar(Action<object> Action)
        {
            this.Action = Action;
        }

    }
}
