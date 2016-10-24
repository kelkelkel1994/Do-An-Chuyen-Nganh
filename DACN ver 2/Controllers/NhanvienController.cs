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
    }
}
