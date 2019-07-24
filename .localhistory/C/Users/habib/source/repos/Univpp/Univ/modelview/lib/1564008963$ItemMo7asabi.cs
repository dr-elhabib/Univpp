using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview
{
 public  class ItemMo7asabi : BaseViewModel
    {

        public DateTime date { get; set; }
        public string Cost { get; set; }
        public string visa { get; set; } = "لا  توجد";
        public string  nowcost { get; set; }
        public string  oldcost { get; set; }
        public int num { get; set; }
        public Command remove { get; set; }
        public Command edit { get; set; }
        public Command open { get; set; }
        public Command add_Mo7asabi { get; set; }
        public Action<card_mo7sabi> action_edit { get; set; }
        public Action action_Mo7asabi { get; set; }
        public Action end { get; set; }
        public Action start { get; set; }

        public Visibility visibility { get; set; }
        public Visibility tashiravis { get; set; }
        public Visibility edittashiravis { get; set; }

        public Action<card_mo7sabi> edittashiraaction { get; set; }
        public Action<card_mo7sabi> addtashira { get; set; }
        public Command tashira { get; set; }
        public Command edittashira { get; set; }

        public ItemMo7asabi(card_mo7sabi card_mo7sabi)
        {
            this.Cost = String.Format("{0:0.00}", card_mo7sabi.cost);
            this.num = card_mo7sabi.card.num;
            this.date = card_mo7sabi.card.date; ;
            this.oldcost = String.Format("{0:0.00}", card_mo7sabi.oldCost);
            this.nowcost = String.Format("{0:0.00}", card_mo7sabi.oldCost-card_mo7sabi.cost);


            visibility = Visibility.Visible;
            tashiravis = Visibility.Visible;
            edittashiravis = Visibility.Collapsed;

            if (card_mo7sabi.visa != null)
            {
                visibility = Visibility.Collapsed;
                edittashiravis = Visibility.Visible;
                tashiravis= Visibility.Collapsed;

                visa = card_mo7sabi.visa;
            }


            edit = new Command(() => {
                action_edit(card_mo7sabi);
            });

            open = new Command(() => {
                Card_mo7asabiExecl card_ = new Card_mo7asabiExecl(card_mo7sabi);
                card_.CreateCard();
            });
            tashira = new Command(() => {

                addtashira(card_mo7sabi);
            });
            edittashira = new Command(() => {

                edittashiraaction(card_mo7sabi);
            });
            remove = new Command(() => {
                start();
                Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_mo7sabi.part.Id_Pro).ToList().SingleOrDefault().NewCost += card_mo7sabi.cost;
                Ico.getValue<db>().changedNumCard(card_mo7sabi.card);
                Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(get_data(card_mo7sabi));
                IEnumerable<card_dafa3> get_data(card_mo7sabi card)
                {

                    var cs = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => (c.id_part == card_mo7sabi.id_part) && (c.tswiya == null) && (c.date > card_mo7sabi.card.date));

                    foreach (var c in cs)
                    {
                        Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == card_mo7sabi.id_part).First().nowcost -= c.Cost;
                    }
                    return cs;
                }
                Ico.getValue<db>().GetUnivdb().card_mo7sabi.Remove(Ico.getValue<db>().GetUnivdb().card_mo7sabi.
   ToList().Where(c => c.Id == card_mo7sabi.Id).ToList().SingleOrDefault());

                Ico.getValue<db>().GetUnivdb().cards.Remove(Ico.getValue<db>().GetUnivdb().cards.
   ToList().Where(c => c.Id == card_mo7sabi.id_card).ToList().SingleOrDefault());
                foreach (var c in Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.num > card_mo7sabi.num)) {
                    c.oldCost += card_mo7sabi.cost;
                    c.num -= 1;

                }
          
                Ico.getValue<db>().savedb();
                end();
            });
            add_Mo7asabi = new Command(() => {
                //                Ico.getValue<ContentApp>().page = new AddPartCard(part);

       //         action_Mo7asabi();
            });
         
        }

    
}
}
