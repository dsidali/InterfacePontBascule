using InterfacePontBascule.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.Models
{
    public class ReceptionTransfertRondBeton
    {

        public int Id { get; set; }

        [Display(Name = "Parc")]
        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        [Display(Name = "Bon")]
        [Required]
        public string NumBL { get; set; }
        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        [Display(Name = "Date")]
        public DateTime DateOp { get; set; }

        [Required]
        public string Transporteur { get; set; }

        [Display(Name = "Source")]
        [Required]
        public string Provenance { get; set; }


        //   public TimeSpan HeureOP { get; set; }



        [Display(Name = "Transport")]
        public int TypeDeTransportId { get; set; }
        [Display(Name = "Transport")]
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        [Display(Name = "Camion")]
        public int TypeDeCamionId { get; set; }
        [Display(Name = "Camion")]
        public TypeDeCamion TypeDeCamion { get; set; } //a benne ou a grappin

        [Display(Name = "Matricule")]
        [Required]
        public string Mat { get; set; }

        public int Diametre { get; set; }

        [Display(Name = "Charge")]
        [PoidsPositif]
        public int PCC { get; set; } //pesage a charge

        [Display(Name = "Tare")]
        [PoidsPositif]
        public int PCV { get; set; } //pesage a vide

        [Display(Name = "Net")]
        [PoidsPositif]
        public int PQS { get; set; }


        public string Observation { get; set; }
        public bool Termine { get; set; }

    }
}
