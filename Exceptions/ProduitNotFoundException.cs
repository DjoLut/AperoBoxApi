using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class ProduitNotFoundException : PersonnalException
    {
        public ProduitNotFoundException()
            : base("Produit non trouvé") { }

        public ProduitNotFoundException(string message)
            : base(message) { }
    }
    
}