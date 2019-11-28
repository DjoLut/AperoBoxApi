using System;

namespace AperoBoxApi.DTO
{
    public partial class BoxDTO
    {
        public decimal id { get; set; }
        public string nom { get; set; }
        public decimal prixUnitaireHtva { get; set; }
        public decimal tva { get; set; }
        public decimal? promotion { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public byte affichable { get; set; }
        public DateTime dateCreation { get; set; }
    }
}