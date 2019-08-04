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
    class Viewsa7abViewModel : BaseViewModel<process>
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
               
        
        public double newcost { get; set; } =0;

        public String  visa { get; set; }

        public Command addmo7asabi { get; set; }
        public Command back { get; set; }

        public ObservableCollection<Itemsa7ab> Itemsa7abs { get; set; }

        public Viewsa7abViewModel(process process)
        {

            this.actionUP = () => {
                this.val = Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == process.Id).ToList().FirstOrDefault();
            };
            inTilData();

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
            this.inTilData();
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


        public void inTilData()
        {
            actionUP();
            this.process = val;
               this.newcost = val.NewCost;
            
            Itemsa7abs = new ObservableCollection<Itemsa7ab>(Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c=>c.card.id_prosess==process.Id).ToList().Select(c => new Itemsa7ab(c)
            {
                start=()=>{ AcceptSample4Dialog();
                    AcceptSample4Dialog();
                },
                end= CancelSample4Dialog,
                addtashira = (t) => {
                  Sample4Content = new Addtashira_sa7ab(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                },
                edittashiraaction = (t) => {
                    Sample4Content = new Edittashira_sa7ab(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }



            }));

            addmo7asabi = new Command(() => {
      /*      MessageBox.Show((process.cards.ToList().LastOrDefault() != null)+"");
            MessageBox.Show((Ico.getValue<Date>().GetPevDate() != null )+"");
                MessageBox.Show((process.cards.ToList().LastOrDefault().year1.Id != Ico.getValue<Date>().GetPevDate().Id) + "");
                     MessageBox.Show((Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c => (c.card.id_prosess == process.Id) &&
                (c.card.year1.Id == Ico.getValue<Date>().GetNowDate().Id)).ToList().Count != 0) + "");
                */
                
                
                    if ((process.date.Year== Ico.getValue<Date>().GetPevDate().year1.Year) || (Ico.getValue<Date>().GetPevDate() == null) ||(process.cards.ToList().LastOrDefault()!=null&&Ico.getValue<Date>().GetPevDate() != null && process.cards.ToList().LastOrDefault().year1.Id!= Ico.getValue<Date>().GetPevDate().Id&&Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c=>(c.card.id_prosess== process.Id)&&(c.card.year1.Id== Ico.getValue<Date>().GetNowDate().Id)).ToList().Count!=0))
                {
                    MessageBox.Show("لا تستطيغ إستخراج باقة سحب بعد");
                }
                else
                {

                    Sample4Content = new Addsa7ab(process, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();

                }
            });

        }

    }
    }
