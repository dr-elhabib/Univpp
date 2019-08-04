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
using Univ.page.lib;

namespace Univ.modelview
{
    class Editmo7asabiVewModel : BaseViewModel<card_mo7sabi>
    {

        public part part { get; set; }

        public double Cost { get; set; }

        public string namepro { get; set; }
        public string namepart { get; set; }
        public double cost { get; set; }
        public string nameclient { get; set; }
        public string bankclient { get; set; }
        public string codebankclient { get; set; }

        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }

        public Editmo7asabiVewModel(card_mo7sabi card_mo7sabi)
        {
            part = card_mo7sabi.part;
            this.namepro = card_mo7sabi.part.process.Name;
            this.cost = card_mo7sabi.cost;
            this.namepart = part.Name;
            var client = card_mo7sabi.client;
            this.nameclient = client.Name;
            this.codebankclient = client.num_account;
            this.bankclient = client.bank;
            this.Cost = card_mo7sabi.cost;
            savecommand = new Command(async() =>
           {

           var d = 0d;
           foreach (var c in part.card_dafa3.ToList().Where(c => c.num < card_mo7sabi.num))
           {
               d += c.Cost;
           }

               Ico.getValue<ContentApp>().OpenSample4Dialog();
               if ((part.Cost - d) > Cost)
               {
                   
                   Ico.getValue<ContentApp>().AcceptSample4Dialog();
                   await Task.Run(() =>
                   {
                       Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.Id == card_mo7sabi.Id).SingleOrDefault().cost = Cost;
                        //                Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == card_mo7sabi.id_part).First().nowcost -= (card_mo7sabi.cost - Cost);
                        //           Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_mo7sabi.part.Id_Pro).First().parts.ToList().Where(p => p.Id == card_mo7sabi.id_part).First().nowcost -= (card_mo7sabi.cost - Cost);
                        Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_mo7sabi.part.Id_Pro).First().NewCost += (card_mo7sabi.cost - Cost);
                       Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(get_data(card_mo7sabi));
                       Ico.getValue<db>().savedb();
                       acc();
                       Ico.getValue<ContentApp>().CancelSample4Dialog();
                   });
                    }
               else {
                   Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { " المبلغ أكبر من الرصيد المتاح  " }, Ico.getValue<ContentApp>().CancelSample4Dialog);
                   

               }

           });


            IEnumerable<card_dafa3> get_data(card_mo7sabi card)
            {

                var cs = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => (c.id_part == card_mo7sabi.id_part) && (c.kasima == null) && (c.date > card_mo7sabi.card.date));

                foreach (var c in cs)
                {
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == card_mo7sabi.id_part).First().nowcost -= c.Cost;
                }
                return cs;
            }

            Cancelcommand = new Command(() => {
                Ico.getValue<ContentApp>().CancelSample4Dialog();
            });
        }

    }

}
    

