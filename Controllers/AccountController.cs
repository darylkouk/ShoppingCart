using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scrypt;
using ShoppingCart.Data;
using ShoppingCart.Models;


namespace ShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
            //return View();
        }
        public IActionResult Validate([FromServices] DataContext dbcontext, string username, string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (username == null || password == null)
            {
                ViewData["fail"] = "All Fields Required";
                return View("Login");
            }
            var usernameValidate = dbcontext.users.Where(x => x.Username == username).FirstOrDefault();

            if (usernameValidate == null)
            {
                ViewData["fail"] = "Invalid Username or Password";
                return View("Login");
            }
            else
            {
                bool passwordValidate = encoder.Compare(password, usernameValidate.Password);
                if (passwordValidate)
                {
                    HttpContext.Session.SetString("username", username);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["fail"] = "Invalid Username or Password";
                    return View("Login");
                }
            }

            
        }

        //*this one optional for now, Martin 2020-04-09*
        //public iactionresult checkout()
        //{
        //    viewdata["username"] = httpcontext.session.getstring("username");

        //    return view()

        //}

        public IActionResult NewPurchase([FromServices] DataContext dbcontext, string productidlist)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            
            if (ViewData["username"] == null)
            {
                return View("Login");
            }
            
            else if(productidlist == null)
            {
                return RedirectToAction("Gallery", "Home");
            }

            string username = ViewData["username"] as string;
          User currentuser = dbcontext.users.Where(x => x.Username == username).FirstOrDefault();
            string userid = currentuser.Id;
            string[] productids = productidlist.Split(" ");

            foreach (string productid in productids)
            {
                PurchaseDetails newpurchase = new PurchaseDetails()
                {
                    Id = Guid.NewGuid().ToString(),
                    ActivationCode = Guid.NewGuid().ToString(),
                    ProductId = productid,
                    UserId = userid,
                    CreatedDate = DateTime.UtcNow
                };
                dbcontext.Add(newpurchase);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("PurchaseHistory");
        }

        public IActionResult PurchaseHistory([FromServices] DataContext dbcontext)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");

            if (ViewData["username"] == null)
            {
                return View("Login");
            }

            List<PurchaseDetails> history = dbcontext.purchaseDetails.Where(x => x.UserId == dbcontext.users.Where(x => x.Username == ViewData["username"] as string).FirstOrDefault().Id).ToList();

            IEnumerable<PurchaseDetails> sortedhistory = from his in history orderby his.ProductId select his;

            List<PurchaseDetails> sortedhistorylist = new List<PurchaseDetails>();

            foreach (PurchaseDetails x in sortedhistory)
            {
                sortedhistorylist.Add(x);
            }
            
            ViewData["purchase history"] = sortedhistorylist;
            
            return View();
        }


        

    }
}