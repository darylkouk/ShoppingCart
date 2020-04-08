using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Data;
using ShoppingCart.Models;

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

        //Search function
        public IActionResult Search([FromServices] DataContext dbcontext, string searchInput)
        {
            List<Product> products = dbcontext.products.Where(x => x.Name.Contains(searchInput) || x.Description.Contains(searchInput)).ToList();
            ViewData["search"] = products;
            return View("Gallery");
        }
        //View Product Details
        public IActionResult ViewProduct([FromServices] DataContext dbcontext, string selected)
        {
            //Product selectedProduct = dbcontext.products.Where(x => x.Name == selected).FirstOrDefault();
            //List<Product> recommendedProducts = dbcontext.products.Where(x => x.Genre == selectedProduct.Genre).ToList();
            Product selectedProduct = dbcontext.products.Where(x => x.Name.ToLower() == "borderland 3").FirstOrDefault();
            List<Product> recommendedProducts = dbcontext.products.Where(x => x.Genre.ToLower() == "shooter").ToList();

            ViewData["selectedProduct"] = selectedProduct;
            ViewData["recommendedProducts"] = recommendedProducts;
            return View();
        }

        public IActionResult Index()
        {
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
