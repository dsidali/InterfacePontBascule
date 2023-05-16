using System;

namespace InterfacePontBascule.Models
{
    public class Achat
    {
        public int Id { get; set; }        


        public int ParcId { get; set; }
        public Parc Parc { get; set; }


        public string NumBonA { get; set; } //numero bon d'access, genere automatiquement par cette application

        public string NumTicket { get; set; }




        public string Mat { get; set; } //Matricule Camion

        public string Transporteur { get; set; }

        public string Source { get; set; }


        public int TypeDeTransportId { get; set; }
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        public int TypeDeCamionId { get; set; }
        public TypeDeCamion TypeDeCamion { get; set; } //a benne ou a grappin


        public int TypeDeDechetId { get; set; }
        public TypeDeDechet TypeDeDechet { get; set; } // touvenant or massif


        public DateTime DateOP { get; set; }
       // public TimeSpan HeureOP { get; set; }

       public int PCC { get; set; } = 0;//pesage a charge

       public int PCV { get; set; } = 0;//pesage a vide

        public int PB { get; set; } = 0;//poids brut

        public int PQRa { get; set; } = 0;

        public int PQS { get; set; } = 0;





        public string Observation { get; set; }

        public bool Termine { get; set; } = false;

    }
}
