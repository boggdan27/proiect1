using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect1.Models
{
    public class ConsultatieCreare
    {
        public string ID_Medic { get; set; }
        public string ID { get; set; }
        public DateTime Data { get; set; }
        public float Cost { get; set; }
        public string Boala { get; set; }
        public string Cauze { get; set; }
        public string Simptome { get; set; }
        public string Analize_Recomandate { get; set; }
        public string Prescriptie_Medicala { get; set; }
        public string CNP_Pacient { get; set; }

    }
}