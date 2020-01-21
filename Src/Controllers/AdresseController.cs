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
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AdresseController : ControllerBase
    {
        private readonly AperoBoxApi_dbContext context;
        private readonly AdresseDAO adresseDAO;
        private readonly IMapper mapper;
        public AdresseController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.adresseDAO = new AdresseDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdresseDTO>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAdresses()
        {
            List<Adresse> adresses = await adresseDAO.GetAllAdresses();
            if (adresses == null)
                return NotFound();

            return Ok(mapper.Map<List<AdresseDTO>>(adresses));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdresseDTO>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Adresse>> GetAdresseById(int id)
        {
            Adresse adresse = await adresseDAO.GetAdresseById(id);
            if (adresse == null)
                return NotFound();

            return Ok(mapper.Map<AdresseDTO>(adresse));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(AdresseDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> AjouterAdresse([FromBody]AdresseDTO adresseDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Adresse adresse = mapper.Map<Adresse>(adresseDTO);
            adresse = await adresseDAO.AjouterAdresse(adresse);
            return Created($"api/Adresse/{adresse.Id}", mapper.Map<AdresseDTO>(adresse));
        }
    }
}
