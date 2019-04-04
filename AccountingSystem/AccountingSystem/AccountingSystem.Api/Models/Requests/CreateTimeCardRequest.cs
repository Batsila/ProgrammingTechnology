using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Create time card request model
    /// </summary>
    public class CreateTimeCardRequest
    {
        /// <summary>
        /// TimeCard comments
        /// </summary>
        [Required]
        public string Comment { get; set; }

        /// <summary>
        /// Total time (Logged)
        /// </summary>
        [Required]
        [Range(1, 24, ErrorMessage = "Value must be between 1 and 24")]
        public double Time { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }
    }
}
