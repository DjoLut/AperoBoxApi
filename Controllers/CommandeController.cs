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
    public class CommandeController : ControllerBase
    {
        private readonly AperoBoxApi_dbContext context;
        private readonly CommandeDAO commandeDAO;
        private readonly IMapper mapper;
        public CommandeController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.commandeDAO = new CommandeDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommandeDTO>))]
        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandes()
        {
            List<Commande> commandes = await commandeDAO.GetAllCommandes();
            if(commandes == null)
                return NotFound();

            return Ok(mapper.Map<List<CommandeDTO>>(commandes));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Utilisateur)]
        [ProducesResponseType(201, Type = typeof(CommandeDTO))]
        public async Task<ActionResult> AjouterCommande([FromBody] CommandeDTO commandeDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Commande commande = mapper.Map<Commande>(commandeDTO);
            commande = await commandeDAO.AjouterCommande(commande);
            return Created($"api/Commande/{commande.Id}", mapper.Map<CommandeDTO>(commande));
        }
    }
}
