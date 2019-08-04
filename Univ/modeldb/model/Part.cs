using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univ.modeldb.model
{
   public class Part
    {
        public String Name { get; set; }
        public double Cost { get; set; }
        public string type { get; set; }
        public part part { get; set; }
        public Part(part part)
        {
            Name = part.Name;
            Cost = part.Cost;
            type = new TypePart(part.num_type).name;
            this.part = part;
        }
    }
}
