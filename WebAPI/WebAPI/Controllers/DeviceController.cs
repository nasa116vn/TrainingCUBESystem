using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Dynamic;

namespace WebAPI.Controllers
{
    public class DeviceController : Controller
    {
        Device device = new Device();

        //Giao dien Store
        public ActionResult Device()
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            StoreModel store = new StoreModel();
            Bumon bumon = new Bumon();
            //Dung ViewModel de ket 2 danh sach gui qua giao dien Device de thuc hien cho select khi add va edit
            ViewModel modelStoreBumon = new ViewModel();
            modelStoreBumon.Stores = store.GetStoreName();
            modelStoreBumon.Bumons = bumon.GetBumonName();
            return View(modelStoreBumon);
        }
        
        //Lay tat ca Device len giao dien
        public JsonResult LoadDevice()
        {
            return Json(device.GetAllDevices(),JsonRequestBehavior.AllowGet);
        }

        //Xoa device //chua xoa duoc nhieu dong cung luc bang cach xet theo flag
        public JsonResult DeleteDevice(string IMEI)
        {
            return Json(device.DeleteDevice(IMEI),JsonRequestBehavior.AllowGet);   
        }

        //Add Device
        public JsonResult AddDevice(Device deviceAdd)
        {
            string username = Session["USERNAME"].ToString();
            return Json(device.AddDevice(deviceAdd, username), JsonRequestBehavior.AllowGet);
        }

        //Edit Device
        //Lay 1 device de hien thi
        public JsonResult GetDevice(String IMEI)
        {
            var getDevice = device.GetAllDevices().Find(x => x.IMEI.Equals(IMEI));
            return Json(getDevice, JsonRequestBehavior.AllowGet);
        }
        //Thuc hien update
        public JsonResult UpdateDevice(Device deviceEdit)
        {
            string username = Session["USERNAME"].ToString();
            return Json(device.UpdateDevice(deviceEdit, username), JsonRequestBehavior.AllowGet);
        }

    }
}