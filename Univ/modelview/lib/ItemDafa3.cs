using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.page.lib;

namespace Univ.modelview
{
 public  class ItemDafa3:BaseViewModel<card_dafa3>
    {

        public DateTime date { get; set; }
        public double Cost { get; set; }
        public string alCost { get; set; }
        public string kasima { get; set; } = "لا توجد يعد";
        public string tswiyas { get; set; }
        public int num { get; set; }
        public Visibility visibility { get; set; }
        public Visibility tswiyavis { get; set; }
        public Visibility edittswiyavis { get; set; }

        public Action end { get; set; }
        public Viewdafa3VewModel Viewdafa3VewModel { get; set; }

        public Command remove { get; set; }
        public Command edit { get; set; }
        public Command open { get; set; }
        public Command tswiya { get; set; }
        public Command edittswiya { get; set; }
        public Command print { get; set; }
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
            this.tswiyas = card_dafa3.tswiya;
            visibility = Visibility.Visible;
            tswiyavis = Visibility.Visible;
            edittswiyavis = Visibility.Collapsed;

            if (card_dafa3.kasima!=null)
            {
                visibility = Visibility.Collapsed;
                edittswiyavis = Visibility.Visible;
                tswiyavis = Visibility.Collapsed;
                this.kasima = card_dafa3.kasima;

            }

            remove = new Command(() => {
                //     var cardm = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.Id == mo7asbi.Id).SingleOrDefault();
                //     Ico.getValue<ContentApp>().SetPage(new Viewdafa3(cardm));
                action(Cost);

            });


            edit = new Command(() => {

                action_edit(card_dafa3);
            });
            tswiya = new Command(() => {

            bool can = true;
            foreach (var c in Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.id_part == card_dafa3.id_part && c.date < card_dafa3.date))
            {
                if (c.tswiya == null)
                {
                    can = false;
                    break;
                }
            }

                if (can)
                {

                    addtswiya(card_dafa3);
                }
                else
                {
                    MessageBox.Show("هنالك بطاقة ليست لها تأشيرة قبل هذه البطاقة ");
                }
            });
            edittswiya = new Command(() => {

                edittswiyaaction(card_dafa3);
            });


            open = new Command(async () => {
                Viewdafa3VewModel.Sample4Contentviw(new Progressbar());

                await Task.Run(() => {
                    ExcelHlper.OpenFile(card_dafa3.location);
                    end();
                });
            });
            print = new Command(async () => {
                Viewdafa3VewModel.Sample4Contentviw(new Progressbar());
                await Task.Run(() => {
                    ExcelHlper.PrintFile(card_dafa3.location);
                    end();
                });

            });

        }

    
}
}
