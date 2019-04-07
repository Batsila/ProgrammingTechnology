using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Update time card request model
    /// </summary>
    public class UpdateTimeCardRequest
    {
        /// <summary>
        /// TimeCard id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// TimeCard comments
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Total time (Logged)
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
