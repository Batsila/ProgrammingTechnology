using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Update employee request model
    /// </summary>
    public class UpdateEmployeeRequest
    {
        /// <summary>
        /// Employee id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Employee first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee second name
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Employee address
        /// </summary>
        
        public string Address { get; set; }

        /// <summary>
        /// Salary id
        /// </summary>
        public int SalaryId { get; set; }

        /// <summary>
        /// Department id
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
