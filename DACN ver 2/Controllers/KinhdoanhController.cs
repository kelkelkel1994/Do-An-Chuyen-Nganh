using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;
using System.IO;

namespace DACN_ver_2.Controllers
{
    public class KinhdoanhController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ThemPYC()
        {
            ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s=>s.TENNV).Where(s=>s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV");
            ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH");
            ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN");
            return View();
        }

        // POST: Kinhdoanh/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPYC(FormCollection collection, PHIEUYEUCAU pyc)
        {
            try
            {
                //TODO: Add insert logic here
                var nv = collection["Nhanvien3"];
                var lts = collection["Loaitaisan3"];
                var kh = collection["Khachhang3"];
                //no lỗi ngay đây. nếu chặn lại thì ko sao.
                pyc.SOPYC = collection["SOPYC"] + "/2016/PYC-AMAX";
                pyc.NGAYTAO = DateTime.Now;
                pyc.TRANGTHAI = true;
                pyc.ID_NHANVIEN = int.Parse(nv);
                pyc.ID_LOAITAISAN = int.Parse(lts);
                pyc.ID_KH = int.Parse(kh);
                data.PHIEUYEUCAUs.InsertOnSubmit(pyc);
                data.SubmitChanges();
                return RedirectToAction("ThemPYC");
            }
            catch
            {

                ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV");
                ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH");
                ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN");
                ViewBag.Thongbao = "0";
                return View();
            }
        }

