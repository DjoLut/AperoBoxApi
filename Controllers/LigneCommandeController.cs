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
    public class LigneCommandeController : ControllerBase
    {
        public LigneCommandeController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<LigneCommande> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var ligneCommandes = context.LigneCommande
                    .ToList();
                    
                return ligneCommandes;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
