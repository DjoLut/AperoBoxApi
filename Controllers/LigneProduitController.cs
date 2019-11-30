using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DTO;
using AperoBoxApi.DAO;
using AutoMapper;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LigneProduitController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private LigneProduitDAO ligneProduitDAO;
        private readonly IMapper mapper;
        public LigneProduitController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.ligneProduitDAO = new LigneProduitDAO(context);
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LigneProduitDTO))]
        public async Task<ActionResult> ajouterLigneProduit([FromBody]LigneProduitDTO ligneProduitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            LigneProduit ligneProduit = mapper.Map<LigneProduit>(ligneProduitDTO);
            ligneProduit = await ligneProduitDAO.ajouterLigneProduit(ligneProduit);
            return Created($"api/LigneProduit/{ligneProduit.Id}", mapper.Map<LigneCommandeDTO>(ligneProduit));
        }
    }
}
