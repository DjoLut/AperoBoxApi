using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AperoBoxApi.Models;

namespace AperoBoxApi.DTO
{
    public class AdresseDTO
    {
        public decimal Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Rue { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        [StringLength(100)]
        public string Localite { get; set; }
        [Required]
        public decimal CodePostal { get; set; }
        [Required]
        [StringLength(100)]
        public string Pays { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }
        public virtual ICollection<Utilisateur> Utilisateur { get; set; }
    }
}