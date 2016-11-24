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
    public class MainController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Main
        public ActionResult Index()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                return View();
            }
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        
        public ActionResult Quantri()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                return View();
            }
                else
                {
                    Response.StatusCode = 403;
                    return null;
                }
        }

        public ActionResult Phongban()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                var pb = data.PHONGBANs
                .ToList()
                .OrderBy(s => s.ID_PHONGBAN);
                return PartialView(pb);
            }
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        public ActionResult Loainhanvien()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                var loainv = data.LOAINHANVIENs
                    .ToList()
                    .OrderBy(s => s.ID_LOAINHANVIEN);
                return PartialView(loainv);
            }
            else
            {
                Response.StatusCode = 403;
                return null;
            }
}

        public ActionResult Loaichinhanh()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                var cn = data.LOAICNs
                .ToList()
                .OrderBy(s => s.ID_LOAICN);
                return PartialView(cn);
            }
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        public ActionResult Nhanvien()
        {
            if (int.Parse(Session["Quyen"].ToString()) == 1)
            {
                var nv = data.NHANVIENs.ToList().Where(c => c.TRANGTHAI == true).OrderBy(s => s.ID_NHANVIEN);
            return View(nv);
        }
            else
            {
                Response.StatusCode = 403;
                return null;
            }
}
        

        public ActionResult Themnhanvien()
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        ViewData["Phongban2"] = new SelectList(data.PHONGBANs.ToList().OrderBy(s => s.TEN), "ID_PHONGBAN", "TEN");
            ViewData["Loainhanvien2"] = new SelectList(data.LOAINHANVIENs.ToList().OrderBy(s => s.TEN), "ID_LOAINHANVIEN", "TEN");
            return PartialView();
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        public bool kiemtrauser(string name)
        {
            var a = data.NHANVIENs.FirstOrDefault(s => s.USER == name);
            if (a == null)
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        public ActionResult Themnhanvien(NHANVIEN nv, FormCollection collection)
        {
            try
            {
                ViewData["Phongban2"] = new SelectList(data.PHONGBANs.ToList().OrderBy(s => s.TEN), "ID_PHONGBAN", "TEN");
                ViewData["Loainhanvien2"] = new SelectList(data.LOAINHANVIENs.ToList().OrderBy(s => s.TEN), "ID_LOAINHANVIEN", "TEN");
                var c = collection["Phongban2"];
                var b = collection["Loainhanvien2"];
                
                    nv.ID_PHONGBAN = int.Parse(c);
                    nv.ID_LOAINHANVIEN = int.Parse(b);
                    nv.NGAYTAO = DateTime.Now;
                    nv.NGUOITAO = int.Parse(Session["ID"].ToString());
                    nv.PASS = "e10adc3949ba59abbe56e057f20f883e";//pass 123456
                    nv.TRANGTHAI = true;
                    nv.ID_PHANQUYEN = 2;
                nv.ANH = "user.jpg";
                    data.NHANVIENs.InsertOnSubmit(nv);
                    data.SubmitChanges();
                    return RedirectToAction("Themnhanvien");
                


}
            catch
            {
                ViewData["Phongban2"] = new SelectList(data.PHONGBANs.ToList().OrderBy(s => s.TEN), "ID_PHONGBAN", "TEN");
                ViewData["Loainhanvien2"] = new SelectList(data.LOAINHANVIENs.ToList().OrderBy(s => s.TEN), "ID_LOAINHANVIEN", "TEN");
                return View();
            }
        }
        public string GenerateMD5(string yourString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
        }
        //sua nhan vien
        public ActionResult SuaNhanvien(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var tt = data.NHANVIENs.First(s => s.ID_NHANVIEN == id);
            ViewData["phongban1204"] = new SelectList(data.PHONGBANs, "ID_PHONGBAN", "TEN", tt.ID_PHONGBAN);
            ViewData["loainhanvien1204"] = new SelectList(data.LOAINHANVIENs, "ID_LOAINHANVIEN", "TEN", tt.ID_LOAINHANVIEN);
            ViewData["phanquyen1204"] = new SelectList(data.PHANQUYENs, "ID_PHANQUYEN", "TEN", tt.ID_PHANQUYEN);
            return View(tt);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        // POST: NhanCMNVien/Edit/5
        [HttpPost]
        public ActionResult SuaNhanvien(int id, FormCollection collection)
        {
            try
            {
                NHANVIEN nvs = data.NHANVIENs.SingleOrDefault(s => s.ID_NHANVIEN == id);
                ViewData["phongban1204"] = new SelectList(data.PHONGBANs, "ID_PHONGBAN", "TEN", nvs.ID_PHONGBAN);
                ViewData["loainhanvien1204"] = new SelectList(data.LOAINHANVIENs, "ID_LOAINHANVIEN", "TEN", nvs.ID_LOAINHANVIEN);
                ViewData["phanquyen1204"] = new SelectList(data.PHANQUYENs, "ID_PHANQUYEN", "TEN", nvs.ID_PHANQUYEN);
                var pb = collection["phongban1204"];
                var lnv = collection["loainhanvien1204"];
                var pq = collection["phanquyen1204"];
                var repass = collection["Repass"];
                var pass12 = collection["PASS12"];
                if (string.IsNullOrEmpty(pass12) || string.IsNullOrEmpty(repass))
                {
                    nvs.NGAYSUA = DateTime.Now;
                    nvs.ID_PHONGBAN = int.Parse(pb);
                    nvs.ID_LOAINHANVIEN = int.Parse(lnv);
                    nvs.ID_PHANQUYEN = int.Parse(pq);
                    UpdateModel(nvs);
                    data.SubmitChanges();
                    return RedirectToAction("Nhanvien");
                }else
                {
                    if (repass != pass12)
                    {
                        ViewData["ErrorNewPass"] = "Không trùng nhau";
                        ViewData["ErrorReNewPass"] = "Không trùng nhau";
                        return View(nvs);
                    }
                    if (repass.Length < 6 || pass12.Length < 6)
                    {
                        ViewData["ErrorNewPass"] = "Không được ít hơn 6 ký tự";
                        ViewData["ErrorReNewPass"] = "Không được ít hơn 6 ký tự";
                        return View(nvs);
                    }
                    nvs.NGAYSUA = DateTime.Now;
                    nvs.ID_PHONGBAN = int.Parse(pb);
                    nvs.ID_LOAINHANVIEN = int.Parse(lnv);
                    nvs.ID_PHANQUYEN = int.Parse(pq);
                    nvs.PASS = GenerateMD5(pass12);
                    UpdateModel(nvs);
                    data.SubmitChanges();
                    return RedirectToAction("Nhanvien");
                }
    
            }
            catch
            {
                NHANVIEN tt = data.NHANVIENs.SingleOrDefault(s => s.ID_NHANVIEN == id);
                ViewData["phongban1204"] = new SelectList(data.PHONGBANs, "ID_PHONGBAN", "TEN", tt.ID_PHONGBAN);
                ViewData["loainhanvien1204"] = new SelectList(data.LOAINHANVIENs, "ID_LOAINHANVIEN", "TEN", tt.ID_LOAINHANVIEN);
                ViewData["phanquyen1204"] = new SelectList(data.PHANQUYENs, "ID_PHANQUYEN", "TEN", tt.ID_PHANQUYEN);
                return View(tt);
            }
        }
        public ActionResult Themchinhanh()
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        return PartialView();
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Themchinhanh(LOAICN cn)
        {
            try
            {
                // TODO: Add insert logic here
                //cn.NGAYTAO = DateTime.Now;
                //cn.TRANGTHAI = true;
                data.LOAICNs.InsertOnSubmit(cn);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Themphongban()
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        return PartialView();
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Themphongban(PHONGBAN pb)
        {
            try
            {
                // TODO: Add insert logic here
                pb.NGAYTAO = DateTime.Now;
                pb.TRANGTHAI = true;
                data.PHONGBANs.InsertOnSubmit(pb);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Themloainhanvien()
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        return PartialView();
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Themloainhanvien(LOAINHANVIEN lnv)
        {
            try
            {
                // TODO: Add insert logic here
                lnv.NGAYTAO = DateTime.Now;
                lnv.TRANGTHAI = true;
                data.LOAINHANVIENs.InsertOnSubmit(lnv);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }
                
        public ActionResult Suaphongban(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var spb = data.PHONGBANs.FirstOrDefault(s => s.ID_PHONGBAN == id);
            return PartialView(spb);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Suaphongban(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                PHONGBAN pb = data.PHONGBANs.FirstOrDefault(s => s.ID_PHONGBAN == id);
                pb.NGAYSUA = DateTime.Now;
                UpdateModel(pb);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoaphongban(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var xpb = data.PHONGBANs
                .FirstOrDefault(s => s.ID_PHONGBAN == id);
            return PartialView(xpb);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult Xoaphongban(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PHONGBAN pb = data.PHONGBANs.FirstOrDefault(s => s.ID_PHONGBAN == id);
                data.PHONGBANs.DeleteOnSubmit(pb);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Suachinhanh(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var scn = data.LOAICNs.FirstOrDefault(s => s.ID_LOAICN == id);
            return PartialView(scn);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Suachinhanh(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                LOAICN cn = data.LOAICNs.FirstOrDefault(s => s.ID_LOAICN == id);
                cn.NGAYSUA = DateTime.Now;
                UpdateModel(cn);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoachinhanh(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var a = data.LOAICNs
                .FirstOrDefault(s => s.ID_LOAICN == id);
            return PartialView(a);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult Xoachinhanh(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                LOAICN a = data.LOAICNs.FirstOrDefault(s => s.ID_LOAICN == id);
                data.LOAICNs.DeleteOnSubmit(a);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Sualoainhanvien(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var a = data.LOAINHANVIENs.FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
            return PartialView(a);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult Sualoainhanvien(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                LOAINHANVIEN a = data.LOAINHANVIENs.FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
                a.NGAYSUA = DateTime.Now;
                UpdateModel(a);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoaloainhanvien(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var a = data.LOAINHANVIENs
                .FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
            return PartialView(a);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult Xoaloainhanvien(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                LOAINHANVIEN a = data.LOAINHANVIENs.FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
                data.LOAINHANVIENs.DeleteOnSubmit(a);
                data.SubmitChanges();
                return RedirectToAction("Quantri");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Menu()
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var menu = data.MENUs.ToList();
            return View(menu);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        public ActionResult Submenu(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var submenu = data.SUBMENUs.ToList().Where(s => s.ID_MENU == id);
            return PartialView(submenu);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }

        public ActionResult SuaMenu(int id)
        {
    if (int.Parse(Session["Quyen"].ToString()) == 1)
    {
        var sua = data.MENUs.FirstOrDefault(s => s.ID_MENU == id);
            return View(sua);
}
            else
            {
                Response.StatusCode = 403;
                return null;
            }
        }
        [HttpPost]
        public ActionResult SuaMenu(int id, FormCollection col)
        {
            MENU sua = data.MENUs.FirstOrDefault(s => s.ID_MENU == id);
            if (int.Parse(col["ADMIN"].ToString()) == 1)
            {
                sua.ADMIN = true;
            }
            else
                sua.ADMIN = false;
            
            TryUpdateModel(sua);
            data.SubmitChanges();
            return RedirectToAction("SuaMenu", "Main", new { id = id });
        }

    }
}