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

            inTilData();

            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

        }
        public void inTilData()
        {
            this.card = val.card;
            this.process = val.card.process;
            this.cost = val.cost;

            this.part = new Part(val.part);
            this.client = val.client.Name;



            visibility = Visibility.Visible;
            tashiravis = Visibility.Visible;
            edittashiravis = Visibility.Collapsed;

            if (val.visa != null)
            {
                visibility = Visibility.Collapsed;
                edittashiravis = Visibility.Visible;
                tashiravis = Visibility.Collapsed;

                visa = val.visa;
            }

            tashira = new Command(() => {
                Sample4Content = new Addtashira_kanoni(val, AcceptSample4Dialog, CancelSample4Dialog);
                OpenSample4Dialog();
                this.inTilData();
            });
            edittashira = new Command(() => {
                Sample4Content = new Edittashira_kanoni(val, AcceptSample4Dialog, CancelSample4Dialog);
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
            this.inTilData();
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }

    }
}
