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
    public class MessageboxModelView : BaseViewModel<string>
    {
        public string [] Mes { set; get; }
        public Command ok { get; set; }
        public MessageboxModelView(string[] s,Action ok)
        {
            this.Mes = s;
           
            this.ok = new Command(()=> {
                ok();
            });

        }
    }
    

}
