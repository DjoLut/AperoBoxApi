using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.ExceptionsPackage;

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

        public async Task<UtilisateurRole> GetUtilisateurRoleByIdRoleAndUserId(string idRole, int userId)
        {
            return await context.UtilisateurRole
                .Where(u => u.IdRole.Equals(idRole) && u.IdUtilisateur == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<UtilisateurRole> AjouterUtilisateurRole(UtilisateurRole utilisateurRole)
        {
            if (utilisateurRole == null)
                throw new UtilisateurRoleNotFoundException();

            context.UtilisateurRole.Add(utilisateurRole);
            await context.SaveChangesAsync();
            return utilisateurRole;
        }

        public async Task<UtilisateurRole> AjouterDefaultRole(Utilisateur utilisateur)
        {
            UtilisateurRole utilisateurRole = new UtilisateurRole();
            utilisateurRole.IdRole = Constants.Roles.Utilisateur;
            utilisateurRole.IdUtilisateur = utilisateur.Id;
            context.Add(utilisateurRole);
            await context.SaveChangesAsync();
            return utilisateurRole;
        }

        public async Task SuppressionUtilisateurRole(UtilisateurRole utilisateurRole)
        {
            if (utilisateurRole == null)
                throw new UtilisateurRoleNotFoundException();

            context.Remove(utilisateurRole);
            await context.SaveChangesAsync();
        }
    }

}
