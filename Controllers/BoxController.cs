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
    public class BoxController : ControllerBase
    {
        public BoxController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<Box> Get()
        {
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var boxes = context.Box
                    .ToList();
                    
                return boxes;
            }
        }


        [HttpPost]
        public void Post()
        {
            
        }
    }
}
