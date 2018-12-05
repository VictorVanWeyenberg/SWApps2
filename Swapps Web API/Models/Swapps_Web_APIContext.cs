﻿using System;
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

        public System.Data.Entity.DbSet<Swapps_Web_API.Models.Establishment> Establishments { get; set; }
        public System.Data.Entity.DbSet<Swapps_Web_API.Models.Address> Addresses { get; set; }
        public System.Data.Entity.DbSet<Swapps_Web_API.Models.EstablishmentEvent> Events { get; set; }
        public System.Data.Entity.DbSet<Swapps_Web_API.Models.Promotion> Promotions { get; set; }
        public System.Data.Entity.DbSet<Swapps_Web_API.Models.TimeInterval> TimeIntervals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
