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
     private Action reload { get; set; }

    public Action GetReload()
    {
        return this.reload;
    }

    public void SetReload(Action value)
    {
        this.reload = value;
    }
}
}