        //xem PYC
        public ActionResult XemPYC (int id)
        {
            var detail = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_PYC == id).Count();
            return View(detail);
        }
        //them file dinh kem
        public ActionResult ThemfilePYC(int id)
        {
            var d = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
            return View(d);
        }
        
        public ActionResult SaveDropzoneJsUploadedFiles(int id, FILEDINHKEM dk)
        {
            string fName = "";

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //You can Save the file content here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
                    var originalDirectory = new DirectoryInfo(string.Format("{0}FilePYC\\" + id, Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString()/*, pyc.ID_PYC.ToString()*/);

                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                    dk.LIENKET = path;
                    dk.ID_PYC = id;
                    dk.TENFILE = file.FileName;
                    dk.NGAYTAO = DateTime.Now;
                    dk.TRANGTHAI = true;
                    data.FILEDINHKEMs.InsertOnSubmit(dk);
                    data.SubmitChanges();
                    
            }
            }

            return Json(new { Message = string.Empty });
        }

        public ActionResult DanhsachFilePYC(int id)
        {
            var sa = data.FILEDINHKEMs.ToList().Where(s => s.ID_PYC == id).OrderBy(s => s.ID_FILEDINHKEM);
            return PartialView(sa);
        }
        //sua PYC
        public ActionResult SuaPYC(int id)
        {
            var spb = data.PHONGBANs.FirstOrDefault(s => s.ID_PHONGBAN == id);
            return PartialView(spb);
        }
        [HttpPost]
        public ActionResult SuaPYC(int id, FormCollection collection)
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

        //Danh sach PYC
        public ActionResult DanhsachPYC()
        {
            var pyc = data.PHIEUYEUCAUs.ToList();
            return View(pyc);
        }


        //Thêm Chứng thư
        public ActionResult ThemCT()
        {
            ViewData["hd"] = new SelectList(data.HOPDONGs.ToList().OrderBy(s => s.SOHD), "ID_HOPDONG", "SOHD");
            ViewData["pyc"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderByDescending(s => s.NGAYVIETPHIEU), "ID_PYC", "SOPYC");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemCT(FormCollection collection, CHUNGTHUTDG ct)
        {
            try
            {
                // TODO: Add insert logic here
                var hd = collection["hd"];
                var pyc = collection["pyc"];
                ct.SOCHUNGTHU = collection["SOCHUNGTHU"] + "/2016/CTTDG-AMAX";
                ct.ID_HOPDONG = int.Parse(hd);
                ct.ID_PYC = int.Parse(pyc);
                ct.NGAYTAO = DateTime.Now;
                ct.TRANGTHAI = true;
                data.CHUNGTHUTDGs.InsertOnSubmit(ct);
                data.SubmitChanges();
                return RedirectToAction("ThemCT");
            }
            catch
            {
                ViewData["hd"] = new SelectList(data.HOPDONGs.ToList().OrderBy(s => s.SOHD), "ID_HOPDONG", "SOHD");
                ViewData["pyc"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderByDescending(s => s.NGAYVIETPHIEU), "ID_PYC", "SOPYC");
                return View();
            }
        }

        //danhsach chung thu
        public ActionResult DanhsachCT()
        {
            var ct = data.CHUNGTHUTDGs.ToList();
            return View(ct);
        }

        //thêm hợp đồng
        public ActionResult ThemHD()
        {
            ViewData["loaihd"] = new SelectList(data.LOAIHOPDONGs.ToList().OrderBy(s => s.TEN), "ID_LOAIHOPDONG", "TEN");
            ViewData["loaitrangthai"] = new SelectList(data.TRANGTHAIs.ToList().OrderBy(s => s.TEN), "ID_TRANGTHAI", "TEN");
            ViewData["kinhdoanh"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 3), "ID_NHANVIEN", "TENNV");
            ViewData["pyc"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemHD(FormCollection collection, HOPDONG hd)
        {
            try
            {
                // TODO: Add insert logic here
                var lhd = collection["loaihd"];
                var ltt = collection["loaitrangthai"];
                var kd = collection["kinhdoanh"];
                var pyc = collection["pyc"];
                hd.SOHD = collection["SOHOPDONG"] + "/2016/HDTDG-AMAX";
                hd.ID_LOAIHOPDONG = int.Parse(lhd);
                hd.ID_TRANGTHAI = int.Parse(ltt);
                hd.ID_NHANVIEN = int.Parse(kd);
                hd.ID_PYC = int.Parse(pyc);
                hd.NGAYTAO = DateTime.Now;
                hd.TRANGTHAI = true;
                data.HOPDONGs.InsertOnSubmit(hd);
                data.SubmitChanges();
                return RedirectToAction("ThemHD");
            }
            catch
            {
                ViewData["loaihd"] = new SelectList(data.LOAIHOPDONGs.ToList().OrderBy(s => s.TEN), "ID_LOAIHOPDONG", "TEN");
                ViewData["loaitrangthai"] = new SelectList(data.TRANGTHAIs.ToList().OrderBy(s => s.TEN), "ID_TRANGTHAI", "TEN");
                ViewData["kinhdoanh"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 3), "ID_NHANVIEN", "TENNV");
                ViewData["pyc"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC");
                return View();
            }
        }
        
        // danh sách hợp đồng
        public ActionResult DanhsachHD()
        {
            var hd = data.HOPDONGs.ToList().OrderBy(s => s.ID_HOPDONG);
            return View(hd);
        }

        // danh sahv khach hang
        public ActionResult Danhsachkhachhang()
        {
            var danhsach = data.KHACHHANGs
                .ToList()
                .OrderBy(s => s.ID_KH);
            return View(danhsach);
        }

        public ActionResult Themkhachhang()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Themkhachhang(KHACHHANG kh)
        {
            try
            {
                // TODO: Add insert logic here
                kh.NGAYTAO = DateTime.Now;
                kh.TRANGTHAI = true;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Danhsachkhachhang");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Suakhachhang(int id)
        {
            var spb = data.KHACHHANGs.FirstOrDefault(s => s.ID_KH == id);
            return PartialView(spb);
        }
        [HttpPost]
        public ActionResult Suakhachhang(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                KHACHHANG pb = data.KHACHHANGs.FirstOrDefault(s => s.ID_KH == id);
                pb.NGAYSUA = DateTime.Now;
                UpdateModel(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhsachkhachhang");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoakhachhang(int id)
        {
            var xpb = data.KHACHHANGs
                .FirstOrDefault(s => s.ID_KH == id);
            return PartialView(xpb);
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult Xoakhachhang(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                KHACHHANG pb = data.KHACHHANGs.FirstOrDefault(s => s.ID_KH == id);
                data.KHACHHANGs.DeleteOnSubmit(pb);
                data.SubmitChanges();
                return RedirectToAction("Danhsachkhachhang");
            }
            catch
            {
                return View();
            }
        }

    }
}
