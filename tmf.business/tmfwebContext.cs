using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using tmf.entities;

namespace tmf.business
{
    public class tmfwebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<tmf.web.Models.tmfwebContext>());

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Waiter> Waiters { get; set; }

        public DbSet<Zone> Zones { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            modelBuilder.Entity<Restaurant>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}