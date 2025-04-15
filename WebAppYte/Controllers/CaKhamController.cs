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

namespace WebAppYte.Controllers
{
    public class CaKhamController : Controller
    {
        private modelWeb db = new modelWeb();

        // GET: CaKham

        public ActionResult Index(int id, int? page)
        {

            var lich = db.CaKhams.Where(x => x.mand == id && x.trangthai == 1).OrderByDescending(x => x.ngaykham).ThenBy(x => x.maca).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create(int? id)
        {
            ViewBag.mand = new SelectList(db.NguoiDungs.Where(h => h.mand == id), "mand", "hoten");

            return View();
        }

        // POST: Admin/Tintucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "maca, ngaykham, hinhthuc, ca, mand")] CaKham cakham)
        {
            if (ModelState.IsValid)
            {
                cakham.trangthai = 1;
                db.CaKhams.Add(cakham);
                db.SaveChanges();
                return RedirectToAction("Index", "CaKham", new { id = cakham.mand });

            }
            ViewBag.mand = new SelectList(db.NguoiDungs, "mand", "hoten", cakham.mand);
            return View(cakham);
        }

        // GET: Admin/Tintucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaKham cakham = db.CaKhams.Find(id);
            if (cakham == null)
            {
                return HttpNotFound();
            }
            @ViewBag.ngay = (cakham.ngaykham).Value.ToString("yyyy-MM-dd");

            return View(cakham);
        }

        // POST: Admin/Tintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CaKham model)
        {
            if (ModelState.IsValid)
            {
                var userBS = Session["userBS"] as WebAppYte.Models.NguoiDung;
                CaKham cakham = db.CaKhams.Find(model.maca);
                if (cakham == null) return HttpNotFound();


                cakham.hinhthuc = model.hinhthuc;
                cakham.ngaykham = model.ngaykham;
                cakham.ca = model.ca;
                cakham.trangthai = 1;
                cakham.mand = userBS.mand;

                db.Entry(cakham).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { id = userBS.mand });
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaKham cakham = db.CaKhams.Find(id);
            if (cakham == null)
            {
                return HttpNotFound();
            }
            @ViewBag.ngay = (cakham.ngaykham).Value.ToString("yyyy-MM-dd");

            return View(cakham);
        }

        // POST: Admin/Tintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Delete(CaKham model)
        {
            if (ModelState.IsValid)
            {
                var cakham = db.CaKhams.Find(model.maca);
                if (cakham == null) return HttpNotFound();

                cakham.trangthai = 0;
                db.Entry(cakham).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index", new { id = cakham.mand });
            }

            return View(model);
        }
        // GET: Admin/Tintucs/Delete/5


        // POST: Admin/Tintucs/Delete/5


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