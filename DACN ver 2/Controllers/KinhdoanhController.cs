using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;
using System.IO;
using Rotativa.Options;
using Rotativa;

namespace DACN_ver_2.Controllers
{
    public class KinhdoanhController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ThemPYC()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV");
            ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH");
            ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN");
            return View();
        }

        // POST: Kinhdoanh/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPYC(FormCollection collection, PHIEUYEUCAU pyc, THONGBAO tb)
        {
            try
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
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
                var id = data.PHIEUYEUCAUs.FirstOrDefault(s => s.SOPYC == pyc.SOPYC);
                tb.ID_PYC = id.ID_PYC;
                tb.ID_NGUOIGUI = 1;
                tb.ID_NGUOINHAN = int.Parse(nv);
                tb.NOIDUNG = "Bạn được giao: " + collection["SOPYC"] + "/2016/PYC-AMAX";
                tb.NGAYGUI = DateTime.Now;
                tb.TRANGTHAIXEM = false;
                tb.TRANGTHAI = true;
                data.THONGBAOs.InsertOnSubmit(tb);
                data.SubmitChanges();
                return RedirectToAction("ThemPYC");
            }
            catch
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV");
                ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH");
                ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN");
                ViewBag.Thongbao = "0";
                return View();
            }
        }

        //xem PYC
        public ActionResult XemPYC(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var detail = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
            if (detail == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_PYC == id).Count();
            return View(detail);
        }
        //them file dinh kem
        public ActionResult ThemfilePYC(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var d = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
            return View(d);
        }

        public ActionResult SaveDropzoneJsUploadedFiles(int id, FILEDINHKEM dk)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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
                    var luu = "~/FilePYC/" + id + "/" + file.FileName;
                    file.SaveAs(path);
                    dk.LIENKET = luu;
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var sa = data.FILEDINHKEMs.ToList().Where(s => s.ID_PYC == id).OrderBy(s => s.ID_FILEDINHKEM);
            return PartialView(sa);
        }
        //sua PYC
        public ActionResult SuaPYC(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_PYC == id).Count();
            ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV", pyc.ID_NHANVIEN);
            ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH", pyc.ID_KH);
            ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN", pyc.ID_LOAITAISAN);
            return View(pyc);
        }
        [HttpPost]
        public ActionResult SuaPYC(int id, FormCollection collection)
        {
            try
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                // TODO: Add update logic here
                PHIEUYEUCAU pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
                var nv = collection["Nhanvien3"];
                var lts = collection["Loaitaisan3"];
                var kh = collection["Khachhang3"];
                pyc.ID_NHANVIEN = int.Parse(nv);
                pyc.ID_KH = int.Parse(nv);
                pyc.ID_LOAITAISAN = int.Parse(lts);
                pyc.NGAYSUA = DateTime.Now;
                UpdateModel(pyc);
                data.SubmitChanges();
                return RedirectToAction("XemPYC", "Kinhdoanh", new { id = id });
            }
            catch
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
                ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 2), "ID_NHANVIEN", "TENNV", pyc.ID_NHANVIEN);
                ViewData["Khachhang3"] = new SelectList(data.KHACHHANGs.ToList().OrderBy(s => s.TENKH), "ID_KH", "TENKH", pyc.ID_KH);
                ViewData["Loaitaisan3"] = new SelectList(data.LOAITAISANs.ToList().OrderBy(s => s.TEN), "ID_LOAITAISAN", "TEN", pyc.ID_LOAITAISAN);
                return View();
            }
        }

        //Danh sach PYC
        public ActionResult DanhsachPYC()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var pyc = data.PHIEUYEUCAUs.ToList();
            return View(pyc);
        }


        //Thêm Chứng thư
        public ActionResult ThemCT()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                // TODO: Add insert logic here
                var hd = collection["hd"];
                var pyc = collection["pyc"];
                ct.SOCHUNGTHU = collection["SOCHUNGTHU"] + "/2016/CTTDG-AMAX";
                if (collection["hd"] != "")
                {
                    ct.ID_HOPDONG = int.Parse(hd);
                }

                ct.ID_PYC = int.Parse(pyc);
                ct.NGAYTAO = DateTime.Now;
                ct.TRANGTHAI = true;
                data.CHUNGTHUTDGs.InsertOnSubmit(ct);
                data.SubmitChanges();
                return RedirectToAction("ThemCT");
            }
            catch
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                ViewData["hd"] = new SelectList(data.HOPDONGs.ToList().OrderBy(s => s.SOHD), "ID_HOPDONG", "SOHD");
                ViewData["pyc"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderByDescending(s => s.NGAYVIETPHIEU), "ID_PYC", "SOPYC");
                return View();
            }
        }

        //danhsach chung thu
        public ActionResult DanhsachCT()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var ct = data.CHUNGTHUTDGs.ToList();
            return View(ct);
        }

        //thêm hợp đồng
        public ActionResult ThemHD()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
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
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var hd = data.HOPDONGs.ToList().OrderBy(s => s.ID_HOPDONG).Where(s => s.TRANGTHAI == true);
            return View(hd);
        }
        //Xem chi tiết hop đồng
        public ActionResult XemHD(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var detail = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_HOPDONG == id).Count();
            return View(detail);
        }

        //sua hd
        public ActionResult SuaHD(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var hd = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_HOPDONG == id).Count();
            ViewData["lhd3"] = new SelectList(data.LOAIHOPDONGs.ToList().OrderBy(s => s.TEN), "ID_LOAIHOPDONG", "TEN", hd.ID_LOAIHOPDONG);
            ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 3), "ID_NHANVIEN", "TENNV", hd.ID_NHANVIEN);
            ViewData["Loaihopdong3"] = new SelectList(data.TRANGTHAIs.ToList().OrderBy(s => s.TEN), "ID_TRANGTHAI", "TEN", hd.ID_TRANGTHAI);
            ViewData["Pyc3"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC", hd.ID_PYC);
            return View(hd);
        }
        [HttpPost]
        public ActionResult SuaHD(int id, FormCollection collection)
        {
            try
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                // TODO: Add update logic here
                HOPDONG hd = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
                ViewData["lhd3"] = new SelectList(data.LOAIHOPDONGs.ToList().OrderBy(s => s.TEN), "ID_LOAIHOPDONG", "TEN", hd.ID_LOAIHOPDONG);
                var lhd = collection["Lhd3"];
                var nv = collection["Nhanvien3"];
                var tt = collection["Loaihopdong3"];
                var pyc = collection["Pyc3"];
                hd.ID_LOAIHOPDONG = int.Parse(lhd);
                hd.ID_NHANVIEN = int.Parse(nv);
                hd.ID_TRANGTHAI = int.Parse(tt);
                hd.ID_PYC = int.Parse(pyc);
                hd.NGAYSUA = DateTime.Now;
                UpdateModel(hd);
                data.SubmitChanges();
                return RedirectToAction("XemHD", "Kinhdoanh", new { id = id });
            }
            catch
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var hd = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
                ViewData["Nhanvien3"] = new SelectList(data.NHANVIENs.ToList().OrderBy(s => s.TENNV).Where(s => s.PHONGBAN.ID_PHONGBAN == 3), "ID_NHANVIEN", "TENNV", hd.ID_NHANVIEN);
                ViewData["Loaihopdong3"] = new SelectList(data.TRANGTHAIs.ToList().OrderBy(s => s.TEN), "ID_TRANGTHAI", "TEN", hd.ID_TRANGTHAI);
                ViewData["Pyc3"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC", hd.ID_PYC);
                return View();
            }
        }

        //xoa hd
        public ActionResult XoaHD(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var xpb = data.HOPDONGs
                .FirstOrDefault(s => s.ID_HOPDONG == id);
            return PartialView(xpb);
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult XoaHD(int id, FormCollection collection)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                // TODO: Add delete logic here
                HOPDONG pb = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
                pb.TRANGTHAI = false;
                UpdateModel(pb);
                data.SubmitChanges();
                return RedirectToAction("DanhsachHD");
            }
            catch
            {
                return View();
            }
        }

        //thê file hop đồng
        public ActionResult ThemfileHD(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var d = data.HOPDONGs.FirstOrDefault(s => s.ID_HOPDONG == id);
            return View(d);
        }
        //danh sách file chứng thư
        public ActionResult DanhsachFileHD(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var sa = data.FILEDINHKEMs.ToList().Where(s => s.ID_HOPDONG == id).OrderBy(s => s.ID_FILEDINHKEM);
            return PartialView(sa);
        }
        public ActionResult SaveDropzoneJsUploadedFilesHD(int id, FILEDINHKEM dk)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string fName = "";

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //You can Save the file content here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
                    var originalDirectory = new DirectoryInfo(string.Format("{0}FileHD\\" + id, Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString()/*, pyc.ID_HOPDONG.ToString()*/);

                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    var luu = "~/FileHD/" + id + "/" + file.FileName;
                    file.SaveAs(path);
                    dk.LIENKET = luu;
                    dk.ID_HOPDONG = id;
                    dk.TENFILE = file.FileName;
                    dk.NGAYTAO = DateTime.Now;
                    dk.TRANGTHAI = true;
                    data.FILEDINHKEMs.InsertOnSubmit(dk);
                    data.SubmitChanges();

                }
            }

            return Json(new { Message = string.Empty });
        }


        // danh sahv khach hang
        public ActionResult Danhsachkhachhang()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var danhsach = data.KHACHHANGs
                .ToList()
                .OrderBy(s => s.ID_KH);
            return View(danhsach);
        }

        public ActionResult Themkhachhang()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return PartialView();
        }
        [HttpPost]
        public ActionResult Themkhachhang(KHACHHANG kh)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var spb = data.KHACHHANGs.FirstOrDefault(s => s.ID_KH == id);
            return PartialView(spb);
        }
        [HttpPost]
        public ActionResult Suakhachhang(int id, FormCollection collection)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var xpb = data.KHACHHANGs
                .FirstOrDefault(s => s.ID_KH == id);
            return PartialView(xpb);
        }

        // POST: asdas/Delete/5
        [HttpPost]
        public ActionResult Xoakhachhang(int id, FormCollection collection)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
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

        //Xem va in chung thu
        public ActionResult ChiTietCT(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var ct = from c in data.CHUNGTHUTDGs
                     where c.ID_CHUNGTHU == id
                     select c;
            return PartialView(ct.Single());
        }
        public ActionResult XemTruoc(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var ct = from c in data.CHUNGTHUTDGs
                     where c.ID_CHUNGTHU == id
                     select c;
            ViewBag.IDCT = id;
            return PartialView(ct.Single());
        }
        public ActionResult PrintChiTietCT(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return new ActionAsPdf(
                           "ChiTietCT",
                           new { id = id })
            {
                FileName = "Invoice.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 25, Right = 25, Top = 10, Bottom = 10 },
                PageWidth = 210,
                PageHeight = 297

            };
        }
        //Xem chứng thư
        public ActionResult XemCTChungThu(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var detail = data.CHUNGTHUTDGs.FirstOrDefault(s => s.ID_CHUNGTHU == id);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_CHUNGTHU == id).Count();
            return View(detail);
        }
        //sua chung thu
        public ActionResult SuaCT(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var pyc = data.CHUNGTHUTDGs.FirstOrDefault(s => s.ID_CHUNGTHU == id && s.TRANGTHAI == true);
            ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_CHUNGTHU == id && s.TRANGTHAI == true).Count();
            ViewData["hopdong3"] = new SelectList(data.HOPDONGs.ToList().OrderBy(s => s.SOHD), "ID_HOPDONG", "SOHD", pyc.ID_HOPDONG);
            ViewData["PYC3"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC", pyc.ID_PYC);
            return View(pyc);
        }
        [HttpPost]
        public ActionResult SuaCT(int id, FormCollection collection)
        {
            try
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                // TODO: Add update logic here
                CHUNGTHUTDG pyc = data.CHUNGTHUTDGs.FirstOrDefault(s => s.ID_CHUNGTHU == id);
                var hd = collection["hopdong3"];
                if (hd != "")
                {
                    pyc.ID_HOPDONG = int.Parse(hd);
                }
                var a = collection["pyc3"];

                pyc.ID_PYC = int.Parse(a);
                pyc.NGAYSUA = DateTime.Now;
                UpdateModel(pyc);
                data.SubmitChanges();
                return RedirectToAction("XemCTChungthu", "Kinhdoanh", new { id = id });
            }
            catch
            {
                if (Session["ID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var pyc = data.CHUNGTHUTDGs.FirstOrDefault(s => s.ID_CHUNGTHU == id && s.TRANGTHAI == true);
                ViewBag.Demfile = data.FILEDINHKEMs.Where(s => s.ID_CHUNGTHU == id && s.TRANGTHAI == true).Count();
                ViewData["hopdong3"] = new SelectList(data.HOPDONGs.ToList().OrderBy(s => s.SOHD), "ID_HOPDONG", "SOHD", pyc.ID_HOPDONG);
                ViewData["PYC3"] = new SelectList(data.PHIEUYEUCAUs.ToList().OrderBy(s => s.SOPYC), "ID_PYC", "SOPYC", pyc.ID_PYC);
                return View();
            }
        }
        //thê file chứng thư
        public ActionResult ThemfileCT(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var d = data.CHUNGTHUTDGs.FirstOrDefault(s => s.ID_CHUNGTHU == id);
            return View(d);
        }
        //danh sách file chứng thư
        public ActionResult DanhsachFileCT(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var sa = data.FILEDINHKEMs.ToList().Where(s => s.ID_CHUNGTHU == id).OrderBy(s => s.ID_FILEDINHKEM);
            return PartialView(sa);
        }
        public ActionResult SaveDropzoneJsUploadedFilesCT(int id, FILEDINHKEM dk)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string fName = "";

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //You can Save the file content here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var pyc = data.PHIEUYEUCAUs.FirstOrDefault(s => s.ID_PYC == id);
                    var originalDirectory = new DirectoryInfo(string.Format("{0}FileCT\\" + id, Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString()/*, pyc.ID_CHUNGTHU.ToString()*/);

                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    var luu = "~/FileCT/" + id + "/" + file.FileName;
                    file.SaveAs(path);
                    dk.LIENKET = luu;
                    dk.ID_CHUNGTHU = id;
                    dk.TENFILE = file.FileName;
                    dk.NGAYTAO = DateTime.Now;
                    dk.TRANGTHAI = true;
                    data.FILEDINHKEMs.InsertOnSubmit(dk);
                    data.SubmitChanges();

                }
            }

            return Json(new { Message = string.Empty });
        }

        //sửa nội dung chứng thư
        public ActionResult SuaNDCT()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var x = data.NOIDUNGCTs.FirstOrDefault(s => s.ID_NOIDUNG == 1);
            return View(x);
        }
        [HttpPost]
        public ActionResult SuaNDCT(FormCollection collection)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                // TODO: Add update logic here
                NOIDUNGCT x = data.NOIDUNGCTs.FirstOrDefault(s => s.ID_NOIDUNG == 1);
                UpdateModel(x);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LoadFile(int id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var abc = data.FILEDINHKEMs.FirstOrDefault(s => s.ID_FILEDINHKEM == id);
            return View(abc);
        }

    }
}
