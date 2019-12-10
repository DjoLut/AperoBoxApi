using System;

namespace AperoBoxApi.Models
{
    public class UtilisateurRole
    {
        public string IdRole {get; set;}
        public int IdUtilisateur {get; set;}

        public Utilisateur utilisateur {get; set;}

        public Role Role {get; set;}
        
    }

}