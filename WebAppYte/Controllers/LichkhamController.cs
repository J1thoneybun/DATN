using System;

using System.Collections.Generic;

using System.Data;

using System.Data.Entity;

using System.Linq;

using System.Net;

using System.Web;

using System.Web.Mvc;

using WebAppYte.DAO;

using WebAppYte.Models;

using WebAppYte.Common;

using PagedList;

using System.Threading.Tasks;



namespace WebAppYte.Controllers

{

    public class LichkhamController : Controller

    {

        private modelWeb db = new modelWeb();





        // GET: Lichkham

        public class listofSegments

        {

            public string Text { get; set; }

            public string Value { get; set; }

        }

        public ActionResult Index(int? id, int? page)

        {

            LichKham ls = new LichKham();



            var lichKhams = ls.DSLichKham().Where(h => h.mabn == id)

             .OrderByDescending(x => x.ngaydat).ThenBy(x => x.madat);

            foreach (var item in lichKhams)

            {

                if (@item.ngaykham < DateTime.Now && @item.trangthai == 0)

                {

                    DatLich dl = db.DatLiches.Find(@item.madat);

                    dl.trangthai = 3;

                    db.Entry(dl).State = EntityState.Modified;

                    db.SaveChanges();

                }

            }

            int pageSize = 3;

            int pageNumber = (page ?? 1);

            ViewBag.id = id;

            return View(lichKhams.ToPagedList(pageNumber, pageSize));

        }



        public ActionResult Dangxuly(int? id, int? page)
        {
            if (id == null) return RedirectToAction("Index");
            var ls = new LichKham();
            var lichKhams = ls.DSLichKham()
                             .Where(h => h.mabn == id && h.trangthai == 0)
                             .OrderByDescending(x => x.ngaydat)
                             .ThenBy(x => x.madat);

            ViewBag.id = id;
            return View(lichKhams.ToPagedList(page ?? 1, 3));
        }


        public ActionResult Daxacnhan(int? id, int? page)

