using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview
{



    class ViewpartViewModel : BaseViewModel
    {
        public process process { get; set; }

        public TypePart type { get;set;}
        public double cost { get; set; }
        public double nowcost { get; set; } = 0;
        public string Code { get; set; }
        public string Name { get; set; }
        public string name { get; set; }
        public string alhcost { get; set; }
        public Command back { get; set; }
        public ViewpartViewModel(part part)
        {
            this.cost = part.Cost;
            this.name = part.Name;
            this.process = part.process;

            this.Code = process.Code;
            this.Name = process.Name;
            this.alhcost = part.alpart;

            foreach (part p in process.parts.ToList()) {
                nowcost += p.Cost;
            }
            type = new TypePart(part.num_type);
          
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });
        }
    }
}
