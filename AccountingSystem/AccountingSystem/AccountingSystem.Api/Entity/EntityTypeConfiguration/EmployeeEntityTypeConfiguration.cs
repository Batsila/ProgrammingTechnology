using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.API.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// Model entity type configuration
    /// </summary>
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Override cofigure method  
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();

            builder.Property(m => m.FirstName).IsRequired();

            builder.Property(m => m.SecondName);
        }
    }
}
