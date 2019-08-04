﻿using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page.lib;
using System.Text.RegularExpressions;

namespace Univ.modelview
{
    class NewprocessesViewModel: BaseViewModel<process>
    {
        public string Name
        {
            get; set;
        } = "";
        public string Code
        {
            get; set;
        } = "";
        public string Num
        {
            get;set;
        } = "";
        public DateTime date { get; set; } = DateTime.Now;
        private List<string> erour= new List<string>();
        public Command back { get; set; }
        public Command save { get; set; }
        public NewprocessesViewModel() {
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

                save = new Command(async()=> {
                    erour=  new List<string>();
                    if (Name.ToString().Length == 0)
                    {
                        erour.Add("الرجاء كتابة إسم العملية ");

                    }
                    if (Code.ToString().Length != 15)
                    {
                        erour.Add(" كود العملية  يجب أن يحتوي على 15 حرف");

                    }
                    if (Num.ToString().Length == 0)
                    {
                        erour.Add("الرجاء كتابة رقم الثابت للعملية ");

                    }
                    else
                    {
                        string pattern = "[1-9]+/[1-9]+";
                        Regex rgx = new Regex(pattern);
                        if (!rgx.IsMatch(Num.ToString()))
                        {
                            erour.Add("الرجاء كتابة رقم الثابت بشكل الصحيح ##/##   ");

                        }


                    }




                    if (erour.Count == 0)
                    {
                        AcceptSample4Dialog();

                        await Task.Run(() =>
                        {


                            double totalCoast = 0;
                            var p = new process()
                            {
                                Name = Name,
                                date = date,
                                Code = Code,
                                num = Num,
                                NewCost = totalCoast,
                                location = Ico.getValue<IO>().CREATE_F_PRO(Code)
                            };
                            Ico.getValue<db>().GetUnivdb().processes.Add(p);
                            Ico.getValue<db>().GetUnivdb().SaveChanges();
                            CancelSample4Dialog();
                        });

                        Ico.getValue<ContentApp>().back();
                        Ico.getValue<ContentApp>().clear();

                    }
                    else
                    {


                        OpenSample4Dialog();
                        Sample4Content = new Messagebox(erour, () => {
                            CancelSample4Dialog();
                     
                        });

                    }
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
