//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proiect1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medic_Pacient
    {
        public int ID { get; set; }
        public int ID_Medic { get; set; }
        public int ID_Pacient { get; set; }
    
        public virtual Medic Medic { get; set; }
        public virtual Pacient Pacient { get; set; }
    }
}
