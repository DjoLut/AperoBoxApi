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
    public class LigneCommandeController : ControllerBase
    {
        private readonly AperoBoxApi_dbContext context;
        private readonly LigneCommandeDAO ligneCommandeDAO;
        private readonly IMapper mapper;
        public LigneCommandeController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.ligneCommandeDAO = new LigneCommandeDAO(context);
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Utilisateur)] 
        [ProducesResponseType(201, Type = typeof(LigneCommandeDTO))]
        public async Task<ActionResult> AjouterLigneCommande([FromBody]LigneCommandeDTO ligneCommandeDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            LigneCommande ligneCommande = mapper.Map<LigneCommande>(ligneCommandeDTO);
            ligneCommande = await ligneCommandeDAO.AjouterLigneCommande(ligneCommande);
            return Created($"api/LigneCommande/{ligneCommande.Id}", mapper.Map<LigneCommandeDTO>(ligneCommande));
        }

        [HttpPut]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(LigneCommandeDTO))]
        public async Task<ActionResult> ModifierLigneCommande([FromBody] LigneCommandeDTO ligneCommandeDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = Decimal.ToInt32(ligneCommandeDTO.Id);
            LigneCommande ligneCommande = await ligneCommandeDAO.GetLigneCommandeById(id);
            if(ligneCommande == null)
                return NotFound();

            await ligneCommandeDAO.ModifierLigneCommande(ligneCommande, ligneCommandeDTO);
            return Ok(ligneCommande);
        }
    }
}
