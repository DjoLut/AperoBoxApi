using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Context;
using AperoBoxApi.Models;

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
            return await context.Box.ToListAsync();
        }

        public async Task<Box> getBoxById(int id)
        {
            return await context.Box
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task suppressionBox(Box box)
        {
            context.Remove(box);
            await context.SaveChangesAsync();
        }
    }
}