using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.DTO;
using AperoBoxApi.Exceptions;

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

        public async Task<List<Utilisateur>> getAllUtilisateurs()
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Adresse)
                .Include(u => u.Commande)
                .ToListAsync();
        }

        public async Task<Utilisateur> getUtilisateurById(int id)
        {
            return await context.Utilisateur
                .Include(u => u.Commentaire)
                .Include(u => u.Adresse)
                .Include(u => u.Commande)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Utilisateur> ajouterUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                throw new UtilisateurNotFoundException();

            context.Utilisateur.Add(utilisateur);
            await context.SaveChangesAsync();
            return utilisateur;
        }

        public async Task modifierUtilisateur(Utilisateur utilisateur, UtilisateurDTO utilisateurDTO)
        {
            utilisateur.Id = utilisateurDTO.Id;
            utilisateur.Nom = utilisateurDTO.Nom;
            utilisateur.Prenom = utilisateurDTO.Prenom;
            utilisateur.DateNaissance = utilisateurDTO.DateNaissance;
            utilisateur.Mail = utilisateurDTO.Mail;
            utilisateur.Telephone = utilisateurDTO.Telephone;
            utilisateur.Gsm = utilisateurDTO.Gsm;
            utilisateur.Username = utilisateurDTO.Username;
            utilisateur.Authorities = utilisateurDTO.Authorities;
            utilisateur.MotDePasse = utilisateurDTO.MotDePasse;
            utilisateur.Adresse = utilisateurDTO.Adresse;

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

            context.Remove(utilisateur);
            await context.SaveChangesAsync();
        }

    }
}