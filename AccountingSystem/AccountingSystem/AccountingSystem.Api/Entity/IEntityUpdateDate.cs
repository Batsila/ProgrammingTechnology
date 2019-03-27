using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// Update date interface
    /// </summary>
    public interface IEntityUpdateDate
    {
        /// <summary>
        /// Create date
        /// </summary>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Last update date
        /// </summary>
        DateTime LastUpdateDate { get; set; }
    }
}
