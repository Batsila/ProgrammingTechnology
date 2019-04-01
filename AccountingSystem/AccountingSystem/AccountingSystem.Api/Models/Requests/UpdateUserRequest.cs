using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Update user request model
    /// </summary>
    public class UpdateUserRequest : CreateUserRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }
    }
}
