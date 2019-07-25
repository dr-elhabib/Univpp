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
    class Addtashira_kanoni_VewModel : BaseViewModel
    {
        public string visa { get; set; }
        public string num { get; set; }
        public string part { get; set; }
        public string cost { get; set; }
       


        public Command savecommand { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Addtashira_kanoni_VewModel(card_kanoni card_kanoni)
        {
            this.num = card_kanoni.card.num.ToString();
            this.part = card_kanoni.part.Name;
            this.cost = String.Format("{0:0.00}", card_kanoni.cost);
            savecommand = new Command( () =>
            {
                acc();
                Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(d => d.id == card_kanoni.id).ToList().FirstOrDefault().visa= visa;
                Ico.getValue<db>().savedb();
                con();
            });

            Cancelcommand = new Command(() => {
                con();

            });
        }
    }

}
    

