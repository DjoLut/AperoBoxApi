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
    public class AdresseDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public AdresseDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Adresse>> GetAllAdresses()
        {
            return await context.Adresse
                .Include(a => a.Utilisateur)
                .Include(a => a.Commande)
                .ToListAsync();
        }

        public async Task<Adresse> GetAdresseById(int id)
        {
            return await context.Adresse
                .Include(a => a.Utilisateur)
                .Include(a => a.Commande)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Adresse> GetAdresseByAdresse(Adresse adresse)
        {
            return await context.Adresse
                .Include(a => a.Utilisateur)
                .Include(a => a.Commande)
                .Where(a => a.Localite.ToLower().Equals(adresse.Localite.ToLower()) 
                    && a.Numero == adresse.Numero 
                    && a.Pays.ToLower().Equals(adresse.Pays.ToLower())
                    && a.CodePostal == adresse.CodePostal
                    && a.Rue.ToLower().Equals(adresse.Rue.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<Adresse> AjouterAdresse(Adresse adresse)
        {
            if (adresse == null)
                throw new AdresseNotFoundException();

            Adresse adresseExiste = await GetAdresseByAdresse(adresse);
            if (adresseExiste != null)
                return adresseExiste;

            context.Adresse.Add(adresse);
            await context.SaveChangesAsync();
            return adresse;
        }
    }
}