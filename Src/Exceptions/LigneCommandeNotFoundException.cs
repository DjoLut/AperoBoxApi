using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class LigneCommandeNotFoundException : PersonnalException
    {
        public LigneCommandeNotFoundException()
            : base("Ligne de commande non trouvée") { }
        public LigneCommandeNotFoundException(string message)
            : base(message) { }
    }
    
}