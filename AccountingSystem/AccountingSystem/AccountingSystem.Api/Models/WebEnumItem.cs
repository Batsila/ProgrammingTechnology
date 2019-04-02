using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Role model
    /// </summary>
    public class WebEnumItem
    {
        /// <summary>
        /// Number of role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
    }
}
