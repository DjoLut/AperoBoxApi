using System;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public partial class UtilisateurDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        public decimal? Telephone { get; set; }
        public decimal Gsm { get; set; }
        public string Username { get; set; }
        public string Authorities { get; set; }
        public string MotDePasse { get; set; }
        public decimal Adresse { get; set; }
    }
}