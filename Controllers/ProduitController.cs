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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProduitController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private ProduitDAO produitDAO;
        private readonly IMapper mapper;
        public ProduitController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.produitDAO = new ProduitDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Constants.Roles.Gestionnaire)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProduitDTO>))]
        public async Task<ActionResult<IEnumerable<Produit>>> getAllProduits()
        {
            List<Produit> produits = await produitDAO.getAllProduits();
            if (produits == null)
                return NotFound();

            return Ok(mapper.Map<List<ProduitDTO>>(produits));
        }

        [HttpGet("id")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ProduitDTO))]
        public async Task<ActionResult<Produit>> getProduitById(int id)
        {
            Produit produit = await produitDAO.getProduitById(id);
            if (produit == null)
                return NotFound();

            return Ok(mapper.Map<ProduitDTO>(produit));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(201, Type = typeof(ProduitDTO))]
        public async Task<ActionResult> ajouterProduit([FromBody]ProduitDTO produitDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Produit produit = mapper.Map<Produit>(produitDTO);
            produit = await produitDAO.ajouterProduit(produit);
            return Created($"api/Produit/{produit.Id}", mapper.Map<ProduitDTO>(produit));
        }
    }
}
