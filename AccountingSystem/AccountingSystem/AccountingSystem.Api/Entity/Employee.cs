using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.API.Entity
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
        /// Create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Last update date
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
    }
}
