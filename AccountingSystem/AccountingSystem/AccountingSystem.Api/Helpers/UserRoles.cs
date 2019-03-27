using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Helpers
{
    /// <summary>
    /// Enum for user status
    /// </summary>
    public enum UserRoles
    {
        /// <summary>
        /// Default user
        /// </summary>
        Default = 0,
        /// <summary>
        /// Clerk user
        /// </summary>
        Clerk = 1,
        /// <summary>
        /// Accountant user
        /// </summary>
        Accountant = 2,
        /// <summary>
        /// Admin user
        /// </summary>
        Admin = 3
    }
}
