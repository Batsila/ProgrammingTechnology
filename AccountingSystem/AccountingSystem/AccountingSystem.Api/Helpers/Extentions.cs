using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.API.Entity;
using AccountingSystem.API.Models;

namespace AccountingSystem.API.Helpers
{
    /// <summary>
    /// Extentions
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// RSA key
        /// </summary>
        public const string Key = "183419391965b09eab3c013d4ca54922bb802bec8fd5318192b0a" +
                                                     "75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731";

        /// <summary>
        /// Extention for db Migration
        /// </summary>
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<AccountingSystemContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }

    }
}
