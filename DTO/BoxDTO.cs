using System;

namespace AperoBoxApi.DTO
{
    public partial class BoxDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal PrixUnitaireHtva { get; set; }
        public decimal Tva { get; set; }
        public decimal? Promotion { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte Affichable { get; set; }
        public DateTime DateCreation { get; set; }
    }
}