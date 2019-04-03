using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Models.Requests
{
    /// <summary>
    /// Salary info model for create request
    /// </summary>
    public class CreateSalaryInfoRequest
    {
        /// <summary>
        /// Salary type (Fixed or Time)
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Tariff rate
        /// </summary>
        [Required]
        [Range(40, 240, ErrorMessage = "Value must be between 40 and 240")]
        public double Rate { get; set; }

        /// <summary>
        /// Salary amount
        /// </summary>
        [Required]
        [Range(1, 1000000, ErrorMessage = "Value must be between 1 and 1000000")]
        public double Salary { get; set; }

        /// <summary>
        /// Bank account number
        /// </summary>
        [Required]
        public string BankAccount { get; set; }

        /// <summary>
        /// Payment type (Advance or Payday)
        /// </summary>
        [Required]
        public string PaymentType { get; set; }
    }
}
