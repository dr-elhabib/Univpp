using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univ.modelview
{
    class ViewClientViewModel:BaseViewModel<client>
    {
        public ObservableCollection<ClientPartViewModel> Clients { get; set; }
        public Command new_ { get; set; }
        public CommandPar Serech { get; set; }

        public ViewClientViewModel() {
            actionUP = () =>
            {

                Clients = new ObservableCollection<ClientPartViewModel>(Ico.getValue<db>().GetUnivdb().clients.ToList().Select(c => new ClientPartViewModel(c)
                {
                    deleteAc = (t) =>
                    {
                        Clients.Remove(t as ClientPartViewModel);

                    },
                    editAc = () =>
                    {

                        Ico.getValue<ContentApp>().page = new EditClient(c);
                    }
                }));

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
                        Clients.Remove(tc as ClientPartViewModel);

                    },
                    editAc = () =>
                    {

                        Ico.getValue<ContentApp>().page = new EditClient(c);
                    }

                }

                ).Where((l) => l.Name.Contains(text) || l.numaccount.Contains(text)|| l.bank.Contains(text))
                    );
            });


        }

    }
}
