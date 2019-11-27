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
    public class ProduitController : ControllerBase
    {
        public ProduitController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<Produit> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var produits = context.Produit
                    .ToList();
                    
                return produits;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
