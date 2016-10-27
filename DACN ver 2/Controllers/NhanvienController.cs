using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;

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

        // GET: Nhanvien/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Nhanvien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nhanvien/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nhanvien/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Nhanvien/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nhanvien/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Nhanvien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        //them cnhh
        public ActionResult ThemCHCC ()
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
