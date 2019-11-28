using System;

namespace AperoBoxApi.DTO
{
    public partial class CommandeDTO
    {
        public decimal id { get; set; }
        public DateTime dateCreation { get; set; }
        public decimal utilisateur { get; set; }
        public decimal promotion { get; set; }
        public decimal adresse { get; set; }
    }
}