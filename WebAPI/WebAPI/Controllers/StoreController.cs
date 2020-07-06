using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class StoreController : Controller
    {
        StoreModel store = new StoreModel();
       
        //View Store
        public ActionResult Store()
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        //Load all Store 
        public JsonResult LoadStore()
        {
            return Json(store.GetAllStore(), JsonRequestBehavior.AllowGet); 
        }
        
        //Xoa Store// phai reload trang //chua xoa duoc theo checkbox
        public JsonResult DeleteStore(string STORE_CD)
        {            
            return Json(store.DeleteStore(STORE_CD), JsonRequestBehavior.AllowGet);
        }

        //thuc hien update  // update duoc chua load lai duoc table ma phai reload trang 
        public JsonResult UpdateStore(StoreModel storeEdit)
        {
            String username = Session["USERNAME"].ToString();
            return Json(store.UpdateStore(storeEdit, username), JsonRequestBehavior.AllowGet);
        }

        // Lay 1 store de hien thi khi edit 
        public JsonResult GetStore(string STORE_CD)
        {
            var storeEdit = store.GetAllStore().Find(x => x.STORE_CD.Equals(STORE_CD));
            return Json(storeEdit,JsonRequestBehavior.AllowGet);
        }

        //Them store
        public JsonResult Add(StoreModel storeEdit)
        {
            string username = Session["USERNAME"].ToString();
            return Json(store.AddStore(storeEdit,username), JsonRequestBehavior.AllowGet);
        }

        
        
    }

    
}