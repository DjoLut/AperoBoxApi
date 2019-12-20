using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly AperoBoxApi_dbContext context;
        private readonly UtilisateurDAO utilisateurDAO;
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
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllUtilisateurs()
        {
            List<Utilisateur> utilisateurs = await utilisateurDAO.GetAllUtilisateurs();
            if (utilisateurs == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
        }

        /*[HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByUsername(string username)
        {
          Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurByUsername(username);
          if (utilisateur == null)
            return NotFound();

          return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
        }

		[HttpGet("{mail}")]
		[ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
		public async Task<ActionResult<Utilisateur>> GetUtilisateurByMail(string mail)
		{
			Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurByMail(mail);
			if (utilisateur == null)
			  return NotFound();

			return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
		}*/

		[HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(UtilisateurDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> AjouterUtilisateur([FromBody]UtilisateurDTO utilisateurDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //TEST UNIQUE EMAIL ET UNIQUE USERNAME
            Utilisateur utilisateurEmail = await utilisateurDAO.GetUtilisateurByMail(utilisateurDTO.Mail);
            Utilisateur utilisateurUsername = await utilisateurDAO.GetUtilisateurByMail(utilisateurDTO.Username);
            if(utilisateurEmail != null || utilisateurUsername != null)
                return BadRequest();

            utilisateurDTO.MotDePasse = Bcrypt.HashPassword(utilisateurDTO.MotDePasse);
            Utilisateur utilisateur = mapper.Map<Utilisateur>(utilisateurDTO);
            utilisateur = await utilisateurDAO.AjouterUtilisateur(utilisateur);
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

            Utilisateur utilisateurEmail = await utilisateurDAO.GetUtilisateurByMail(utilisateurDTO.Mail);
            Utilisateur utilisateurUsername = await utilisateurDAO.GetUtilisateurByMail(utilisateurDTO.Username);
            if((utilisateurEmail != null && utilisateurEmail.Mail.ToLower().Equals(utilisateur.Mail.ToLower())) 
            || (utilisateurUsername != null && utilisateurUsername.Username.ToLower().Equals(utilisateur.Username.ToLower())))
                return BadRequest();

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
        public async Task<ActionResult> SuppressionUtilisateur(int id) 
        {
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            await utilisateurDAO.SuppressionUtilisateur(utilisateur);
            return Ok();
        }

    }
}
