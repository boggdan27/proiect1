using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect1.Models
{
    public class CConsultatie
    {
        public int ID { get; set; } 
        public string Data { get; set; }
        public float Cost { get; set; }
        public int ID_Medic { get; set; }
        public int ID_Pacient { get; set; }
        public string Nume_Medic { get; set; }
        public string Nume_pacient { get; set; }

        public string Boala { get; set; }
        public string Cauze { get; set; }
        public string Simptome { get; set; }
        public string Analize_recomandate { get; set; }
        public string Prescriptie_medicala { get; set; }
    }
}