using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AperoBoxApi.ExceptionsPackage;

namespace AperoBoxApi.DTO
{
    public class UtilisateurDTO
    {
        public decimal Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nom { get; set; }
        [Required]
        [StringLength(100)]
        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public decimal? Telephone { get; set; }
        [Required]
        public decimal Gsm { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        public string MotDePasse { get; set; }
        [Compare("MotDePasse", ErrorMessage = "Erreur : le mot de passe est différent du mot de passe de confirmation")]
        public string MotDePasseConf { get; set; }
        public decimal Adresse { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<UtilisateurRoleDTO> UtilisateurRole { get; set; }
        public virtual ICollection<CommandeDTO> Commande { get; set; }
        public virtual ICollection<CommentaireDTO> Commentaire { get; set; }
    }
}