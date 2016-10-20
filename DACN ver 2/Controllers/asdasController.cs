using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class asdasController : Controller
    {
        // GET: asdas
        public ActionResult Index()
        {
            return View();
        }

        // GET: asdas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: asdas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asdas/Create
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

        // GET: asdas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: asdas/Edit/5
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

        // GET: asdas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: asdas/Delete/5
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
