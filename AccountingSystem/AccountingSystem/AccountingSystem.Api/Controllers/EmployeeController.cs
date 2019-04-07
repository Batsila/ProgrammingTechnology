using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Api.Helpers;
using AccountingSystem.Api.Managers;
using AccountingSystem.Api.Models;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Models.Requests;
using System.Net;

namespace AccountingSystem.Api.Controllers
{
    /// <summary>
    /// Employees controller
    /// </summary>
    [Route("api/employee/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManager _employeeManager;

        /// <summary>
        /// Constructor with injected parameters
        /// </summary>
        /// <param name="employeeManager"></param>
        public EmployeeController(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        /// <summary>
        /// Creates new employee
        /// </summary>
        /// <param name="createEmployeeRequest"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebEmployee), (int)HttpStatusCode.OK)]
        [HttpPost]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult CreateEmployee([FromBody]CreateEmployeeRequest createEmployeeRequest)
        {
            var employee = _employeeManager.CreateEmployee(createEmployeeRequest);
            return Ok(employee);
        }

        /// <summary>
        /// Update exist employee
        /// </summary>
        /// <param name="updateEmployeeRequest">Employee to update</param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebEmployee), (int)HttpStatusCode.OK)]
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ACCOUNTANT)]
        public IActionResult UpdateEmployee([FromBody]UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = _employeeManager.UpdateEmployee(updateEmployeeRequest);
            return Ok(employee);
        }

        /// <summary>
        /// Deletes exist employee
        /// </summary>
        /// <param name="id">Employee id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeManager.DeleteEmployee(id);
            return Ok();
        }

        /// <summary>
        /// Return employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebEmployee), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        [Authorize(Policy = Const.POLICY_USER)]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeManager.GetEmployee(id);
            return Ok(employee);
        }

        /// <summary>
        /// Return all employees
        /// </summary>
        /// <returns>All employees</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<WebEmployee>), (int)HttpStatusCode.OK)]
        [HttpGet("all")]
        [Authorize(Policy = Const.POLICY_USER)]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeManager.GetAllEmployees();
            return Ok(employees);
        }

    }
}
