using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;

namespace AperoBoxApi.DAO
{
    public class CommentaireDAO
    {
        private AperoBoxApi_dbContext context;

        public CommentaireDAO(AperoBoxApi_dbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Commentaire>> getCommentaires()
        {
            return await context.Commentaire.ToListAsync();
        }

        public async Task<Commentaire> getCommentaireById(int id)
        {
            return await context.Commentaire
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task suppressionCommentaire(Commentaire commentaire)
        {
            context.Remove(commentaire);
            await context.SaveChangesAsync();
        }


    }
}