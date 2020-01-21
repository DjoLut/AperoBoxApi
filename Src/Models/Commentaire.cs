using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Commentaire
    {
        public decimal Id { get; set; }
        public string Texte { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal Utilisateur { get; set; }
        public decimal Box { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Box BoxNavigation { get; set; }
        public virtual Utilisateur UtilisateurNavigation { get; set; }
    }
}
