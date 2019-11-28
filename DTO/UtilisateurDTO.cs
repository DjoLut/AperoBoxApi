using System;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public partial class UtilisateurDTO
    {
        public decimal id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string prenom { get; set; }
        public DateTime dateNaissance { get; set; }
        [EmailAddress]
        public string mail { get; set; }
        public decimal? telephone { get; set; }
        public decimal gsm { get; set; }
        public string username { get; set; }
        public string authorities { get; set; }
        public string motDePasse { get; set; }
        public int? adresse { get; set; }
    }
}