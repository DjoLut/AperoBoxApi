using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Adresse
    {
        public Adresse()
        {
            Commande = new HashSet<Commande>();
            Utilisateur = new HashSet<Utilisateur>();
        }

        public decimal Id { get; set; }
        public string Rue { get; set; }
        public string Numero { get; set; }
        public string Localite { get; set; }
        public decimal CodePostal { get; set; }
        public string Pays { get; set; }

        public virtual ICollection<Commande> Commande { get; set; }
        public virtual ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
