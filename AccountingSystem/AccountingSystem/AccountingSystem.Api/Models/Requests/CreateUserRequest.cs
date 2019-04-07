using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Create user request model
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Login
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Pass { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        [Required]
        public string Role { get; set; }

        /// <summary>
        /// User department id
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }
    }
}
