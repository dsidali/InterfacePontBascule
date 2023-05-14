using System;

namespace InterfacePontBascule.Models
{
    public class Achat
    {
        public int Id { get; set; }
        public string NumBonA { get; set; } //numero bon d'access, genere automatiquement par cette application

        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        public string Mat { get; set; } //Matricule Camion

        public string Transporteur { get; set; }

        public string Source { get; set; }

        public string TypeTransport { get; set; }//Tosyali or safhadid
        public string TypeCamion { get; set; } //a benne ou a grappin
        public string TypeDechet { get; set; } // touvenant or massif


        public DateTime DateOP { get; set; }
       // public TimeSpan HeureOP { get; set; }
        public string NumTicket { get; set; }

        public int PCC { get; set; } //pesage a charge

        public int PCV { get; set; } //pesage a vide

        public int PB { get; set; } //poids brut

        public int PQRa { get; set; }

        public int PQS { get; set; }

 



        public string Observation { get; set; }

        public bool Termine { get; set; }

    }
}
