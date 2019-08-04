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



    class EditpartViewModel : BaseViewModel<part>
    {
        public process process { get; set; }
        public List<TypePart> types { get;set;}

        public TypePart numtype { get;set;}
        public double cost { get; set; }
        public string name { get; set; }
        public string alhcost { get; set; }
        public Command save { get; set; }
        public Command back { get; set; }
        public EditpartViewModel(part part)
        {
            this.cost = part.Cost;
            this.name = part.Name;
            this.process = part.process;
            this.alhcost = part.alpart;
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
            this.numtype = types.ElementAt(part.num_type - 1);

            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });
            save = new Command(()=> {
               
              var p= Ico.getValue<db>().GetUnivdb().parts.ToList().Where(par => par.Id == part.Id).FirstOrDefault();
                p.num_type = numtype.numType;
                p.Name = name;
                p.Cost = cost;
                p.alpart = alhcost;
                Ico.getValue<db>().savedb();
                Ico.getValue<ContentApp>().back();
            });
        }
    }
}
