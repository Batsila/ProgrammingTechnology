using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// Entity for department
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Department id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Department name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employees of this department
        /// </summary>
        public ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Users of this department
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
