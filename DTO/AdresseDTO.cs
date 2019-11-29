using System;

namespace AperoBoxApi.DTO
{
    public partial class AdresseDTO
    {
        public decimal Id { get; set; }
        public string Rue { get; set; }
        public string Numero { get; set; }
        public string Localite { get; set; }
        public decimal CodePostal { get; set; }
        public string Pays { get; set; }
    }
}