using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VeriSpending1.Models;

namespace VeriSpending1.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
           
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(user users)
        {
            var userInDb = users;
            using (var entites = new veriSpendingdbEntities())
            {
                userInDb = entites.user.FirstOrDefault(x => x.mail == users.mail && x.password == users.password);
                TempData["userIdTD"] = userInDb.id;
                TempData.Keep("userIdTD");
            }

        
            if (userInDb != null)
            {
                
                TempData["userRole"] = userInDb.roles;
               
                FormsAuthentication.SetAuthCookie(users.mail, false);
                if (userInDb.roles=="admin")
                {
                    return RedirectToAction("IndexAdmin", "Home");
                }
                if (userInDb.roles=="accounting")
                {
                    return RedirectToAction("IndexAccounting", "Home");

                }
               
                    return RedirectToAction("IndexUser", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Hatalı giriş";
                return View();
            }
   
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
           
            return RedirectToAction("Index");
        }
    }
}