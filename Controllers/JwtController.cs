using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AperoBoxApi.Models;
using AperoBoxApi.Context;
using AperoBoxApi.DTO;
using AperoBoxApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JwtController : ControllerBase
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly AperoBoxApi_dbContext context;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, AperoBoxApi_dbContext context)
        {
            this._jwtOptions = jwtOptions.Value;
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(JwtToken),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO loginModelDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var repository = new AuthenticationRepository(this.context);
            //Utilisateur userFound = repository.GetUtilisateurs().FirstOrDefault(u => u.Username == loginModelDTO.Username && u.MotDePasse == loginModelDTO.Password);
            Utilisateur userFound = repository.GetUtilisateurs().FirstOrDefault(u => u.Username == loginModelDTO.Username && Bcrypt.Verify(loginModelDTO.Password, u.MotDePasse));
            if(userFound == null)
                return Unauthorized();

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(PrivateClaims.UserId, userFound.Id.ToString())
            };

            //ADD role
            if(userFound.UtilisateurRole != null)
            {
                userFound.UtilisateurRole.ToList().ForEach(u => claims.Add(new Claim("roles", u.IdRoleNavigation.Nom)));
            }

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new{
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
            };

            return Ok(response);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

}