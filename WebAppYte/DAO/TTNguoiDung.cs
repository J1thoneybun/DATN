using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppYte.Models;
namespace WebAppYte.DAO
{
    public class TTNguoiDung
    {
        public int mand { get; set; }
        public string hoten { get; set; }
        public string chucvu { get; set; }
        public string hocham { get; set; }
        public string hocvi { get; set; }
        public string gioithieu { get; set; }
        public string tenkhoa { get; set; }
        public string tenchinhanh { get; set; }
        public string anh { get; set; }
        public string tendn { get; set; } 

        public TTNguoiDung() { }
        public TTNguoiDung(int mand, string hoten, string chucvu, string hocham, string hocvi, string gioithieu, string tenkhoa, string tenchinhanh, string anh, string tendn)
        {
            this.mand = mand;
            this.hoten = hoten;
            this.chucvu = chucvu;
            this.hocham = hocham;
            this.hocvi = hocvi;
            this.gioithieu = gioithieu;
            this.tenkhoa = tenkhoa;
            this.tenchinhanh = tenchinhanh;
            this.anh = anh;
            this.tendn = tendn;
        }
        public List<TTNguoiDung> ListNguoiDung()
        {
            modelWeb db = new modelWeb();
            var lst = (
                           from p in db.NguoiDungs
                           from x in db.Khoas
                           from t in db.ChiNhanhs
                           where p.makhoa == x.makhoa && p.machinhanh==t.machinhanh
                          
                           select new TTNguoiDung()
                           {
                               mand = p.mand,
                               hoten = p.hoten,
                               chucvu = p.chucvu,
                               hocham = p.hocham,
                               hocvi = p.hocvi,
                               gioithieu=p.gioithieu,
                               anh=p.anh,
                               tendn=p.tendn,
							   tenkhoa = ((from z in db.Khoas
										   where z.makhoa == p.makhoa
										   select new
										   {
											   z.tenkhoa
										   }).FirstOrDefault().tenkhoa),

							   tenchinhanh = (from y in db.ChiNhanhs
											  where y.machinhanh == p.machinhanh
											  select new
											  {
												  y.diachi
											  }).FirstOrDefault().diachi,

						   });

            return lst.ToList();
        }

    }
       
  
}