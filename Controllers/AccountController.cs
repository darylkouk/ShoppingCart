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
        
        //public IActionResult Index()
        //{
        //    ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
        //    //ViewData["username"] = HttpContext.Session.GetString("username");
        //    return View();
        //}

        public IActionResult Login()
        {
            TempData["loginfromcart"] = (string)TempData["loginfromcart"];
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");
            return View();
        }

        public IActionResult Logout()
        {
            /* Optional feature to remove cart after logout
            HttpContext.Session.Remove("CartCount");
            HttpContext.Session.Remove("Cart");*/

            HttpContext.Session.Remove("username");
            return RedirectToAction("Gallery", "Home");
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

                    if(HttpContext.Session.GetString("Cart") != null && (string)TempData["loginfromcart"] == "hello")
                    {
                        TempData["loginfromcart"] = null;
                        return RedirectToAction("Cart", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Gallery", "Home");
                    }
                }
                else
                {
                    ViewData["fail"] = "Invalid Username or Password";
                    return View("Login");
                }
            }

            
        }

        public IActionResult NewPurchase([FromServices] DataContext dbcontext)
        {
            string productidlist = HttpContext.Session.GetString("Cart");
            productidlist = productidlist.Substring(1);            
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
            string[] productids = productidlist.Substring(0).Split(" "); //check if the incoming still start with " "

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
            HttpContext.Session.Remove("CartCount");
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("PurchaseHistory");
        }

        public IActionResult PurchaseHistory([FromServices] DataContext dbcontext)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartCount");

            if (ViewData["username"] == null)
            {
                return View("Login");
            }

            List<PurchaseDetails> history = dbcontext.purchaseDetails.Where(x => x.UserId == 
            dbcontext.users.Where(x => x.Username == ViewData["username"] as string).FirstOrDefault().Id).ToList();

            IEnumerable<PurchaseDetails> sortedhistory = from his in history 
                                                         orderby his.CreatedDate descending, his.Product.Name ascending
                                                         select his;
           
            List<PurchaseDetails> sortedhistorylist = new List<PurchaseDetails>();

            foreach (PurchaseDetails x in sortedhistory)
            {
                sortedhistorylist.Add(x);
            }

            List<string> totalproductname = new List<string>();
            List<string> totalactivationcode = new List<string>();
            List<DateTime> totalcreateddate = new List<DateTime>();

            for (int i = 0; i < sortedhistorylist.Count(); i++)
            {
                if (i == 0)
                {
                    totalproductname.Add(sortedhistorylist[i].Product.Name);
                    totalactivationcode.Add(sortedhistorylist[i].ActivationCode);
                    totalcreateddate.Add(sortedhistorylist[i].CreatedDate);
                }                            
                
                else if (i > 0 && sortedhistorylist[i].CreatedDate.Date == sortedhistorylist[i - 1].CreatedDate.Date && 
                    sortedhistorylist[i].Product.Name == sortedhistorylist[i - 1].Product.Name)
                {

                    totalactivationcode[totalactivationcode.Count()-1]  += " " + sortedhistorylist[i].ActivationCode;
                }
                else
                {
                    totalproductname.Add(sortedhistorylist[i].Product.Name);
                    totalactivationcode.Add(sortedhistorylist[i].ActivationCode);
                    totalcreateddate.Add(sortedhistorylist[i].CreatedDate);
                }
            }

            ViewData["productnames"] = totalproductname;
            ViewData["activationcodes"] = totalactivationcode;
            ViewData["createddates"] = totalcreateddate;            
            return View();
        }      
    }
}
