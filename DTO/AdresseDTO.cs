using System;

namespace AperoBoxApi.DTO
{
    public partial class AdresseDTO
    {
        public decimal id { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
        public string localite { get; set; }
        public decimal codePostal { get; set; }
        public string pays { get; set; }
    }
}