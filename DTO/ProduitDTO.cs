using System;

namespace AperoBoxApi.DTO
{
    public partial class ProduitDTO
    {
        public int Id { get; set; }
        public decimal PrixUnitaireHtva { get; set; }
        public decimal Tva { get; set; }
        public string Nom { get; set; }
        public DateTime? DatePeremption { get; set; }
        public byte Alcool { get; set; }
    }
}