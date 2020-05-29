using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ViewMain()
        {
            return View();
        }

        public ActionResult Store()
        {
            return View();
        }

        public ActionResult Device()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SVR_USER objUser)
        {
            if(ModelState.IsValid)
            {
                using (StoreDBContext1 storeDBContext1 = new StoreDBContext1())
                {
                    var obj = storeDBContext1.SVR_USER.Where(a => a.USERNAME.Equals(objUser.USERNAME) && a.PASSWORD.Equals(objUser.PASSWORD)).FirstOrDefault();
                    if(obj != null)
                    {
                        Session["USERNAME"] = obj.USERNAME.ToString();
                        return RedirectToAction("UserDashBoard");
                    }

                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["USERNAME"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


    }
}
