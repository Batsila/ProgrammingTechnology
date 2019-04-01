using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Authenticated user
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// User's security token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// User's identificational number
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User status
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User department
        /// </summary>
        public WebDepartment Department { get; set; }
    }
}
