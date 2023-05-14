using System;

namespace InterfacePontBascule.Models
{
    public class SortieTransfert
    {
        public int Id { get; set; }
        public string NumBL { get; set; }

        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        public DateTime DateOp { get; set; }
        public TimeSpan HeureOP { get; set; }
        public int PCC { get; set; }
        public int PCV { get; set; }
        public int PQS { get; set; }

        public int TypeProduitId { get; set; }
        public TypeProduit TypeProduit { get; set; }

        public bool Termine { get; set; }

        public string Mat { get; set; }
        public string Observation { get; set; }
        public string Destination { get; set; }

        public string Transporteur { get; set; }
        public string TypeTransport { get; set; }
        public string TypeCamion { get; set; }

        public string TypeDechet { get; set; }
        public string Diametre { get; set; }
    }
}
