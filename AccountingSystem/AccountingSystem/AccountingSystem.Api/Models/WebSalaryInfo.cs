using AccountingSystem.Api.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models
{
    /// <summary>
    /// Includes information about employee tariff rate or salary type
    /// </summary>
    public class WebSalaryInfo
    {
        /// <summary>
        /// Salary type (hourly or fixed)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Tariff rate
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Salary amount
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// Bank account number
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Payment type (cash or bank transfer)
        /// </summary>
        public string PaymentType { get; set; }
    }
}
