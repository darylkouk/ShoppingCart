using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;
using ShoppingCart.Data;

namespace ShoppingCart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Gallery([FromServices] DataContext dbcontext)
        {
            //load data first
            //  1. need to retrieve list<product> allProducts
            //  and then use viewdata/viewbag pushing ata in .cshtml

            List<Product> AllProducts = dbcontext.products.ToList();
            //List<ProductDetail> selectedProductDetails = dbcontext.productDetails.ToList();
            ViewData["AllProducts"] = AllProducts;

            //  2. need to achieve Customer Name if login
            ViewData["Name"] = "Daryl Kouk"; //For testing

            // 3. Session part For all users
            // Load and Update the cart information based on Session


            /*Basic Function
             *  1. Add to Cart : click on the button "AddToCart", record in the session state.
             *  2. search function: done by Daryl;
             *  3. Show how many items in the cart on the right upper corner
             *  4. Show whether login or log out.
             */

            /* Advanced Function
            *  1. Give Detials : when cursor moved on the items, show detailed text 
            *      OR click on the item, redirect to product detail page.
            *  2. Recommendation
            *  3. Show how many items in the cart on the right upper corner
            */


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
