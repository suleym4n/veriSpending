using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeriSpending1.Models;

namespace VeriSpending1.Controllers
{
    [Authorize]
    [Log]
    public class SpendingController : Controller
    {
        // GET: Spending

        public veriSpendingdbEntities entites = new veriSpendingdbEntities();
        #region List
        public ActionResult Spending()
        {
            return View();
        }
        public ActionResult Index()
        {
            var mySpendingInDb = entites.spending.ToList().Where(f => f.userId == (int)TempData["userIdTD"]);
            
            return View(mySpendingInDb);
        }
        public ActionResult IndexList()
        {
            var spendingList = entites.spending.ToList();
            return View(spendingList);
        }
        public ActionResult IndexApprove()
        {
            var spendingApprove = entites.spending.ToList().Where(a => (string)a.approval == "Beklemede ");

            return View(spendingApprove);
        }
        public ActionResult ListDecline()
        {
            var listDecline = entites.spending.ToList().Where(ld => ld.approval == "Reddedildi" && ld.userId == (int)TempData["userIdTD"]);
            TempData.Keep("userIdTD");
            return View(listDecline);
        }
        public ActionResult ListAproved()
        {
            var listAp = entites.spending.ToList().Where(x => x.approval == "Onaylandı ");

            return View(listAp);
        }
        #endregion
        #region Approved 
        public ActionResult ApproveComment(int id)
        {
            var spendingDc = entites.spending.FirstOrDefault(s => s.spendingId == id);
            spendingDc.approval = "Reddedildi";
            TempData["id"] = id;
            entites.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult ApproveComment(declineSpending ds)
        {
            ds.spendingId = (int)TempData["id"];
            entites.declineSpending.Add(ds);
            entites.SaveChanges();
            return RedirectToAction("IndexApprove");
        }
        public ActionResult Approved(int id)
        {
            var spendingDc = entites.spending.FirstOrDefault(s => s.spendingId == id);
            spendingDc.approval = "Onaylandı";
            TempData["id"] = id;
            entites.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult Approved()
        {

            return RedirectToAction("IndexApprove");
        }
        #endregion
        [HttpGet]
        public ActionResult EditDecline(int id)
        {
            var find = entites.spending.Where(s => s.spendingId == id).FirstOrDefault();
            TempData["editID"]=id;
            return View(find);
        }
        [HttpPost]
        public ActionResult EditDecline(spending sp1)
        {
            
            using (veriSpendingdbEntities db = new veriSpendingdbEntities())
            {
                sp1.userId = (int)TempData["userIdTD"];
                TempData.Keep("userIdTD");
                //var sp = db.spending.Where(x => x.spendingId == id);
                //db.Entry(sp).State = EntityState.Modified;
                int idd = (int)TempData["editID"];
                var spOld = db.spending.Where(a => a.spendingId== idd).FirstOrDefault();
                spOld.userId= (int)TempData["userIdTD"];
                spOld.approval = "Beklemede";
                spOld.spendingAmount = sp1.spendingAmount;
                spOld.spendingDate = sp1.spendingDate;
              
                spOld.spendingTitle = sp1.spendingTitle;
                spOld.spendingDescription = sp1.spendingDescription;
                db.SaveChanges();
            }
            return RedirectToAction("ListDecline");
           
        }
        public ActionResult Details(int id)
        {
            var spComment = entites.declineSpending.FirstOrDefault(x => x.spendingId==id);
            return View(spComment);
        }
        #region Payment
        public ActionResult Payment(int id)
        {
            var spendingDc = entites.spending.FirstOrDefault(s => s.spendingId == id);
            spendingDc.approval = "Ödendi";
            
            entites.SaveChanges();
            
            return View();
        }
        [HttpPost]
        public ActionResult Payment()
        {

            return RedirectToAction("ListAproved");
        }
        #endregion
        [HttpPost]
        public ActionResult SaveRecord(spending sp)
        {
            #region AddSpending

            if (sp.spendingAmount < 1000 && sp.spendingAmount > 0)
            {
                sp.userId = (int)TempData["userIdTD"];
                TempData.Keep("userIdTD");

                sp.approval = "Beklemede";
                entites.spending.Add(sp);
                entites.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mesaj = "Tutar 1000 birimden büyük ve sıfırdan küçük olamaz";
                return View("Spending");
            }

            #endregion

        }

    }
}