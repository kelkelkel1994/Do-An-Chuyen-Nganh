using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;

namespace DACN_ver_2.Controllers
{
    public class MainController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Main
        public ActionResult Index()
        {
            if(Session["Login"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        
        public ActionResult Quantri()
        {
            return View();
        }

        public ActionResult Phongban()
        {
            var pb = data.PHONGBANs
                .ToList()
                .OrderBy(s => s.ID_PHONGBAN);
            return PartialView(pb);
        }

        public ActionResult Loainhanvien()
        {
            var loainv = data.LOAINHANVIENs
                .ToList()
                .OrderBy(s => s.ID_LOAINHANVIEN);
            return PartialView(loainv);
        }

        public ActionResult Loaichinhanh()
        {
            var cn = data.LOAICNs
                .ToList()
                .OrderBy(s => s.ID_LOAICN);
            return PartialView(cn);
        }
        public ActionResult Nhanvien()
        {
            var nv = data.NHANVIENs.ToList().Where(c => c.TRANGTHAI == true).OrderBy(s => s.ID_NHANVIEN);
            return View(nv);
        }
        

        public ActionResult Themnhanvien()
        {
            ViewData["Phongban2"] = new SelectList(data.PHONGBANs.ToList().OrderBy(s => s.TEN), "ID_PHONGBAN", "TEN");
            ViewData["Loainhanvien2"] = new SelectList(data.LOAINHANVIENs.ToList().OrderBy(s => s.TEN), "ID_LOAINHANVIEN", "TEN");
            return PartialView();
        }
        [HttpPost]
        public ActionResult Themnhanvien(NHANVIEN nv, FormCollection collection)
        {
            try
            {
            var c = collection["Phongban2"];
            var b = collection["Loainhanvien2"];
            nv.ID_PHONGBAN = int.Parse(c);
            nv.ID_LOAINHANVIEN = int.Parse(b);
                data.NHANVIENs.InsertOnSubmit(nv);
                data.SubmitChanges();
                return PartialView();
           }
            catch
            {
                return View();
            }
        }

        public ActionResult Themchinhanh()
        {
            return PartialView();
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
            return PartialView();
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
            return PartialView();
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
            var spb = data.PHONGBANs.FirstOrDefault(s => s.ID_PHONGBAN == id);
            return PartialView(spb);
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
            var xpb = data.PHONGBANs
                .FirstOrDefault(s => s.ID_PHONGBAN == id);
            return PartialView(xpb);
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
            var scn = data.LOAICNs.FirstOrDefault(s => s.ID_LOAICN == id);
            return PartialView(scn);
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
            var a = data.LOAICNs
                .FirstOrDefault(s => s.ID_LOAICN == id);
            return PartialView(a);
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
            var a = data.LOAINHANVIENs.FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
            return PartialView(a);
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
            var a = data.LOAINHANVIENs
                .FirstOrDefault(s => s.ID_LOAINHANVIEN == id);
            return PartialView(a);
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
            var menu = data.MENUs.ToList();
            return View(menu);
        }

        public ActionResult Submenu(int id)
        {
            var submenu = data.SUBMENUs.ToList().Where(s => s.ID_MENU == id);
            return PartialView(submenu);
        }

        public ActionResult SuaMenu(int id)
        {
            var sua = data.MENUs.FirstOrDefault(s => s.ID_MENU == id);
            return View(sua);
        }
        

    }
}