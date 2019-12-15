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
using AperoBoxApi.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(Roles = Constants.Roles.Admin)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<UtilisateurDTO>))]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> getAllUtilisateurs()
        {
            List<Utilisateur> utilisateurs = await utilisateurDAO.getAllUtilisateurs();
            if (utilisateurs == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> getUtilisateurById(int id)
        {
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
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
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> ajouterUtilisateur([FromBody]UtilisateurDTO utilisateurDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            utilisateurDTO.MotDePasse = Bcrypt.HashPassword(utilisateurDTO.MotDePasse);
            Utilisateur utilisateur = mapper.Map<Utilisateur>(utilisateurDTO);
            utilisateur = await utilisateurDAO.ajouterUtilisateur(utilisateur);
            return Created($"api/Utilisateur/{utilisateur.Id}", mapper.Map<UtilisateurDTO>(utilisateur));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> ModifierUtilisateur(int id, [FromBody] UtilisateurDTO utilisateurDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //int id = Decimal.ToInt32(utilisateurDTO.Id);
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();

            //TEST SI ON VEUT SE MODIFIER SOI MEME ???
            int userId = int.Parse(User.Claims.First(c => c.Type == PrivateClaims.UserId).Value);
            if (!User.IsInRole(Constants.Roles.Admin))
                return Forbid();

            await utilisateurDAO.ModifierUtilisateur(utilisateur, utilisateurDTO);
            return Ok(utilisateurDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> suppressionUtilisateur(int id) 
        {
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            await utilisateurDAO.suppressionUtilisateur(utilisateur);
            return Ok();
        }

    }
}
