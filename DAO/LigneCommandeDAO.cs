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
    public class LigneCommandeDAO
    {
        private AperoBoxApi_dbContext context;

        public LigneCommandeDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LigneCommande> ajouterLigneCommande(LigneCommande ligneCommande)
        {
            if (ligneCommande == null)
                throw new LigneCommandeNotFoundException();

            context.LigneCommande.Add(ligneCommande);
            await context.SaveChangesAsync();
            return ligneCommande;
        }
    }
}