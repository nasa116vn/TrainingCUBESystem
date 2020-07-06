using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    public class ViewController : Controller
    {
        ViewDB view = new ViewDB();
        // GET: View
        public ActionResult ViewMain()
        {

            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            return View("ViewMain", view.GetListIMEI());

        }

        [HttpPost]
        public JsonResult LoadView(/*string IMEI*/)
        {
            //foreach (var item in view.GetAllView())
            //{
            //    if (item.IMEI.Equals(IMEI))
            //    {
            //        var obj = new ViewDB();
            //        obj.IMEI = item.IMEI;
            //        obj.BUMON_NAME = item.BUMON_NAME;
            //        obj.STORE_NAME = item.STORE_NAME;
            //        obj.PRODUCT_NAME = item.PRODUCT_NAME;
            //        obj.VIEW_DATE = item.VIEW_DATE;
            //        obj.VIEWS = item.VIEWS;
            //        obj.INSERT_DATE = item.INSERT_DATE;
            //        obj.UPDATE_DATE = item.UPDATE_DATE;

            //        return Json(obj, JsonRequestBehavior.AllowGet);
            //    }
            //}
            
            return Json(view.GetAllView(),JsonRequestBehavior.AllowGet);
        }





        
    }
}