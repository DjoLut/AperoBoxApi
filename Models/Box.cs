using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Box
    {
        public Box()
        {
            Commentaire = new HashSet<Commentaire>();
            LigneCommande = new HashSet<LigneCommande>();
            LigneProduit = new HashSet<LigneProduit>();
        }

        public decimal Id { get; set; }
        public string Nom { get; set; }
        public decimal PrixUnitaireHtva { get; set; }
        public decimal Tva { get; set; }
        public decimal? Promotion { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte Affichable { get; set; }
        public DateTime DateCreation { get; set; }

        public virtual ICollection<Commentaire> Commentaire { get; set; }
        public virtual ICollection<LigneCommande> LigneCommande { get; set; }
        public virtual ICollection<LigneProduit> LigneProduit { get; set; }
    }
}
