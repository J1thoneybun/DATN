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
    public class HoiDapsController : Controller
    {
        // GET: Admin/HoiDaps
        private modelWeb db = new modelWeb();

        // GET: Admin/HoiDaps
        public ActionResult Index()
        {
            var hoiDaps = db.HoiDaps.Include(h => h.BenhNhan).Include(h => h.NguoiDung);
            return View(hoiDaps.ToList());
        }

        // GET: Admin/HoiDaps/Details/5
        public ActionResult Details(int? id)
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
            return View(hoiDap);
        }
      

        // GET: Admin/HoiDaps/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.mabn = new SelectList(db.BenhNhans.Where(x => x.mabn == hoiDap.mabn), "mabn", "tenbn", hoiDap.mabn);
            ViewBag.mand = new SelectList(db.NguoiDungs.Where(x => x.mand == hoiDap.mand), "mand", "hoten", hoiDap.mand);
            @ViewBag.ngay = (hoiDap.ngayhoi).Value.ToString("yyyy-MM-dd");
            return View(hoiDap);
        }

        // POST: Admin/HoiDaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ma,string hoi,string dap, int mabn,DateTime ngayhoi )
        {
            var admin = Session["userAdmin"] as WebAppYte.Models.NguoiDung;
            HoiDap hd = db.HoiDaps.Where(x => x.ma == ma).FirstOrDefault();
            if (ModelState.IsValid)
            {
               

                if (hd.trangthai == 0 && dap != null)
                {
                   
                    hd.trangthai = 1;
                    hd.mand = admin.mand;
                    hd.ngaytl = DateTime.Now;
                }
                hd.hoi = hoi;
                hd.dap = dap;
                hd.mabn = mabn;
              
                hd.ngayhoi = ngayhoi;
                db.Entry(hd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mabn = new SelectList(db.BenhNhans, "mabn", "tenbn", mabn);
            ViewBag.mand = new SelectList(db.NguoiDungs, "mand", "hoten", hd.mand);
            return View();
        }

        // GET: Admin/HoiDaps/Delete/5
        public ActionResult Delete(int? id)
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
            return View(hoiDap);
        }

        // POST: Admin/HoiDaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoiDap hoiDap = db.HoiDaps.Find(id);
            db.HoiDaps.Remove(hoiDap);
            db.SaveChanges();
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
    }
}