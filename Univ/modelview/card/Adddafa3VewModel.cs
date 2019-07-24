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
    class Adddafa3VewModel : BaseViewModel
    {

        public part part { get; set; }

        public double Cost { get; set; }
        public string AlCost { get; set; }

        public string namepro { get; set; }
        public string namepart { get; set; }
        public double cost { get; set; }
        public string nameclient { get; set; }
        public string bankclient { get; set; }
        public string codebankclient { get; set; }

        public Command savecommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Action saveElement;
        public Adddafa3VewModel(part  part)
        {
            this.part = part;
            this.namepro = part.process.Name;
            this.cost = cost;
            this.namepart = part.Name;
            var client = part.card_kanoni.ToList().FirstOrDefault().client;
            this.nameclient = client.Name;
            this.codebankclient = client.num_account;
            this.bankclient = client.bank;


            savecommand = new Command( () =>
            {;
            var d = 0d;
            foreach (var c in part.card_mo7sabi.ToList())
            {
                d += c.cost;
            }
            var d2 = 0d;
            foreach (var c in part.card_dafa3.ToList())
            {
                d2 += c.Cost;

                }

                if ((d - d2) > Cost)
                {

                    acc();
                    var card = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.id_part == part.Id).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                    var num = 1;
                    if (card != null)
                    {
                        num = card.num + 1;
                    }

                    var card_dafa3 = new card_dafa3()
                    {
                        date = DateTime.Now,
                        num = num,
                        id_part = part.Id,
                        Cost = Cost,
                        alcost = AlCost,
                        location = " ",
                        tswiya = null

                    };
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == part.Id).SingleOrDefault().nowcost += Cost;
                    //  Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_kanoni.part.Id_Pro).ToList().First().parts.ToList().Where(p => p.Id == card_kanoni.id_part).ToList().First().nowcost += Cost;

                    Ico.getValue<db>().GetUnivdb().card_dafa3.Add(card_dafa3);
                    Ico.getValue<db>().savedb();
                    saveElement();

                    con();
                }
                else {
                    MessageBox.Show("المبلغ أكبر من الرصيد المتاح");

                }
                //              var cardm = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.Id == card_mo7sabi.Id).SingleOrDefault();
                //    Ico.getValue<ContentApp>().SetPage(new Viewdafa3(cardm));
            });

        }
    }

}
    

