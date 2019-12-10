using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public partial class Role
    {
        public Role()
        {
            UtilisateurRole = new HashSet<UtilisateurRole>();
        }

        public string Nom { get; set; }

        public virtual ICollection<UtilisateurRole> UtilisateurRole { get; set; }
    }
}
