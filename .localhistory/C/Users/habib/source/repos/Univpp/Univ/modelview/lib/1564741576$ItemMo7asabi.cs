using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.modelview
{
 public  class ItemMo7asabi : BaseViewModel<card_mo7sabi>
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
        public Command print { get; set; }
        public Action<card_mo7sabi> action_edit { get; set; }
        public Action end { get; set; }
        public Action start { get; set; }

        public Visibility visibility { get; set; }
        public Visibility tashiravis { get; set; }
        public Visibility edittashiravis { get; set; }

        public Action<card_mo7sabi> edittashiraaction { get; set; }
        public Action<card_mo7sabi> addtashira { get; set; }
        public Command tashira { get; set; }
        public Command edittashira { get; set; }
        public ViewMo7sabiViewModel ViewMo7sabiViewModel { get; set; }
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

            tashira = new Command(() => {
            bool can = true;
            foreach (var c in Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.part.Id_Pro == Ico.getValue<db>().GetUnivdb().cards.
            ToList().Where(cl => cl.Id == card_mo7sabi.id_card).ToList().SingleOrDefault().id_prosess && c.card.date < Ico.getValue<db>().GetUnivdb().cards.
            ToList().Where(cl => cl.Id == card_mo7sabi.id_card).ToList().SingleOrDefault().date))
            {
                if (c.visa == null)
                {
                    can = false;
                    break;
                }
            }

            if (can)
            {

                    addtashira(card_mo7sabi);
                     }   
                else
                {
                    MessageBox.Show("هنالك بطاقة ليست لها تأشيرة قبل هذه البطاقة ");

                }
            });
            edittashira = new Command(() => {

                edittashiraaction(card_mo7sabi);
            });
            remove = new Command(() => {
                start();
               
            });

            open = new Command(async () => {
                ViewMo7sabiViewModel.Sample4Contentviw(new Progressbar());
               
                await Task.Run(() => {
                    ExcelHlper.OpenFile(card_mo7sabi.card.location);
                    end();
                });
            });
            print = new Command(async () => {
                ViewMo7sabiViewModel.Sample4Contentviw(new Progressbar());
                await Task.Run(() => {
                    ExcelHlper.PrintFile(card_mo7sabi.card.location);
                    end();
                });

            });
        }

    
    }
}
