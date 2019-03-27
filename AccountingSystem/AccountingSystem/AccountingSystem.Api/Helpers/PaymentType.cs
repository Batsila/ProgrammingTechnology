using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Helpers
{
    /// <summary>
    /// Enum for paymentType
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Salary in two parts
        /// </summary>
        Advance = 0,
        /// <summary>
        /// Salary in payday
        /// </summary>
        Payday = 1
    }
}
