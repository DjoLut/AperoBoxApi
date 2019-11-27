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
    public class AdresseController : ControllerBase
    {
        public AdresseController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<Adresse> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var adresses = context.Adresse
                    .ToList();
                    
                return adresses;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
