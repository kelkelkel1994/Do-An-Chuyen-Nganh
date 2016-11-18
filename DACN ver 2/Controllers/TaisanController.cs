using DACN_ver_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class TaisanController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        
        // GET: Taisan/Create
        public ActionResult ThemCHCC()
        {
            var tp = data.TINHTHANHs.OrderBy(p => p.TEN);
            ViewBag.Tentp = tp;
            ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
            ViewData["tiendo12"] = new SelectList(data.TIENDOs, "ID_TIENDO", "TEN");
            ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["chieurongmatduong12"] = new SelectList(data.CHIEURONGMATDUONGs, "ID_CRMD", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies.Where(s => s.ID_LOAITAISAN == 2), "ID_DDPL", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s => s.ID_LOAITAISAN == 2), "ID_LTT", "TEN");
            ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs.Where(s => s.ID_LOAITAISAN == 2), "ID_CHITIETLOAI", "TEN");
            ViewData["capduong12"] = new SelectList(data.CAPDUONGs, "ID_CAPDUONG", "TEN");
            ViewData["ketcau12"] = new SelectList(data.KETCAUDUONGs, "ID_KETCAUDUONG", "TEN");
            ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
            return View();
        }

        // POST: Taisan/Create
        [HttpPost]
        public ActionResult ThemCHCC(FormCollection collection,CANHOCHUNGCU chcc)
        {
            try
            {
                // TODO: Add insert logic here
                chcc.NGUOILAPPHIEU = int.Parse(collection["nlp12"]);
                chcc.NGUOIKIEMDUYET = int.Parse(collection["nkd12"]);
                chcc.ID_DDPL = int.Parse(collection["ddpl12"]);
                chcc.ID_TIENDO = int.Parse(collection["tiendo12"]);
                var qh = collection["qqqq"];
                chcc.ID_QUANHUYEN = int.Parse(qh);
                chcc.ID_LOAIHINH = int.Parse(collection["loaihinh12"]);
                chcc.ID_LTT = int.Parse(collection["loaithongtin12"]);
                chcc.ID_CHITIETLOAI = int.Parse(collection["chitietloai12"]);
                chcc.ID_CAPDUONG = int.Parse(collection["capduong12"]);
                chcc.ID_KETCAUDUONG = int.Parse(collection["ketcau12"]);
                chcc.ID_CRMD = int.Parse(collection["chieurongmatduong12"]);
                chcc.SOHIEU = collection["SOHIEU"] + "/2016/PTT-CHCC";
                chcc.NGAYTAO = DateTime.Now;
                data.CANHOCHUNGCUs.InsertOnSubmit(chcc);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ThemDAT()
        {
            var tp = data.TINHTHANHs.OrderBy(p => p.TEN);
            ViewBag.Tentp = tp;
            ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
            ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies.Where(s => s.ID_LOAITAISAN == 1), "ID_DDPL", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s => s.ID_LOAITAISAN == 1), "ID_LTT", "TEN");
            ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs, "ID_CHITIETLOAI", "TEN");
            ViewData["capduong12"] = new SelectList(data.CAPDUONGs, "ID_CAPDUONG", "TEN");
            ViewData["ketcau12"] = new SelectList(data.KETCAUDUONGs, "ID_KETCAUDUONG", "TEN");
            ViewData["chieurongmatduong12"] = new SelectList(data.CHIEURONGMATDUONGs, "ID_CRMD", "TEN");
            ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
            return View();
        }

        // POST: Dat/Create
        [HttpPost]
        public ActionResult ThemDAT(FormCollection collection, DAT dat)
        {
            try
            {
                // TODO: Add insert logic here
                dat.NGUOILAPPHIEU = int.Parse(collection["nlp12"]);
                dat.NGUOIKIEMDUYET = int.Parse(collection["nkd12"]);
                dat.ID_DDPL = int.Parse(collection["ddpl12"]);
                var qh = collection["qqqq"];
                dat.ID_QUANHUYEN = int.Parse(qh);
                dat.ID_LOAIHINH = int.Parse(collection["loaihinh12"]);
                dat.ID_LTT = int.Parse(collection["loaithongtin12"]);
                dat.ID_CHITIETLOAI = int.Parse(collection["chitietloai12"]);
                dat.ID_CAPDUONG = int.Parse(collection["capduong12"]);
                dat.ID_KETCAUDUONG = int.Parse(collection["ketcau12"]);
                dat.ID_CRMD = int.Parse(collection["chieurongmatduong12"]);
                dat.SOHIEU = collection["SOHIEU"] + "/2016/PTT-DAT";
                dat.NGAYTAO = DateTime.Now;
                data.DATs.InsertOnSubmit(dat);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
                ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
                ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
                ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
                ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
                ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies.Where(s => s.ID_LOAITAISAN == 1), "ID_DDPL", "TEN");
                ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s => s.ID_LOAITAISAN == 1), "ID_LTT", "TEN");
                ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs, "ID_CHITIETLOAI", "TEN");
                ViewData["capduong12"] = new SelectList(data.CAPDUONGs, "ID_CAPDUONG", "TEN");
                ViewData["ketcau12"] = new SelectList(data.KETCAUDUONGs, "ID_KETCAUDUONG", "TEN");
                ViewData["chieurongmatduong12"] = new SelectList(data.CHIEURONGMATDUONGs, "ID_CRMD", "TEN");
                ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
                return View();
            }
        }

        //Them văn phong cho thue
        public ActionResult ThemVPCT()
        {
            var tp = data.TINHTHANHs.OrderBy(p => p.TEN);
            ViewBag.Tentp = tp;
            ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
            ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s => s.ID_LOAITAISAN == 3), "ID_LTT", "TEN");
            ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs.Where(s => s.ID_LOAITAISAN == 3), "ID_CHITIETLOAI", "TEN");
            ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
            return View();
        }
        
        [HttpPost]
        public ActionResult ThemVPCT(FormCollection collection, VANPHONGCHOTHUE vp)
        {
            try
            {
                // TODO: Add insert logic here
                vp.NGUOILAPPHIEU = int.Parse(collection["nlp12"]);
                vp.NGUOIKIEMDUYET = int.Parse(collection["nkd12"]);
                var qh = collection["qqqq"];
                vp.ID_QUANHUYEN = int.Parse(qh);
                vp.ID_LOAIHINH = int.Parse(collection["loaihinh12"]);
                vp.ID_LTT = int.Parse(collection["loaithongtin12"]);
                vp.ID_CHITIETLOAI = int.Parse(collection["chitietloai12"]);
                vp.SOHIEU = collection["SOHIEU"] + "/2016/PTT-CHCC";
                vp.NGAYTAO = DateTime.Now;
                data.VANPHONGCHOTHUEs.InsertOnSubmit(vp);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
                ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
                ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
                ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
                ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s => s.ID_LOAITAISAN == 3), "ID_LTT", "TEN");
                ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs.Where(s => s.ID_LOAITAISAN == 3), "ID_CHITIETLOAI", "TEN");
                ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
                return View();
            }
        }

    }
}
