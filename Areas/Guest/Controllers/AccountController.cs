using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scrypt;
using ShoppingCart.Data;

namespace ShoppingCart.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
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
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                else
                {
                    ViewData["fail"] = "Invalid Username or Password";
                    return View("Login");
                }
            }
        }
    }
}