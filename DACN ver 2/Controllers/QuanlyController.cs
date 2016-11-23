using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN_ver_2.Models;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class QuanlyController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Quanly
        public ActionResult Danhmuc()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        // Loai tai san
        public ActionResult Loaitaisan()
        {
            var loaits = data.LOAITAISANs
                .ToList()
                .OrderBy(s => s.ID_LOAITAISAN);
            return PartialView(loaits);
        }
        // Them loai tai san
        public ActionResult ThemLoaitaisan()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ThemLoaitaisan(LOAITAISAN a)
        {
            try
            {
                // TODO: Add insert logic here
                a.NGAYTAO = DateTime.Now;
                a.TRANGTHAI = true;
                data.LOAITAISANs.InsertOnSubmit(a);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }
        //sua loai tai san

        public ActionResult SuaLoaitaisan(int id)
        {
            var spb = data.LOAITAISANs.FirstOrDefault(s => s.ID_LOAITAISAN == id);
            return PartialView(spb);
        }
        [HttpPost]
        public ActionResult SuaLoaitaisan(int id, FormCollection collection)
        {
            try
            {
                LOAITAISAN pb = data.LOAITAISANs.FirstOrDefault(s => s.ID_LOAITAISAN == id);
                pb.NGAYSUA = DateTime.Now;
                UpdateModel(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }

        //xoa loai tai san
        public ActionResult XoaLoaitaisan(int id)
        {
            var xpb = data.LOAITAISANs
                .FirstOrDefault(s => s.ID_LOAITAISAN == id);
            return PartialView(xpb);
        }

        
        [HttpPost]
        public ActionResult XoaLoaitaisan(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                LOAITAISAN pb = data.LOAITAISANs.FirstOrDefault(s => s.ID_LOAITAISAN == id);
                data.LOAITAISANs.DeleteOnSubmit(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }

        //loai thong tin
        public ActionResult Loaithongtin()
        {
            var loaitt = data.LOAITHONGTINs
                .ToList()
                .OrderBy(s => s.ID_LTT);
            return PartialView(loaitt);
        }
        // Them loai tai san
        public ActionResult ThemLoaithongtin()
        {
            ViewData["lts"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN");
            return PartialView();
        }
        [HttpPost]
        public ActionResult ThemLoaithongtin(LOAITHONGTIN a, FormCollection col)
        {
            try
            {
                // TODO: Add insert logic here
                a.ID_LOAITAISAN = int.Parse(col["lts"]);
                a.NGAYTAO = DateTime.Now;
                a.TRANGTHAI = true;
                data.LOAITHONGTINs.InsertOnSubmit(a);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return PartialView();
            }
        }

        //sua loai thong tin

        public ActionResult SuaLoaithongtin(int id)
        {
            var spb = data.LOAITHONGTINs.FirstOrDefault(s => s.ID_LTT == id);
            ViewData["lts"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN", spb.ID_LOAITAISAN);
            return PartialView(spb);
        }
        [HttpPost]
        public ActionResult SuaLoaithongtin(int id, FormCollection collection)
        {
            try
            {

                LOAITHONGTIN pb = data.LOAITHONGTINs.FirstOrDefault(s => s.ID_LTT == id);
                pb.ID_LOAITAISAN = int.Parse(collection["lts"]);
                pb.NGAYSUA = DateTime.Now;
                UpdateModel(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }

        //xoa loai thong tin
        public ActionResult XoaLoaithongtin(int id)
        {
            var xpb = data.LOAITHONGTINs
                .FirstOrDefault(s => s.ID_LTT == id);
            return PartialView(xpb);
        }


        [HttpPost]
        public ActionResult XoaLoaithongtin(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                LOAITHONGTIN pb = data.LOAITHONGTINs.FirstOrDefault(s => s.ID_LTT == id);
                data.LOAITHONGTINs.DeleteOnSubmit(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Loaihinh()
        {
            var loaihinh = data.LOAIHINHs
                .ToList()
                .OrderBy(s => s.ID_LOAIHINH);
            return PartialView(loaihinh);
        }        

        public ActionResult SuaLoaihinh (int id)
        {
            var d = data.LOAIHINHs.FirstOrDefault(s => s.ID_LOAIHINH == id);
            return PartialView(d);
        }
        [HttpPost]
        public ActionResult SuaLoaihinh(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                LOAIHINH d = data.LOAIHINHs.FirstOrDefault(s => s.ID_LOAIHINH == id);
                d.NGAYSUA = DateTime.Now;
                UpdateModel(d);
                data.SubmitChanges();
                return RedirectToAction("Danhmuc");
            }
            catch
            {
                return View();
            }
        }
    }
}