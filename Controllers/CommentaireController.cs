using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperoBoxApi.Models;
using AperoBoxApi.Context;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentaireController : ControllerBase
    {
        public CommentaireController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<Commentaire> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var commentaires = context.Commentaire
                    .ToList();
                    
                return commentaires;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
