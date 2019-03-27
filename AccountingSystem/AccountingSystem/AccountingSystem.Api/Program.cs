using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AccountingSystem.Api.Helpers;

namespace AccountingSystem.Api
{
    /// <summary>
    /// Main class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main(string[] args)
        {
            BuildWebHost(args).MigrateDatabase().Run();
        }

        /// <summary>
        /// Build web host
        /// </summary>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .UseUrls("http://*:57286;http://localhost:57286")
                .UseStartup<Startup>()
                .UseIISIntegration()
                .Build();
        }
    }
}
