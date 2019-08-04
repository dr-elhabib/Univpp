﻿using Univ.lib;
using Univ.modeldb;
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
    class View7isabViewModel:BaseViewModel<card_7isab>
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





        public List<part> parts { get; set; }
       public process process { get; set; }

        public double oldcost { get; set; } =0;
        public double newcost { get; set; } =0;
        public card card { get; set; }
        public String  visa { get; set; }

        public Command back { get; set; }

        public Visibility visibility { get; set; }
        public Visibility tashiravis { get; set; }
        public Visibility edittashiravis { get; set; }

        public Action<card_mo7sabi> edittashiraaction { get; set; }
        public Action<card_mo7sabi> addtashira { get; set; }

        public Command tashira { get; set; }
        public Command open { get; set; }
        public Command print { get; set; }
        public Command edittashira { get; set; }

        public  View7isabViewModel(process process)
        {
            

            this.actionUP = () =>
            {
                this.val = Ico.getValue<db>().GetUnivdb().card_7isab.ToList().Where(c => c.card.id_prosess == process.Id).FirstOrDefault();

            };

            this.process = process;
            parts = process.parts.ToList();
            foreach (part part in parts)
            {
                newcost += part.Cost;
                MessageBox.Show(part.Name);
            }
            MessageBox.Show(""+newcost);

            var card_7 = Ico.getValue<db>().GetUnivdb().card_7isab.ToList().Where(c => c.card.id_prosess == process.Id).FirstOrDefault();
            if (card_7 == null || card_7.visa == null)
            {
                if (card_7 != null) {
                    Ico.getValue<IO>().DELETE_FILE(card_7.card.location);

                }
                OpenSample4Dialog();
                AcceptSample4Dialog();
                this.CreateCard(card_7);

            }
            else
            {


                this.inTilData();
            }



            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

        }
        public void inTilData( )
        {
            this.actionUP();
            this.card = val.card;
            this.visa = val.visa;


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
                OpenSample4Dialog();

                Sample4Content = new YesOrNo(" إذا أضفت التأشيرة لن تستطيع تعديل على بيانات العملية , الرجاء والتأكد قبل ذالك ", () => {
                    OpenSample4Dialog();

                    Sample4Content = new Addtashira_7isabi(val, AcceptSample4Dialog, CancelSample4Dialog);

                }, CancelSample4Dialog);

            });
            edittashira = new Command(() => {
                Sample4Content = new Edittashira_7isabi(val, AcceptSample4Dialog, CancelSample4Dialog);
                OpenSample4Dialog();

            });

            open = new Command(async () => {
                OpenSample4Dialog();
                AcceptSample4Dialog();
                await Task.Run(() => {
                    ExcelHlper.OpenFile(val.card.location);
                    CancelSample4Dialog();
                });
            });
            print = new Command(async() => {
                OpenSample4Dialog();
                AcceptSample4Dialog();
                await Task.Run(() => {
                    ExcelHlper.PrintFile(val.card.location);
                    CancelSample4Dialog();
                });

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
        public async Task CreateCard( card_7isab card_7)
        {
            await Task.Run(() => {
               
                Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == process.Id).First().NewCost = newcost;

                var d = DateTime.Now;
                var name = "بطاقة  أخذ بحساب رقم " + 1 + " سنة " + d.Year;

                var car = new card()
                {
                    date = DateTime.Now,
                    id_prosess = process.Id,
                    num = 1,
                    year = Ico.getValue<Date>().GetNowDate().Id,
                    location = process.location + "\\" + name,

                };
                card_7 = new card_7isab()
                {
                    card = car,
                    visa = null
                };
                Ico.getValue<db>().GetUnivdb().cards.Add(car);
                Ico.getValue<db>().GetUnivdb().card_7isab.Add(card_7);
                Ico.getValue<db>().savedb();
                card_7 = Ico.getValue<db>().GetUnivdb().card_7isab.ToList().Where(c => c.card.id_prosess == process.Id).FirstOrDefault();
                Card_7isabExecl c7 = new Card_7isabExecl(card_7);
                c7.CreateCard();
                card = card_7.card;
                this.inTilData();

                CancelSample4Dialog();
            });
        }

    }

}
