using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppYte.Models;
namespace WebAppYte.DAO
{
    public class ListHoiDap
    {
        public int ma { get; set; }
        public string hoi { get; set; }
        public DateTime ngayhoi { get; set; }
        public string dap { get; set; }
        public DateTime ngaytl { get; set; }
        public int mand { get; set; }
        public string hoten{ get; set; }
        public int mabn { get; set; }
        public string tenbn { get; set; }
        public int trangthai { get; set; }

        public ListHoiDap() { }
        public ListHoiDap(int ma, string hoi, DateTime ngayhoi, string dap, DateTime ngaytl, int mand, string hoten, int mabn, string tenbn, int trangthai)
        {
            this.ma = ma;
            this.hoi = hoi;
            this.ngayhoi = ngayhoi;
            this.dap= dap;
            this.ngaytl = ngaytl;
            this.mand= mand;
            this.mabn = mabn;
            this.tenbn= tenbn;
            this.trangthai = trangthai;
            this.hoten = hoten;
          
        }
        public List<ListHoiDap> DSHoiDap()
        {
            modelWeb db = new modelWeb();
            var lst = (
                           from p in db.HoiDaps
                           from x in db.NguoiDungs
                           from t in db.BenhNhans
                           where p.mand == x.mand && p.mabn == t.mabn

                           select new ListHoiDap()
                           {
                               ma = p.ma,
                               hoi = p.hoi,
                               ngayhoi =(DateTime)p.ngayhoi,
                               dap = p.dap,
                               ngaytl = (DateTime)p.ngaytl,
                               mand = (int)p.mand,
                               mabn =(int) p.mabn,
                               trangthai = (int)p.trangthai,
                               hoten = ((from z in db.NguoiDungs
                                           where z.mand == p.mand
                                           select new
                                           {
                                               z.hoten
                                           }).FirstOrDefault().hoten),

                               tenbn = (from y in db.BenhNhans

                                              where y.mabn == p.mabn
                                              select new
                                              {
                                                  y.tenbn
                                              }).FirstOrDefault().tenbn,
                           }).ToList();

            return lst;
        }
    }
}