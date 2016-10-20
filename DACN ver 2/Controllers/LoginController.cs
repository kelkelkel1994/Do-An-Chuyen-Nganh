using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;

namespace DACN_ver_2.Controllers
{
    public class LoginController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user = collection["Username"];
            var pass = collection["Password"];
            if (String.IsNullOrEmpty(user))
            {
                ViewData["ErrorUser"] = "Username không được để trống";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["ErrorPass"] = "Password không được để trống";

            }
            else
            {
                NHANVIEN TK = data.NHANVIENs.SingleOrDefault(n => n.USER == user && n.PASS == pass);
                if (TK != null)
                {
                    Session["TKAdmin"] = TK.ID_NHANVIEN;
                    Session["Ten"] = TK.TENNV;
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.Thongbao = "Đăng nhập không thành công";
                }
            }
            return View();
        }
    }
}