//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Univ.modeldb
{
    using System;
    using System.Collections.Generic;
    
    public partial class p_sa7ab
    {
        public int id { get; set; }
        public int id_sa7ab { get; set; }
        public int id_part { get; set; }
        public double cost { get; set; }
    
        public virtual card_sa7ab card_sa7ab { get; set; }
        public virtual part part { get; set; }
    }
}