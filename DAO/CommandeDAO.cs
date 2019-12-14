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
    public class CommandeDAO
    {
        private AperoBoxApi_dbContext context;

        public CommandeDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Commande>> getAllCommandes()
        {
            return await context.Commande
                .Include(c => c.LigneCommande)
                .ToListAsync();
        }

        public async Task<Commande> ajouterCommande(Commande commande)
        {
            if (commande == null)
                throw new CommandeNotFoundException();

            context.Commande.Add(commande);
            await context.SaveChangesAsync();
            return commande;
        }
    }
}