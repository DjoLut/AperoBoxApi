using System;
using System.Collections.Generic;
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
    public class UtilisateurRoleController : ControllerBase
    {
        private readonly AperoBoxApi_dbContext context;
        private readonly UtilisateurRoleDAO utilisateurRoleDAO;
        private readonly IMapper mapper;
        public UtilisateurRoleController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.utilisateurRoleDAO = new UtilisateurRoleDAO(context);
            this.mapper = mapper;
        }

        [HttpGet("{idUser}")]
        [Authorize(Roles = Constants.Roles.Admin)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<UtilisateurRoleDTO>))]
        public async Task<ActionResult<IEnumerable<UtilisateurRole>>> GetUtilisateurRolesByUserId(int idUser)
        {
            List<UtilisateurRole> utilisateurRoles = await utilisateurRoleDAO.GetUtilisateurRoleByUserId(idUser);
            if (utilisateurRoles == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurRoleDTO>>(utilisateurRoles));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(201, Type = typeof(UtilisateurRoleDTO))]
        public async Task<ActionResult> AjouterUtilisateurRole([FromBody]UtilisateurRoleDTO utilisateurRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UtilisateurRole utilisateurRole = mapper.Map<UtilisateurRole>(utilisateurRoleDTO);
            utilisateurRole = await utilisateurRoleDAO.AjouterUtilisateurRole(utilisateurRole);
            return Created($"api/UtilisateurRole", mapper.Map<UtilisateurRoleDTO>(utilisateurRole));
        }

        [HttpDelete("{idRole}/{userId}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(UtilisateurRoleDTO))]
        public async Task<ActionResult> SuppressionUtilisateurRole(string idRole, int userId)
        {
            UtilisateurRole utilisateurRole = await utilisateurRoleDAO.GetUtilisateurRoleByIdRoleAndUserId(idRole, userId);
            if (utilisateurRole == null)
                return NotFound();

            await utilisateurRoleDAO.SuppressionUtilisateurRole(utilisateurRole);
            return Ok();
        }

    }
}