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
        public List<Dangnhap> kiemtradangnhap()
        {
            List<Dangnhap> ktdangnhap = Session["Login"] as List<Dangnhap>;
            if (ktdangnhap == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                ktdangnhap = new List<Dangnhap>();
                Session["Login"] = ktdangnhap;
            }
            return ktdangnhap;
        }

        public ActionResult Login()
        {
            if(Session["Login"] != null)
            {
                return RedirectToAction("Logout", "Login");
            }else
            {
                return View();
            }
            
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
                    List<Dangnhap> ktdangnhap = kiemtradangnhap();
                    //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
                    Dangnhap dn = ktdangnhap.Find(n => n.iID == TK.ID_NHANVIEN);
                    if (dn == null)
                    {
                        dn = new Dangnhap(TK.ID_NHANVIEN);
                        ktdangnhap.Add(dn);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Main");
                    }
                    Session["ID"] = TK.ID_NHANVIEN;
                    Session["Quyen"] = TK.ID_PHANQUYEN;
                    Session["Ten"] = TK.TENNV;
                    if (TK.ID_PHANQUYEN == 1)
                    {
                        return RedirectToAction("Index", "Main");
                    }
                    else
                        return RedirectToAction("Index", "Nhanvien");
                    
                }
                else
                {
                    ViewBag.Thongbao = "Đăng nhập không thành công";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            List<Dangnhap> ktdangnhap = kiemtradangnhap();
            ktdangnhap.Clear();
            Session["Login"] = null;
            return RedirectToAction("Login", "Login");
        }

        
        public ActionResult Topnavigation()
        {
            List<Dangnhap> ktdangnhap = kiemtradangnhap();
            var thongtin = ktdangnhap.FirstOrDefault(s => s.iID == int.Parse(Session["ID"].ToString()));
            return PartialView(thongtin);
        }
        public ActionResult Leftnavigation()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<Dangnhap> ktdangnhap = kiemtradangnhap();
                var thongtin = ktdangnhap.FirstOrDefault(s => s.iID == int.Parse(Session["ID"].ToString()));
                return PartialView(thongtin);
            }
        }
    }
}