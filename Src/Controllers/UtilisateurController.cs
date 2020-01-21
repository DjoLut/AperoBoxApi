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
        //[ProducesResponseType(200, Type = typeof(IEnumerable<UtilisateurDTO>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PagingResult<UtilisateurDTO>>))]
        //public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllUtilisateurs()
        public async Task<ActionResult<IEnumerable<PagingResult<Utilisateur>>>> GetAllUtilisateurs(int? pageIndex = 0, int? pageSize = 5)
        {
            List<Utilisateur> utilisateurs = await utilisateurDAO.GetAllUtilisateurs(pageIndex, pageSize);
            if (utilisateurs == null)
                return NotFound();

            //PAGING
            int countUtilisateur = await utilisateurDAO.GetCountUtilisateur();
            PagingResult<UtilisateurDTO> resultPage = new PagingResult<UtilisateurDTO>()
            {
                Items = mapper.Map<List<UtilisateurDTO>>(utilisateurs),
                PageIndex = pageIndex.Value,
                PageSize = pageSize.Value,
                TotalCount = countUtilisateur
            };
            return Ok(resultPage);

            //return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));
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
            Utilisateur utilisateurUsername = await utilisateurDAO.GetUtilisateurByUsername(utilisateurDTO.Username);
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
            if((utilisateurEmail != null && !utilisateurEmail.Mail.ToLower().Equals(utilisateur.Mail.ToLower())))
                return BadRequest();
            Utilisateur utilisateurUsername = await utilisateurDAO.GetUtilisateurByUsername(utilisateurDTO.Username);
            if(utilisateurUsername != null && !utilisateurUsername.Username.ToLower().Equals(utilisateur.Username.ToLower()))
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
