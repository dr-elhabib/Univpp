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
    public class Messagebox : BaseViewModel<string>
    {
        public string Mes { set; get; }
        public Command yes { get; set; }
        public Command no { get; set; }
        public Messagebox(string s,Action yes,Action no)
        {
            this.Mes = s;
            this.yes = new Command(()=> {
                yes();
            });
            this.no = new Command(()=> {
                no();
            });

        }
    }
    

}
