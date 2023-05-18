﻿namespace InterfacePontBascule.Models
{
    public class SortieTransfertRondBeton
    {
        public int Id { get; set; }


        public int ParcId { get; set; }
        public Parc Parc { get; set; }


        public string NumBL { get; set; }
        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        public DateTime DateOp { get; set; }

        public string Transporteur { get; set; }
        public string Destination { get; set; }


        //   public TimeSpan HeureOP { get; set; }




        public int TypeDeTransportId { get; set; }
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        public int TypeDeCamionId { get; set; }
        public TypeDeCamion TypeDeCamion { get; set; } //a benne ou a grappin
        public string Mat { get; set; }

        public int Diametre { get; set; }

        public int PCC { get; set; }
        public int PCV { get; set; }
        public int PQS { get; set; }


        public string Observation { get; set; }
        public bool Termine { get; set; }
    }
}
