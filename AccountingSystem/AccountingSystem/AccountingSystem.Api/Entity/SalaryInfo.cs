using AccountingSystem.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// Entity for salary info
    /// </summary>
    public class SalaryInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Salary type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Salary type name
        /// </summary>
        public string TypeName
        {
            get
            {
                return ((SalaryType)Type).ToString();
            }
        }

        /// <summary>
        /// Number, hours, that worker should work for, if time - null
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Salary if fixed - per month, if time - per hour
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// Bank account
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Payment type
        /// </summary>
        public int PaymentType { get; set; }

        /// <summary>
        /// Payment type name
        /// </summary>
        public string PaymentTypeName
        {
            get
            {
                return ((PaymentType)PaymentType).ToString();
            }
        }

        /// <summary>
        /// Employees that have this salaryInfo
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}
