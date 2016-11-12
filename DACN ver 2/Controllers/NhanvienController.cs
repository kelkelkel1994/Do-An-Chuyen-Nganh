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
    public class NhanvienController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Nhanvien
        public ActionResult Index()
        {
            return View();
        }

        // GET: Nhanvien/Create
        public ActionResult Create()
        {
            ViewData["phongban1204"] = new SelectList(data.PHONGBANs, "ID_PHONGBAN", "TEN");
            ViewData["loainhanvien1204"] = new SelectList(data.LOAINHANVIENs, "ID_LOAINHANVIEN", "TEN");
            ViewData["phanquyen1204"] = new SelectList(data.PHANQUYENs, "ID_PHANQUYEN", "TEN");
            return View();
        }

        // POST: Nhanvien/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,NHANVIEN nvs)
        {
            try
            {
                // TODO: Add insert logic here
                var pb= collection["phongban1204"];
                var lnv= collection["loainhanvien1204"];
                var pq= collection["phanquyen1204"];
                var pass12 = collection["PASS12"];
                nvs.NGAYTAO = DateTime.Now;
                nvs.ID_PHONGBAN = int.Parse(pb);
                nvs.ID_LOAINHANVIEN = int.Parse(lnv);
                nvs.ID_PHANQUYEN = int.Parse(pq);
                nvs.PASS = GenerateMD5(pass12);               
                data.NHANVIENs.InsertOnSubmit(nvs);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["phongban1204"] = new SelectList(data.PHONGBANs, "ID_PHONGBAN", "TEN");
                ViewData["loainhanvien1204"] = new SelectList(data.LOAINHANVIENs, "ID_LOAINHANVIEN", "TEN");
                ViewData["phanquyen1204"] = new SelectList(data.PHANQUYENs, "ID_PHANQUYEN", "TEN");
                return View();
            }
        }
        public string GenerateMD5(string yourString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
        }
     
        
        //them cnhh
        public ActionResult ThemCHCC()
        {
            //ViewData["quann"] = new SelectList(data.QUANHUYENs, "ID_QUANHUYEN", "TEN");
            //ViewData["tinhh"] = new SelectList(data.TINHTHANHs, "ID_TINHTHANH", "TEN");
            //var ptypes = db.ProductTypes.OrderBy(p => p.Name);
            //ViewBag.ProductTypes = ptypes;

            var tp = data.TINHTHANHs.OrderBy(p => p.TEN);
            ViewBag.Tentp = tp;


            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadQuannHuyen(string tpid)
        {
            if (string.IsNullOrEmpty(tpid))
                return Json(HttpNotFound());
            var categoryList = GetQuanHuyenList(Convert.ToInt32(tpid));
            var categoryData = categoryList.Select(m => new SelectListItem()
            {
                Text = m.TEN,
                Value = m.ID_TINHTHANH.ToString()
            });
            return Json(categoryData, JsonRequestBehavior.AllowGet);
        }
        private IList<QUANHUYEN> GetQuanHuyenList(int tpid)
        {
            return data.QUANHUYENs.OrderBy(c => c.TEN).Where(c => c.ID_TINHTHANH == tpid).ToList();
        }
    }
}
