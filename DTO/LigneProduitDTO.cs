using System;

namespace AperoBoxApi.DTO
{
    public partial class LigneProduitDTO
    {
        public decimal Id { get; set; }
        public decimal? Quantite { get; set; }
        public decimal? Box { get; set; }
        public decimal? Produit { get; set; }
    }
}