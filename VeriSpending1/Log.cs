using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeriSpending1.Models;

namespace VeriSpending1
{
  
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            log Log = new log()
            {
                logId = Guid.NewGuid(),
                userId = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                browser = request.Browser.Browser,
                ıpAddres = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                areaAccessed = request.RawUrl,
                timeStamp = DateTime.Now
            };
            veriSpendingdbEntities db = new veriSpendingdbEntities();
            db.log.Add(Log);
            db.SaveChanges();
            base.OnActionExecuting(filterContext);
        }



    }
}