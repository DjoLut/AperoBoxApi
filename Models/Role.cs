using System;
using System.Collections.Generic;

namespace AperoBoxApi.Models
{
    public class Role
    {
        public string Nom {get; set;}
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }

    }

}