        {

            LichKham ls = new LichKham();

            var lichKhams = ls.DSLichKham().Where(h => h.mabn == id && h.trangthai == 1)

              .OrderByDescending(x => x.ngaydat).ThenBy(x => x.madat);

            int pageSize = 3;

            int pageNumber = (page ?? 1);

            ViewBag.id = id;

            return View(lichKhams.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Datuvanxong(int? id, int? page)

        {

            LichKham ls = new LichKham();

            var lichKhams = ls.DSLichKham()

              .Where(h => h.mabn == id && (h.trangthai == 2 || h.trangthai == 4))

              .OrderByDescending(x => x.ngaydat)

              .ThenBy(x => x.madat)

              .ToList();

            var lichKhamsTemp = ls.DSLichKham();

            if (lichKhamsTemp == null)

            {

                // Ghi log hoặc thực hiện các xử lý lỗi nếu cần

                Console.WriteLine("DSLichKham trả về null");

            }

            else

            {

                Console.WriteLine("DSLichKham có dữ liệu");

            }



            ViewBag.LichKhams = lichKhams;  // Truyền dữ liệu cho view



            int pageSize = 3;

            int pageNumber = (page ?? 1);

            ViewBag.id = id;

            return View(lichKhams.ToPagedList(pageNumber, pageSize));

        }



        // GET: Lichkham/Details/5

        public ActionResult Details(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            LichKham ls = new LichKham();

            LichKham lichkham = ls.DSLichKham().Where(x => x.madat == id).FirstOrDefault();



            if (lichkham == null)

            {

                return HttpNotFound();

            }

            return View(lichkham);

        }







        public ActionResult Create(string hinhthuc, string chinhanh, string khoa, string bacsi, string ngaykham, string cakham, string mota)

        {



            var u = Session["user"] as WebAppYte.Models.BenhNhan;



            hinhthuc = LKSave.hinhthuc;

            chinhanh = LKSave.chinhanh;

            khoa = LKSave.khoa;

            bacsi = LKSave.bacsi;

            ngaykham = LKSave.ngaykham;

            cakham = LKSave.cakham;

            var segmentList = new List<listofSegments>();

            listofSegments segmentItem;

            var strArr = new string[] { "Khám trong giờ", "Khám ngoài giờ", "Khám online" };

            for (int index = 0; index < strArr.Length; index++)

            {

                segmentItem = new listofSegments();

                segmentItem.Text = strArr[index];

                segmentItem.Value = strArr[index];

                segmentList.Add(segmentItem);

            }



            ViewBag.hinhthuc = segmentList;

            ViewBag.ht = hinhthuc;



            List<string> dd = (from p in db.ChiNhanhs select p.diachi).ToList();

            ViewBag.chinhanh = dd;

            ViewBag.cn = chinhanh;

            List<string> tenkhoa = (from p in db.Khoas select p.tenkhoa).ToList();

            ViewBag.khoa = tenkhoa;

            ViewBag.k = khoa;

            TTNguoiDung ls = new TTNguoiDung();

            var bss = ls.ListNguoiDung().ToList();

            var bs1 = (from p in bss select p.hoten).ToList();

            ViewBag.bacsi = bs1;

            ViewBag.bs = bacsi;

            DateTime a = DateTime.Now;

            var ngay = db.CaKhams.Where(x => x.ngaykham >= a.Date).ToList();

            LichKham lichkham = new LichKham();


            ViewBag.ngaykham = (from p in ngay select p.ngaykham.Value.ToString("yyyy-MM-dd")).ToList();

            ViewBag.nk = ngaykham;

            var ca = db.CaKhams.Where(x => x.ngaykham >= a.Date).ToList();

            if (LKSave.chinhanh != null && LKSave.khoa != null && LKSave.bacsi != null && LKSave.hinhthuc != null && LKSave.ngaykham != null)

            {


                ca = db.CaKhams.Where(x => x.mand == LKSave.mand && x.hinhthuc == LKSave.hinhthuc && (x.ngaykham).ToString() == LKSave.ngaykham && x.trangthai == 1).ToList();

            }

            ViewBag.cakham = (from p in ca select (p.ca)).ToList().Distinct();

            ViewBag.ca = cakham;

            DatLich f = new DatLich();

            f.ngaydat = DateTime.Now;

            f.trangthai = 0;

            if (LKSave.chinhanh != null && LKSave.khoa != null && LKSave.bacsi != null && LKSave.hinhthuc != null && LKSave.ngaykham != null && LKSave.cakham != null && LKSave.mand != null && mota != null)

            {

                string bs = (LKSave.ngaykham + "," + LKSave.cakham).ToString();

                LKSave.maca = lichkham.FindMaCa(bs, LKSave.hinhthuc, LKSave.bacsi);

                f.maca = LKSave.maca;

                if (u != null)

                {

                    f.mabn = u.mabn;

                    f.ngaysinh = u.ngaysinh;  // Gán giá trị từ thông tin đăng nhập

                    f.hoten = u.tenbn;        // Gán giá trị từ thông tin đăng nhập

                    f.sdt = u.sdt;

                    if (mota != null)

                    {

                        f.mota = mota;

                        if (ModelState.IsValid)

                        {

                            int check = db.DatLiches.Where(x => x.maca == LKSave.maca).Count();

                            if (check == 0)

                            {

                                db.DatLiches.Add(f);

                                db.SaveChanges();

                                return RedirectToAction("Index", "LichKham", new { id = u.mabn });

                            }

                            else

                            {

                                ViewBag.Fail = "Ca khám này đã có người đặt!";

                                return PartialView();

                            }

                            db.DatLiches.Add(f);

                            db.SaveChanges();



                            return RedirectToAction("Index", "LichKham", new { id = u.mabn });

                        }

                    }

                }

                else

                {

                    if (mota != null)

                    {

                        f.mota = mota;

                        return RedirectToAction("NhapTTDK", "LichKham");

                    }

                }

            }

            else

            {

                ViewBag.Fail = "Vui lòng nhập đầy đủ thông tin";

                return PartialView();

            }

            return PartialView();

        }
        [HttpPost]
        public JsonResult ActionPostData(string hinhthuc,
                                  string chinhanh,
                                  string khoa,
                                  string bacsi,
                                  string ngaykham,
                                  string cakham,
                                  string mota)
        {
            try
            {
                var u = Session["user"] as BenhNhan;
                if (u == null)
                    return Json(new { success = false, message = "Vui lòng đăng nhập lại." });

                // Tính mã ca khám
                string key = $"{ngaykham},{cakham}";
                int maca = new LichKham().FindMaCa(key, hinhthuc, bacsi);

                // Kiểm tra trùng
                if (db.DatLiches.Any(d => d.maca == maca))
                    return Json(new
                    {
                        success = false,
                        message = "Ca khám này đã có người đặt!",
                        // Không redirect
                    });

                // Thêm mới
                var f = new DatLich
                {
                    mabn = u.mabn,
                    hoten = u.tenbn,
                    sdt = u.sdt,
                    ngaysinh = u.ngaysinh,
                    mota = mota,
                    maca = maca,
                    ngaydat = DateTime.Now,
                    trangthai = 0
                };
                db.DatLiches.Add(f);
                db.SaveChanges();

                // Trả về JSON kèm đường dẫn đến trang Đang xử lý
                return Json(new
                {
                    success = true,
                    message = "Lên lịch thành công!",
                    redirectUrl = Url.Action("Dangxuly", "LichKham", new { id = u.mabn })
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public ActionResult DanhGia(int? id, int? fi, int? madat)

        {

            // Chỉ gán vào Bien nếu id và fi có giá trị

            if (id.HasValue)

            {

                Bien.mabn = id.Value;

            }



            if (fi.HasValue)

            {

                Bien.mabs = fi.Value;

            }



            // Tìm kiếm DatLich chỉ khi madat có giá trị

            if (madat.HasValue)

            {

                DatLich dl = db.DatLiches.Find(madat.Value);

                if (dl != null)

                {

                    db.Entry(dl).State = EntityState.Modified;

                    db.SaveChanges();

                    dl.trangthai = 4;

                }

            }



            // Tìm bác sĩ và bệnh nhân nếu id và fi có giá trị, nếu không thì để trống

            var nd = id.HasValue ? db.NguoiDungs.FirstOrDefault(h => h.mand == id.Value) : null;

            var bn = fi.HasValue ? db.BenhNhans.FirstOrDefault(h => h.mabn == fi.Value) : null;



            // Đặt giá trị cho ViewBag hoặc để trống nếu không có thông tin

            ViewBag.mand = nd != null ? nd.hoten : "Không có thông tin bác sĩ";

            ViewBag.mabn = bn != null ? bn.tenbn : "Không có thông tin bệnh nhân";



            // Hiển thị view bình thường

            return View();

        }



        // POST: Admin/Tintucs/Create

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]

        [ValidateAntiForgeryToken]

        [ValidateInput(false)]

        public ActionResult DanhGia([Bind(Include = "ngay, noidung, rating, mand, mabn")] DanhGia danhgia)

        {



            if (ModelState.IsValid)

            {

                danhgia.mabn = Bien.mabn;

                danhgia.mand = Bien.mabs;

                var d = DateTime.Now;

                danhgia.ngay = d;

                db.DanhGias.Add(danhgia);

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            ViewBag.mand = new SelectList(db.NguoiDungs, "mand", "hoten", danhgia.mand);

            ViewBag.mabn = new SelectList(db.BenhNhans, "mabn", "tenbn", danhgia.mabn);

            return View(danhgia);

        }

        public ActionResult Delete(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            LichKham ls = new LichKham();

            var lichkham = ls.DSLichKham().Where(x => x.madat == id).FirstOrDefault();

            if (lichkham == null)

            {

                return HttpNotFound();

            }

            return View(lichkham);

        }

         // POST: Lichkham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            DatLich lichKham = db.DatLiches.Find(id);
            if (lichKham == null)
                return HttpNotFound();

            
            int mabn = lichKham.mabn ?? 0;

          
            db.DatLiches.Remove(lichKham);
            db.SaveChanges();

          
            return RedirectToAction("Index", new { id = mabn });
        }




    }

}

