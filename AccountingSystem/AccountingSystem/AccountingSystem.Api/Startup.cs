using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using AccountingSystem.Api.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AccountingSystem.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AccountingSystem.Api.Managers;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using AccountingSystem.Api.Managers.Exceptions;

namespace AccountingSystem.Api
{
    /// <summary>
    /// Application sturtup class
    /// </summary>
    public class Startup
    {
        private const string TOKEN_ISSUER = "AccountingSystem.Issuer";
        private const string TOKEN_AUDIENCE = "AccountingSystem.Audience";

        private IConfiguration _configuration;
        private TokenAuthOptions _tokenOptions;

        /// <summary>
        /// Default constructor method
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Application services configuration method
        /// </summary>
        /// <param name="services">Collection of services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var globalConnectionString = _configuration.GetConnectionString("GlobalConnection");

            services.AddDbContext<AccountingSystemContext>(x => x.UseSqlServer(globalConnectionString));
            services.AddTransient<EmployeeManager>();

            var pathToDoc = _configuration["Swagger:FileName"];
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = "Accounting System API",
                        Version = "v1",
                        Description = "A simple api for Accounting System",
                        TermsOfService = "None"
                    }
                 );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
            });

            var key = Encoding.UTF8.GetBytes(Extentions.Key);

            _tokenOptions = new TokenAuthOptions
            {
                Audience = TOKEN_AUDIENCE,
                Issuer = TOKEN_ISSUER,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.RsaSha256Signature)
            };

            services.AddSingleton<TokenAuthOptions>(_tokenOptions);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                options.AddPolicy(Const.POLICY_USER,
                    policy => policy.RequireRole(nameof(UserRoles.Clerk), nameof(UserRoles.Accountant), nameof(UserRoles.Admin)));
                options.AddPolicy(Const.POLICY_ACCOUNTING_OFFICER,
                    policy => policy.RequireRole(nameof(UserRoles.Clerk), nameof(UserRoles.Accountant)));
                options.AddPolicy(Const.POLICY_ACCOUNTANT,
                    policy => policy.RequireRole(nameof(UserRoles.Accountant)));
                options.AddPolicy(Const.POLICY_ADMIN,
                    policy => policy.RequireRole(nameof(UserRoles.Admin)));

            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidAudience = _tokenOptions.Audience,
                        ValidIssuer = _tokenOptions.Issuer,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );

        }

        /// <summary>
        /// Application configuration method
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerFeature>().Error;
                    var logger = context.RequestServices.GetService<ILoggerFactory>().CreateLogger("ExceptionHandler");

                    logger.LogInformation("Start processing");

                    context.Response.ContentType = "text/html";
                    var message = String.Empty;

                    switch (error)
                    {
                        case SecurityTokenInvalidSignatureException _:
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            message = "Invalid token";
                            break;

                        case SecurityTokenExpiredException _:
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            message = "Expired token";
                            break;

                        case IncorrectDataException _:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            message = error.Message;
                            break;

                        case NotFoundException _:
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            message = error.Message;
                            break;

                        default:
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            logger.LogError(error, "Unhandled exception");
                            message = error.Message;
                            break;
                    }

                    await context.Response.WriteAsync(message);
                });
            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
    }
}
