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
        public string Cost { get; set; } 
        public string nowCost { get; set; }
        public string date { get; set; }
        public Command view { get; set; }
        public Visibility visibility { get; set;}
        public Command edit { get; set; }
        public processesViewMODEL(process process)
        {
            Name = process.Name;
            Code = process.Code;
            date = process.date.ToShortDateString();
            double d = 0d;
            foreach (part p in process.parts.ToList()) {
                d += p.Cost;
              }
            var lc = process.cards;
            visibility = Visibility.Visible;
            if (lc.ToList().Count > 0)
            {
                var a = lc.ToList().Where(c => c.card_7isab.Count > 0).FirstOrDefault();
                if (a != null)
                {
                    if (a.card_7isab?.ToList().FirstOrDefault() != null)
                    {
                        visibility = Visibility.Hidden;
                    }
                }
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
            removte = new Command(()=> {
                Ico.getValue<ContentApp>().page = new EditProcesses(process);
               // MessageBox.Show(Ico.getValue<ContentApp>().page.ToString());
            });

        }

    }
}
