﻿using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview
{
 public  class Itemsa7ab : BaseViewModel<card_sa7ab>
    {

        public DateTime date { get; set; }
        public string Cost { get; set; }
        public string visa { get; set; } = "لا  توجد";
        public string  nowcost { get; set; }
        public string  oldcost { get; set; }
        public int num { get; set; }
        public Command open { get; set; }
        public Action end { get; set; }
        public Action start { get; set; }

        public Visibility tashiravis { get; set; }
        public Visibility edittashiravis { get; set; }

        public Action<card_sa7ab> edittashiraaction { get; set; }
        public Action<card_sa7ab> addtashira { get; set; }
        public Command tashira { get; set; }
        public Command edittashira { get; set; }

        public Itemsa7ab(card_sa7ab card_sa7ab)
        {
            this.Cost = String.Format("{0:0.00}", card_sa7ab.cost);
            this.num = card_sa7ab.card.num;
            this.date = card_sa7ab.card.date; ;
            this.oldcost = String.Format("{0:0.00}", card_sa7ab.old_cost);
            this.nowcost = String.Format("{0:0.00}", card_sa7ab.old_cost- card_sa7ab.cost);


            tashiravis = Visibility.Visible;
            edittashiravis = Visibility.Collapsed;

            if (card_sa7ab.visa != null)
            {
                edittashiravis = Visibility.Visible;
                tashiravis= Visibility.Collapsed;
                visa = card_sa7ab.visa;
            }
            

            open = new Command(() => {
            //    Card_mo7asabiExecl card_ = new Card_mo7asabiExecl(card_sa7ab);
              //  card_.CreateCard();
            });
            tashira = new Command(() => {

                addtashira(card_sa7ab);
            });
            edittashira = new Command(() => {

                edittashiraaction(card_sa7ab);
            });
         
        }

    
}
}