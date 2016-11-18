using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN_ver_2.Models;

namespace DACN_ver_2.Controllers
{
    public class TracuuController : Controller
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Tracuu
        public ActionResult Index()
        {
            var ltinhthanh = data.TINHTHANHs.OrderBy(l => l.TEN);
            ViewBag.LTinhThanh = ltinhthanh;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadQuanHuyen(string typeid)
        {
            if (string.IsNullOrEmpty(typeid))
                return Json(HttpNotFound());
            var categoryList = GetQuanHuyenList(Convert.ToInt32(typeid));
            var categoryData = categoryList.Select(m => new SelectListItem()
            {
                Text = m.TEN,
                Value = m.ID_QUANHUYEN.ToString()
            });
            return Json(categoryData, JsonRequestBehavior.AllowGet);
        }
        private IList<QUANHUYEN> GetQuanHuyenList(int typeid)
        {
            return data.QUANHUYENs.OrderBy(c => c.TEN).Where(c => c.ID_TINHTHANH == typeid).ToList();
        }

        public ActionResult GetGiaDat(string Id)
        {
            int id = Convert.ToInt32(Id);
            var lgiadat = data.BANGGIADATs.OrderBy(p => p.TENDUONG).Where(p => p.ID_QUANHUYEN == id);
            return PartialView(lgiadat);
        }
        public ActionResult SearchTenduong(string searchString)
        {
            var tenduong = from t in data.BANGGIADATs
                           select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tenduong = tenduong.Where(s => s.TENDUONG.Contains(searchString));
            }
            return PartialView(tenduong);
        }

        // load danh sach tai san dat
        public ActionResult BanggiadatNB()
        {
            var a = data.DATs.ToList();
            return View(a);
        }

        public ActionResult BanggiaCanho()
        {
            var a = data.CANHOCHUNGCUs.ToList();
            return View(a);
        }
        public ActionResult BanggiaVanphong()
        {
            var a = data.VANPHONGCHOTHUEs.ToList();
            return View(a);
        }
    }
}
