using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Update user request model
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [MaxLength(50)]
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User department id
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
