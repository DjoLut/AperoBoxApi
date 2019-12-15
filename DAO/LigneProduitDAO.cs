using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.ExceptionsPackage;
using AperoBoxApi.DTO;

namespace AperoBoxApi.DAO
{
    public class LigneProduitDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public LigneProduitDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LigneProduit> GetLigneProduitById(int id)
        {
            return await context.LigneProduit
                .FirstOrDefaultAsync(lp => lp.Id == id);
        }

        public async Task<LigneProduit> AjouterLigneProduit(LigneProduit ligneProduit)
        {
            if (ligneProduit == null)
                throw new LigneProduitNotFoundException();

            context.LigneProduit.Add(ligneProduit);
            await context.SaveChangesAsync();
            return ligneProduit;
        }

        public async Task ModifierLigneProduit(LigneProduit ligneProduit, LigneProduitDTO ligneProduitDTO)
        {
            ligneProduit.Id = ligneProduitDTO.Id;
            context.Entry(ligneProduit).OriginalValues["RowVersion"] = ligneProduitDTO.RowVersion;

            await context.SaveChangesAsync();
        }

        public async Task SuppressionLigneProduit(LigneProduit ligneProduit)
        {
            if(ligneProduit == null)
                throw new LigneProduitNotFoundException();
            
            context.Remove(ligneProduit);
            await context.SaveChangesAsync();
        }
    }
}