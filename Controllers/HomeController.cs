using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Gallery");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Gallery([FromServices] DataContext dbcontext,string cmd,string ProductId)
        {
            /* Basic Function
            *  1. Add to Cart : click on the button "AddToCart", record in the session state.
            *  2. search function: (done by Daryl)
            *  3. Show whether login or log out. (Done in the layout)
                 Advanced Function
            *  1. Give Detials : when cursor moved on the items, show detailed text 
            *      OR click on the item, redirect to product detail page.
            *  2. Recommendation [done by Daryl]
            *  3. Show how many items in the cart on the right upper corner
            */

            //load data first
            //  1. need to retrieve list<product> allProducts
            //  and then use viewdata/viewbag pushing ata in .cshtml
            List<Product> AllProducts = dbcontext.products.ToList();
            ViewData["AllProducts"] = AllProducts;
            ViewData["search"] = null;
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (cmd == "AddToCart")
            {
                string AddedProductId = HttpContext.Session.GetString("Cart"); 
                string newAdded = AddedProductId + " " + ProductId;
                HttpContext.Session.SetString("Cart", newAdded);
                return View("Gallery");
            }

            // 3. Session part For all users
            // Load and Update the cart information based on Session
            // If new coming, then generate Session "Cart" as empty string
            if (HttpContext.Session.GetString("Cart") == null)
                HttpContext.Session.SetString("Cart", "");
            return View("Gallery");
        }

        public IActionResult Cart()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Daryl Part here
        //Search function
        //haven't implement in layout
        public IActionResult Search([FromServices] DataContext dbcontext, string searchInput)
        {
            List<Product> products = dbcontext.products.Where(x => x.Name.Contains(searchInput) /*|| x.Description.Contains(searchInput)*/).ToList();
            ViewData["search"] = products;
            return View("Gallery");
        }
        //View Product Details
        public IActionResult ViewProduct([FromServices] DataContext dbcontext, string selected)
        {
            Product selectedProduct = dbcontext.products.Where(x => x.Name == selected).FirstOrDefault();
            List<ProductDetail> selectedProductDetails = dbcontext.productDetails.Where(x => x.ProductId == selectedProduct.Id).ToList();
            List<Product> recommendedProducts = dbcontext.products.Where(x => x.Genre == selectedProduct.Genre && x.Name != selectedProduct.Name).ToList();

            //for testing purposes
            /*Product selectedProduct = dbcontext.products.Where(x => x.Name.ToLower() == "borderland 3").FirstOrDefault();
            List<ProductDetail> selectedProductDetails = dbcontext.productDetails.Where(x => x.ProductId == selectedProduct.Id).ToList();
            List<Product> recommendedProducts = dbcontext.products.Where(x => x.Genre.ToLower() == "shooter" && x.Name != selectedProduct.Name).ToList();

    */
            ViewData["selectedProduct"] = selectedProduct;
            ViewData["recommendedProducts"] = recommendedProducts;

            if (selectedProductDetails.Count() == 0)
            {
                ViewData["selectedProductDetails"] = null;
                ViewData["Rating"] = 0;
            }
            else
            {
                ViewData["selectedProductDetails"] = selectedProductDetails;
                ViewData["Rating"] = Average(selectedProductDetails);
            }
            return View();
        }
        public int Average(List<ProductDetail> productDetail)
        {
            int aveRating = 0;
            foreach (var select in productDetail)
            {
                aveRating += select.Rating;
            }
            return aveRating = aveRating / productDetail.Count();
        }

        //Add product reviews function
        public IActionResult AddComment([FromServices] DataContext dbcontext, string comment, string rating, string trackProduct)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                //redirect to login screen
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ProductDetail addedComment = new ProductDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = dbcontext.products.Where(x => x.Name == trackProduct).FirstOrDefault().Id,
                    UserId = dbcontext.users.Where(x => x.Username == HttpContext.Session.GetString("username")).FirstOrDefault().Id,
                    Comment = comment,
                    Rating = int.Parse(rating)
                };
                dbcontext.Add(addedComment);
                dbcontext.SaveChanges();
            }
            return RedirectToAction("ViewProduct", new { selected = trackProduct });
        }
    }
}
