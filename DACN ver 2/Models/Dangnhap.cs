using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN_ver_2.Models;

namespace DACN_ver_2.Models
{
    public class Dangnhap
    {
        DatabaseClassesDataContext data = new DatabaseClassesDataContext();
        public int iID { set; get; }
        public string sTennv { set; get; }
        public int iPhanquyen { set; get; }
        public string sUser { set; get; }
        public string sAvatar { set; get; }
        public Dangnhap(int id)
        {
            iID = id;
            NHANVIEN sp = data.NHANVIENs.Single(n => n.ID_NHANVIEN == iID);
            sTennv = sp.TENNV;
            iPhanquyen = sp.ID_PHANQUYEN;
            sUser = sp.USER;
            sAvatar = sp.ANH;
        }
    }
}