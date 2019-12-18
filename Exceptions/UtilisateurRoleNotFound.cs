using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class UtilisateurRoleNotFoundException : PersonnalException
    {
        public UtilisateurRoleNotFoundException()
            : base("Role utilisateur non trouvé") { }
        public UtilisateurRoleNotFoundException(string message)
            : base(message) { }
    }
    
}