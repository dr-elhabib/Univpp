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
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.modelview
{
    class Adddafa3VewModel : BaseViewModel<part>
    {

        public part part { get; set; }

        public double Cost { get; set; } = 0;
        public string AlCost { get; set; } = "";

        public string namepro { get; set; }
        public string namepart { get; set; }
        public double cost { get; set; }
        public string nameclient { get; set; }
        public string bankclient { get; set; }
        public string codebankclient { get; set; }
        private List<string> erour = new List<string>();
        public UserControl Sample4Content { get; set; }

        public Command savecommand { get; set; }
        public Command Cancelcommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Action saveElement;
        public Adddafa3VewModel(part part)
        {

            this.part = part;

            this.namepro = part.process.Name;
            this.cost = cost;
            this.namepart = part.Name;

            var client = part.card_kanoni.ToList().FirstOrDefault().client;

            this.nameclient = client.Name;
            this.codebankclient = client.num_account;
            this.bankclient = client.bank;


            savecommand = new Command(() =>
           {
               

               erour = new List<string>();


               string pattern = "[0-9]+";
               Regex rgx = new Regex(pattern);
               if (Cost == 0 || !rgx.IsMatch(cost.ToString()))
               {
                   erour.Add("الرجاء كتابة المبلغ ");

               }
               else
               {
                   if (!((part.mcost - part.nowcost) >= Cost))
                   {
                       erour.Add("المبلغ أكبر من الرصيد المتاح");
                   }

               }



               if (AlCost.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة  المبلغ حرفيا   ");

               }
               if (erour.Count == 0)
               {

                   acc();
                   Sample4Content = new Messagebox(erour, con);
               }
               else {
                   Creat_card(part);

               }
               //              var cardm = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.Id == card_mo7sabi.Id).SingleOrDefault();
               //    Ico.gtValue<ContentApp>().SetPage(new Viewdafa3(cardm));
           });
            Cancelcommand = new Command(() =>
            {
                con();

            });
        }

        public async Task Creat_card(part part)
        {
                await Task.Run(() =>
            {


                var card = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.id_part == part.Id && c.id_year == Ico.getValue<Date>().GetNowDate().Id).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                var num = 1;
                if (card != null)
                {
                    num = card.num + 1;
                }
                var d = DateTime.Now;
                var name = "بطاقة دفع رقم " + num + " سنة " + d.Year;
                var card_dafa3 = new card_dafa3()
                {
                    date = d,
                    num = num,
                    id_part = part.Id,
                    Cost = Cost,
                    alcost = AlCost,
                    location = Ico.getValue<IO>().CREATE_F_dafa3(part.process.location) + "\\" + name,
                    id_year = Ico.getValue<Date>().GetNowDate().Id,
                    tswiya = null

                };
                Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == part.Id).SingleOrDefault().nowcost += Cost;
                //  Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_kanoni.part.Id_Pro).ToList().First().parts.ToList().Where(p => p.Id == card_kanoni.id_part).ToList().First().nowcost += Cost;

                Ico.getValue<db>().GetUnivdb().card_dafa3.Add(card_dafa3);
                Ico.getValue<db>().savedb();
                Card_dafa3Execl c7 = new Card_dafa3Execl(Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.num == num && c.id_year == Ico.getValue<Date>().GetNowDate().Id).FirstOrDefault());
                c7.CreateCard();
                 con();

            });
        }


    }

}
    

