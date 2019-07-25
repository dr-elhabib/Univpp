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
    class EdittswiyaVewModel : BaseViewModel
    {
        public string tswiya { get; set; }
        public string num { get; set; }
        public string part { get; set; }
        public string cost { get; set; }
       


        public Command savecommand { get; set; }
        public Command Cancelcommand { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public EdittswiyaVewModel(card_dafa3 card_dafa3)
        {
            this.num = card_dafa3.num.ToString();
            this.part = card_dafa3.part.Name;
            this.tswiya = card_dafa3.tswiya;
            this.cost = String.Format("{0:0.00}", card_dafa3.Cost);
            savecommand = new Command( () =>
            {
                acc();
                Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(d => d.Id == card_dafa3.Id).ToList().FirstOrDefault().tswiya=tswiya;
                Ico.getValue<db>().savedb();
                Card_dafa3Execl card_Dafa3Execl = new Card_dafa3Execl(Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(d => d.Id == card_dafa3.Id).ToList().FirstOrDefault());
                card_Dafa3Execl.CreateCard();
             con();
            });

            Cancelcommand = new Command(() => {
                con();

            });
        }
    }

}
    

