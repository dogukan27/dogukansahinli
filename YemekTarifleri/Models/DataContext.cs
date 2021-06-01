using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YemekTarifleri.Models
{
    public class DataContext:DbContext
    {
        public DataContext() : base("foodConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}