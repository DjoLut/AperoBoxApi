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
    public class AdresseController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private AdresseDAO adresseDAO;
        private readonly IMapper mapper;
        public AdresseController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.adresseDAO = new AdresseDAO(context);
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AdresseDTO))]
        public async Task<ActionResult> ajouterAdresse([FromBody]AdresseDTO adresseDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Adresse adresse = mapper.Map<Adresse>(adresseDTO);
            adresse = await adresseDAO.ajouterAdresse(adresse);
            return Created($"api/Adresse/{adresse.Id}", mapper.Map<AdresseDTO>(adresse));
        }
    }
}
