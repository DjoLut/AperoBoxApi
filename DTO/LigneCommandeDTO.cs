using System;

namespace AperoBoxApi.DTO
{
    public partial class LigneCommandeDTO
    {
        public decimal Id { get; set; }
        public decimal? Quantite { get; set; }
        public decimal Commande { get; set; }
        public decimal? Box { get; set; }
        public decimal? Produit { get; set; }
    }
}