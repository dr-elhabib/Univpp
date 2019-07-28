using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview.lib
{
    class ItemPart : BaseViewModel<part>
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public int num { get; set; }

        public Command remove { get; set; }
        public Command edit { get; set; }
        public Command view { get; set; }
        public Command add_Mo7asabi { get; set; }
        public Command add_kanoni { get; set; }
        public Action<ItemPart> action { get; set; }
        public Action action_kanoni { get; set; }
        public Command dafa3action { get; set; }
        public Visibility visibility { get; set; } = Visibility.Visible;

        public ItemPart(part part)
        {
            this.Name = part.Name;
            this.Cost = part.Cost;
            this.num = part.num;
            var lc = part.process.cards;
            if (lc.ToList().Count > 0)
            {
                var a = lc.ToList().Where(c => c.card_7isab.Count > 0).FirstOrDefault();
                if (a != null)
                {
                    if (a.card_7isab?.ToList().FirstOrDefault().visa != null)
                    {
                        visibility = Visibility.Hidden;
                    }
                }
            }
            remove = new Command(() =>
                {

                    action(this);
                });


                edit = new Command(() =>
                {

                    Ico.getValue<ContentApp>().page = new EditPart(part);

                });


                view = new Command(() =>
                {

                    Ico.getValue<ContentApp>().page = new ViewPart(part);

                });
                add_Mo7asabi = new Command(() =>
                {
                    var card = Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(c => c.part.Id == part.Id).FirstOrDefault();
                    if (card != null)
                    {

                        Ico.getValue<ContentApp>().page = new ViewMo7sabi(card);
                    }
                    else
                    {
                        MessageBox.Show("الرجاء إستخرج بطاقة إلتزام قانوني مسبقااا");
                    }
                });

                add_kanoni = new Command(() =>
                {
                    action_kanoni();
                });
                dafa3action = new Command(() =>
                {
                    var card = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.id_part == part.Id).FirstOrDefault();
                    if (card != null)
                    {
                        Ico.getValue<ContentApp>().page = new Viewdafa3(card.part);
                    }
                    else
                    {
                        MessageBox.Show("الرجاء إستخرج بطاقة إلتزام محاسبي مسبقااا");
                    }
                });
            }

        
    }
}
