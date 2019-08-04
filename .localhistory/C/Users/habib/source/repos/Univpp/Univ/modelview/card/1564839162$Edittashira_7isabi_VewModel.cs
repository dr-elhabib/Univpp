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
    class Edittashira_7isabi_VewModel : BaseViewModel<card_7isab>
    {
        public string visa { get; set; }
        public string num { get; set; }
        public string part { get; set; }


        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public Edittashira_7isabi_VewModel(card_7isab card_7isab)
        {
            this.num = card_7isab.card.num.ToString();
            this.visa = card_7isab.visa;
            this.part = card_7isab.card.process.Name;
            savecommand = new Command( () =>
            {
                Ico.getValue<ContentApp>().CancelSample4Dialog();
                Ico.getValue<db>().GetUnivdb().card_7isab.ToList().Where(d => d.Id == card_7isab.Id).ToList().FirstOrDefault().visa= visa;
                Ico.getValue<db>().savedb();
                acc();
                Ico.getValue<ContentApp>().CancelSample4Dialog();

            });

            Cancelcommand = new Command(() => {
                Ico.getValue<ContentApp>().CancelSample4Dialog();
            });
        }
    }

}
    

