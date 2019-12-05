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
    public class ProduitDAO
    {
        private AperoBoxApi_dbContext context;

        public ProduitDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Produit>> getAllProduits()
        {
            return await context.Produit
                .Include(p => p.LigneCommande)
                .Include(p => p.LigneProduit)
                .ToListAsync();
        }
        
        public async Task<Produit> getProduitById(int id)
        {
            return await context.Produit
                .Include(p => p.LigneCommande)
                .Include(p => p.LigneProduit)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produit> ajouterProduit(Produit produit)
        {
            if (produit == null)
                throw new ProduitNotFoundException();

            context.Produit.Add(produit);
            await context.SaveChangesAsync();
            return produit;
        }
    }
}