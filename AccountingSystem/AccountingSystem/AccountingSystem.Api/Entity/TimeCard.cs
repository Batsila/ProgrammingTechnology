using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// TimeCard entity
    /// </summary>
    public class TimeCard : IEntityUpdateDate
    {
        /// <summary>
        /// TimeCard id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comment with description of logged work
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Logged time
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Employee id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Last update date 
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
    }
}
