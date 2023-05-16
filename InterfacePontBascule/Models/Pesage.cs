using System;

namespace InterfacePontBascule.Models
{
    public class Pesage
    {


        public int Id { get; set; }
        public string NumBonA { get; set; } //numero bon d'access, genere automatiquement par cette application

        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        public string Mat { get; set; } //Matricule Camion

        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        public string Transporteur { get; set; }



        public string TypeTransport { get; set; }
        public string TypeCamion { get; set; }
    
    //    public TimeSpan HeureOP { get; set; }
        public string NumTicket { get; set; }
    public DateTime DateOP { get; set; }
        public int PCC { get; set; } //pesage a charge

        public int PCV { get; set; } //pesage a vide


        public int QP { get; set; }

        public bool Termine { get; set; }
        public string Observation { get; set; }

    }
}
