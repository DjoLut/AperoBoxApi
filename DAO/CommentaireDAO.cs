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
    public class CommentaireDAO
    {
        private readonly AperoBoxApi_dbContext context;

        public CommentaireDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Commentaire>> GetAllCommentaires()
        {
            return await context.Commentaire
                .ToListAsync();
        }

        public async Task<Commentaire> GetCommentaireById(int id)
        {
            return await context.Commentaire
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Commentaire> AjouterCommentaire(Commentaire commentaire)
        {
            if(commentaire == null)
                throw new CommentaireNotFoundException();

            context.Commentaire.Add(commentaire);
            await context.SaveChangesAsync();
            return commentaire;
        }

        public async Task SuppressionCommentaire(Commentaire commentaire)
        {
            if(commentaire == null)
                throw new CommentaireNotFoundException();

            context.Remove(commentaire);
            await context.SaveChangesAsync();
        }
    }
}