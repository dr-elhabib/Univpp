using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.Model;

namespace Univ
{
   public   class processesViewModel
    {
        public List<process> items { get; set; }
        public processesViewModel() {
            var db = new db_univ();
            this.items =db.processes.ToList();

        }

    }
}
