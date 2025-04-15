using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppYte.Models;
using WebAppYte.DAO;
using PagedList;

namespace WebAppYte.Controllers
{
    public class BacsiController : Controller
    {
        private modelWeb db = new modelWeb();

        // GET: Bacsi
        public ActionResult Index()
        {
            var quanTris = db.NguoiDungs.Include(q => q.Khoa).Where(x => x.quyen==1);
            return View(quanTris.ToList());
        }

        // GET: Bacsi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung quanTri = db.NguoiDungs.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }
        // GET: Bacsi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung quanTri = db.NguoiDungs.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa", quanTri.makhoa);
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi", quanTri.machinhanh);
            ViewBag.ngay = (quanTri.ngaysinh).Value.ToString("yyyy-MM-dd");
            ViewBag.gioithieu = quanTri.gioithieu;
            ViewBag.quyen = quanTri.quyen;
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mand,hoten,diachi,ngaysinh,gioitinh,sdt,email,chucvu,hocham,hocvi,gioithieu,makhoa,machinhanh,tendn, mk,quyen,anh")] NguoiDung quanTri)
        {

            string[] arrListStr = (quanTri.anh).Split('/');

            if (arrListStr.Length > 2)
            {
                quanTri.anh = arrListStr[3];
            }

            if (ModelState.IsValid)
            {
                quanTri.trangthai = 1;
                db.Entry(quanTri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","BacSi",new { id = quanTri.mand});
            }
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa", quanTri.makhoa);
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi", quanTri.machinhanh);

            return View(quanTri);
        }



        public ActionResult Quanlyhoidap(int? page)
        {
            if (page == null) page = 1;
            var hoiDaps = db.HoiDaps.Include(h => h.BenhNhan).Include(h => h.NguoiDung).Where(n => n.trangthai== 0).OrderByDescending(a=>a.ngayhoi).ThenBy(a=>a.ma).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hoiDaps.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Traloicauhoi(int? id)
        {
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiDap hoiDap = db.HoiDaps.Find(id);
            if (hoiDap == null)
            {
                return HttpNotFound();
            }
            ViewBag.mabn= new SelectList(db.BenhNhans, "mabn", "tenbn", hoiDap.mabn);
            ViewBag.mand= new SelectList(db.NguoiDungs, "mand", "tendn", hoiDap.mand);
            return View(hoiDap);
        }

        // POST: Hoidap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Traloicauhoi(int ma, string hoi, string dap, int mabn)
        {
            if (ModelState.IsValid)
            {
                var userBS = Session["userBS"] as WebAppYte.Models.NguoiDung;
                HoiDap hoiDap = db.HoiDaps.Find(ma);
                hoiDap.ngaytl = DateTime.Now;
                hoiDap.mand = userBS.mand; ;
                hoiDap.dap = dap;
                hoiDap.trangthai = 1;
                db.Entry(hoiDap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Quanlyhoidap");
            }
           
            return View();
        }
        
        public ActionResult Kiemtralichhen(int id,int ? page)
        {
            LichKham lichkham = new LichKham();
            var lich = lichkham.DSLichKham().Where(x=>x.mand==id).OrderByDescending(x => x.ngaykham).ThenBy(y => y.madat).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Lichdangcho(int id,int? page)
        {
            LichKham lichkham = new LichKham();
            var lich = lichkham.DSLichKham().OrderByDescending(x => x.ngaykham).ThenBy(y => y.madat).Where(x=>x.trangthai==0 && x.mand==id).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Lichdaxacnhan(int id,int? page)
        {
            LichKham lichkham = new LichKham();
            var lich = lichkham.DSLichKham().OrderByDescending(x => x.ngaykham).ThenBy(y => y.madat).Where(x => x.trangthai == 1 && x.mand == id).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich.ToPagedList(pageNumber, pageSize));
        }

        // GET: Lichkham/Edit/5
        public ActionResult Xacnhanlichhen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichKham ls = new LichKham();
          
            LichKham lichKham = ls.DSLichKham().Where(x=>x.madat==id).FirstOrDefault();
            if (lichKham == null)
            {
                return HttpNotFound();
            }
           // NguoiDung n = new NguoiDung();
            ViewBag.mabn= new SelectList(db.BenhNhans.Where(x => x.mabn== lichKham.mabn), "mabn", "tenbn", lichKham.mabn);
            ViewBag.hoten = lichKham.tenbn;
            ViewBag.ngaykham = lichKham.ngaykham.ToString("yyyy-MM-dd");
            ViewBag.mand = new SelectList(db.NguoiDungs.Where(x => x.mand== lichKham.mand), "mand", "hoten", lichKham.mand);
            return View(lichKham);
        }

        // POST: Lichkham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xacnhanlichhen([Bind(Include = "madat,mota,ngaykham,buoi,hinhthuc,trangthai,tenbn, mand")] LichKham lichKham)
        {
            if (ModelState.IsValid)
            {
                DatLich ls = db.DatLiches.Find(lichKham.madat);
                ls.trangthai = lichKham.trangthai;
                db.Entry(ls).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Kiemtralichhen","Bacsi", new { id = lichKham.mand });
            }
            ViewBag.mabn = new SelectList(db.BenhNhans, "mabn", "tenbn", lichKham.mabn);
            ViewBag.mand = new SelectList(db.NguoiDungs, "mand", "hoten", lichKham.mand);
            ViewBag.hoten = lichKham.tenbn;
            return View(lichKham);
        }

        public ActionResult Comment(int? id)
        {
     
            var danhgia = db.DanhGias.Include(h => h.BenhNhan).Include(h => h.NguoiDung).Where(h => h.mand == id).OrderByDescending(a => a.ngay).ThenBy(a => a.madanhgia).ToList();
       
            return PartialView(danhgia);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
