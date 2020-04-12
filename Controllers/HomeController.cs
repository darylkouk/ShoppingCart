using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        protected DataContext dbcontext;

        private readonly Product product;

        public HomeController(DataContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Gallery([FromServices] DataContext dbcontext, string cmd, string ProductId)
        {

            //load data first
            //  1. need to retrieve list<product> allProducts
            //  and then use viewdata/viewbag pushing ata in .cshtml
            List<Product> AllProducts = dbcontext.products.ToList();
            ViewData["AllProducts"] = AllProducts;
            ViewData["search"] = null;
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");

            if (cmd == "AddToCart")
            {

                string AddedProductId = HttpContext.Session.GetString("Cart");
                string newAdded = AddedProductId + " " + ProductId;
                HttpContext.Session.SetString("Cart", newAdded);
                if (!HttpContext.Session.GetInt32("CartCount").HasValue)
                {
                    int cartcount = 1;
                    HttpContext.Session.SetInt32("CartCount", cartcount);
                }
                else
                {
                    HttpContext.Session.SetInt32("CartCount", HttpContext.Session.GetInt32("CartCount").Value + 1);
                }
                ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");

                return View("Gallery");
            }

            if (cmd == "RemoveFromCartFromCartView")
            {

                string AddedProductId = HttpContext.Session.GetString("Cart");             
                int indexof = AddedProductId.IndexOf(ProductId);
                string newAdded = AddedProductId.Remove(indexof-1,ProductId.Length+1);
                HttpContext.Session.SetString("Cart", newAdded);

                if (!HttpContext.Session.GetInt32("CartCount").HasValue)
                {
                    int cartcount = 1;
                    HttpContext.Session.SetInt32("CartCount", cartcount);
                }
                else
                {
                    HttpContext.Session.SetInt32("CartCount", HttpContext.Session.GetInt32("CartCount").Value - 1);
                }
                ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");

                return RedirectToAction("Cart");

            }

            if (cmd == "AddToCartFromCartView")
            {

                string AddedProductId = HttpContext.Session.GetString("Cart");
                string newAdded = AddedProductId + " " + ProductId;
                HttpContext.Session.SetString("Cart", newAdded);
                
                if (!HttpContext.Session.GetInt32("CartCount").HasValue)
                {
                    int cartcount = 1;
                    HttpContext.Session.SetInt32("CartCount", cartcount);
                }
                else
                {
                    HttpContext.Session.SetInt32("CartCount", HttpContext.Session.GetInt32("CartCount").Value + 1);
                }
                ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
                
                return RedirectToAction("Cart");
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
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
            ViewData["username"] = HttpContext.Session.GetString("username");

            bool isEmpty = false;
            if(HttpContext.Session.GetString("Cart") == null || HttpContext.Session.GetString("Cart").Length == 0)
            {
                isEmpty = true;
                ViewData["showcart"] = null;
            }
            else
            {
                
                List<Product> products = ShowCartItems();

                Dictionary<Product, int> productWithQty = new Dictionary<Product, int>();

                foreach (var product in products)
                {
                    if (!productWithQty.ContainsKey(product))
                    {
                        productWithQty.Add(product, 1);
                    }
                    else
                    {
                        productWithQty[product]++;
                    }
                }
                ViewData["showcart"] = productWithQty;
            }
            ViewData["isEmpty"] = isEmpty;           

            return View();
        }
        public List<Product> ShowCartItems()
        {
            
            string productidList = HttpContext.Session.GetString("Cart").Substring(1);
            

            List<Product> prod = new List<Product>();
            
            if(productidList != null && productidList.Length != 0)
            {
                string[] productid = productidList.Split(" ");

                foreach (string pid in productid)
                {
                    prod.Add(GetId(pid));
                }
            }
            return prod;
        }
        public Product GetId(string id)
        {
            Product product = dbcontext.products
                .Where(p => p.Id == id).FirstOrDefault();

            return product;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Daryl Part here
        //Search function
        public IActionResult Search([FromServices] DataContext dbcontext, string searchInput = "")
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
            if(searchInput == "")
            {
                List<Product> products = dbcontext.products.ToList();
                ViewData["searchInput"] = searchInput;
                ViewData["search"] = products;
                return View("Gallery");
            }
            else
            {
                List<Product> products = dbcontext.products.Where(x => x.Name.Contains(searchInput) || x.Description.Contains(searchInput)).ToList();

                ViewData["searchInput"] = searchInput;
                ViewData["search"] = products;
                return View("Gallery");
            }
        }
        //View Product Details
        public IActionResult ViewProduct([FromServices] DataContext dbcontext, string selected)
        {
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
            ViewData["username"] = HttpContext.Session.GetString("username");
            Product selectedProduct = dbcontext.products.Where(x => x.Name == selected).FirstOrDefault();
            List<ProductDetail> selectedProductDetails = dbcontext.productDetails.Where(x => x.ProductId == selectedProduct.Id).ToList();
            List<Product> recommendedProducts = dbcontext.products.Where(x => x.Genre == selectedProduct.Genre && x.Name != selectedProduct.Name).ToList();

      
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
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
            if (HttpContext.Session.GetString("username") == null)
            {
                //redirect to login screen
                return RedirectToAction("Login","Account");
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
