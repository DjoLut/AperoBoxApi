using System;

namespace AperoBoxApi.DTO
{
    public partial class CommandeDTO
    {
        public decimal Id { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal Utilisateur { get; set; }
        public decimal Promotion { get; set; }
        public decimal Adresse { get; set; }
    }
}