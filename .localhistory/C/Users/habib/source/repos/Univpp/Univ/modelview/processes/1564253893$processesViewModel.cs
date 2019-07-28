using Univ.lib;
using Univ.modeldb;
using Univ.page.lib;

using Univ.page;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Univ.modelview
{
    class processesViewModel: BaseViewModel<process>
    {


        public bool IsSample4DialogOpen
        {
            get { return _isSample4DialogOpen; }
            set
            {
                if (_isSample4DialogOpen == value) return;
                _isSample4DialogOpen = value;
            }
        }

        public UserControl Sample4Content
        {
            get { return _sample4Content; }
            set
            {
                if (_sample4Content == value) return;
                _sample4Content = value;
            }
        }
        private bool _isSample4DialogOpen;
        private UserControl _sample4Content;

        public int Count { get; set; }
        public CommandPar Serech { get; set; }
        public Command new_ { get; set; }
        public ObservableCollection<processesViewMODEL> processes { get; set; }

        public processesViewModel() {
            actionUP = () =>
            {
                this.processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p)));
            };
            inTilData();
            Count = processes.Count;
            Serech = new CommandPar((t)=>{
                var text= (string)t;
                 processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p)).Where((l)=> l.Code.Contains(text) || l.Name.Contains(text)));
            });
            new_= new Command(()=>{
          //      MainViewModel.page = new NewProcesses();
               Ico.getValue<ContentApp>().page = new NewProcesses();
            });

        }

        public void inTilData()
        {
            actionUP();
        }




        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new ProgressBar();
        }
    }
}
