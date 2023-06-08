using System;
using System.ComponentModel.DataAnnotations;
using InterfacePontBascule.CustomValidation;

namespace InterfacePontBascule.Models
{
    public class ReceptionRondBeton
    {
        public int Id { get; set; }


        [Display(Name = "Parc")]
        public int ParcId { get; set; }
        public Parc Parc { get; set; }


        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }



        //   public TimeSpan HeureOP { get; set; }
        [Display(Name = "Bon")]
        [Required]
        public string NumBonA { get; set; }

        [Display(Name = "Ticket")]
        [Required]
        public string NumTicket { get; set; }

      


       
        public DateTime DateOp { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string Transporteur { get; set; }


        [Display(Name = "Matricule")]
        [Required]
        public string Mat { get; set; } //Matricule Camion

        [Display(Name = "Transport")]
        public int TypeDeTransportId { get; set; }
        [Display(Name = "Transport")]
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        [Display(Name = "Camion")]
        public int TypeDeCamionId { get; set; }
        [Display(Name = "Camion")]
        public TypeDeCamion TypeDeCamion { get; set; }






        public int Diametre { get; set; }

        [PoidsPositif]
        public int PCC { get; set; } //pesage a charge

        public int PCV { get; set; } //pesage a vide

        public int PB { get; set; } //poids brut

        public int PQRa { get; set; }

        public int PQS { get; set; }

        public string Observation { get; set; }
        public bool Termine { get; set; }
    }
}
