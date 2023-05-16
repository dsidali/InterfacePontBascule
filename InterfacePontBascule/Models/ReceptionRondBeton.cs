using System;

namespace InterfacePontBascule.Models
{
    public class ReceptionRondBeton
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


        public string Observation { get; set; }

        public string FournisseurOuClient { get; set; }


        public string Transporteur { get; set; }





        public int TypeDeTransportId { get; set; }
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        public int TypeDeCamionId { get; set; }
        public TypeDeCamion TypeDeCamion { get; set; }






        public string Diametre { get; set; }       
        public bool Termine { get; set; }
    }
}
