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
    class Addsa7abiVewModel : BaseViewModel
    {


        public double Cost { get; set; }
        public double sa7abCost { get; set; } = 0;
        
        public string namepro { get; set; }
        public string date { get; set; }
        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Action<ItemDafa3> saveElement;
        public Addsa7abiVewModel(process process)
        {
         //   this.date = Ico.getValue<Date>().GetPevDate().year1.Year + "/12/31";
            Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(get_data(process));
            IEnumerable<card_dafa3> get_data(process p)
            {
                  var cs = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.part.Id_Pro == p.Id && c.tswiya == null).ToList();
                foreach (var c in cs)
                {
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr=> pr.Id == c.id_part).First().nowcost -= c.Cost;
                }
                Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(cs);
                return cs;
            }
            
            foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
            {
                foreach (var m in p.card_mo7sabi.ToList().Where(c => c.visa == null).ToList())
                {

                    Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pr => pr.Id == process.Id).ToList().First().NewCost += m.cost;
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == m.id_part).First().mcost-= m.cost;
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.Remove(Ico.getValue<db>().GetUnivdb().card_mo7sabi.
                    ToList().Where(c => c.Id == m.Id).ToList().SingleOrDefault());
                    Ico.getValue<db>().GetUnivdb().cards.Remove(Ico.getValue<db>().GetUnivdb().cards.
                    ToList().Where(c => c.Id == m.id_card).ToList().SingleOrDefault());

                }
            ///    this.sa7abCost+= Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == p.Id).First().mcost-= Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == p.Id).First().nowcost;
            }
            var dn = 0.0;
            foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
            {
                MessageBox.Show(p.Name + " " + p.mcost+" - "+p.nowcost);
                dn += p.mcost-p.nowcost;
            }
            Ico.getValue<db>().savedb();
            this.sa7abCost = dn;
            this.namepro = process.Name;
            this.Cost = Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pr => pr.Id == process.Id).ToList().First().NewCost;

            savecommand = new Command( () =>
            {
                
                    var car = new card()
                    {
                        date = DateTime.Now,
                        id_prosess = process.Id,
                        num = 1,
                        year = Ico.getValue<db>().GetUnivdb().years.ToList().LastOrDefault().Id,
                        location = ""
                    };
                    var card_sa7ab = new card_sa7ab()
                    {
                        cost = sa7abCost,
                        card = car,
                        visa = null,
                        old_cost= Cost

                    };

                    Ico.getValue<db>().GetUnivdb().cards.Add(car);
                    Ico.getValue<db>().GetUnivdb().card_sa7ab.Add(card_sa7ab);
                    Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pr => pr.Id == process.Id).ToList().First().NewCost += sa7abCost;

                foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
                {
                       Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == p.Id).First().mcost =p.nowcost;

                }

                Ico.getValue<db>().savedb();
                  con();
                
            });


            Cancelcommand = new Command(() => {
                con();

            });
        }
    }

}
    

