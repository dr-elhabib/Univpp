using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview
{
    class processesViewMODEL:BaseViewModel<process>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Cost { get; set; } = 0;
        public string nowCost { get; set; } = 0;
        public Command view { get; set; }
        public Command edit { get; set; }
        public processesViewMODEL(process process)
        {
            Name = process.Name;
            Code = process.Code;
            double d = 0d;
            foreach (part p in process.parts.ToList()) {
                d += p.Cost;
              }
            Cost = String.Format("{0:0.00}", d);
            nowCost = String.Format("{0:0.00}", process.NewCost);
            view = new Command(()=> {

                Ico.getValue<ContentApp>().page = new ViewProcesses(process);

            });
            edit = new Command(()=> {
                Ico.getValue<ContentApp>().page = new EditProcesses(process);
               // MessageBox.Show(Ico.getValue<ContentApp>().page.ToString());
            });

        }

    }
}
