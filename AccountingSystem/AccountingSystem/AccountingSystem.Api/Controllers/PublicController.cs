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
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace AccountingSystem.Api.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [AllowAnonymous]
    [Route("api/")]
    public class PublicController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="dbContext">Database context</param>
        public PublicController(IServiceProvider serviceProvider, AccountingSystemContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
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

            var dbUser = _dbContext
                .Users
                .Include(m => m.Department)
                .FirstOrDefault(u => u.Login == user.Login);

            if (dbUser == null)
                return NotFound($"User <{user.Login}> not found!");

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
                    Role = dbUser.RoleName,
                    Department = dbUser.Department?.DepartmentToWebDepartment()
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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]AuthRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbUser = new User
            {
                Login = user.Login,
                Password = user.Pass,
                DepartmentId = Const.DEFAULT_DEPARTMENT_ID
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

        /// <summary>
        /// Returns all roles available in system
        /// </summary>
        /// <returns>A list of roles</returns>
        [HttpGet("roles")]
        [ProducesResponseType(typeof(IEnumerable<WebEnumItem>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllRoles()
        {
            var roles = typeof(UserRoles).GetAllEnumFields();
            return Ok(roles);
        }

        /// <summary>
        /// Returns all payment types available in system
        /// </summary>
        /// <returns>A list of payment types</returns>
        [HttpGet("paymentTypes")]
        [ProducesResponseType(typeof(IEnumerable<WebEnumItem>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllPaymentTypes()
        {
            var paymentTypes = typeof(PaymentType).GetAllEnumFields();
            return Ok(paymentTypes);
        }

        /// <summary>
        /// Returns all salary types available in system
        /// </summary>
        /// <returns>A list of salary types</returns>
        [HttpGet("salaryTypes")]
        [ProducesResponseType(typeof(IEnumerable<WebEnumItem>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllSalaryTypes()
        {
            var salaryTypes = typeof(SalaryType).GetAllEnumFields();
            return Ok(salaryTypes);
        }

    }
}
