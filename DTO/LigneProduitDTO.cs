using System;

namespace AperoBoxApi.DTO
{
    public partial class LigneProduitDTO
    {
        public decimal id { get; set; }
        public decimal? quantite { get; set; }
        public decimal? box { get; set; }
        public decimal? produit { get; set; }
    }
}