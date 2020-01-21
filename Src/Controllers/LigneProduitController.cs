using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DTO;
using AperoBoxApi.DAO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class LigneProduitController : ControllerBase
    {
        private readonly AperoBoxApi_dbContext context;
        private readonly LigneProduitDAO ligneProduitDAO;
        private readonly BoxDAO boxDAO;
        private readonly IMapper mapper;
        public LigneProduitController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.ligneProduitDAO = new LigneProduitDAO(context);
            this.boxDAO = new BoxDAO(context);
            this.mapper = mapper;
        }

        [HttpGet("{idBox}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LigneProduitDTO>))]
        public async Task<ActionResult<IEnumerable<LigneProduit>>> GetLigneProduitByIdBox(int idBox)
        {
            List<LigneProduit> ligneProduits = await ligneProduitDAO.GetLigneProduitByIdBox(idBox);
            if (ligneProduits == null)
                return NotFound();

            return Ok(mapper.Map<List<LigneProduitDTO>>(ligneProduits));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Utilisateur)] 
        [ProducesResponseType(201, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> AjouterLigneProduit([FromBody]LigneProduitDTO ligneProduitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            LigneProduit ligneProduit = mapper.Map<LigneProduit>(ligneProduitDTO);
            ligneProduit = await ligneProduitDAO.AjouterLigneProduit(ligneProduit);
            return Created($"api/LigneProduit/{ligneProduit.Id}", mapper.Map<LigneProduitDTO>(ligneProduit));
        }

        [HttpPut]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> ModifierLigneProduit([FromBody] LigneProduitDTO ligneProduitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = Decimal.ToInt32(ligneProduitDTO.Id);
            LigneProduit ligneProduit = await ligneProduitDAO.GetLigneProduitById(id);
            if(ligneProduit == null)
                return NotFound();

            await ligneProduitDAO.ModifierLigneProduit(ligneProduit, ligneProduitDTO);

            return Ok(ligneProduit);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> SuppressionLigneProduit(int id)
        {
            LigneProduit ligneProduit = await ligneProduitDAO.GetLigneProduitById(id);
            if(ligneProduit == null)
                return NotFound();

            await ligneProduitDAO.SuppressionLigneProduit(ligneProduit);
            return Ok();
        }
    }
}