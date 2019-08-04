﻿using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Univ.page.lib;
using System.Text.RegularExpressions;

namespace Univ.modelview
{



    class AddpartViewModel:BaseViewModel<process>
    {
        public List<TypePart> types { get;set;}
        public TypePart numtype { get; set; }
        public double cost { get; set; } =0.0;
        public string name { get; set; } = "";
        public string stpro { get; set; } = "";
        public string strcode{ get; set; } = "";
        public string costpro { get; set; } = "";
        public string alhcost { get; set; } = "";

        private List<string> erour = new List<string>();

        public Command back { get; set; }
        public Command save { get; set; }
        public int num { get; set; }
        public AddpartViewModel(process process )
        {
            strcode = process.Code;
            stpro = process.Name;
            var d = 0d;
            foreach (var p in process.parts)
            {
                d += p.Cost;

            }
            costpro = String.Format("{0:0.00}", d);


            this.num = process.parts.Count+1;


            types = new List<TypePart>();
            types.Add(new TypePart(1));
            types.Add(new TypePart(2));
            types.Add(new TypePart(3)); 
            types.Add(new TypePart(4)); 
            types.Add(new TypePart(5)); 
            types.Add(new TypePart(6)); 
            types.Add(new TypePart(7)); 
            types.Add(new TypePart(8));
            types.Add(new TypePart(9)); 
            types.Add(new TypePart(10));
            save = new Command(async()=> {
                erour = new List<string>();

                if (name.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة إسم الحصة ");

            }
                string pattern = "[1-9]+";
                Regex rgx = new Regex(pattern);
                if (!rgx.IsMatch(cost.ToString()))
                {
                    erour.Add("الرجاء كتابة المبلغ ");

                }

              

                if (alhcost.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة  المبلغ حرفيا   ");

            }
                if (numtype == null)
                {
                    erour.Add("الرجاء  إختيار نوع الحصة    ");

                }
                OpenSample4Dialog();

                if (erour.Count == 0)
                {
                    AcceptSample4Dialog();

                    await Task.Run(() =>
                    {
                        
                        var part = new part()
                        {
                            Name = name,
                            Cost = cost,
                            Id_Pro = process.Id,
                            num_type = numtype.numType,
                            num = num,
                            alpart = alhcost
                        };

                        Ico.getValue<db>().GetUnivdb().parts.Add(part);
                        Ico.getValue<db>().savedb();
                        OpenSample4Dialog();

                    });
                    Ico.getValue<ContentApp>().back();
                }
                else
                {
                    Sample4Content = new Messagebox(erour, () => {
                        OpenSample4Dialog();

                    });

                }
            });
            back = new Command(()=> {

                Ico.getValue<ContentApp>().back();

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
