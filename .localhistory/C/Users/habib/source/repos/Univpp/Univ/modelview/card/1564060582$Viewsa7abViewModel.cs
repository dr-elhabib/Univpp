using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using Univ.page.lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class Viewsa7abViewModel : BaseViewModel
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
               
        public double oldcost { get; set; } =0;

        
        public double newcost { get; set; } =0;

        public String  visa { get; set; }

        public Command addmo7asabi { get; set; }
        public Command back { get; set; }

        public ObservableCollection<Itemsa7ab> Itemsa7abs { get; set; }

        public Viewsa7abViewModel(process process)
        {
            inTilData(process);

            back = new  Command(()=> {
                Ico.getValue<ContentApp>().back();
            });

        }



        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
      //      this.inTilData(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(c=>c.id== card_kanoni.id).ToList().SingleOrDefault());
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


        public void inTilData(process process)
        {
            this.process = process;
            var cl = Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().ToList().LastOrDefault();
            if (cl != null)
            {
                this.oldcost = cl.old_cost;
            }
            else
            {
                this.oldcost = process.NewCost;
                this.newcost = process.NewCost;
            }
            Itemsa7abs = new ObservableCollection<Itemsa7ab>(Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c=>c.card.id_prosess==process.Id).ToList().Select(c => new Itemsa7ab(c)
            {
                action_edit = (t) => {
                 //   Sample4Content = new Editmo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                },
                start=()=>{ AcceptSample4Dialog();
                    AcceptSample4Dialog();
                },
                end= CancelSample4Dialog,
                addtashira = (t) => {
                  //  Sample4Content = new Addtashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                    this.inTilData(Ico.getValue<db>().GetUnivdb().processes.ToList().Where(N => N.Id == process.Id).ToList().SingleOrDefault());
                },
                edittashiraaction = (t) => {
                    //Sample4Content = new Edittashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }



            }));

            addmo7asabi = new Command(() => {
          //      if (Ico.getValue<Date>().GetPevDate() != null && process.cards.ToList().LastOrDefault().year1.Id== Ico.getValue<Date>().GetPevDate().Id
                if(Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c=>c.card.id_prosess== process.Id&&c.card.year1.Id== Ico.getValue<Date>().GetNowDate().Id).ToList().Count==0)
                {
                    Sample4Content = new Addsa7ab(process, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }
            });

        }

    }
    }
