using AccountingSystem.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Includes info about employee
    /// </summary>
    public class WebEmployee
    {
        /// <summary>
        /// Employee's identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee's second name
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Employee's address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Information about employee tariff rate or salary type
        /// </summary>
        public WebSalaryInfo SalaryInfo { get; set; }

        /// <summary>
        /// Information about employee department
        /// </summary>
        public WebDepartment Department { get; set; }

    }
}
