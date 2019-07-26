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
    class Addtashira_mo7asabi_VewModel : BaseViewModel
    {
        public string visa { get; set; }
        public string num { get; set; }
        public string part { get; set; }
        public string cost { get; set; }


        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Addtashira_mo7asabi_VewModel(card_mo7sabi card_mo7sabi)
        {
            this.num = card_mo7sabi.num.ToString();
            this.part = card_mo7sabi.part.Name;
            this.cost = String.Format("{0:0.00}", card_mo7sabi.cost);
            savecommand = new Command( () =>
            {
                    acc();
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(d => d.Id == card_mo7sabi.Id).ToList().FirstOrDefault().visa = visa;
                    Ico.getValue<db>().savedb();
                    con();
               
            });

            Cancelcommand = new Command(() => {
                con();

            });
        }
    }

}
    

