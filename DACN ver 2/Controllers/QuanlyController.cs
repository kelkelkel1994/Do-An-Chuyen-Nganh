using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN_ver_2.Models;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class QuanlyController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Quanly
        public ActionResult Danhmuc()
        {
            return View();
        }

        public ActionResult Loaitaisan()
        {
            var loaits = data.LOAITAISANs
                .ToList()
                .OrderBy(s => s.ID_LOAITAISAN);
            return PartialView(loaits);
        }

        public ActionResult Loaithongtin()
        {
            var loaitt = data.LOAITHONGTINs
                .ToList()
                .OrderBy(s => s.ID_LTT);
            return PartialView(loaitt);
        }

        public ActionResult Loaihinh()
        {
            var loaihinh = data.LOAIHINHs
                .ToList()
                .OrderBy(s => s.ID_LOAIHINH);
            return PartialView(loaihinh);
        }        

        
    }
}