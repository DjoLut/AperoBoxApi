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
    public class LigneCommandeDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public LigneCommandeDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LigneCommande> GetLigneCommandeById(int id)
        {
            return await context.LigneCommande
                .FirstOrDefaultAsync(lc => lc.Id == id);
        }

        public async Task<LigneCommande> AjouterLigneCommande(LigneCommande ligneCommande)
        {
            if (ligneCommande == null)
                throw new LigneCommandeNotFoundException();

            context.LigneCommande.Add(ligneCommande);
            await context.SaveChangesAsync();
            return ligneCommande;
        }

        public async Task ModifierLigneCommande(LigneCommande ligneCommande, LigneCommandeDTO ligneCommandeDTO)
        {
            ligneCommande.Id = ligneCommandeDTO.Id;
            ligneCommande.Commande = ligneCommandeDTO.Commande;
            ligneCommande.Box = ligneCommandeDTO.Box;
            ligneCommande.Produit = ligneCommandeDTO.Produit;
            context.Entry(ligneCommande).OriginalValues["RowVersion"] = ligneCommandeDTO.RowVersion;

            await context.SaveChangesAsync();
        }
    }
}