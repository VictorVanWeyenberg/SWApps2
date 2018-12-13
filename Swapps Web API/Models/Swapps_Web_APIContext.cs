using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class Swapps_Web_APIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx    
        public Swapps_Web_APIContext() : base("name=Swapps_Web_APIContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany<Establishment>(user => user.Subscriptions)
                .WithMany()
                .Map(bond =>
                {
                    bond.MapLeftKey("EstablishmentRefId");
                    bond.MapRightKey("UserRefId");
                    bond.ToTable("UserSubscriptions");
                });
        }

        public DbSet<Establishment> Establishments { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<EstablishmentEvent> Events { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<TimeInterval> TimeIntervals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entrepreneur> Entrepreneurs { get; set; }
        public DbSet<AbstractUser> AbstractUsers { get; set; }
    }
}
