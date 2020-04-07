using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Areas.Customer.Controllers
{
    public class AccountController : Controller
    {
        [Area("Customer")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login","Account",new { area = "Guest"});
        }
    }
}