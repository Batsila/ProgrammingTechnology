using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Api.Helpers;

namespace AccountingSystem.Api.Entity.EntityTypeConfiguration
{
    /// <summary>
    /// User entity type configuration
    /// </summary>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Override configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Login).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Password).IsRequired();
            builder.Property(b => b.Role).HasDefaultValue((int)UserRoles.Default);
            builder.HasOne(b => b.Department)
                .WithMany(r => r.Users)
                .HasForeignKey(k => k.DepartmentId);
        }
    }
}
