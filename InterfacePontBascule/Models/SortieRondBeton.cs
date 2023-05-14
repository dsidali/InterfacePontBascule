using System;

namespace InterfacePontBascule.Models
{
    public class SortieRondBeton
    {
        public int Id { get; set; }
        public string NumBonA { get; set; }


        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        public string Mat { get; set; } //Matricule Camion

        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }


        public DateTime DateOp { get; set; }
        public TimeSpan HeureOP { get; set; }
        public string NumTicket { get; set; }

        public int PCC { get; set; } //pesage a charge

        public int PCV { get; set; } //pesage a vide

        public int PB { get; set; } //poids brut

        public int PQRa { get; set; }

        public int PQS { get; set; }

        public bool Termine { get; set; }
        public string Observation { get; set; }

        public string FournisseurOuClient { get; set; }
        public string Destination { get; set; }

        public string Transporteur { get; set; }
        public string TypeTransport { get; set; }
        public string TypeCamion { get; set; }

        public string Diametre { get; set; }

    }
}
