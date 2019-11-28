using System;

namespace AperoBoxApi.DTO
{
    public partial class LigneCommandeDTO
    {
        public decimal id { get; set; }
        public decimal? quantite { get; set; }
        public decimal commande { get; set; }
        public decimal? box { get; set; }
        public decimal? produit { get; set; }
    }
}