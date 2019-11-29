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
        public async Task<ActionResult<IEnumerable<Utilisateur>>> getUtilisateurs()
        {
            List<Utilisateur> utilisateurs = await utilisateurDAO.getUtilisateurs();
            if (utilisateurs == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult<Utilisateur>> getUtilisateurById(int id)
        {
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();
            
            return Ok(mapper.Map<UtilisateurDTO>(utilisateur));
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public void Post([FromBody]Utilisateur utilisateur)
        {
            
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UtilisateurDTO))]
        public async Task<ActionResult> modifUtilisateur([FromBody] UtilisateurDTO utilisateurDTO)
        {
            int id = Decimal.ToInt32(utilisateurDTO.Id);
            Utilisateur utilisateur = await utilisateurDAO.GetUtilisateurById(id);
            if(utilisateur == null)
                return NotFound();

            await utilisateurDAO.modifUtilisateur(utilisateur, utilisateurDTO);

            return Ok(utilisateur);
        }

        [HttpDelete("{id}")]
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
