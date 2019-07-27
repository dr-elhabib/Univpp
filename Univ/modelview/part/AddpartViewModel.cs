using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univ.modelview
{



    class AddpartViewModel:BaseViewModel<process>
    {
        public process process { get; set; }
        public List<TypePart> types { get;set;}
        public TypePart numtype { get; set; }
        public int cost { get; set; }
        public string name { get; set; }
        public string alhcost { get; set; }

        public Command back { get; set; }
        public Command save { get; set; }
        public int num { get; set; }
        public AddpartViewModel(process process )
        {
            this.process = process;
           this.num = process.parts.Count+1;
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
            save = new Command(()=> {

                var part = new part() {
                    Name=name,
                    Cost=cost,
                    Id_Pro=process.Id,
                    num_type= numtype.numType,
                    num=num,
                    alpart= alhcost
                };

                Ico.getValue<db>().GetUnivdb().parts.Add(part);
                Ico.getValue<db>().GetUnivdb().SaveChanges();
                Ico.getValue<ContentApp>().back();

            });
            back = new Command(()=> {

                Ico.getValue<ContentApp>().back();

            });
        }
    }
}
