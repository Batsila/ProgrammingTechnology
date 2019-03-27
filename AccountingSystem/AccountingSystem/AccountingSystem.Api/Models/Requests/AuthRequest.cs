using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// User authentication request
    /// </summary>
    public class AuthRequest
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
    }
}
