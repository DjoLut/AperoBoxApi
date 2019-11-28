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
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            //Afficher des utilisateurs
            List<Utilisateur> utilisateurs = await utilisateurDAO.getUtilisateurs();
            if (utilisateurs == null)
                return NotFound();

            return Ok(mapper.Map<List<UtilisateurDTO>>(utilisateurs));

        }


        [HttpPost]
        public void Post([FromBody]Utilisateur utilisateur)
        {
            
        }
    }
}
