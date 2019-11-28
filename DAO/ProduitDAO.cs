using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;

namespace AperoBoxApi.DAO
{
    public class ProduitDAO
    {
        private AperoBoxApi_dbContext context;

        public ProduitDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Produit>> getProduits()
        {
            return await context.Produit.ToListAsync();
        }
    }
}