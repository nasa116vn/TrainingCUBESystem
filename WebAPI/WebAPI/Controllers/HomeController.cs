using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;


namespace WebAPI.Controllers
{
    //Code chinh
    public class HomeController : Controller
    {

       
        User user = new User();
        //Xu ly login
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Login Page";
            if (Session["USERNAME"] != null)
            {
                return RedirectToAction("ViewMain", "View");

            }
            return View();
        }
        // Check username and password
        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            foreach(var item in user.ListUsers())
            {
                if(item.Username.Equals(UserName) && item.Password.Equals(Password))
                {
                    Session["USERNAME"] = UserName;
                    return RedirectToAction("ViewMain", "View");
                }
            } 
            return RedirectToAction("Index", "Home");

        }
        // logout 
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        //change password //da doi duoc mat khau va khi xay ra loi sai se co thong bao nhung phai reload lai trang
        //[HttpPost]
        public JsonResult ChangePassword(string oldPass, string newPass, string confirmPass)
        {
            string username = Session["USERNAME"].ToString();
            var userCheck = user.ListUsers().Find(x => x.Username.Equals(username));
                if (userCheck.Username.Equals(username))
                {
                    if (userCheck.Password.Equals(oldPass))
                    {
                        if (newPass.Equals("") || confirmPass.Equals(""))
                        {
                            return Json(data: "khong duoc rong");
                        }
                        else
                        {
                            if (newPass.Equals(confirmPass) && newPass != null)
                            {
                                user.ChangePass(newPass, username);
                                return Json(data: "changed");
                            }
                            else
                            {
                                return Json(data: "pass khong khop");
                            }
                        }                        
                    }
                    else
                    {
                        return Json(data: "pass cu khong khop");
                    }
                }
           return Json(data: "cant change");
        }
    }
}
