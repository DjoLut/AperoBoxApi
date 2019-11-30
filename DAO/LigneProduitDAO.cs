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
    public class LigneProduitDAO
    {
        private AperoBoxApi_dbContext context;

        public LigneProduitDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LigneProduit> ajouterLigneProduit(LigneProduit ligneProduit)
        {
            if (ligneProduit == null)
                throw new LigneProduitNotFoundException();

            context.LigneProduit.Add(ligneProduit);
            await context.SaveChangesAsync();
            return ligneProduit;
        }
    }
}