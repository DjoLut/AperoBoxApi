using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class UtilisateurNotFoundException : PersonnalException
    {
        public UtilisateurNotFoundException()
            : base("Utilisateur non trouvé") { }
        public UtilisateurNotFoundException(string message)
            : base(message) { }
    }
    
}