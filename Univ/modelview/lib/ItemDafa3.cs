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
 public  class ItemDafa3:BaseViewModel
    {

        public DateTime date { get; set; }
        public double Cost { get; set; }
        public string alCost { get; set; }
        public int num { get; set; }
        public Visibility visibility { get; set; }
        public Visibility tswiyavis { get; set; }
        public Visibility edittswiyavis { get; set; }


        public Command remove { get; set; }
        public Command edit { get; set; }
        public Command open { get; set; }
        public Command tswiya { get; set; }
        public Command edittswiya { get; set; }
        public Command add_Mo7asabi { get; set; }
        public Action<double> action { get; set; }
        public Action<card_dafa3> action_edit { get; set; }
        public Action<card_dafa3> edittswiyaaction { get; set; }
        public Action<card_dafa3> addtswiya { get; set; }
        public Action action_Mo7asabi { get; set; }

        public ItemDafa3(card_dafa3 card_dafa3)
        {
            this.Cost = card_dafa3.Cost;
            this.num = card_dafa3.num;
            this.date = card_dafa3.date;
            this.alCost = card_dafa3.alcost;
            visibility = Visibility.Visible;
            tswiyavis = Visibility.Visible;
            edittswiyavis = Visibility.Collapsed;
            MessageBox.Show(card_dafa3.tswiya);

            if (card_dafa3.tswiya!=null)
            {
                visibility = Visibility.Collapsed;
                edittswiyavis = Visibility.Visible;
                tswiyavis = Visibility.Collapsed;
                MessageBox.Show(card_dafa3.tswiya);
            }

            remove = new Command(() => {
                Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == card_dafa3.id_part).ToList().SingleOrDefault().nowcost -= Cost;
                Ico.getValue<db>().GetUnivdb().card_dafa3.Remove(Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).FirstOrDefault());
                Ico.getValue<db>().savedb();
                //     var cardm = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.Id == mo7asbi.Id).SingleOrDefault();
                //     Ico.getValue<ContentApp>().SetPage(new Viewdafa3(cardm));
                action(Cost);

            });


            edit = new Command(() => {

                action_edit(card_dafa3);
            });
            tswiya = new Command(() => {

                addtswiya(card_dafa3);
            });
            edittswiya = new Command(() => {

                edittswiyaaction(card_dafa3);
            });
            open = new Command(() => {
                Card_dafa3Execl Card_dafa3Execl = new Card_dafa3Execl(card_dafa3);
                Card_dafa3Execl.CreateCard();

            });
            add_Mo7asabi = new Command(() => {
                //                Ico.getValue<ContentApp>().page = new AddPartCard(part);

       //         action_Mo7asabi();
            });
         
        }

    
}
}
