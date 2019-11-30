using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DTO;
using AperoBoxApi.DAO;
using AutoMapper;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private UtilisateurDAO utilisateurDAO;
        private readonly IMapper mapper;
        public UtilisateurController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.utilisateurDAO = new UtilisateurDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UtilisateurDTO>))]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> getAllUtilisateurs()
        {
            List<Utilisateur> utilisateurs = await utilisateurDAO.getAllUtilisateurs();
            if (utilisateurs == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> getUtilisateurById(int id)
        {
            Utilisateur utilisateur = await utilisateurDAO.getUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
        }

        /*[HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> getUtilisateurByUsername(string username)
        {
          Utilisateur utilisateur = await utilisateurDAO.getUtilisateurByUsername(username);
          if (utilisateur == null)
            return NotFound();

          return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
        }

		[HttpGet("{mail}")]
		[ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
		public async Task<ActionResult<Utilisateur>> getUtilisateurByMail(string mail)
		{
			Utilisateur utilisateur = await utilisateurDAO.getUtilisateurByMail(mail);
			if (utilisateur == null)
			  return NotFound();

			return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
		}*/

		[HttpPost]
        [ProducesResponseType(201, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> ajouterUtilisateur([FromBody]UtilisateurDTO utilisateurDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Utilisateur utilisateur = mapper.Map<Utilisateur>(utilisateurDTO);
            utilisateur = await utilisateurDAO.ajouterUtilisateur(utilisateur);
            return Created($"api/Utilisateur/{utilisateur.Id}", mapper.Map<UtilisateurDTO>(utilisateur));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> modifierUtilisateur([FromBody] UtilisateurDTO utilisateurDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = Decimal.ToInt32(utilisateurDTO.Id);
            Utilisateur utilisateur = await utilisateurDAO.getUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();

            await utilisateurDAO.modifierUtilisateur(utilisateur, utilisateurDTO);
            return Ok(utilisateur);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> suppressionUtilisateur(int id) 
        {
            Utilisateur utilisateur = await utilisateurDAO.getUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            await utilisateurDAO.suppressionUtilisateur(utilisateur);
            return Ok();
        }

    }
}
