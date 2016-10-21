using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class KinhdoanhController : Controller
    {
        // GET: Kinhdoanh
        public ActionResult Index()
        {
            return View();
        }

        // GET: Kinhdoanh/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kinhdoanh/Create
        public ActionResult ThemPYC()
        {
            return View();
        }

        // POST: Kinhdoanh/Create
        [HttpPost]
        public ActionResult ThemPYC(FormCollection collection)
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

        public ActionResult ThemCT()
        {
            return View();
        }

        // POST: Kinhdoanh/Create
        [HttpPost]
        public ActionResult ThemCT(FormCollection collection)
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

        // GET: Kinhdoanh/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kinhdoanh/Edit/5
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

        // GET: Kinhdoanh/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kinhdoanh/Delete/5
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
