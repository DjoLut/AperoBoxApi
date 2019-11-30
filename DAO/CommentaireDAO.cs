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
    public class CommentaireDAO
    {
        private AperoBoxApi_dbContext context;

        public CommentaireDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Commentaire>> getAllCommentaires()
        {
            return await context.Commentaire
                .Include(c => c.Utilisateur)
                .Include(c => c.Box)
                .ToListAsync();
        }

        public async Task<Commentaire> getCommentaireById(int id)
        {
            return await context.Commentaire
                .Include(c => c.Utilisateur)
                .Include(c => c.Box)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Commentaire> ajouterCommentaire(Commentaire commentaire)
        {
            if(commentaire == null)
                throw new CommentaireNotFoundException();

            context.Commentaire.Add(commentaire);
            await context.SaveChangesAsync();
            return commentaire;
        }

        public async Task suppressionCommentaire(Commentaire commentaire)
        {
            if(commentaire == null)
                throw new CommentaireNotFoundException();

            context.Remove(commentaire);
            await context.SaveChangesAsync();
        }
    }
}