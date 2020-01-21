using System;

namespace AperoBoxApi.ExceptionsPackage
{
    public class BoxNotFoundException : PersonnalException
    {
        public BoxNotFoundException()
            : base("Box non trouvé") { }

        public BoxNotFoundException(string message)
            : base(message) { }
    }
    
}