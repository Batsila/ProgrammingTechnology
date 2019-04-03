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
using Microsoft.EntityFrameworkCore;

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
        /// <param name="dbContext">Database context</param>
        public UserController(IServiceProvider serviceProvider, AccountingSystemContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="createUserRequest">User to create</param>
        /// <returns>User model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebUser), (int)HttpStatusCode.OK)]
        [HttpPost]
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

            if (_dbContext.Users.Any(u => u.Login == createUserRequest.Login))
            {
                return BadRequest($"Login {createUserRequest.Login} is not unique");
            }

            if (string.IsNullOrWhiteSpace(createUserRequest.Login))
            {
                return BadRequest($"User login is empty");
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

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="updateUserRequest">User to update</param>
        /// <returns>User model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebUser), (int)HttpStatusCode.OK)]
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult UpdateUser([FromBody]UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Guid.TryParse(updateUserRequest.Id, out Guid userId))
            {
                return BadRequest($"Id {updateUserRequest.Id} is invalid");
            }

            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (dbUser == null)
            {
                return BadRequest($"User {updateUserRequest.Id} does not exist");
            }

            if (!string.IsNullOrEmpty(updateUserRequest.Role))
            {
                if (!Enum.TryParse(updateUserRequest.Role, out UserRoles userRole))
                {
                    return BadRequest($"Role {updateUserRequest.Role} does not exist");
                }
                else
                {
                    dbUser.Role = (int)userRole;
                }
            }

            if (updateUserRequest.DepartmentId > 0)
            {
                var department = _dbContext.Departments.First(x => x.Id == updateUserRequest.DepartmentId);
                if (department == null)
                {
                    return BadRequest($"Dpartment {updateUserRequest.DepartmentId} does not exist");
                }
            }

            if (!string.IsNullOrEmpty(updateUserRequest.Login))
            {
                if (_dbContext.Users.Any(u => u.Login == updateUserRequest.Login))
                {
                    return BadRequest($"Login {updateUserRequest.Login} is not unique");
                }
                dbUser.Login = updateUserRequest.Login;
            }

            if (!string.IsNullOrEmpty(updateUserRequest.Pass))
            {
                dbUser.Password = updateUserRequest.Pass;
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Users.Update(dbUser);
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

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult DeleteUser(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                return BadRequest($"Id {id} is invalid");
            }

            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (dbUser == null)
            {
                return BadRequest($"User {id} does not exist");
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Users.Remove(dbUser);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id">User id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebUser), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetUser(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                return BadRequest($"Id {id} is invalid");
            }

            var dbUser = _dbContext.Users.Include(u => u.Department).FirstOrDefault(u => u.Id == userId);

            if (dbUser == null)
            {
                return BadRequest($"User {id} does not exist");
            }

            return Ok(dbUser.UserToWebUser());
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<WebUser>), (int)HttpStatusCode.OK)]
        [HttpGet("all")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.Include(u => u.Department).Select(u => u.UserToWebUser());
            return Ok(users);
        }
    }
}