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
    /// Department controller
    /// </summary>
    [Route("api/department/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="dbContext">Database context</param>
        public DepartmentController(IServiceProvider serviceProvider, AccountingSystemContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create department
        /// </summary>
        /// <param name="createDepartmentRequest">Department to create</param>
        /// <returns>Department info</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebDepartment), (int)HttpStatusCode.Created)]
        [HttpPost]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult CreateDepartment([FromBody]CreateDepartmentRequest createDepartmentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_dbContext.Departments.Any(u => u.Name == createDepartmentRequest.Name))
            {
                return BadRequest($"Name {createDepartmentRequest.Name} is not unique");
            }

            if (string.IsNullOrEmpty(createDepartmentRequest.Name))
            {
                return BadRequest($"Department name is empty");
            }

            var dbDepartment = new Department
            {
                Name = createDepartmentRequest.Name
            };

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Departments.Add(dbDepartment);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbDepartment.DepartmentToWebDepartment());
        }

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="updateDepartmentRequest">Department to update</param>
        /// <returns>Department info</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebDepartment), (int)HttpStatusCode.OK)]
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult UpdateDepartment([FromBody]WebDepartment updateDepartmentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbDepartment = _dbContext.Departments.FirstOrDefault(u => u.Id == updateDepartmentRequest.Id);

            if (dbDepartment == null)
            {
                return BadRequest($"Department {updateDepartmentRequest.Id} does not exist");
            }

            if (_dbContext.Departments.Any(u => u.Name == updateDepartmentRequest.Name))
            {
                return BadRequest($"Name {updateDepartmentRequest.Name} is not unique");
            }

            if (!string.IsNullOrEmpty(updateDepartmentRequest.Name))
            {
                dbDepartment.Name = updateDepartmentRequest.Name;
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Departments.Update(dbDepartment);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbDepartment.DepartmentToWebDepartment());
        }

        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="id">Department id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult DeleteDepartment(int id)
        {
            var dbDepartment = _dbContext.Departments.FirstOrDefault(u => u.Id == id);

            if (dbDepartment == null)
            {
                return BadRequest($"Department {id} does not exist");
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Departments.Remove(dbDepartment);
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
        /// Get department
        /// </summary>
        /// <param name="id">Department id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(WebDepartment), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetDepartment(int id)
        {
            var dbDepartment = _dbContext.Departments.FirstOrDefault(u => u.Id == id);

            if (dbDepartment == null)
            {
                return NotFound($"Department <{id}> not found!");
            }

            return Ok(dbDepartment.DepartmentToWebDepartment());
        }


        /// <summary>
        /// Get all departments
        /// </summary>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<WebDepartment>), (int)HttpStatusCode.OK)]
        [HttpGet("all")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetAllDepartments()
        {
            var departments = _dbContext.Departments.Select(u => u.DepartmentToWebDepartment());
            return Ok(departments);
        }
    }
}
