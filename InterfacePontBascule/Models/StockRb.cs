using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacePontBascule.CustomValidation;

namespace InterfacePontBascule.Models
{
    public class StockRb
    {
        public int Id { get; set; }


        [Display(Name = "Charge")]
        [PoidsPositif] public int Qte { get; set; } = 0;

    }
}
