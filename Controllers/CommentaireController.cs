using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DAO;
using AperoBoxApi.DTO;
using AutoMapper;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentaireController : ControllerBase
    {
        private AperoBoxApi_dbContext context;
        private CommentaireDAO commentaireDAO;
        private readonly IMapper mapper;
        public CommentaireController(AperoBoxApi_dbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.commentaireDAO = new CommentaireDAO(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentaireDTO>))]
        public async Task<ActionResult<IEnumerable<Commentaire>>> getCommentaires()
        {
            List<Commentaire> commentaires = await commentaireDAO.getCommentaires();
            if (commentaires == null)
                return NotFound();

            return Ok(mapper.Map<List<CommentaireDTO>>(commentaires));
        }

        [HttpPost]
        public void Post()
        {
            
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(CommentaireDTO))]
        public async Task<ActionResult> suppressionCommentaire(int id) 
        {
            Commentaire commentaire = await commentaireDAO.getCommentaireById(id);
            if(commentaire == null)
                return NotFound();
            
            await commentaireDAO.suppressionCommentaire(commentaire);
            return Ok();
        }
    }
}
