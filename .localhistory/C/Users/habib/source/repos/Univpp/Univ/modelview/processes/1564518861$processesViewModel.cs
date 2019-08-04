using Univ.lib;
using Univ.modeldb;
using Univ.page.lib;

using Univ.page;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;

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
                this.processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p){

                    removeaction = () => {
                        OpenSample4Dialog();

                        Sample4Content =  new YesOrNo("هل أنت مـتأكد من قيامك بحذف هذه العملية  ؟ , لا يمكن التراجع عن هذه الحذف ",
                        async    ()=> {


                            AcceptSample4Dialog();
                            await Task.Run(()=> {
                                var ps=      Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pro => pro.Id == p.Id).ToList().FirstOrDefault();

                                foreach(var i in ps.cards.ToList()){
                                Ico.getValue<db>().GetUnivdb().card_7isab.AddRange(i.card_7isab);
                               }

                                foreach (var i in ps.cards.ToList()){

                                    Ico.getValue<db>().GetUnivdb().card_kanoni.AddRange(i.card_kanoni);
                               }

                                foreach (var i in ps.cards.ToList()){

                                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.AddRange(i.card_mo7sabi);
                               }

                                foreach (var i in ps.cards.ToList())
                                {

                                    foreach (var ic in ps.parts.ToList())
                                    {
                                        Ico.getValue<db>().GetUnivdb().p_sa7ab.RemoveRange(ic.p_sa7ab);
                                    }

                                    Ico.getValue<db>().GetUnivdb().card_sa7ab.AddRange(i.card_sa7ab);
                                }
                                
                                    Ico.getValue<db>().GetUnivdb().parts.RemoveRange(ps.parts);  
                                    Ico.getValue<db>().GetUnivdb().cards.RemoveRange(ps.cards);  
                                Ico.getValue<db>().GetUnivdb().processes.Remove(ps);
                                Ico.getValue<db>().savedb();

                                CancelSample4Dialog();
                                inTilData();

                            });
                        },
                        ()=> {
                            CancelSample4Dialog();
                        });

                    }
                }));
            };
            inTilData();
            Count = processes.Count;
            Serech = new CommandPar((t)=>{
                var text= (string)t;
                 processes = new ObservableCollection<processesViewMODEL>(Ico.getValue<db>().GetUnivdb().processes.ToList().Select(p => new processesViewMODEL(p)
                 {

                     removeaction = () => {
                         OpenSample4Dialog();

                         Sample4Content = new YesOrNo("هل أنت مـنأكد من قيامك بحذف هذه العملية  ؟ , لا يمكن التراجع عن هذه الحذف ",

                             async () => {
                                 AcceptSample4Dialog();

                                 await Task.Run(() => {
                                 Ico.getValue<db>().GetUnivdb().processes.Remove(Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pro => pro.Id == p.Id).ToList().FirstOrDefault());
                                     Ico.getValue<db>().savedb();
                                 CancelSample4Dialog();
                                     inTilData();

                                 });
                         },
                         () => {
                             CancelSample4Dialog();
                         });

                     }
                 }

                 ).Where((l)=> l.Code.Contains(text) || l.Name.Contains(text))
                     );
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
            Sample4Content = new Progressbar();
        }
    }
}
