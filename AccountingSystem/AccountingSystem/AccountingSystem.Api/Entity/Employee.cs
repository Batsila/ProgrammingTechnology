using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee : IEntityUpdateDate
    {
        /// <summary>
        /// Identity number
        /// </summary>
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
        /// Salary info id
        /// </summary>
        public int SalaryInfoId { get; set; }

        /// <summary>
        /// Employee salary info
        /// </summary>
        public SalaryInfo SalaryInfo { get; set; }

        /// <summary>
        /// Employee department id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Employee department 
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Employee's TimeCards
        /// </summary>
        public ICollection<TimeCard> TimeCards { get; set; }

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
