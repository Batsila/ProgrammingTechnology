using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// Employee entity type configuration
    /// </summary>
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Override cofigure method  
        /// </summary>
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();

            builder.Property(m => m.FirstName).IsRequired();

            builder.Property(m => m.SecondName);

            builder.HasOne<SalaryInfo>(m => m.SalaryInfo)
                .WithMany(s => s.Employees)
                .HasForeignKey(m => m.SalaryInfoId);

            builder.HasOne(b => b.Department)
                .WithMany(r => r.Employees)
                .HasForeignKey(k => k.DepartmentId);

            builder.HasMany<TimeCard>(m => m.TimeCards)
                .WithOne(s => s.Employee)
                .HasForeignKey(k => k.EmployeeId);
        }
    }
}
