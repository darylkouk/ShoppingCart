using Scrypt;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class DbSeeder
    {
        public DbSeeder(DataContext dbcontext)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Daryl Kouk",
                Username = "Daryl",
                Password = encoder.Encode("Password")
            };
            dbcontext.Add(user);
            dbcontext.SaveChanges();
        }
    }
}
