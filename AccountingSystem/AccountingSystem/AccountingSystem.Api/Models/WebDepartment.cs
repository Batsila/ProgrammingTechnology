using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Department description
    /// </summary>
    public class WebDepartment
    {
        /// <summary>
        /// Department's identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Department's name
        /// </summary>
        public string Name { get; set; }
    }
}
