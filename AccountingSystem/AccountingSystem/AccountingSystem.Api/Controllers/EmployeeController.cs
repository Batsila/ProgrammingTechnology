using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccountingSystem.API.Helpers;
using AccountingSystem.API.Managers;
using AccountingSystem.API.Models;
using AccountingSystem.API.Entity;

namespace AccountingSystem.API.Controllers
{
    /// <summary>
    /// Models controller
    /// </summary>
    [Route("api/employee/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManager _EmployeeManager;

        /// <summary>
        /// Constructor with injected parameters
        /// </summary>
        /// <param name="employeeManager"></param>
        public EmployeeController(EmployeeManager employeeManager)
        {
            _EmployeeManager = employeeManager;
        }

        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns>All employees</returns>
        [HttpGet("all")]
        [Authorize(Policy = Const.POLICY_CLERK)]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var models = _EmployeeManager.GetAllEmployees();
            return Ok(models);
        }

        /// <summary>
        /// Returns employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = Const.POLICY_CLERK)]
        public ActionResult<IEnumerable<Employee>> GetEmployee(int id)
        {
            var employee = _EmployeeManager.GetEmployee(id);
            return Ok(employee);
        }

        /// <summary>
        /// Creates new employee
        /// </summary>
        /// <param name="createEmployeeRequest"></param>
        /// <returns>Employee</returns>
        [HttpPost]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public ActionResult<IEnumerable<Employee>> CreateEmployee([FromBody]Employee createEmployeeRequest)
        {
            var employee = _EmployeeManager.CreateEmployee(createEmployeeRequest);
            return Ok(employee);
        }

        /// <summary>
        /// Updates exist employee
        /// </summary>
        /// <param name="updateEmployeeRequest">Employee to update</param>
        /// <returns>Employee</returns>
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ACCOUNTANT)]
        public ActionResult<IEnumerable<Employee>> UpdateEmployee([FromBody]Employee updateEmployeeRequest)
        {
            var employee = _EmployeeManager.UpdateEmployee(updateEmployeeRequest);
            return Ok(employee);
        }

        /// <summary>
        /// Deletes exist employee
        /// </summary>
        /// <param name="id">Employee id</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            _EmployeeManager.DeleteEmployee(id);
            return Ok();
        }

    }
}
