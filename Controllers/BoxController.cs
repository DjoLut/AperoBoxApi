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
        [ProducesResponseType(201, Type = typeof(BoxDTO))]
        public async Task<ActionResult> ajouterBox([FromBody] BoxDTO boxDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Box box = mapper.Map<Box>(boxDTO);
            box = await boxDAO.ajouterBox(box);
            return Created($"api/Box/{box.Id}", mapper.Map<BoxDTO>(box));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BoxDTO))]
        public async Task<ActionResult> modifierBox([FromBody] BoxDTO boxDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            int id = Decimal.ToInt32(boxDTO.Id);
            Box box = await boxDAO.getBoxById(id);

            await boxDAO.modifierBox(box, boxDTO);
            return Ok(box);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(BoxDTO))]
        public async Task<ActionResult> suppressionBox(int id) 
        {
            Box box = await boxDAO.getBoxById(id);
            if(box == null)
                return NotFound();
            
            await boxDAO.suppressionBox(box);
            return Ok();
        }
        
    }
}
