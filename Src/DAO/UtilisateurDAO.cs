using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.DTO;
using AperoBoxApi.ExceptionsPackage;
using AutoMapper;

namespace AperoBoxApi.DAO
{
    public class UtilisateurDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public UtilisateurDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Utilisateur>> GetAllUtilisateurs(int? pageIndex, int? pageSize)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .Include(u => u.UtilisateurRole)
                .OrderBy(u => u.Id)
                .Skip(pageIndex.Value * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync();
        }

        public async Task<int> GetCountUtilisateur()
        {
            return await context.Utilisateur
                .CountAsync();
        }

        public async Task<Utilisateur> GetUtilisateurById(int id)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .ThenInclude(l => l.LigneCommande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Utilisateur> GetUtilisateurByUsername(string username)
        {
            return await context.Utilisateur
                .Include(u=> u.Commentaire)
                .Include(u => u.Commande)
                .ThenInclude(l => l.LigneCommande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u=> u.Username.ToLower().Equals(username.ToLower()));
        }

        public async Task<Utilisateur> GetUtilisateurByMail(string mail)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .ThenInclude(l => l.LigneCommande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u => u.Mail.ToLower().Equals(mail.ToLower()));
        }

        public async Task<Utilisateur> AjouterUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                throw new UtilisateurNotFoundException();

            context.Utilisateur.Add(utilisateur);

            UtilisateurRoleDAO utilisateurRoleDAO = new UtilisateurRoleDAO(context);

            await context.SaveChangesAsync();

            await utilisateurRoleDAO.AjouterDefaultRole(utilisateur);
            return utilisateur;
        }

        public async Task ModifierUtilisateur(Utilisateur utilisateur, UtilisateurDTO utilisateurDTO)
        {
            //utilisateur = mapper.Map<UtilisateurDTO, Utilisateur>(utilisateurDTO, utilisateur);
            utilisateur.Id = utilisateurDTO.Id;
            utilisateur.Nom = utilisateurDTO.Nom;
            utilisateur.Prenom = utilisateurDTO.Prenom;
            utilisateur.DateNaissance = utilisateurDTO.DateNaissance;
            utilisateur.Mail = utilisateurDTO.Mail;
            utilisateur.Telephone = utilisateurDTO.Telephone;
            utilisateur.Gsm = utilisateurDTO.Gsm;
            utilisateur.Username = utilisateurDTO.Username;
            utilisateur.Adresse = utilisateurDTO.Adresse;
            context.Entry(utilisateur).OriginalValues["RowVersion"] = utilisateurDTO.RowVersion;
            await context.SaveChangesAsync();
        }

        public async Task SuppressionUtilisateur(Utilisateur utilisateur)
        {
            if(utilisateur == null)
                throw new UtilisateurNotFoundException();

            if(utilisateur.Commande != null)
            {
                foreach(var commande in utilisateur.Commande)
                {
                    if(commande.LigneCommande != null)
                    {
                        foreach (var lc in commande.LigneCommande)
                            context.Remove(lc);
                    }
                    context.Remove(commande);
                }
            }

            if(utilisateur.Commentaire != null)
            {
                foreach(var commentaire in utilisateur.Commentaire)
                    context.Remove(commentaire);
            }

            if(utilisateur.UtilisateurRole != null)
            {
                foreach(var utilisateurRole in utilisateur.UtilisateurRole)
                    context.Remove(utilisateurRole);
            }

            context.Remove(utilisateur);
            await context.SaveChangesAsync();
        }

    }
}