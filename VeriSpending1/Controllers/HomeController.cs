using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeriSpending1.Controllers
{
    [AllowAnonymous]
    [Log]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {   
            if ((string)TempData["userRole"] == "admin")
            {
                return RedirectToAction("IndexAdmin");
            }
            if ((string)TempData["userRole"] == "accounting")
            {
                return RedirectToAction("IndexAccounting");
            }
            if ((string)TempData["userRole"] == "user")
            {
                return RedirectToAction("IndexUser");
            }
           
                return View();
        }

        public ActionResult IndexUser()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult IndexAccounting()
        {
            return View();
        }


    }
}