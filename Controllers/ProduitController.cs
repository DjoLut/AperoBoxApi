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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProduitDTO>))]
        public async Task<ActionResult<IEnumerable<Produit>>> getProduits()
        {
            List<Produit> produits = await produitDAO.getProduits();
            if (produits == null)
                return NotFound();

            return Ok(mapper.Map<List<ProduitDTO>>(produits));
        }

        [HttpPost]
        public void Post()
        {
            
        }
    }
}
