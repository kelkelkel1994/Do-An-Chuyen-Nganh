using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace DACN_ver_2.Controllers
{
    public class LoginController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Login
        //public List<Dangnhap> kiemtradangnhap()
        //{
        //    List<Dangnhap> ktdangnhap = Session["ID"] as List<Dangnhap>;
        //    if (ktdangnhap == null)
        //    {
        //        //Neu gio hang chua ton tai thi khoi tao listGiohang
        //        ktdangnhap = new List<Dangnhap>();
        //        Session["ID"] = ktdangnhap;
        //    }
        //    return ktdangnhap;
        //}
        public string BamMD5(string yourString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
        }
        public ActionResult Login()
        {
            if(Session["ID"] != null)
            {
                return RedirectToAction("Logout", "Login");
            }else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection/*, bool CaptchaValid*/)
        {
            var user = collection["Username"];
            var pass = BamMD5(collection["Password"]);
            if (String.IsNullOrEmpty(user))
            {
                ViewData["ErrorUser"] = "Username không được để trống";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["ErrorPass"] = "Password không được để trống";

            }
            //else if(!CaptchaValid)
            //    {
            //    //Captcha failed to validate
            //    ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
            //    }
            else
            {
                
                NHANVIEN TK = data.NHANVIENs.SingleOrDefault(n => n.USER == user && n.PASS == pass);
                if (TK != null)
                {
                    //List<Dangnhap> ktdangnhap = kiemtradangnhap();
                    ////Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
                    //Dangnhap dn = ktdangnhap.Find(n => n.iID == TK.ID_NHANVIEN);
                    //if (dn == null)
                    //{
                    //    dn = new Dangnhap(TK.ID_NHANVIEN);
                    //    ktdangnhap.Add(dn);
                    //}
                    //else
                    //{
                    //    return View();
                    //}
                    Session["ID"] = TK.ID_NHANVIEN;
                    Session["Quyen"] = TK.ID_PHANQUYEN;
                    if (TK.ID_PHANQUYEN == 1)
                    {
                        return RedirectToAction("Index", "Main");
                    }
                    else if(TK.ID_PHANQUYEN == 2 && TK.ID_PHONGBAN == 2)
                    {
                        return RedirectToAction("Index", "Nhanvien");
                    } else if (TK.ID_PHANQUYEN == 2 && TK.ID_PHONGBAN == 3)
                    {
                        return RedirectToAction("Index", "Kinhdoanh");
                    }
                        //chèn them trang
                        //return RedirectToAction("Index", "Nhanvien");
                    
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
            //List<Dangnhap> ktdangnhap = kiemtradangnhap();
            //ktdangnhap.Clear();
            Session["ID"] = null;
            Session["Quyen"] = null;
            return RedirectToAction("Login", "Login");
        }

        
        public ActionResult Topnavigation()
        {
            //List<Dangnhap> ktdangnhap = kiemtradangnhap();
            var thongtin = data.NHANVIENs.FirstOrDefault(s => s.ID_NHANVIEN == int.Parse(Session["ID"].ToString()));
            return PartialView(thongtin);
        }
        public ActionResult Leftnavigation()
        {
            try
            {

                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    //List<Dangnhap> ktdangnhap = kiemtradangnhap();
                    var thongtin = data.NHANVIENs.FirstOrDefault(s => s.ID_NHANVIEN == int.Parse(Session["ID"].ToString()));
                    return PartialView(thongtin);
                }
            }
            catch
            {
                return View("Login", "Login");
            }
        }

        public ActionResult Menuleft(int id)
        {
            
            if(id == 1)
            {
                var menu1 = data.MENUs.ToList().Where(s => s.ADMIN == true);
                return PartialView(menu1);
            }
            else if(id == 2)
            {
                var menu1 = data.MENUs.ToList().Where(s => s.THAMDINH == true);
                return PartialView(menu1);
            }
            else
            {
                var menu1 = data.MENUs.ToList().Where(s => s.KINHDOANH == true);
                return PartialView(menu1);
            }            
        }

        public ActionResult Submenu(int id)
        {
            var submenu = data.SUBMENUs.ToList().Where(s => s.ID_MENU == id);
            return PartialView(submenu);
        }
    }
}