using Microsoft.EntityFrameworkCore;
using ShoppingCart_controller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart_controller.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            //Composite keys for ProductDetail db
            //model.Entity<ProductDetail>().HasAlternateKey(model => new { model.Id,model.CommentId });
        }
        public DbSet<User> users { get; set; }
    }

    

    

}
