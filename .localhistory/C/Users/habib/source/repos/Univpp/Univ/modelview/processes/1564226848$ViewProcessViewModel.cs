﻿using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using Univ.page.lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class ViewProcessViewModel : BaseViewModel<process>
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

        public Visibility visibility { get; set; } = Visibility.Visible;
        public string name { get; set; }
        public string code { get; set; }
        public string num { get; set; }
        public string date { get; set; }
        public double nowcost { get; set; }
        public ObservableCollection<ItemPart> parts { get; set; }
        public Command back { get; set; }
        public Command addpart { get; set; }
        public Command card_7isab { get; set; }
        public Command card_mo7asbi { get; set; }
        public Command viewcardmo7sabi { get; set; }
        public Command card_s7ab { get; set; }

        public ViewProcessViewModel(process process)
        {

            this.actionUP = () =>
            {
                this.val = Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == process.Id).ToList().FirstOrDefault();
            };
            this.inTilData();
            back = new Command(() =>
            {
                Ico.getValue<ContentApp>().back();
                this.inTilData();

            });
            addpart = new Command(() =>
            {
                Ico.getValue<ContentApp>().page = new AddPart(val);
            });
            card_s7ab = new Command(() =>
            {
                Ico.getValue<ContentApp>().page = new Viewsa7ab(val);
            });
            card_7isab = new Command(() =>
            {
                //   Univ.lib.Card_7isab card = new Univ.lib.Card_7isab(new processes(process));
                Ico.getValue<ContentApp>().page = new View7isab(process);

            });
            viewcardmo7sabi = new Command(() =>
            {
                //  Ico.getValue<ContentApp>().page = new Addmo7asbi( process);
                OpenSample4Dialog();
            });
            card_mo7asbi = new Command(() =>
            {
                //  Task.Run(()=> { Card_mo7sabi card = new Card_mo7sabi(new processes(process)); });
            });



        }



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


        public void inTilData()
        {
            this.actionUP();

            this.name = val.Name;
            this.code = val.Code;
            this.num = val.num;
            var lc = val.cards;
            if (lc.ToList().Count > 0)
            {
                var a=lc.ToList().Where(c=>c.card_7isab.Count>0).FirstOrDefault();
                if (a != null)
                {
                    if (a.card_7isab?.ToList().FirstOrDefault().visa != null)
                    {
                        visibility = Visibility.Hidden;
                    }
                }
            }
            this.date = val.date.GetDateTimeFormats()[0];
            this.nowcost = val.NewCost;
            parts = new ObservableCollection<ItemPart>(val.parts.ToList().Select(p => new ItemPart(p)
            {
                action = (t) =>
                {
                    parts.Remove(t);
                },

                action_kanoni = () =>
                {


                    var card = Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(c => c.id_part == p.Id).FirstOrDefault();

                    if (card == null)
                    {
                        Sample4Content = new AddPartCard(p, AcceptSample4Dialog, CancelSample4Dialog);
                        OpenSample4Dialog();
                    }
                    else
                    {
                        Ico.getValue<ContentApp>().page = new Viewkanoni(card);

                    }
                }
            }));

        }

    }
    }
