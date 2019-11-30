using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.Exceptions;
using AperoBoxApi.DTO;

namespace AperoBoxApi.DAO
{
    public class LigneProduitDAO
    {
        private AperoBoxApi_dbContext context;

        public LigneProduitDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LigneProduit> getLigneProduitById(int id)
        {
            return await context.LigneProduit
                .Include(lp => lp.Box)
                .Include(lp => lp.Produit)
                .FirstOrDefaultAsync(lp => lp.Id == id);
        }

        public async Task<LigneProduit> ajouterLigneProduit(LigneProduit ligneProduit)
        {
            if (ligneProduit == null)
                throw new LigneProduitNotFoundException();

            context.LigneProduit.Add(ligneProduit);
            await context.SaveChangesAsync();
            return ligneProduit;
        }

        public async Task modifierLigneProduit(LigneProduit ligneProduit, LigneProduitDTO ligneProduitDTO)
        {
            ligneProduit.Id = ligneProduitDTO.Id;
            ligneProduit.Quantite = ligneProduitDTO.Quantite;
            ligneProduit.Box = ligneProduitDTO.Box;
            ligneProduit.Produit = ligneProduitDTO.Produit;

            await context.SaveChangesAsync();
        }
    }
}