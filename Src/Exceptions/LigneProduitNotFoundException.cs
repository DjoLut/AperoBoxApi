using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class LigneProduitNotFoundException : PersonnalException
    {
        public LigneProduitNotFoundException()
            : base("Ligne de produit non trouvée") { }
        public LigneProduitNotFoundException(string message) 
            : base(message) { }
    }
    
}