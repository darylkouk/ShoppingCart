using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductDetail> productDetails { get; set; }
        public DbSet<PurchaseDetails> purchaseDetails { get; set; }
    }

    

    

}
