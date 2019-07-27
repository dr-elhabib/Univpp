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
    class Addsa7abiVewModel : BaseViewModel<process>
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
        List<p_sa7ab> ps = new List<p_sa7ab>();

        public Addsa7abiVewModel(process process)
        {
             this.date = Ico.getValue<Date>().GetPevDate().year1.Year + "/12/31";
            Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(get_data(process));
            IEnumerable<card_dafa3> get_data(process p)
            {
                  var cs = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.part.Id_Pro == p.Id && c.tswiya == null).ToList();
                foreach (var c in cs)
                {
                    Ico.getValue<IO>().DELETE_FILE(c.location+".xlsx");
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr=> pr.Id == c.id_part).First().nowcost -= c.Cost;
                }
                Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(cs);
                return cs;
            }
            
            foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
            {
                foreach (var m in p.card_mo7sabi.ToList().Where(c => c.visa == null).ToList())
                {

                    Ico.getValue<IO>().DELETE_FILE(m.card.location+ ".xlsx");
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
                var d = p.mcost-p.nowcost;
                if (d != 0) {

                    ps.Add(new p_sa7ab()
                    {
                        id_part = p.Id,
                        cost = d,
                    });
                    dn += d;
                }
            }
            Ico.getValue<db>().savedb();
            this.sa7abCost = dn;
            this.namepro = process.Name;
            this.Cost = Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pr => pr.Id == process.Id).ToList().First().NewCost;

            savecommand = new Command( () =>
            {

                if (sa7abCost != 0)
                {
                    acc();
                    Craete_Card(process);
                }
                else {
                    MessageBox.Show("لا تستطيع إستخراج بطاقة سحب لأن المبلغ = 0 دج");
                    con();

                }

            });


            Cancelcommand = new Command(() => {
                con();

            });
        }
        public async Task Craete_Card(process process) {
            await Task.Run(()=>{
                var d = DateTime.Now;
                var name = "بطاقة سحب الإلتزام سنة " + Ico.getValue<Date>().GetNowDate().year1.Year;

                var car = new card()
                {
                    date = DateTime.Now,
                    id_prosess = process.Id,
                    num = 1,
                    year = Ico.getValue<Date>().GetNowDate().Id,
                    location = Ico.getValue<IO>().CREATE_F_sa7ab(process.location) + "\\" + name
                };
                var card_sa7ab = new card_sa7ab()
                {
                    cost = sa7abCost,
                    card = car,
                    visa = null,
                    old_cost = Cost

                };

                Ico.getValue<db>().GetUnivdb().cards.Add(car);
                Ico.getValue<db>().GetUnivdb().card_sa7ab.Add(card_sa7ab);
                Ico.getValue<db>().GetUnivdb().processes.ToList().Where(pr => pr.Id == process.Id).ToList().First().NewCost += sa7abCost;

                foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
                {
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == p.Id).First().mcost = p.nowcost;

                }

                Ico.getValue<db>().savedb();
                var i = Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c => c.card.year == Ico.getValue<Date>().GetNowDate().Id).FirstOrDefault().id;
                foreach (p_sa7ab p in ps)
                {
                    p.id_sa7ab = i;
                    Ico.getValue<db>().GetUnivdb().p_sa7ab.Add(p);

                }
                Ico.getValue<db>().savedb();
                Card_sa7abExecl c7 = new Card_sa7abExecl(Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c => c.card.year== Ico.getValue<Date>().GetNowDate().Id).FirstOrDefault());
                c7.CreateCard();

                con();

            });
        }
    }

}
    

