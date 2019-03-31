using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Helpers;
using AccountingSystem.Api.Models;
using AccountingSystem.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="vokaContext">Database context</param>
        public UserController(IServiceProvider serviceProvider, AccountingSystemContext vokaContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = vokaContext;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="createUserRequest">User to create</param>
        /// <returns>User info with token</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebUser), (int)HttpStatusCode.Created)]
        [HttpPost("create")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult CreaterUser([FromBody]CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse(createUserRequest.Role, out UserRoles userRole))
            {
                return BadRequest($"Role {createUserRequest.Role} does not exist");
            }

            var department = _dbContext.Departments.First(x => x.Id == createUserRequest.DepartmentId);
            if (userRole != UserRoles.Admin && department == null)
            {
                return BadRequest($"Dpartment {createUserRequest.DepartmentId} does not exist");
            }

            var dbUser = new User
            {
                Login = createUserRequest.Login,
                Password = createUserRequest.Pass,
                Role = (int)userRole,
                Department = department
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

            return Ok(dbUser.UserToWebUser());
        }
    }
}