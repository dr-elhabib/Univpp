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
                    if (a.card_7isab?.ToList().FirstOrDefault() != null&& a.card_7isab?.ToList().FirstOrDefault().visa != null)
                    {
                        visibility = Visibility.Collapsed;
                    }
                }
            }
            remove = new Command(() =>
                {

                    action(this);
                });


                edit = new Command(() =>
                {
                    var prr = Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id == part.Id).FirstOrDefault();

                    if (prr.card_mo7sabi.ToList().Count == 0 && prr.card_dafa3.ToList().Count == 0)
                    {
 Ico.getValue<ContentApp>().page = new EditPart(part);
                    }
                    else {

                        Ico.getValue<ContentApp>().OpenSample4Dialog();
                        Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "الحصة تحتوي على مجموعه من البطاقات الرجاء حذفها وإعد المحاولة  ..   " },
                       Ico.getValue<ContentApp>().CancelSample4Dialog);

                    }
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
                          Ico.getValue<ContentApp>().OpenSample4Dialog();
                           Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "الرجاء إستخرج بطاقة إلتزام قانوني مسبقااا" },
                            Ico.getValue<ContentApp>().CancelSample4Dialog);
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
                        Ico.getValue<ContentApp>().OpenSample4Dialog();
                        Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "الرجاء إستخرج بطاقة إلتزام محاسبي مسبقااا" },
                         Ico.getValue<ContentApp>().CancelSample4Dialog);
                    }
                });
            }

        
    }
}
