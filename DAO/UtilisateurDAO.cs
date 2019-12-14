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
        private AperoBoxApi_dbContext context;

        public UtilisateurDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Utilisateur>> getAllUtilisateurs()
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .Include(u => u.UtilisateurRole)
                .ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateurById(int id)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Utilisateur> getUtilisateurByUsername(string username)
        {
            return await context.Utilisateur
                .Include(u=> u.Commentaire)
                .Include(u => u.Commande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u=> u.Username == username);
        }

        public async Task<Utilisateur> getUtilisateurByMail(string mail)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Commande)
                .Include(u => u.UtilisateurRole)
                .FirstOrDefaultAsync(u => u.Mail == mail);
        }

        public async Task<Utilisateur> ajouterUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                throw new UtilisateurNotFoundException();

            context.Utilisateur.Add(utilisateur);
            await context.SaveChangesAsync();
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

        public async Task suppressionUtilisateur(Utilisateur utilisateur)
        {
            if(utilisateur == null)
                throw new UtilisateurNotFoundException();

            if(utilisateur.Commande != null)
            {
                foreach(var commande in utilisateur.Commande)
                    context.Remove(commande);
            }

            if(utilisateur.Commentaire != null)
            {
                foreach(var commentaire in utilisateur.Commentaire)
                    context.Remove(commentaire);
            }

            if(utilisateur.UtilisateurRole != null)
            {
                foreach(var utilisateurRole in utilisateur.UtilisateurRole)
                    context.Remove(utilisateur);
            }

            context.Remove(utilisateur);
            await context.SaveChangesAsync();
        }

    }
}