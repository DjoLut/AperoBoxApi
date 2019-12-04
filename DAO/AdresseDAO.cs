using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.Exceptions;

namespace AperoBoxApi.DAO
{
    public class AdresseDAO
    {
        private AperoBoxApi_dbContext context;

        public AdresseDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Adresse>> getAllAdresses()
        {
            return await context.Adresse
                .Include(a => a.Utilisateur)
                .Include(a => a.Commande)
                .ToListAsync();
        }

        public async Task<Adresse> getAdresseById(int id)
        {
            return await context.Adresse
                .Include(a => a.Utilisateur)
                .Include(a => a.Commande)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Adresse> ajouterAdresse(Adresse adresse)
        {
            if (adresse == null)
                throw new AdresseNotFoundException();

            context.Adresse.Add(adresse);
            await context.SaveChangesAsync();
            return adresse;
        }
    }
}