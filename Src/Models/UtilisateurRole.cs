using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class UtilisateurRole
    {
        public string IdRole { get; set; }
        public decimal IdUtilisateur { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual Utilisateur IdUtilisateurNavigation { get; set; }
    }
}
