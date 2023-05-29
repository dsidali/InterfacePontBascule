using System;
using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.Models
{
    public class Achat
    {
        public int Id { get; set; }

        [Display(Name = "Parc")]
        public int ParcId { get; set; }
        public Parc Parc { get; set; }

        [Display(Name = "Bon")]
        public string NumBonA { get; set; } //numero bon d'access, genere automatiquement par cette application
        [Display(Name = "Ticket")]
        public string NumTicket { get; set; }



        [Display(Name = "Matricule")]
        public string Mat { get; set; } //Matricule Camion

        public string Transporteur { get; set; }

        public string Source { get; set; }

        [Display(Name = "Transport")]
        public int TypeDeTransportId { get; set; }
        [Display(Name = "Transport")]
        public TypeDeTransport TypeDeTransport { get; set; }//Tosyali or safhadid

        [Display(Name = "Camion")]
        public int TypeDeCamionId { get; set; }
        [Display(Name = "Camion")]

        public TypeDeCamion TypeDeCamion { get; set; } //a benne ou a grappin

        [Display(Name = "Dechet")]
        public int TypeDeDechetId { get; set; }
        [Display(Name = "Dechet")]

        public TypeDeDechet TypeDeDechet { get; set; } // touvenant or massif

        [Display(Name = "Date")]

        public DateTime DateOP { get; set; }
        // public TimeSpan HeureOP { get; set; }

        [Display(Name = "Charge")]
        public int PCC { get; set; } = 0;//pesage a charge

        [Display(Name = "Tare")]
        public int PCV { get; set; } = 0;//pesage a vide

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
