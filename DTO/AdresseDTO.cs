using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AperoBoxApi.DTO
{
    public class AdresseDTO
    {
        public decimal Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Rue { get; set; }
        [Required]
        public decimal Numero { get; set; }
        [Required]
        [StringLength(100)]
        public string Localite { get; set; }
        [Required]
        public decimal CodePostal { get; set; }
        [Required]
        [StringLength(100)]
        public string Pays { get; set; }
        public virtual ICollection<CommandeDTO> Commande { get; set; }
        public virtual ICollection<UtilisateurDTO> Utilisateur { get; set; }
    }
}