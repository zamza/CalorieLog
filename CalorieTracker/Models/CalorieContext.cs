using System;
using System.Collections.Generic;
using System.Data.Entity;
using CalorieTracker.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CalorieTracker.Models
{
    public class CalorieContext : DbContext
    {
        public CalorieContext(string connString)
            : base(connString)
        {
        }

        public DbSet<Update> Updates { get; set; }
    }
}