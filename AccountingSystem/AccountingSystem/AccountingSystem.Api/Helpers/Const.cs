using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Helpers
{
    /// <summary>
    /// Constants container class
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// Allowed to Clerk, Accountant and Admin
        /// </summary>
        public const string POLICY_CLERK = "Clerk";
        /// <summary>
        /// Allowed to Accountant and Admin
        /// </summary>
        public const string POLICY_ACCOUNTANT = "Accountant";
        /// <summary>
        /// Allowed to Admin
        /// </summary>
        public const string POLICY_ADMIN = "Admin";
        /// <summary>
        /// Default department id
        /// </summary>
        public const int DEFAULT_DEPARTMENT_ID = 1;
    }
}
