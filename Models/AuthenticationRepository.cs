using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AperoBoxApi.Models;
using System.Linq;
using AperoBoxApi.Context;
using Microsoft.EntityFrameworkCore;

namespace AperoBoxApi.Models
{
    public class AuthenticationRepository {
        private  AperoBoxApi_dbContext context;
        public AuthenticationRepository(AperoBoxApi_dbContext context) {
            this.context = context;
        }
        
        public IEnumerable<Utilisateur> GetUtilisateurs(){
            return context.Utilisateur
            .Include(u => u.UtilisateurRole)
                .ThenInclude(r => r.IdRole)
            .ToList();
        }
        
    }
}