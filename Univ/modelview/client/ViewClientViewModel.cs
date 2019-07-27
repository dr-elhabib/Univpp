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
        public ObservableCollection<ClientPartViewModel> partsClient { get; set; }
        public Command new_ { get; set; }

        public ViewClientViewModel() {

            partsClient = new ObservableCollection<ClientPartViewModel>(Ico.getValue<db>().GetUnivdb().clients.ToList().Select(c => new ClientPartViewModel(c)
            {
                deleteAc = (t) => {
                    partsClient.Remove(t as ClientPartViewModel);
                }
            }));
            new_ = new Command(()=> {

                Ico.getValue<ContentApp>().page = new AddClient();
            });

        }
    }
}
