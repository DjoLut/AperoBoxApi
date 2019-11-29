using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DAO;
using AperoBoxApi.DTO;
using AutoMapper;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private BoxDAO boxDAO;
        private readonly IMapper mapper;
        public BoxController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.boxDAO = new BoxDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxDTO>))]
        public async Task<ActionResult<IEnumerable<Box>>> getBoxes()
        {
            List<Box> boxes = await boxDAO.getBoxes();
            if (boxes == null)
                return NotFound();

            return Ok(mapper.Map<List<BoxDTO>>(boxes));
        }

        [HttpPost]
        public void Post()
        {
            
        }
    }
}
