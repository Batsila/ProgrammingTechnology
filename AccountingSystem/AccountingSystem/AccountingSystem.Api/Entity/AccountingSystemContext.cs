using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity.EntityTypeConfiguration;

namespace AccountingSystem.Api.Entity
{
    /// <summary>
    /// Database context for voka
    /// </summary>
    public class AccountingSystemContext : DbContext
    {
        /// <summary>
        /// Users dbset
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Models dbset
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        /// <summary>
        /// Departments dbset
        /// </summary>
        public DbSet<Department> Departments{ get; set; }
        /// <summary>
        /// Salaries dbset
        /// </summary>
        public DbSet<SalaryInfo> Salaries { get; set; }
        /// <summary>
        /// TimeCards dbset
        /// </summary>
        public DbSet<TimeCard> TimeCards { get; set; }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="options">Options</param>
        public AccountingSystemContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Override save changes method
        /// </summary>
        public override int SaveChanges()
        {
            UpdateDateFields();
            return base.SaveChanges();
        }

        /// <summary>
        /// Override async save changes method
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateDateFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Override save changes method
        /// </summary>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateDateFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Override async save changes method
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateDateFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateDateFields()
        {
            ChangeTracker.DetectChanges();
            var selectedEntityList = ChangeTracker.Entries().Where(x => new[] { EntityState.Added, EntityState.Modified }.Contains(x.State));

            foreach (EntityEntry entityEntry in selectedEntityList)
            {
                if (entityEntry.Entity is IEntityUpdateDate ent)
                {
                    var today = DateTime.Now;

                    if (ent.CreateDate == DateTime.MinValue) { ent.CreateDate = today; }
                    ent.LastUpdateDate = today;
                }
            }
        }

        /// <summary>
        /// Overrided OnModelCreating method
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryInfoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TimeCardEntityTypeConfiguration());
        }
    }
}
