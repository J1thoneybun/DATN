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

namespace WebAppYte.Areas.Admin.Controllers
{
    public class QuanTrisController : Controller
    {
        // GET: Admin/QuanTris
        private modelWeb db = new modelWeb();

        // GET: Admin/QuanTris
        public ActionResult Index()
        {
            var quanTris = db.NguoiDungs.Include(q => q.Khoa).Include(q => q.ChiNhanh).Where(x => x.trangthai == 1);
            var model = quanTris.ToList();
            return View(model);
        }

        // GET: Admin/QuanTris/Details/5
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

        // GET: Admin/QuanTris/Create
        public ActionResult Create()
        {
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa");
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi");
            return View();
        }

        // POST: Admin/QuanTris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mand,hoten,diachi,ngaysinh,gioitinh,sdt,email,chucvu,hocham,hocvi,gioithieu,makhoa,machinhanh,tendn, mk,quyen,anh")] NguoiDung quanTri)
        {
            string[] arrListStr = (quanTri.anh).Split('/');
            if (arrListStr.Length > 2)
            {
                quanTri.anh = arrListStr[3];
            }
            if (ModelState.IsValid)
            {
                quanTri.trangthai = 1;
                db.NguoiDungs.Add(quanTri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa", quanTri.makhoa);
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi", quanTri.machinhanh);
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa", quanTri.makhoa);
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi", quanTri.machinhanh);
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Delete/5
        public ActionResult Delete(int? id)
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
            ViewBag.ngay = (quanTri.ngaysinh).Value.ToString("dd-MM-yyyy");
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int mand, string hoten, string diachi, DateTime ngaysinh, string gioitinh, string sdt, string email, string hocham, string hocvi, int makhoa, int machinhanh, string tendn, string mk, int quyen, string anh)
        {
            NguoiDung quanTri = db.NguoiDungs.Find(mand);
            quanTri.hoten = hoten;
            quanTri.diachi = diachi;
            quanTri.ngaysinh = ngaysinh;
            quanTri.gioitinh = gioitinh;
            quanTri.sdt = sdt;
            quanTri.email = email;

            quanTri.hocham = hocham;
            quanTri.hocvi = hocvi;

            quanTri.makhoa = makhoa;
            quanTri.machinhanh = machinhanh;
            quanTri.tendn = tendn;
            quanTri.mk = mk;
            quanTri.quyen = quyen;
            quanTri.anh = anh;
            quanTri.trangthai = 0;
            db.Entry(quanTri).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.makhoa = new SelectList(db.Khoas, "makhoa", "tenkhoa", quanTri.makhoa);
            ViewBag.machinhanh = new SelectList(db.ChiNhanhs, "machinhanh", "diachi", quanTri.machinhanh);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Search(string searchTerm)
        {
            // Truy vấn dữ liệu ban đầu
            var quanTris = db.NguoiDungs.Include(q => q.Khoa).Include(q => q.ChiNhanh)
                                        .Where(x => x.trangthai == 1);

            // Nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Không phân biệt chữ hoa/thường
                quanTris = quanTris.Where(x => x.hoten.ToLower().Contains(searchTerm) ||
                                               x.sdt.Contains(searchTerm) ||
                                               x.ChiNhanh.diachi.ToLower().Contains(searchTerm) ||
                                               x.Khoa.tenkhoa.ToLower().Contains(searchTerm));
            }

            // Chuyển danh sách sang View
            var model = quanTris.ToList();

            // Gửi từ khóa lại cho View để hiển thị
            ViewBag.SearchTerm = searchTerm;

            return View("Index", model);
        }

    }
}