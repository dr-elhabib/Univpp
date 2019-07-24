using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modeldb.model
{
    public class partsMo7asabiModel
    {
        public string NamePart { get; set; }
        public int Num { get; set; }
        public DateTime DateTime { get; set; }
        public double NewCost { get; set; }
        public double OldCost { get; set; }
        public partsMo7asabiModel(card_mo7sabi card_mo7sabi) {
            this.NamePart = card_mo7sabi.part.Name;
            this.Num = card_mo7sabi.card.num;
            this.DateTime = card_mo7sabi.card.date;
            this.OldCost = card_mo7sabi.part.Cost;
            MessageBox.Show((card_mo7sabi.part.Cost - card_mo7sabi.cost)+"");
            this.NewCost = card_mo7sabi.part.Cost - card_mo7sabi.cost ;
        }

    }

}
