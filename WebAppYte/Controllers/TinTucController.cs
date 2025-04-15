using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebAppYte.Models;
namespace WebAppYte.Controllers
{
    public class TinTucController : Controller
    {
        modelWeb db = new modelWeb();
        // GET: TinTuc
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;

            List<BaiViet> bai_viet = db.BaiViets.ToList();
            return View(bai_viet.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiviet = db.BaiViets.Find(id);
            if (baiviet == null)
            {
                return HttpNotFound();
            }
            return View(baiviet);
        }
        public ActionResult IndexBS(int id, int? page)
        {
            var hoiDaps = db.BaiViets.Where(h => h.mand == id)
                .OrderByDescending(x => x.ngaydang).ThenBy(x => x.mabv).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            return View(hoiDaps.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create(int? id)
        {
            ViewBag.mabn = new SelectList(db.NguoiDungs.Where(h => h.mand == id), "mand", "hoten");
            ViewBag.maloai = new SelectList(db.Loais, "maloai", "tenloai");
            return View();
        }

        // POST: Admin/Tintucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "mabv, tieude, noidung, hinhanh, mota, maloai, mand")] BaiViet tintuc)
        {
            string[] arrListStr = (tintuc.hinhanh).Split('/');
            if (arrListStr.Length > 2)
            {
                tintuc.hinhanh = arrListStr[3];
            }
            if (ModelState.IsValid)
            {
                var userBS = Session["userBS"] as WebAppYte.Models.NguoiDung;
                tintuc.mand = userBS.mand;
                var d = DateTime.Now;
                tintuc.ngaydang = d;
                db.BaiViets.Add(tintuc);
                db.SaveChanges();
                return RedirectToAction("IndexBS", "TinTuc", new { id = userBS.mand });
            }
            ViewBag.maloai = new SelectList(db.Loais, "maloai", "tenloai", tintuc.maloai);
         

            return View(tintuc);
        }

        // GET: Admin/Tintucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet tintuc = db.BaiViets.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.maloai = new SelectList(db.Loais, "maloai", "tenloai", tintuc.maloai);
            @ViewBag.ngay = (tintuc.ngaydang).Value.ToString("dd-MM-yyyy");
            return View(tintuc);
        }

        // POST: Admin/Tintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "mabv, tieude, noidung, hinhanh, mota, ngaydang, maloai, mand")] BaiViet tintuc)
        {
            string[] arrListStr = (tintuc.hinhanh).Split('/');
            if (arrListStr.Length > 2)
            {
                tintuc.hinhanh = arrListStr[3];
            }
            if (ModelState.IsValid)
            {
                var userBS = Session["userBS"] as WebAppYte.Models.NguoiDung;
                db.Entry(tintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexBS","TinTuc",new { id = userBS.mand });
              
            }
            ViewBag.maloai = new SelectList(db.Loais, "maloai", "tenloai", tintuc.maloai);
            return View(tintuc);
        }

        // GET: Admin/Tintucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet tintuc = db.BaiViets.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // POST: Admin/Tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userBS = Session["userBS"] as WebAppYte.Models.NguoiDung;
            BaiViet tintuc = db.BaiViets.Find(id);
            db.BaiViets.Remove(tintuc);
            db.SaveChanges();
            return RedirectToAction("IndexBS", "TinTuc", new { id = userBS.mand });
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