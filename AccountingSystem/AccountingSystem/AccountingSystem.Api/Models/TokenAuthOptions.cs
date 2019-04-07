using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api
{
    /// <summary>
    /// TokenAuthOptions
    /// </summary>
    public class TokenAuthOptions
    {
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// SigningCredentials
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
