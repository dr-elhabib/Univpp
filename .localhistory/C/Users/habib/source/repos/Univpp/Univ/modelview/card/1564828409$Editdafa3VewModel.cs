using Univ.lib;
using Univ.lib.Enum;
using Univ.modeldb.model;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.page;

namespace Univ.modelview
{
    class Editdafa3VewModel : BaseViewModel<card_dafa3>
    {

        public part part { get; set; }

        public double Cost { get; set; }
        public string AlCost { get; set; }
        public string tswiya { get; set; }

        public string namepro { get; set; }
        public string namepart { get; set; }
        public double cost { get; set; }
        public string nameclient { get; set; }
        public string bankclient { get; set; }
        public string codebankclient { get; set; }

        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public Command back{get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }

        public Editdafa3VewModel(card_dafa3 card_dafa3)
        {
            part = card_dafa3.part;
            this.namepro = card_dafa3.part.process.Name;
            this.cost = card_dafa3.Cost;
            this.namepart = part.Name;
            var client = card_dafa3.part.card_kanoni.ToList().FirstOrDefault().client;
            this.nameclient = client.Name;
            this.codebankclient = client.num_account;
            this.bankclient = client.bank;
            this.Cost = card_dafa3.Cost;
            this.AlCost = card_dafa3.alcost;
            this.tswiya = card_dafa3.tswiya;
            savecommand = new Command( () =>
            {
                
                var d = 0d;
                foreach (var c in part.card_mo7sabi.ToList()) {
                    d += c.cost;
                }
                var d2 = 0d;
                foreach (var c in part.card_dafa3.ToList().Where(c => c.num < card_dafa3.num))
                {
                    d2 += c.Cost;
                }
                Ico.getValue<ContentApp>().OpenSample4Dialog();

                if ((d - d2) > Cost)
                {
                    Ico.getValue<ContentApp>().AcceptSample4Dialog();
                    Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).SingleOrDefault().Cost = Cost;
                    Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).SingleOrDefault().tswiya = tswiya;
                    Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).SingleOrDefault().alcost = AlCost;
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == card_dafa3.id_part).SingleOrDefault().nowcost -= (card_dafa3.Cost - Cost);
                    Ico.getValue<db>().savedb();
                    acc();
                    Ico.getValue<ContentApp>().CancelSample4Dialog();
                }
                else {
                    Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { " المبلغ أكبر من الرصيد المتاح " }, Ico.getValue<ContentApp>().CancelSample4Dialog);

                }
            });
            back = new Command(()=> {
                Ico.getValue<ContentApp>().back();
            });

            Cancelcommand = new Command(() => {
                Ico.getValue<ContentApp>().CancelSample4Dialog();

            });
        }
    }

}
    

