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
    public class LigneProduitController : ControllerBase
    {
        public LigneProduitController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<LigneProduit> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var ligneProduits = context.LigneProduit
                    .ToList();
                    
                return ligneProduits;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
