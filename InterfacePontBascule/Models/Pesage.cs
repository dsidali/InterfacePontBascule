using InterfacePontBascule.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.Models
{
    public class Pesage
    {


        public int Id { get; set; }

        [Display(Name = "Parc")]
        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        [Display(Name = "Bon")]
        [Required]
        public string NumBonA { get; set; } //numero bon d'access, genere automatiquement par cette application

        [Display(Name = "Ticket")]
        [Required]
        public string NumTicket { get; set; }




        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        [Required]
        public string Transporteur { get; set; }


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
        public string Mat { get; set; } //Matricule Camion




        //    public TimeSpan HeureOP { get; set; }
        [Display(Name = "Date")]
        public DateTime DateOP { get; set; }

        [Display(Name = "Charge")]
        [PoidsPositif]
        public int PCC { get; set; } //pesage a charge

        [Display(Name = "Tare")]
        [PoidsPositif]
        public int PCV { get; set; } //pesage a vide

        [Display(Name = "Net")]
        [PoidsPositif]
        public int QP { get; set; }


        public string Observation { get; set; }

        public bool Termine { get; set; }

    }
}
