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
        public ActionResult SetMenu(int id)
        {
            MENU menu = data.MENUs.SingleOrDefault(a => a.ID_MENU == id);
            ViewData["admin1"] = data.MENUs.SingleOrDefault(a => a.ID_MENU == id).ADMIN.Value;
            ViewData["kinhdoanh1"] = data.MENUs.SingleOrDefault(a => a.ID_MENU == id).KINHDOANH.Value;
            ViewData["thamdinh1"] = data.MENUs.SingleOrDefault(a => a.ID_MENU == id).THAMDINH.Value;
            return View(menu);
        }
        [HttpPost]
        public ActionResult SetMenu(int id, FormCollection collection)
        {

            var a1 = collection["admin1"].Contains("true");
            var b1 = collection["kinhdoanh1"].Contains("true");
            var c1 = collection["thamdinh1"].Contains("true");
            MENU menu = data.MENUs.SingleOrDefault(a => a.ID_MENU == id);
            menu.ADMIN = Convert.ToBoolean(a1);
            menu.KINHDOANH = Convert.ToBoolean(b1);
            menu.THAMDINH = Convert.ToBoolean(c1);


            UpdateModel(menu);
            data.SubmitChanges();
            return RedirectToAction("SetMenu", "Quanly", new { id = id });

        }

        public ActionResult SetSubMenu(int id)
        {
            SUBMENU submenu = data.SUBMENUs.SingleOrDefault(a => a.ID_SUBMENU == id);
            ViewData["admin12"] = data.SUBMENUs.SingleOrDefault(a => a.ID_SUBMENU == id).ADMIN.Value;
            ViewData["kinhdoanh12"] = data.SUBMENUs.SingleOrDefault(a => a.ID_SUBMENU == id).KINHDOANH.Value;
            ViewData["thamdinh12"] = data.SUBMENUs.SingleOrDefault(a => a.ID_SUBMENU == id).THAMDINH.Value;
            ViewData["tenmenu"] = new SelectList(data.MENUs.ToList().OrderBy(s => s.TEN), "ID_MENU", "TEN", submenu.ID_MENU);
            return View(submenu);
        }
        [HttpPost]

        public ActionResult SetSubMenu(int id, FormCollection collection)
        {
            var a1 = collection["admin12"].Contains("true");
            var b1 = collection["kinhdoanh12"].Contains("true");
            var c1 = collection["thamdinh12"].Contains("true");

            SUBMENU submenu = data.SUBMENUs.SingleOrDefault(a => a.ID_SUBMENU == id);
            ViewData["tenmenu"] = new SelectList(data.MENUs.ToList().OrderBy(s => s.TEN), "ID_MENU", "TEN", submenu.ID_MENU);

            submenu.ADMIN = Convert.ToBoolean(a1);
            submenu.KINHDOANH = Convert.ToBoolean(b1);
            submenu.THAMDINH = Convert.ToBoolean(c1);
            submenu.ID_MENU = int.Parse(collection["tenmenu"]);
            UpdateModel(submenu);
            data.SubmitChanges();
            return RedirectToAction("SetMenu", "Quanly", new { id = submenu.ID_MENU });
        }

        public ActionResult danhsachsub(int id)
        {
            var ds = data.SUBMENUs.ToList().Where(a => a.ID_MENU == id).OrderBy(a => a.ID_SUBMENU);
            return PartialView(ds);
        }
    }
}