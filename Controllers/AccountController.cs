using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scrypt;
using ShoppingCart_controller.Data;

namespace ShoppingCart_controller.Controllers
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

        public IActionResult PurchasedHistory()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }


        

    }
}