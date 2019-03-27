using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Api.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// TimeCard entity type configuration
    /// </summary>
    public class TimeCardEntityTypeConfiguration : IEntityTypeConfiguration<TimeCard>
    {
        /// <summary>
        /// Override cofigure method  
        /// </summary>
        public void Configure(EntityTypeBuilder<TimeCard> builder)
        {
            builder.ToTable("TimeCards", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();

            builder.Property(m => m.Comment).IsRequired();

            builder.Property(m => m.Time).IsRequired();

            builder.HasOne(b => b.Employee)
                .WithMany(r => r.TimeCards)
                .HasForeignKey(k => k.EmployeeId);
        }
    }
}
