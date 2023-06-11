using InterfacePontBascule.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.Models
{
    public class SortieRondBeton
    {
        public int Id { get; set; }
        [Display(Name = "Parc")]
        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        [Display(Name = "Bon")]
        [Required]
        public string NumBonA { get; set; }

        [Display(Name = "Ticket")]
        [Required]
        public string NumTicket { get; set; }




        //public int TransporteurId { get; set; }
        //public Transporteur Transporteur { get; set; }

        [Display(Name = "Date")]
        public DateTime DateOp { get; set; }
        // public TimeSpan HeureOP { get; set; }





        //   public string Source { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Transporteur { get; set; }


        [Display(Name = "Transport")]
        public int TypeDeTransportId { get; set; }

        [Display(Name = "Transport")]
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        [Display(Name = "Camion")]
        public int TypeDeCamionId { get; set; }
        [Display(Name = "Camion")]
        public TypeDeCamion TypeDeCamion { get; set; }



        [Display(Name = "Matricule")]
        [Required]
        public string Mat { get; set; } //Matricule Camion








        public int Diametre { get; set; } = 0;


        [Display(Name = "Charge")]
        [PoidsPositif]
        public int PCC { get; set; } //pesage a charge

        [Display(Name = "Tare")]
        [PoidsPositif]
        public int PCV { get; set; } //pesage a vide

        [Display(Name = "Brut")]
        public int PB { get; set; } = 0;//poids brut
        [Display(Name = "Rabais")]
        public int PQRa { get; set; } = 0;
        [Display(Name = "Net")]
        public int PQS { get; set; } = 0;


        public string Observation { get; set; }
        public bool Termine { get; set; } = false;



    }
}
