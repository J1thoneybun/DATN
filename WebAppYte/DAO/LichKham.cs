using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using WebAppYte.Models;

namespace WebAppYte.DAO
{
    public class LichKham
    {
        public int madat { get; set; }
        public DateTime ngaydat { get; set; }
        public string mota { get; set; }
        public int trangthai { get; set; }
        public int maca { get; set; }
        public int mabn { get; set; }
        public string tenbn { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaykham { get; set; }
        public string ca { get; set; }
        public string hinhthuc { get; set; }
        public int mand { get; set; }
        public string hoten { get; set; }
        public string sdt { get; set; }
        public DateTime ngaysinh { get; set; }
        public string anh { get; set; }
        public LichKham() { }
        public LichKham(int madat, DateTime ngaydat, DateTime ngaysinh, string mota, int trangthai, int maca, int mabn, string tenbn, string sdt, DateTime ngaykham, string ca, string hinhthuc, int mand, string hoten,string anh)
        {
            this.madat = madat;
            this.ngaydat = ngaydat;
            this.mota = mota;
            this.trangthai = trangthai;
            this.maca = maca;
            this.mabn = mabn;
            this.tenbn = tenbn;
            this.ngaykham = ngaykham;
            this.ca = ca;
            this.hinhthuc = hinhthuc;
            this.mand = mand;
            this.hoten = hoten;
            this.sdt = sdt;
            this.ngaysinh = ngaysinh;
            this.anh = anh; 
        }
        public List<LichKham> DSLichKham()
        {

            modelWeb db = new modelWeb();
            var lst = (
                           from p in db.DatLiches
                           from x in db.CaKhams
                           from z in db.NguoiDungs

                           where p.maca == x.maca && x.mand == z.mand && x.trangthai == 1

                           select new LichKham()
                           {
                               madat = p.madat,
                               ngaydat = (DateTime)p.ngaydat,
                               mota = p.mota,
                               trangthai = (int)p.trangthai,
                               maca = (int)x.maca,

                               mabn = (int)p.mabn,

                               tenbn = p.hoten,
                               ngaysinh = (DateTime)p.ngaysinh,
                               sdt = p.sdt,

                               mand = (int)(from q in db.CaKhams
                                            where q.maca == x.maca
                                            select new
                                            {
                                                q.mand
                                            }).FirstOrDefault().mand,

                               hoten = (from y in db.NguoiDungs

                                        where y.mand == x.mand
                                        select new
                                        {
                                            y.hoten
                                        }).FirstOrDefault().hoten,
                               ngaykham = (DateTime)(from y in db.CaKhams

                                                     where y.maca == p.maca
                                                     select new
                                                     {
                                                         y.ngaykham
                                                     }).FirstOrDefault().ngaykham,
                               ca = (from y in db.CaKhams

                                     where y.maca == p.maca
                                     select new
                                     {
                                         y.ca
                                     }).FirstOrDefault().ca,
                               hinhthuc = (from y in db.CaKhams

                                           where y.maca == p.maca
                                           select new
                                           {
                                               y.hinhthuc
                                           }).FirstOrDefault().hinhthuc,
                               anh = z.anh,
                           });


            return lst.ToList();
        }
        public int FindMaCa(string bs, string hinhthuc, string bacsi)
        {
            string[] name = bs.Split(',');
            string ngay = name[0]; // yyyy-MM-dd
            string ca = name[1];
            int bacsiInt = int.Parse(bacsi);

            DateTime ngayParsed;
            if (!DateTime.TryParse(ngay, out ngayParsed))
                throw new Exception("Ngày không hợp lệ!");

            using (modelWeb db = new modelWeb())
            {
                var result = db.CaKhams.FirstOrDefault(y =>
    y.ngaykham.HasValue &&
    y.ngaykham.Value.Year == ngayParsed.Year &&
    y.ngaykham.Value.Month == ngayParsed.Month &&
    y.ngaykham.Value.Day == ngayParsed.Day &&
    y.ca == ca &&
    y.hinhthuc == hinhthuc &&
    y.mand == bacsiInt
);


                if (result == null)
                {
                    throw new Exception("Không tìm thấy ca khám phù hợp.");
                }

                return result.maca;
            }
        }


        public int FindMaCa(string ngaykham, string ca, string hinhthuc, int bacsi)
        {
            try
            {
                // Chuyển đổi ngaykham sang DateTime trước
                if (!DateTime.TryParseExact(ngaykham, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedNgay))
                {
                    throw new FormatException("Định dạng ngày không hợp lệ. Định dạng phải là 'yyyy-MM-dd'.");
                }

                modelWeb db = new modelWeb();

                // Thực hiện truy vấn với parsedNgay
                var res = (from y in db.CaKhams
                           where DbFunctions.TruncateTime(y.ngaykham) == parsedNgay.Date
                                 && y.ca == ca
                                 && y.hinhthuc == hinhthuc
                                 && y.mand == bacsi
                           select new
                           {
                               y.maca
                           }).FirstOrDefault();

                // Lấy kết quả
                int ma = 0;
                if (res != null)
                {
                    ma = res.maca; // Gán giá trị maca nếu tìm thấy
                }
                return ma; // Trả về 0 nếu không tìm thấy
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tìm maca: {ex.Message}");
                return -1; // Trả về -1 nếu xảy ra lỗi
            }
        }





        public int FindMaBS(string khoa, string chinhanh)
        {
            modelWeb db = new modelWeb();

            var res = (from y in db.NguoiDungs
                       from x in db.Khoas
                       from z in db.ChiNhanhs

                       where y.makhoa == x.makhoa && y.machinhanh == z.machinhanh && x.tenkhoa == khoa && z.diachi == chinhanh
                       select new
                       {
                           y.mand
                       }).FirstOrDefault();
            int ma = 0;
            if (res != null) ma = res.mand;
            return ma;
        }
        public int FindMaBS(string khoa, string chinhanh, string bacsi)
        {
            // Khởi tạo đối tượng DbContext (thay modelWeb bằng tên DbContext thực tế của bạn)
            modelWeb db = new modelWeb();

            // Truy vấn kết hợp bảng NguoiDungs, Khoas, và ChiNhanhs
            var res = (from y in db.NguoiDungs
                       join x in db.Khoas on y.makhoa equals x.makhoa
                       join z in db.ChiNhanhs on y.machinhanh equals z.machinhanh
                       where x.tenkhoa == khoa && z.diachi == chinhanh && y.hoten == bacsi
                       select y.mand) // Chỉ lấy cột y.mand
                       .FirstOrDefault();

            // Trả về giá trị res hoặc 0 nếu không tìm thấy
            return res > 0 ? res : 0;
        }
        public List<CaKham> GetCaKhamTheoHinhThuc(string hinhthuc)
        {
            using (modelWeb db = new modelWeb())
            {
                var list = db.CaKhams
                    .Where(c => c.hinhthuc == hinhthuc && c.trangthai == 1)
                    .ToList();

                return list;
            }
        }




    }
}