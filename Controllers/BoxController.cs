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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxDTO>))]
        public async Task<ActionResult<IEnumerable<Box>>> getAllBoxes()
        {
            List<Box> boxes = await boxDAO.getAllBoxes();
            if (boxes == null)
                return NotFound();

            return Ok(mapper.Map<List<BoxDTO>>(boxes));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(BoxDTO))]
        public async Task<ActionResult<Box>> getBoxById(int id)
        {
            Box box = await boxDAO.getBoxById(id);
            if (box == null)
                return NotFound();

            return Ok(mapper.Map<BoxDTO>(box));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
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
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(BoxDTO))]
        public async Task<ActionResult> modifierBox([FromBody] BoxDTO boxDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            int id = Decimal.ToInt32(boxDTO.Id);
            Box box = await boxDAO.getBoxById(id);
            if(box == null)
                return NotFound();

            if(!User.IsInRole(Constants.Roles.Admin))
                return Forbid();

            await boxDAO.modifierBox(box, boxDTO);
            return Ok(boxDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
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
