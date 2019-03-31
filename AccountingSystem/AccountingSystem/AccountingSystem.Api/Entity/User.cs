using AccountingSystem.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User : IEntityUpdateDate
    {
        /// <summary>
        /// User's identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User's login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// User role name
        /// </summary>
        public string RoleName
        {
            get
            {
                return ((UserRoles)Role).ToString();
            }
        }

        /// <summary>
        /// User department id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// User department
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// User create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// User last update date
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
    }
}
