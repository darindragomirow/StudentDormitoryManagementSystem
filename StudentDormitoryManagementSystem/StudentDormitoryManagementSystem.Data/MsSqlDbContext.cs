using Microsoft.AspNet.Identity.EntityFramework;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using StudentDormitoryManagementSystem.Data.Model.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using StudentDormitoryManagementSystem.Common.Exceptions;

namespace StudentDormitoryManagementSystem.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Inventory> Inventories { get; set; }

        public IDbSet<Room> Rooms { get; set; }

        public IDbSet<StudentInfo> Students { get; set; }

        public IDbSet<ItemCategory> ItemCategories { get; set; }

        public IDbSet<UserRole> UserRoles { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Breakdown> Breakdowns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
    }
}