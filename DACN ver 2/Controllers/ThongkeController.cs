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
        //Số lhop đồng ngoài trong tháng
        public ContentResult GetSoHopDongNgoai()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_LOAIHOPDONG == 1 //hop đồng ngoài
                       group tags by tags.NGAYLAP.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count(),
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetSoHopDongTrong()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_LOAIHOPDONG == 2 //hop đồng trong
                       group tags by tags.NGAYLAP.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count()
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }

        //phieu yeu cau/tháng
        public ContentResult GetPYCThang()
        {
            var res = (from tags in data.PHIEUYEUCAUs
                       where tags.NGAYVIETPHIEU.Value.Year == DateTime.Now.Year
                       group tags by tags.NGAYVIETPHIEU.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           //tennv = data.NHANVIENs.Single(d => d.ID_NHANVIEN == gp.Key).TENNV,
                           sl = gp.Count()
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetPYC5(string Id)
        {
            int nam = Convert.ToInt32(Id);
            var res = (from tags in data.PHIEUYEUCAUs
                       where tags.NGAYVIETPHIEU.Value.Year == nam
                       group tags by tags.NGAYVIETPHIEU.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count()
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }

        //doanh thu thực theo tháng
        public ContentResult GetDoanhthuthuc()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.NGAYLAP.Value.Year == DateTime.Now.Year
                       group tags by tags.NGAYLAP.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           dt = gp.Sum(x => x.DOANHTHUTHUC)
                       }).ToList();

            return Content(JsonConvert.SerializeObject(res));
        }

        //Thống kê

        public ContentResult GetSoHop1()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_TRANGTHAI == 1 && tags.NGAYTAO.Value.Year==DateTime.Now.Year//ĐANG CHỜ
                       group tags by tags.NGAYTAO.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count(),
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetSoHop2()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_TRANGTHAI == 2 && tags.NGAYTAO.Value.Year == DateTime.Now.Year//ĐANG THỰC HIỆN
                       group tags by tags.NGAYTAO.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count(),
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetSoHop3()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_TRANGTHAI == 3 && tags.NGAYTAO.Value.Year == DateTime.Now.Year//ĐÃ KKẾT THÚC
                       group tags by tags.NGAYTAO.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count(),
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
        public ContentResult GetSoHop4()
        {
            var res = (from tags in data.HOPDONGs
                       where tags.ID_TRANGTHAI == 4 && tags.NGAYTAO.Value.Year == DateTime.Now.Year//ĐÃ HUỶ
                       group tags by tags.NGAYTAO.Value.Month into gp
                       select new
                       {
                           thang = gp.Key,
                           sl = gp.Count(),
                       }).ToList();
            return Content(JsonConvert.SerializeObject(res));
        }
    }
}