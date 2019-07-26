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
    class ViewMo7sabiViewModel : BaseViewModel
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
        public card_kanoni card_kanoni { get; set; }
        public Part part { get; set; }
               
        public double oldcost { get; set; } =0;

        public string client { get; set; }
        public double newcost { get; set; } =0;
        public String  visa { get; set; }

        public Command addmo7asabi { get; set; }
        public Command back { get; set; }

        public ObservableCollection<ItemMo7asabi> ItemMo7asabis { get; set; }

        public ViewMo7sabiViewModel(card_kanoni card_kanoni)
        {
            this.card_kanoni = card_kanoni;
            inTilData(card_kanoni);

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
            this.inTilData(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(c=>c.id== card_kanoni.id).ToList().SingleOrDefault());
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


        public void inTilData(card_kanoni card_kanoni)
        {
            this.process = card_kanoni.card.process;
            this.part = new Part(card_kanoni.part);
            this.client = card_kanoni.client.Name;
            this.newcost = card_kanoni.part.process.NewCost;
            ItemMo7asabis = new ObservableCollection<ItemMo7asabi>(card_kanoni.part.card_mo7sabi.Select(c => new ItemMo7asabi(c)
            {
                action_edit = (t) => {
                    Sample4Content = new Editmo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                },
                start=()=>{ AcceptSample4Dialog();
                    AcceptSample4Dialog();
                },
                end= CancelSample4Dialog,
                addtashira = (t) => {
                    Sample4Content = new Addtashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                    this.inTilData(Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(N => N.id == card_kanoni.id).ToList().SingleOrDefault());
                },
                edittashiraaction = (t) => {
                    Sample4Content = new Edittashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }



            }));

            addmo7asabi = new Command(() => {
                
                var card = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.id_part == card_kanoni.id_part).ToList().FirstOrDefault();
                MessageBox.Show((card != null) + "");
                MessageBox.Show((card.card.year == Ico.getValue<Date>().GetNowDate()?.Id) + "");
                MessageBox.Show((Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c =>
                     c.card.id_prosess == card_kanoni.card.id_prosess&& c.card.year == Ico.getValue<Date>().GetNowDate().Id).ToList().ToList().Count == 0) + "");

                if (card != null && card.card.year != Ico.getValue<Date>().GetNowDate()?.Id && Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c =>
                   c.card.id_prosess == card_kanoni.card.id_prosess&&c.card.year== Ico.getValue<Date>().GetNowDate().Id).ToList().ToList().Count == 0)
                {
                    
                    MessageBox.Show(" الرجاء التأكد من إستخراج بظاقة سحب إلتزام مسبقاا  ");
                }
                else {

                    Sample4Content = new Addmo7asabi(card_kanoni, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }
            });

        }

    }
    }
