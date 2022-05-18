using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect1.Models
{
    public class CMedic
    {
        public int ID { get; set; } 
        public string Nume { get; set; }    
        public string Prenume { get; set; } 
        public string Serie_Parafa { get; set; }
        public string CNP { get; set; }

        public string Specializare { get; set; }

    }
}