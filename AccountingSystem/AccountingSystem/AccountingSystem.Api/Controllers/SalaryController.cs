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
    [Route("api/salary")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="dbContext">Database context</param>
        public SalaryController(IServiceProvider serviceProvider, AccountingSystemContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create salary rate
        /// </summary>
        /// <param name="createSalaryInfoRequest">Salary to create</param>
        /// <returns>SalaryInfo web model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebSalaryInfo), (int)HttpStatusCode.OK)]
        [HttpPost]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult CreateSalary([FromBody]CreateSalaryInfoRequest createSalaryInfoRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse(createSalaryInfoRequest.Type, out SalaryType salaryType))
            {
                return BadRequest($"Salary type {createSalaryInfoRequest.Type} does not exist");
            }

            if (!Enum.TryParse(createSalaryInfoRequest.PaymentType, out PaymentType paymentType))
            {
                return BadRequest($"Salary type {createSalaryInfoRequest.Type} does not exist");
            }

            var dbSalaryInfo = new SalaryInfo
            {
               Type = (int)salaryType,
               PaymentType = (int)paymentType,
               Rate = createSalaryInfoRequest.Rate,
               Salary = createSalaryInfoRequest.Salary,
               BankAccount = createSalaryInfoRequest.BankAccount
            };

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Salaries.Add(dbSalaryInfo);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbSalaryInfo.SalaryInfoToWebSalaryInfo());
        }

        /// <summary>
        /// Update salary rate
        /// </summary>
        /// <param name="updateSalaryInfoRequest">Salary to update</param>
        /// <returns>SalaryInfo web model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebSalaryInfo), (int)HttpStatusCode.OK)]
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult UpdateSalary([FromBody]WebSalaryInfo updateSalaryInfoRequest)
        {
            var dbSalaryInfo = _dbContext.Salaries.FirstOrDefault(u => u.Id == updateSalaryInfoRequest.Id);

            if (dbSalaryInfo == null)
            {
                return BadRequest($"Salary {updateSalaryInfoRequest.Id} does not exist");
            }

            if (updateSalaryInfoRequest.Rate > 0)
            {
                dbSalaryInfo.Rate = updateSalaryInfoRequest.Rate;
            }

            if (updateSalaryInfoRequest.Salary > 0)
            {
                dbSalaryInfo.Salary = updateSalaryInfoRequest.Salary;
            }

            if (!string.IsNullOrEmpty(updateSalaryInfoRequest.BankAccount))
            {
                dbSalaryInfo.BankAccount = updateSalaryInfoRequest.BankAccount;
            }

            if (!string.IsNullOrEmpty(updateSalaryInfoRequest.Type))
            {
                if (!Enum.TryParse(updateSalaryInfoRequest.Type, out SalaryType salaryType))
                {
                    return BadRequest($"Salary type {updateSalaryInfoRequest.Type} does not exist");
                }
                dbSalaryInfo.Type = (int)salaryType;
            }

            if (!string.IsNullOrEmpty(updateSalaryInfoRequest.PaymentType))
            {
                if (!Enum.TryParse(updateSalaryInfoRequest.PaymentType, out PaymentType paymentType))
                {
                    return BadRequest($"Salary type {updateSalaryInfoRequest.PaymentType} does not exist");
                }
                dbSalaryInfo.Type = (int)paymentType;
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Salaries.Update(dbSalaryInfo);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbSalaryInfo.SalaryInfoToWebSalaryInfo());
        }

        /// <summary>
        /// Delete salary rate
        /// </summary>
        /// <param name="salaryId">Salary id to delete</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult DeleteSalary(int salaryId)
        {
            var dbSalaryInfo = _dbContext.Salaries.FirstOrDefault(u => u.Id == salaryId);

            if (dbSalaryInfo == null)
            {
                return BadRequest($"Salary {salaryId} does not exist");
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Salaries.Remove(dbSalaryInfo);
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
        /// Return all salaries
        /// </summary>
        /// <returns>List of salaries</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetSalarys()
        {
            var salaries = _dbContext.Salaries.Select(m => m.SalaryInfoToWebSalaryInfo());
            return Ok(salaries);
        }
    }
}