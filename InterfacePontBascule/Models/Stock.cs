using System;

namespace InterfacePontBascule.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public int TypeProduitId { get; set; }
        public TypeProduit TypeProduit { get; set; }

        public DateTime DateJour { get; set; }


    }
}
