using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Univ.lib
{
   public abstract MPage 
    
    {
     private Action Reload { get; set; }

    public Action GetReload()
    {
        return this.Reload;
    }

    public void SetReload(Action value)
    {
        this.Reload = value;
    }
}
}
