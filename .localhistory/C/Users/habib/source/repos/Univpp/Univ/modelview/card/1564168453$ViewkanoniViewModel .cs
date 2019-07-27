using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using Univ.page.lib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class ViewkanoniViewModel : BaseViewModel<card_kanoni>
    {

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







        public process process { get; set; }
        public card card { get; set; }
        public Part part { get; set; }


        public string client { get; set; }
        public double cost { get; set; } = 0;
        public String visa { get; set; } = "لم يتم الحصول على التأشرة بعد";

        public Command back { get; set; }

        public Visibility visibility { get; set; }
        public Visibility tashiravis { get; set; }
        public Visibility edittashiravis { get; set; }

        public Action<card_mo7sabi> edittashiraaction { get; set; }
        public Action<card_mo7sabi> addtashira { get; set; }
        public Command tashira { get; set; }
        public Command edittashira { get; set; }


        public ViewkanoniViewModel(card_kanoni card)
        {
            /* Card_kanoniExecl card_Kanoni = new Card_kanoniExecl(card);
            card_Kanoni.CreateCard();
            */

            inTilData(card);

            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

        }
        public void inTilData(card_kanoni card)
        {
            this.card = card.card;
            this.process = card.card.process;
            this.cost = card.cost;

            this.part = new Part(card.part);
            this.client = card.client.Name;



            visibility = Visibility.Visible;
            tashiravis = Visibility.Visible;
            edittashiravis = Visibility.Collapsed;

            if (card.visa != null)
            {
                visibility = Visibility.Collapsed;
                edittashiravis = Visibility.Visible;
                tashiravis = Visibility.Collapsed;

                visa = card.visa;
            }

            tashira = new Command(() => {
                Sample4Content = new Addtashira_kanoni(card, AcceptSample4Dialog, CancelSample4Dialog);
                OpenSample4Dialog();
                this.inTilData(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(N => N.id == card.id).ToList().SingleOrDefault());

            });
            edittashira = new Command(() => {
                Sample4Content = new Edittashira_kanoni(card, AcceptSample4Dialog, CancelSample4Dialog);
                OpenSample4Dialog();

            });


        }
        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
            this.inTilData(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(N => N.id_card == card.Id).ToList().SingleOrDefault());

        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }

    }
}
