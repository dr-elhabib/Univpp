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
    
    public partial class card_kanoni
    {
        public int id { get; set; }
        public int id_card { get; set; }
        public double cost { get; set; }
        public string visa { get; set; }
        public int id_client { get; set; }
        public int id_part { get; set; }
    
        public virtual card card { get; set; }
        public virtual client client { get; set; }
        public virtual part part { get; set; }
    }
}