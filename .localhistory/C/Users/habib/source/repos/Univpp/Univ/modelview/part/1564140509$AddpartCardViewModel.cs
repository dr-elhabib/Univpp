﻿using Univ.lib;
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

namespace Univ.modelview
{



    class AddpartCardViewModel : BaseViewModel
    {
        public List<client>clients { get;set;}
        public client client { get; set; }
        public double cost { get; set; }
        public string nameprocess { get; set; }
        public string namepart { get; set; }
        public part part { set; get; }
        public Command savecommand { get; set; }
        public Command Cancelcommand { get; set; }
        public Command addclient { get; set; }
        
        public AddpartCardViewModel(part part , Action accept, Action Cancel)
        {

            this.namepart = part.Name;
            this.cost = part.Cost;
            this.nameprocess = part.process.Name;
            this.part = part;
            clients = Ico.getValue<db>().GetUnivdb().clients.ToList();

            savecommand = new Command(() => {

                accept();
                var cardn = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == part.Id_Pro && c.year == Ico.getValue<Date>().GetNowDate().Id).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                var num = 1;
                if (cardn != null)
                {
                    num = cardn.num + 1;
                }


                var d = DateTime.Now;
                var name = "بطاقة إلتزام قانوني رقم " + num + " سنة " + d.Year;

                var card = new card() {
                    id_prosess = part.process.Id,
                    year = Ico.getValue<db>().GetUnivdb().years.ToList().LastOrDefault().Id,
                    num = num,
                     location = Ico.getValue<IO>().CREATE_F_kanoni(part.process.location) + "\\" + name,
                    date = DateTime.Now,
                };
                var kanoni = new card_kanoni() {
                    card = card,
                    id_client = client.Id,
                    id_part = part.Id,
                    cost = part.Cost ,
                    visa=null
                };

                Ico.getValue<db>().GetUnivdb().cards.Add(card);
                Ico.getValue<db>().GetUnivdb().card_kanoni.Add(kanoni);
                Ico.getValue<db>().savedb();
                Cancel();

            });
        Cancelcommand = new Command(() => {

            Cancel();
            });
            addclient = new Command(() => {
                Ico.getValue<ContentApp>().page = new AddClient();


            });



            

        }
    }

}
