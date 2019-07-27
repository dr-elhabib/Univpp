using Univ.lib;
using Univ.lib.Enum;
using Univ.modeldb.model;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.page;

namespace Univ.modelview
{
    class Addtashira_sa7ab_VewModel : BaseViewModel
    {
        public string visa { get; set; }
        public string num { get; set; }
        public string part { get; set; }
        public string cost { get; set; }


        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Addtashira_sa7ab_VewModel(card_sa7ab card_sa7ab)
        {
            this.num = card_sa7ab.card.num.ToString();
            this.part = card_sa7ab.card.process.Name;
            this.cost = String.Format("{0:0.00}", card_sa7ab.cost);
            savecommand = new Command( () =>
            {
                    acc();
                    Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(d => d.id == card_sa7ab. id).ToList().FirstOrDefault().visa = visa;
                    Ico.getValue<db>().savedb();
                    con();
               
            });

            Cancelcommand = new Command(() => {
                con();

            });
        }
    }

}
    

