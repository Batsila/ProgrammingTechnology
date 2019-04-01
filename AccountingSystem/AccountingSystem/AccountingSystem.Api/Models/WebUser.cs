using AccountingSystem.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Create user response model
    /// </summary>
    public class WebUser
    {
        /// <summary>
        /// User's identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// User last update date
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// User deparment
        /// </summary>
        public WebDepartment Department { get; set; }
    }
}
