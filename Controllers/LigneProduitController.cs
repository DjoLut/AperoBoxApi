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

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LigneProduitController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private LigneProduitDAO ligneProduitDAO;
        private readonly IMapper mapper;
        public LigneProduitController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.ligneProduitDAO = new LigneProduitDAO(context);
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> ajouterLigneProduit([FromBody]LigneProduitDTO ligneProduitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            LigneProduit ligneProduit = mapper.Map<LigneProduit>(ligneProduitDTO);
            ligneProduit = await ligneProduitDAO.ajouterLigneProduit(ligneProduit);
            return Created($"api/LigneProduit/{ligneProduit.Id}", mapper.Map<LigneCommandeDTO>(ligneProduit));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> modifierLigneProduit([FromBody] LigneProduitDTO ligneProduitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = Decimal.ToInt32(ligneProduitDTO.Id);
            LigneProduit ligneProduit = await ligneProduitDAO.getLigneProduitById(id);
            if(ligneProduit == null)
                return NotFound();

            await ligneProduitDAO.modifierLigneProduit(ligneProduit, ligneProduitDTO);

            return Ok(ligneProduit);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> suppressionLigneProduit(int id)
        {
            LigneProduit ligneProduit = await ligneProduitDAO.getLigneProduitById(id);
            if(ligneProduit == null)
                return NotFound();

            await ligneProduitDAO.suppressionLigneProduit(ligneProduit);
            return Ok();
        }
    }
}