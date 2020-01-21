using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.DTO;
using AperoBoxApi.ExceptionsPackage;

namespace AperoBoxApi.DAO
{
    public class BoxDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public BoxDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Box>> GetAllBoxes()
        {
            return await context.Box
                .Include(b => b.Commentaire)
                .Include(b => b.LigneProduit)
                .Include(b => b.LigneCommande)
                .ToListAsync();
        }

        public async Task<Box> GetBoxById(int id)
        {
            return await context.Box
                .Include(b => b.Commentaire)
                .Include(b => b.LigneProduit)
                .Include(b => b.LigneCommande)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task ModifierBox(Box box, BoxDTO boxDTO)
        {
            box.Id = boxDTO.Id;
            box.Nom = boxDTO.Nom;
            box.PrixUnitaireHtva = boxDTO.PrixUnitaireHtva;
            box.Tva = boxDTO.Tva;
            box.Promotion = boxDTO.Promotion;
            box.Description = boxDTO.Description;
            box.Photo = boxDTO.Photo;
            box.Affichable = boxDTO.Affichable;
            box.DateCreation = boxDTO.DateCreation;
            context.Entry(box).OriginalValues["RowVersion"] = boxDTO.RowVersion;

            await context.SaveChangesAsync();
        }

        public async Task<Box> AjouterBox(Box box)
        {
            if (box == null)
                throw new BoxNotFoundException();

            context.Box.Add(box);
            await context.SaveChangesAsync();
            return box;
        }

        public async Task SuppressionBox(Box box)
        {
            if(box == null)
                throw new BoxNotFoundException();

            if(box.Commentaire != null)
            {
                foreach(var commentaire in box.Commentaire)
                    context.Remove(commentaire);
            }

            if(box.LigneCommande != null)
            {
                foreach(var lc in box.LigneCommande)
                    context.Remove(lc);
            }

            if(box.LigneProduit != null)
            {
                foreach(var lp in box.LigneProduit)
                    context.Remove(lp);
            }

            context.Remove(box);
            await context.SaveChangesAsync();
        }
    }
}