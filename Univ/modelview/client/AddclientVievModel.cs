using Univ.lib;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview
{
    class AddclientVievModel : BaseViewModel
    {
        public string Name { get; set; }
        public string numaccount { get; set; }
        public string agence { get; set; }
        public string bank { get; set; }
        public string address { get; set; }
        public Command save { get; set; }
        public AddclientVievModel() {

               save = new Command(() => {
                   var client = new client()
                    {
                        Name = Name,
                        address = address,
                        bank = bank,
                        gence = agence,
                        num_account = numaccount
                    };
                   Ico.getValue<db>().GetUnivdb().clients.Add(client);
                   Ico.getValue<db>().savedb();
                   Ico.getValue<ContentApp>().back();

               });
        } 

        
        
    }
}
