using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppYte.Models;
using PagedList;
using WebAppYte.Common;
using WebAppYte.DAO;
using System.Net;
using System.Data.Entity;

namespace WebAppYte.Controllers
{
    public class BenhNhanController : Controller
    {
        // GET: BenhNhan
        private modelWeb db = new modelWeb();
        public ActionResult Index()
        {
            var benhnhan= db.BenhNhans.ToList();

            return View(benhnhan);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BenhNhan benhnhan = db.BenhNhans.Find(id);
            @ViewBag.ngay = (benhnhan.ngaysinh).Value.ToString("yyyy-MM-dd");
            if (benhnhan == null)
            {
                return HttpNotFound();
            }
            return View(benhnhan);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BenhNhan benhnhan = db.BenhNhans.Find(id);
            if (benhnhan == null)
            {
                return HttpNotFound();
            }
            @ViewBag.ngay = (benhnhan.ngaysinh).Value.ToString("yyyy-MM-dd");
            return View(benhnhan);
        }

        // POST: Nguoidung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mabn,tenbn,sdt,email,diachi,ngaysinh, gioitinh, tendn,mk")] BenhNhan benhnhan)
        {
            if (ModelState.IsValid)
            {
                benhnhan.trangthai = 1;
                db.Entry(benhnhan).State = EntityState.Modified;
                db.SaveChanges();
  
                return RedirectToAction("Details", "BenhNhan", new { id = benhnhan.mabn });
            } 
     
            return View(benhnhan);
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