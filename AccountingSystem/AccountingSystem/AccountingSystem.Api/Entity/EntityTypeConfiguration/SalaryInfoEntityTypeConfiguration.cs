using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// SalaryInfo entity type configuration
    /// </summary>
    public class SalaryInfoEntityTypeConfiguration : IEntityTypeConfiguration<SalaryInfo>
    {
        /// <summary>
        /// Override cofigure method  
        /// </summary>
        public void Configure(EntityTypeBuilder<SalaryInfo> builder)
        {
            builder.ToTable("Salaries", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();

            builder.Property(m => m.PaymentType).IsRequired();

            builder.Property(m => m.Rate);

            builder.Property(m => m.Salary);

            builder.Property(m => m.Type);

            builder.Property(m => m.PaymentType);

            builder.Property(m => m.BankAccount);

            builder.HasMany(m => m.Employees)
                .WithOne(e => e.SalaryInfo)
                .HasForeignKey(m => m.SalaryInfoId);
        }
    }
}
