using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;

namespace AperoBoxApi.DAO
{
    public class UtilisateurRoleDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public UtilisateurRoleDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<UtilisateurRole>> GetUtilisateurRoleByUserId(int userId)
        {
            return await context.UtilisateurRole
                .Where(u => u.IdUtilisateur == userId)
                .ToListAsync();
        }
    }

}
