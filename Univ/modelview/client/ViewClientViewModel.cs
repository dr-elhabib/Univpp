using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.page.lib;

namespace Univ.modelview
{
    class ViewClientViewModel:BaseViewModel<ObservableCollection<ClientPartViewModel>>
    {
        public ObservableCollection<ClientPartViewModel> Clients { get; set; }
        public Command new_ { get; set; }
        public CommandPar Serech { get; set; }

        public ViewClientViewModel() {
            actionUP = () =>

            {

                val = new ObservableCollection<ClientPartViewModel>(Ico.getValue<db>().GetUnivdb().clients.ToList().Select(c => new ClientPartViewModel(c)
                {
                    deleteAc = (t) =>
                    {
                        Ico.getValue<ContentApp>().OpenSample4Dialog();
                        Ico.getValue<ContentApp>().Sample4Content = new YesOrNo("هل أنت متأكد من قيامك بحذف هذا العميل  ,لا يمكن التراجع عن الحذف",
                           async () => {
                               Ico.getValue<ContentApp>().AcceptSample4Dialog();
                               await Task.Run(() =>
                                   {
                                       Ico.getValue<db>().GetUnivdb().clients.Remove(
                                    Ico.getValue<db>().GetUnivdb().clients.ToList().Where(cl => cl.Id == c.Id).FirstOrDefault());
                                       Ico.getValue<db>().savedb();
                                       inTilData();
                                   });
                               Ico.getValue<ContentApp>().CancelSample4Dialog();
                           }, Ico.getValue<ContentApp>().CancelSample4Dialog);
                           },
                    editAc = () =>
                    {

                        Ico.getValue<ContentApp>().page = new EditClient(c, inTilData);
                    }
                }));
                Clients = val;
            };
            actionUP();
                new_ = new Command(()=> {

                Ico.getValue<ContentApp>().page = new AddCLient();
            });
            Serech = new CommandPar((t) => {
                var text = (string)t;
                Clients = new ObservableCollection<ClientPartViewModel>(Ico.getValue<db>().GetUnivdb().clients.ToList().Select(c => new ClientPartViewModel(c)
                {
                    deleteAc = (tc) => {
                        Ico.getValue<ContentApp>().OpenSample4Dialog();
                        Ico.getValue<ContentApp>().Sample4Content = new YesOrNo("هل أنت متأكد من قيامك بحذف هذا العميل  ,لا يمكن التراجع عن الحذف",
                           async () => {
                               Ico.getValue<ContentApp>().AcceptSample4Dialog();
                               await Task.Run(() =>
                               {
                                   Ico.getValue<db>().GetUnivdb().clients.Remove(
                                Ico.getValue<db>().GetUnivdb().clients.ToList().Where(cl => cl.Id == c.Id).FirstOrDefault());
                                   Ico.getValue<db>().savedb();
                                   inTilData();
                               });
                               Ico.getValue<ContentApp>().CancelSample4Dialog();
                           }, Ico.getValue<ContentApp>().CancelSample4Dialog);

                    },
                    editAc = () =>
                    {

                        Ico.getValue<ContentApp>().page = new EditClient(c, inTilData);
                    }

                }

                ).Where((l) => l.Name.Contains(text) || l.numaccount.Contains(text)|| l.bank.Contains(text))
                    );
            });


        }

        public void inTilData()
        {
            actionUP();
        }
    }
}
