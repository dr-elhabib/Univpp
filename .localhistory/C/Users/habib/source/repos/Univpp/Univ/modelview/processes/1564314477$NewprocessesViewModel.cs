using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class NewprocessesViewModel: BaseViewModel<process>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Num { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
    
        public Command back { get; set; }
        public Command save { get; set; }
        public NewprocessesViewModel() {
            save = new Command(() => {
                Ico.getValue<ContentApp>.back();
            });

                save = new Command(()=> {

             double totalCoast = 0;
              var p = new process() {
                      Name = Name,
                      date=date,
                      Code = Code,
                      num=Num,
                      NewCost=totalCoast,
                     location= Ico.getValue<IO>().CREATE_F_PRO(Code)
               }; 
                Ico.getValue<db>().GetUnivdb().processes.Add(p);
                Ico.getValue<db>().GetUnivdb().SaveChanges();
                Ico.getValue<ContentApp>().page = new PCrad();
                Ico.getValue<ContentApp>().clear();
            });

        }

        

    }
}
