using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class CommandeNotFoundException : PersonnalException
    {
        public CommandeNotFoundException()
            : base("Commande non trouvée") { }

        public CommandeNotFoundException(string message)
            : base(message) { }
    }
    
}