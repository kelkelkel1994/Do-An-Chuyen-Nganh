using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return PartialView();
        }

        // GET: Error/Error
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}
