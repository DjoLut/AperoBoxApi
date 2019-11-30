using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;
using AperoBoxApi.DTO;
using AperoBoxApi.Exceptions;

namespace AperoBoxApi.DAO
{
    public class BoxDAO
    {
        private AperoBoxApi_dbContext context;

        public BoxDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Box>> getBoxes()
        {
            return await context.Box
                .Include(b => b.Commentaire)
                .Include(b => b.LigneProduit)
                    .ThenInclude(l => l.Produit)
                .ToListAsync();
        }

        public async Task<Box> getBoxById(int id)
        {
            return await context.Box
                .Include(b => b.Commentaire)
                .Include(b => b.LigneProduit)
                    .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task modifierBox(Box box, BoxDTO boxDTO)
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

            await context.SaveChangesAsync();
        }

        public async Task<Box> ajouterBox(Box box)
        {
            if (box == null)
                throw new BoxNotFoundException();

            context.Box.Add(box);
            await context.SaveChangesAsync();
            return box;
        }

        public async Task suppressionBox(Box box)
        {
            context.Remove(box);
            await context.SaveChangesAsync();
        }
    }
}