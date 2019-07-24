using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class processesViewModel: BaseViewModel
    {
        public int Count { get; set; }
        public CommandPar Serech { get; set; }
        public Command new_ { get; set; }
        public ObservableCollection<processesViewMODEL> processes { get; set; }
        public processesViewModel() {
            this.processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p)));
            Count = processes.Count;
            Serech = new CommandPar((t)=>{
                var text= (string)t;
                 processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p)).Where((l)=> l.Code.Contains(text) || l.Name.Contains(text)));
            });
            new_= new Command(()=>{
                MainViewModel.page = new NewProcesses();
              //  Ico.getValue<ContentApp>().page = new NewProcesses();
            });

        }


    }
}
