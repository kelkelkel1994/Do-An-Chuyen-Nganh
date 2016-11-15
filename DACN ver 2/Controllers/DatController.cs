﻿using DACN_ver_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DACN_ver_2.Controllers
{
    public class DatController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        public ActionResult Index()
        {
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            var d = data.DATs.ToList();
            return View(d);
        }

        // GET: Dat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dat/Create
        public ActionResult ThemDAT()
        {
            var tp = data.TINHTHANHs.OrderBy(p => p.TEN);
            ViewBag.Tentp = tp;
            ViewData["tp12"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
            ViewData["nlp12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["nkd12"] = new SelectList(data.NHANVIENs, "ID_NHANVIEN", "TENNV");
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies.Where(s=>s.ID_LOAITAISAN == 1), "ID_DDPL", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs.Where(s=>s.ID_LOAITAISAN == 1), "ID_LTT", "TEN");
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
                dat.ID_DDPL = int.Parse(collection["ddpl12"]);
                var qh = collection["qqqq"];
                dat.ID_QUANHUYEN = int.Parse(qh);
                dat.ID_LOAIHINH = int.Parse(collection["loaihinh12"]);
                dat.ID_LTT = int.Parse(collection["loaithongtin12"]);
                dat.ID_CHITIETLOAI = int.Parse(collection["chitietloai12"]);
                dat.ID_CAPDUONG = int.Parse(collection["capduong12"]);
                dat.ID_KETCAUDUONG = int.Parse(collection["ketcau12"]);
                dat.ID_CRMD = int.Parse(collection["chieurongmatduong12"]);
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

        // GET: Dat/Edit/5
        public ActionResult Edit(int id)
        {
            var dat = data.DATs.First(d => d.ID_DAT == id);
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs, "ID_LTT", "TEN");
            ViewData["chitietloai12"] = new SelectList(data.CHITIETLOAIs, "ID_CHITIETLOAI", "TEN");
            ViewData["capduong12"] = new SelectList(data.CAPDUONGs, "ID_CAPDUONG", "TEN");
            ViewData["ketcau12"] = new SelectList(data.KETCAUDUONGs, "ID_KETCAUDUONG", "TEN");
            ViewData["chieurongmatduong12"] = new SelectList(data.CHIEURONGMATDUONGs, "ID_CRMD", "TEN");
            ViewData["quanhuyen12"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
            return View(dat);
        }

        // POST: Dat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var dat = data.DATs.First(d => d.ID_DAT == id);
                dat.ID_DDPL = int.Parse(collection["ddpl12"]);
                dat.ID_LOAIHINH = int.Parse(collection["loaihinh12"]);
                dat.ID_LTT = int.Parse(collection["loaithongtin12"]);
                dat.ID_CHITIETLOAI = int.Parse(collection["chitietloai12"]);
                dat.ID_CAPDUONG = int.Parse(collection["capduong12"]);
                dat.ID_KETCAUDUONG = int.Parse(collection["ketcau12"]);
                dat.ID_CRMD = int.Parse(collection["chieurongmatduong12"]);
                //dat.ID_QUANHUYEN = int.Parse(collection["quanhuyen12"]);
                dat.NGAYSUA = DateTime.Now;
                UpdateModel(dat);
                data.SubmitChanges();
                return RedirectToAction("LayoutTimKiem");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dat/Delete/5
        public ActionResult Delete(int id)
        {
            DAT dat = data.DATs.SingleOrDefault(m => m.ID_DAT == id);
            data.DATs.DeleteOnSubmit(dat);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LayoutTimKiem()
        {
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs, "ID_LTT", "TEN");
            List<DAT> DatList = data.DATs.ToList();
            return PartialView(DatList);
        }


        public ActionResult TimKiem(FormCollection collection)
        {
            ViewData["ddpl12"] = new SelectList(data.DACDIEMPHAPLies, "ID_DDPL", "TEN");
            ViewData["loaihinh12"] = new SelectList(data.LOAIHINHs, "ID_LOAIHINH", "TEN");
            ViewData["loaithongtin12"] = new SelectList(data.LOAITHONGTINs, "ID_LTT", "TEN");
            List<DAT> DatList;
            //if (String.IsInterned(collection["loaihinh12"]) && String.IsInterned(collection["ddpl12"]))
            //{

            //}
            if (String.IsNullOrEmpty(collection["loaihinh12"]))
            {

                DatList = data.DATs.Where(d => d.ID_DDPL == int.Parse(collection["ddpl12"])).ToList();
                if (DatList.Count == 0) { ViewBag.ThongBao = "Không tìm thấy sản phẩm nào"; }

            }
            else
            {

                DatList = data.DATs.Where(d => d.ID_DDPL == int.Parse(collection["ddpl12"])
                                    && d.ID_LOAIHINH == int.Parse(collection["loaihinh12"])).ToList();
                if (DatList.Count == 0) { ViewBag.ThongBao = "Không tìm thấy sản phẩm nào"; }

            }
            ViewBag.ThongBao = "Đã tìm thấy" + DatList.Count + "kết quả";
            return View(DatList);

        }
    }
}
