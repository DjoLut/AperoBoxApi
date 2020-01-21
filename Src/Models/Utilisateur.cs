using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Commande = new HashSet<Commande>();
            Commentaire = new HashSet<Commentaire>();
            UtilisateurRole = new HashSet<UtilisateurRole>();
        }

        public decimal Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Mail { get; set; }
        public decimal? Telephone { get; set; }
        public decimal Gsm { get; set; }
        public string Username { get; set; }
        public string MotDePasse { get; set; }
        public decimal Adresse { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Adresse AdresseNavigation { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }
        public virtual ICollection<Commentaire> Commentaire { get; set; }
        public virtual ICollection<UtilisateurRole> UtilisateurRole { get; set; }
    }
}
