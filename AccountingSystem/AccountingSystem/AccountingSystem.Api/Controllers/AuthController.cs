using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Helpers;
using AccountingSystem.Api.Models;

namespace AccountingSystem.Api.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [AllowAnonymous]
    [Route("api/")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="vokaContext">Database context</param>
        public AuthController(IServiceProvider serviceProvider, AccountingSystemContext vokaContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = vokaContext;
        }

        /// <summary>
        /// Auth method
        /// </summary>
        /// <param name="user">User to authorize</param>
        /// <returns>User info with token</returns>
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpPost("auth")]
        public IActionResult GetToken([FromBody]AuthRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbUser = _dbContext.Users.SingleOrDefault(u => u.Login == user.Login);
            if (dbUser == null)
                return NotFound($"User <{user.Login}> not found!");

            // Maybe use hasher
            if (dbUser.Password != user.Pass)
                return BadRequest($"User and Password do not match");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenOptions = _serviceProvider.GetService(typeof(TokenAuthOptions));

            if (tokenOptions is TokenAuthOptions options)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Extentions.Key));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var identity = new ClaimsIdentity(new GenericIdentity(dbUser.Login),
                    new[]
                    {
                        new Claim("userId", dbUser.Id.ToString()),
                        new Claim("userLogin", dbUser.Login),
                        new Claim(ClaimTypes.Role, dbUser.RoleName)
                    });

                var token = tokenHandler.CreateJwtSecurityToken
                (
                    subject: identity,
                    issuer: options.Issuer,
                    audience: options.Audience,
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddDays(1)
                );

                var tokenResult = tokenHandler.WriteToken(token);
                return Ok(new AuthResponse
                {
                    Token = tokenResult,
                    Id = dbUser.Id.ToString(),
                    Login = dbUser.Login,
                    Status = dbUser.RoleName
                });
            }
            throw new Exception("TokenAuthOptions is null.");
        }

        /// <summary>
        /// Register method
        /// </summary>
        /// <param name="user">User to register</param>
        /// <returns>User info with token</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.Created)]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]AuthRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbUser = new User
            {
                Login = user.Login,
                Password = user.Pass,//Maybe use hasher
            };

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Users.Add(dbUser);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            var authResult = GetToken(new AuthRequest { Login = user.Login, Pass = user.Pass });
            return Created($"user/{dbUser.Id.ToString()}", ((Microsoft.AspNetCore.Mvc.OkObjectResult)authResult).Value);
        }
    }
}
