using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// Department entity type configuration
    /// </summary>
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        /// <summary>
        /// Override cofigure method
        /// </summary>
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();

            builder.Property(m => m.Name).IsRequired();

            builder.HasMany<Employee>(m => m.Employees)
                .WithOne(s => s.Department)
                .HasForeignKey(k => k.DepartmentId);

            builder.HasMany(m => m.Users)
                .WithOne(s => s.Department)
                .HasForeignKey(k => k.DepartmentId);
        }
    }
}
