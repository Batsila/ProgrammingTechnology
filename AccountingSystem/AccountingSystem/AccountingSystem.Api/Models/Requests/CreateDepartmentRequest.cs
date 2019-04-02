using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Create department request model
    /// </summary>
    public class CreateDepartmentRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
