using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Models;

namespace AccountingSystem.Api.Helpers
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
        
        /// <summary>
        /// Coverts User to WebUser
        /// </summary>
        public static WebUser UserToWebUser(this User user)
        {
            var webUser = new WebUser
            {
                Login = user.Login,
                Id = user.Id.ToString(),
                Role = user.RoleName,
                CreateDate = user.CreateDate,
                LastUpdateDate = user.LastUpdateDate,
                Department = user.Department?.DepartmentToWebDepartment()
            };
            return webUser;
        }

        /// <summary>
        /// Coverts Department to WebDepartment
        /// </summary>
        public static WebDepartment DepartmentToWebDepartment(this Department department)
        {
            var webDepartment = new WebDepartment
            {
                Id = department.Id,
                Name = department.Name
            };
            return webDepartment;
        }

        /// <summary>
        /// Coverts SalaryInfo to WebSalaryInfo
        /// </summary>
        public static WebSalaryInfo SalaryInfoToWebSalaryInfo(this SalaryInfo salaryInfo)
        {
            var webSalaryInfo = new WebSalaryInfo
            {
                Id = salaryInfo.Id,
                Salary = salaryInfo.Salary,
                BankAccount = salaryInfo.BankAccount,
                PaymentType = salaryInfo.PaymentTypeName,
                Rate = salaryInfo.Rate,
                Type = salaryInfo.TypeName
            };
            return webSalaryInfo;
        }

        /// <summary>
        /// Coverts Employee to WebEmployee
        /// </summary>
        public static WebEmployee EmployeeToWebEmployee(this Employee employee)
        {
            var webEmployee = new WebEmployee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Address = employee.Address,
                Department = employee?.Department.DepartmentToWebDepartment(),
                SalaryInfo = employee?.SalaryInfo.SalaryInfoToWebSalaryInfo()
            };
            return webEmployee;
        }

        /// <summary>
        /// Coverts TimeCard to WebTimeCard
        /// </summary>
        public static WebTimeCard TimeCardToWebTimeCard(this TimeCard timeCard)
        {
            var webTimeCard = new WebTimeCard
            {
               Id = timeCard.Id,
               Comment = timeCard.Comment,
               Time = timeCard.Time,
               CreateDate = timeCard.CreateDate,
               Employee = timeCard.Employee?.EmployeeToWebEmployee()
            };
            return webTimeCard;
        }

        /// <summary>
        /// Returns all enum fields
        /// </summary>
        public static IEnumerable<WebEnumItem> GetAllEnumFields(this Type type)
        {
            List<WebEnumItem> ret = new List<WebEnumItem>();
            foreach (var role in Enum.GetValues(type))
            {
                WebEnumItem temp = new WebEnumItem()
                {
                    Id = (int)role,
                    Name = role.ToString()
                };
                ret.Add(temp);
            }
            return ret;
        }

    }
}
