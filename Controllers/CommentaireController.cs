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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AperoBoxApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(Roles = Constants.Roles.Admin)] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentaireDTO>))]
        public async Task<ActionResult<IEnumerable<Commentaire>>> getAllCommentaires()
        {
            List<Commentaire> commentaires = await commentaireDAO.getAllCommentaires();
            if (commentaires == null)
                return NotFound();

            return Ok(mapper.Map<List<CommentaireDTO>>(commentaires));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentaireDTO>))]
        public async Task<ActionResult<Commentaire>> getCommentaireById(int id)
        {
            Commentaire commentaire = await commentaireDAO.getCommentaireById(id);
            if (commentaire == null)
                return NotFound();

            return Ok(mapper.Map<CommentaireDTO>(commentaire));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Utilisateur)]
        [ProducesResponseType(201, Type = typeof(CommentaireDTO))]
        public async Task<ActionResult> ajouterCommentaire([FromBody]CommentaireDTO commentaireDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Commentaire commentaire = mapper.Map<Commentaire>(commentaireDTO);
            commentaire = await commentaireDAO.ajouterCommentaire(commentaire);
            return Created($"api/Commentaire/{commentaire.Id}", mapper.Map<CommentaireDTO>(commentaire));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
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
