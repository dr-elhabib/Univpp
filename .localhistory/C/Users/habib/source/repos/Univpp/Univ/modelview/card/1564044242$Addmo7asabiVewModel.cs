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
    class Addmo7asabiVewModel : BaseViewModel
    {

        public part part { get; set; }

        public double Cost { get; set; }
        
        public string namepro { get; set; }
        public string namepart { get; set; }
        public string client { get; set; }
        public string subject { get; set; }
        public double cost { get; set; }
      
        public Command savecommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Action<ItemDafa3> saveElement;
        public Addmo7asabiVewModel(card_kanoni card_kanoni)
        {
            part = card_kanoni.part;
            this.namepro = card_kanoni.part.process.Name;
            this.cost = card_kanoni.cost;
            this.namepart = part.Name;
            this.client = card_kanoni.client.Name;
            //  var carda = Ico.getValue<db>().GetUnivdb().years.Where(y => y.year1.Year == DateTime.Now.Year).ToList().FirstOrDefault().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro)
            //    .ToList().FirstOrDefault();

            var carda = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => (c.card.year1.Id == Ico.getValue<db>().GetUnivdb().years.ToList()
            .Where(y => y.year1.Year == DateTime.Now.Year).ToList().FirstOrDefault().Id)&& c.id_part == card_kanoni.id_part)
                .ToList().OrderByDescending(c => c.num).ToList().FirstOrDefault();
            //.card_mo7sabi.Where(c=>c.id_part== card_kanoni.id_part).OrderByDescending(c=>c.num).LastOrDefault();
            
            var numm = 1;
            if (carda != null) {
                numm = carda.num + 1;
            }
            var nums = (numm.ToString().Length == 1) ? "0" + numm.ToString() : numm.ToString();
            this.subject = "الإلتزام المحاسبي رقم "+ nums + " للعقد المتعلق بالعملية "+ part.process.Name;

            savecommand = new Command( () =>
            {

            var d = 0d;
            foreach (var c in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p=>p.Id==part.Id).ToList().FirstOrDefault().card_mo7sabi.ToList())
            {
                d += c.cost;
            }
                MessageBox.Show(d+"");
                if ((part.Cost - d) >= Cost)
                {
                    acc();

                    var card = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                    var num = 1;
                    if (card != null)
                    {
                        num = card.num + 1;
                    }


                    var car = new card()
                    {
                        date = DateTime.Now,
                        id_prosess = card_kanoni.part.Id_Pro,
                        num = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro).LastOrDefault().num + 1,
                        year = Ico.getValue<db>().GetUnivdb().years.ToList().LastOrDefault().Id
                     ,
                        location = ""
                    };
                    var card_mo7sabi = new card_mo7sabi()
                    {
                        id_client = card_kanoni.id_client,
                        id_part = card_kanoni.id_part,
                        cost = Cost,
                        oldCost = card_kanoni.part.process.NewCost,
                        card = car,
                        num = numm,
                        visa = null,
                        subject = subject


                    };

                    Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_kanoni.part.Id_Pro).ToList().First().NewCost -= Cost;
                    Ico.getValue<db>().GetUnivdb().cards.Add(car);
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.Add(card_mo7sabi);
                    Ico.getValue<db>().savedb();

                    con();
                }
                else {

                    MessageBox.Show("المبلغ أكبر من الرصيد المتاح");

                }
            });

        }
    }

}
    

