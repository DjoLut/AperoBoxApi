using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Produit
    {
        public Produit()
        {
            LigneCommande = new HashSet<LigneCommande>();
            LigneProduit = new HashSet<LigneProduit>();
        }

        public decimal Id { get; set; }
        public decimal PrixUnitaireHtva { get; set; }
        public decimal Tva { get; set; }
        public string Nom { get; set; }
        public DateTime? DatePeremption { get; set; }
        public byte Alcool { get; set; }

        public virtual ICollection<LigneCommande> LigneCommande { get; set; }
        public virtual ICollection<LigneProduit> LigneProduit { get; set; }
    }
}
