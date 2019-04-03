using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Web TimeCard
    /// </summary>
    public class WebTimeCard
    {
        /// <summary>
        /// TimeCard identifier
        /// </summary>
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

        /// <summary>
        /// TimeCard creation time
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
