using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.modelview
{



    class AddpartCardViewModel : BaseViewModel<part>
    {
        public List<client> clients { get; set; }
        public client client { get; set; }
        public double cost { get; set; }
        public string nameprocess { get; set; }
        public string namepart { get; set; }
        public part part { set; get; }
        public Command savecommand { get; set; }
        public Command Cancelcommand { get; set; }
        public Command addclient { get; set; }
        public Action Cancel { get; set; }

        public AddpartCardViewModel(part part, Action accept, Action Cancel)
        {

            this.actionUP = () => {
                this.val = Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.Id).ToList().FirstOrDefault();
            };
            this.namepart = part.Name;
            this.cost = part.Cost;
            this.nameprocess = part.process.Name;
            this.part = part;
            this.Cancel = Cancel;
            clients = Ico.getValue<db>().GetUnivdb().clients.ToList();

            savecommand = new Command(() =>
            {
                Ico.getValue<ContentApp>().OpenSample4Dialog();
                Ico.getValue<ContentApp>().AcceptSample4Dialog();
                Creat_card(part);


            });
            Cancelcommand = new Command(() =>
            {

                Ico.getValue<ContentApp>().CancelSample4Dialog();
            });
            addclient = new Command(() =>
            {
                Ico.getValue<ContentApp>().page = new AddCLient();


            });





        }

        public async Task Creat_card(part part)
        {
          await  Task.Run(() =>
            {
            var kanon=   Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.Id).ToList().FirstOrDefault().card_kanoni.ToList().FirstOrDefault();
                Ico.getValue<db>().GetUnivdb().card_kanoni.Remove(kanon);
                Ico.getValue<db>().GetUnivdb().cards.Remove(Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c=>c.Id==kanon.id_card).ToList().FirstOrDefault());
                Ico.getValue<db>().savedb();
                var cardn = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == part.Id_Pro && c.year == Ico.getValue<Date>().GetNowDate().Id).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                var num = 1;
                if (cardn != null)
                {
                    num = cardn.num + 1;
                }


                var d = DateTime.Now;
                var name = "بطاقة إلتزام قانوني رقم " + num + " سنة " + d.Year;

                var card = new card()
                {
                    id_prosess = part.process.Id,
                    year = Ico.getValue<Date>().GetNowDate().Id,
                    num = num,
                    location = Ico.getValue<IO>().CREATE_F_kanoni(part.process.location) + "\\" + name,
                    date = DateTime.Now,
                };
                var kanoni = new card_kanoni()
                {
                    card = card,
                    id_client = client.Id,
                    id_part = part.Id,
                    cost = part.Cost,
                    visa = null
                };

                Ico.getValue<db>().GetUnivdb().cards.Add(card);
                Ico.getValue<db>().GetUnivdb().card_kanoni.Add(kanoni);
                Ico.getValue<db>().savedb();
                Card_kanoniExecl card_Kanoni = new Card_kanoniExecl(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(c => c.id_part == part.Id).ToList().FirstOrDefault());
                card_Kanoni.CreateCard();

                Ico.getValue<ContentApp>().CancelSample4Dialog();
            });
        }

        public bool IsSample4DialogOpen
        {
            get { return _isSample4DialogOpen; }
            set
            {
                if (_isSample4DialogOpen == value) return;
                _isSample4DialogOpen = value;
            }
        }

        public UserControl Sample4Content
        {
            get { return _sample4Content; }
            set
            {
                if (_sample4Content == value) return;
                _sample4Content = value;
            }
        }
        private bool _isSample4DialogOpen;
        private UserControl _sample4Content;


        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }

    }
}
