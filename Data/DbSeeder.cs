using Scrypt;
using ShoppingCart.Models;
using System;

namespace ShoppingCart.Data
{
    public class DbSeeder
    {
        public DbSeeder(DataContext dbcontext)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            //User DB
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Daryl Kouk",
                Username = "Daryl",
                Password = encoder.Encode("Password")
            };
            dbcontext.Add(user);

            //Products
            //Shooter Games
            Product product1 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Borderland 3",
                Genre = "Shooter",
                Description = "The original shooter-looter returns, packing bazillions of guns and a mayhem-fueled adventure! Blast through new worlds & enemies and save your home from the most ruthless cult leaders in the galaxy.",
                Price = 81.90,
            };
            dbcontext.Add(product1);
            Product product2 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Call of Duty® WWII",
                Genre = "Shooter",
                Description = "Call of Duty® returns to its roots with Call of Duty®: WWII - a breathtaking experience that redefines World War II for a new gaming generation.",
                Price = 85.00,
            };
            dbcontext.Add(product2);
            Product product3 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Far Cry® 5",
                Genre = "Shooter",
                Description = "Welcome to Hope County, Montana, home to a fanatical doomsday cult known as Eden’s Gate. Stand up to cult leader Joseph Seed & his siblings, the Heralds, to spark the fires of resistance & liberate the besieged community.",
                Price = 70.00,
            };
            dbcontext.Add(product3);

            //Action
            Product product4 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "MONSTER HUNTER WORLD",
                Genre = "Action",
                Description = "Welcome to a new world! In Monster Hunter: World, the latest installment in the series, you can enjoy the ultimate hunting experience, using everything at your disposal to hunt monsters in a new world teeming with surprises and excitement.",
                Price = 41.00,
            };
            dbcontext.Add(product4);
            Product product5 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "DARK SOULS™ III",
                Genre = "Action",
                Description = "Dark Souls continues to push the boundaries with the latest, ambitious chapter in the critically-acclaimed and genre-defining series. Prepare yourself and Embrace The Darkness!",
                Price = 59.90,
            };
            dbcontext.Add(product5);
            Product product6 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sekiro™ Shadows Die Twice",
                Genre = "Action",
                Description = "Game of the Year - The Game Awards 2019, Best Action Game of 2019 - IGN. Carve your own clever path to vengeance in the award winning adventure from developer FromSoftware, creators of Bloodborne and the Dark Souls series. Take Revenge. Restore Your Honor. Kill Ingeniously.",
                Price = 69.90,
            };
            dbcontext.Add(product6);

            //RPG
            Product product7 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Divinity Original Sin 2 - Definitive Edition",
                Genre = "RPG",
                Description = "The eagerly anticipated sequel to the award-winning RPG. Gather your party. Master deep, tactical combat. Join up to 3 other players - but know that only one of you will have the chance to become a God.",
                Price = 49.00,
            };
            dbcontext.Add(product7);
            Product product8 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "STAR WARS Jedi Fallen Order™",
                Genre = "RPG",
                Description = "A galaxy-spanning adventure awaits in Star Wars Jedi: Fallen Order, a 3rd person action-adventure title from Respawn. An abandoned Padawan must complete his training, develop new powerful Force abilities, and master the art of the lightsaber - all while staying one step ahead of the Empire.",
                Price = 69.90,
            };
            dbcontext.Add(product8);
            Product product9 = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "FINAL FANTASY XV WINDOWS EDITION",
                Genre = "RPG",
                Description = "Take the journey, now in ultimate quality. Boasting a wealth of bonus content and supporting ultra high-resolution graphical options and HDR 10, you can now enjoy the beautiful and carefully-crafted experience of FINAL FANTASY XV like never before.",
                Price = 69.90,
            };
            dbcontext.Add(product9);

            //ProductDetails
            //Rating is out of 5
            ProductDetail productDetail1 = new ProductDetail()
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                UserId = user.Id,
                Comment = "Borderlands 3, on the whole is a solid game. The gunplay is much improved on previous entries as are the vehicles and most of the side missions are pretty fun, best bought on sale.",
                Rating = 4
            };
            dbcontext.productDetails.Add(productDetail1);
            ProductDetail productDetail2 = new ProductDetail()
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                UserId = user.Id,
                Comment = "Apart from the janky story and the overload of terrible attempts at comedy it was a pretty good game, but since Randy Pitchford treats his employees like cattle I just can't recommend this game.",
                Rating = 2
            };
            dbcontext.productDetails.Add(productDetail2);

            //PurchaseDetails
            //Past purchase history of user Daryl
            PurchaseDetails purchaseDetails1 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 04, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails1);
            PurchaseDetails purchaseDetails2 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 04, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails2);
            PurchaseDetails purchaseDetails3 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product4.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 04, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails3);
            PurchaseDetails purchaseDetails4 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 04, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails4);

            //seeded by Martin 2020-04-10
            PurchaseDetails purchaseDetails5 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 05, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails5);

            PurchaseDetails purchaseDetails6 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 05, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails6);

            PurchaseDetails purchaseDetails7 = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = product9.Id,
                UserId = user.Id,
                CreatedDate = new DateTime(2020, 05, 01, 10, 00, 00).ToUniversalTime()
            };
            dbcontext.Add(purchaseDetails7);

            dbcontext.SaveChanges();
        }
    }
}
