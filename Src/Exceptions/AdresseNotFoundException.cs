using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class AdresseNotFoundException : PersonnalException
    {
        public AdresseNotFoundException()
            : base("Adresse non trouvée") { }
        public AdresseNotFoundException(string message)
            : base(message) { }
    }
    
}