using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;

namespace AperoBoxApi.DAO
{
    public class UtilisateurDAO
    {
        private AperoBoxApi_dbContext context;

        public UtilisateurDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /*public async Task<Utilisateur> ajoutUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                throw new UserNotFoundException();
            context.utilisateur.Add(utilisateur);
            UserRole userRole= new UserRole();
            UserRoleDAO userRoleDAO = new UserRoleDAO(context);
            await context.SaveChangesAsync();
            await userRoleDAO.AddUserRole(utilisateur);
            return utilisateur;
        }*/

        public async Task<List<Utilisateur>> getUtilisateur(string email)
        {
            return await context.Utilisateur.ToListAsync();
        }

    }
}