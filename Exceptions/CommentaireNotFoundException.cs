using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class CommentaireNotFoundException : PersonnalException
    {
        public CommentaireNotFoundException()
            : base("Commentaire non trouvé") { }

        public CommentaireNotFoundException(string message)
            : base(message) { }
    }
    
}