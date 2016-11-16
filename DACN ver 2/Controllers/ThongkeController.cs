using DACN_ver_2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN_ver_2.Controllers
{
    public class ThongkeController : Controller
    {
        // GET: Thongke
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.nam = DateTime.Now.Year;
            return View();
        }
        public ContentResult GetDoanhthuNV()
        {
            var res = (from tags in data.HOPDONGs
                       select new
                       {
                           tags.ID_NHANVIEN,
                           tags.TONGTIEN
                       }).GroupBy(a => a.ID_NHANVIEN).Select(b => new
                       { manv = b.Key, tennv = data.NHANVIENs.Single(d => d.ID_NHANVIEN == b.Key).TENNV, tong = b.Sum(d => d.TONGTIEN) }
                       ).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult Getsoluonghd()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.NGAYLAP.Value.Year == DateTime.Now.Year
                       group tags by tags.NGAYLAP.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count()
                       }).ToList();

            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetPYCNhanVien()
        {
            var res = (from tags in data.PHIEUYEUCAUs
                       group tags by tags.ID_NHANVIEN into gp
                       select new
                       {
                           id = gp.Key,
                           tennv = data.NHANVIENs.Single(d => d.ID_NHANVIEN == gp.Key).TENNV,
                           sl = gp.Count()
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
    }
}