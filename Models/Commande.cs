using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Commande
    {
        public Commande()
        {
            LigneCommande = new HashSet<LigneCommande>();
        }

        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal Promotion { get; set; }
        public decimal Utilisateur { get; set; }
        public decimal Adresse { get; set; }

        public virtual Adresse AdresseNavigation { get; set; }
        public virtual Utilisateur UtilisateurNavigation { get; set; }
        public virtual ICollection<LigneCommande> LigneCommande { get; set; }
    }
}
