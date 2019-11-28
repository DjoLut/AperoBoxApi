using System;

namespace AperoBoxApi.DTO
{
    public partial class ProduitDTO
    {
        public decimal id { get; set; }
        public decimal prixUnitaireHtva { get; set; }
        public decimal tva { get; set; }
        public string nom { get; set; }
        public DateTime? datePeremption { get; set; }
        public byte alcool { get; set; }
    }
}