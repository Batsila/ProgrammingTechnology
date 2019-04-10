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
    /// TimeCard controller
    /// </summary>
    [Route("api/timeCard")]
    [ApiController]
    public class TimeCardController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="dbContext">Database context</param>
        public TimeCardController(IServiceProvider serviceProvider, AccountingSystemContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create timeCard
        /// </summary>
        /// <param name="createTimeCardRequest">TimeCard to create</param>
        /// <returns>TimeCard model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebTimeCard), (int)HttpStatusCode.OK)]
        [HttpPost]
        [Authorize(Policy = Const.POLICY_ACCOUNTING_OFFICER)]
        public IActionResult CreaterTimeCard([FromBody]CreateTimeCardRequest createTimeCardRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbEmployee = _dbContext.Employees.First(e => e.Id == createTimeCardRequest.EmployeeId);

            if (dbEmployee == null)
            {
                return BadRequest($"Employee {createTimeCardRequest.EmployeeId} does not exist");
            }

            var dbTimeCard = new TimeCard
            {
                Comment = createTimeCardRequest.Comment,
                Time = createTimeCardRequest.Time,
                EmployeeId = createTimeCardRequest.EmployeeId,
                Employee = dbEmployee
            };
            

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.TimeCards.Add(dbTimeCard);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbTimeCard.TimeCardToWebTimeCard());
        }

        /// <summary>
        /// Update timeCard
        /// </summary>
        /// <param name="updateTimeCardRequest">TimeCard to update</param>
        /// <returns>TimeCard model</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebTimeCard), (int)HttpStatusCode.OK)]
        [HttpPatch]
        [Authorize(Policy = Const.POLICY_ACCOUNTING_OFFICER)]
        public IActionResult UpdateTimeCard([FromBody]UpdateTimeCardRequest updateTimeCardRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbTimeCard = _dbContext.TimeCards.First(c => c.Id == updateTimeCardRequest.Id);

            if (dbTimeCard == null)
            {
                return BadRequest($"TimeCard {updateTimeCardRequest.Id} does not exist");
            }

            if (updateTimeCardRequest.EmployeeId > 0)
            {
                var dbEmployee = _dbContext.Employees.First(e => e.Id == updateTimeCardRequest.EmployeeId);

                if (dbEmployee == null)
                {
                    return BadRequest($"Employee {updateTimeCardRequest.EmployeeId} does not exist");
                }

                dbTimeCard.Employee = dbEmployee;
                dbTimeCard.EmployeeId = updateTimeCardRequest.EmployeeId;
            }

            if (updateTimeCardRequest.Time > 0)
            {
                if (updateTimeCardRequest.Time > 24)
                {
                    return BadRequest("Only 24 hours a day");
                }

                dbTimeCard.Time = updateTimeCardRequest.Time;
            }

            if (!string.IsNullOrEmpty(updateTimeCardRequest.Comment))
            {
                dbTimeCard.Comment = updateTimeCardRequest.Comment;
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.TimeCards.Update(dbTimeCard);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return Ok(dbTimeCard.TimeCardToWebTimeCard());
        }

        /// <summary>
        /// Delete timeCard
        /// </summary>
        /// <param name="id">TimeCard id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [Authorize(Policy = Const.POLICY_ACCOUNTING_OFFICER)]
        public IActionResult DeleteTimeCard(int id)
        {
            var dbTimeCard = _dbContext.TimeCards.First(c => c.Id == id);

            if (dbTimeCard == null)
            {
                return BadRequest($"TimeCard {id} does not exist");
            }

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.TimeCards.Remove(dbTimeCard);
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
        /// Get timecard by id
        /// </summary>
        /// <param name="id">TimeCard id</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WebTimeCard), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetTimeCardById(int id)
        {
            var dbTimeCard = _dbContext.TimeCards.FirstOrDefault(u => u.Id == id);

            if (dbTimeCard == null)
            {
                return BadRequest($"TimeCard {id} does not exist");
            }

            return Ok(dbTimeCard.TimeCardToWebTimeCard());
        }

        /// <summary>
        /// Get employee timecards
        /// </summary>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<WebTimeCard>), (int)HttpStatusCode.OK)]
        [HttpGet("all")]
        [Authorize(Policy = Const.POLICY_ADMIN)]
        public IActionResult GetTimeCard(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var timeCards = _dbContext.TimeCards.Where(u => u.EmployeeId == employeeId).Select(u => u.TimeCardToWebTimeCard());
            if (fromDate != default(DateTime))
            {
                if (toDate != default(DateTime))
                {
                    timeCards = _dbContext.TimeCards.Where(u => u.EmployeeId == employeeId).Where(u => u.CreateDate >= fromDate).Where(u => u.CreateDate <= toDate).Select(u => u.TimeCardToWebTimeCard());
                }
                else
                {
                    timeCards = _dbContext.TimeCards.Where(u => u.EmployeeId == employeeId).Where(u => u.CreateDate >= fromDate).Select(u => u.TimeCardToWebTimeCard());
                }
            }

            if (timeCards == null)
            {
                return BadRequest($"TimeCards {employeeId} does not exist");
            }
            return Ok(timeCards);
        }
    }
}