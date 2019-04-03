using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Create employee request model 
    /// </summary>
    public class CreateEmployeeRequest
    {
        /// <summary>
        /// Employee first name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Employee second name
        /// </summary>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Employee address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Salary id
        /// </summary>
        [Required]
        public int SalaryId { get; set; }

        /// <summary>
        /// Department id
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }
    }
}
