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
    public class LichKhamsController : Controller
    {
        // GET: Admin/LichKhams
        private modelWeb db = new modelWeb();
        LichKham ls = new LichKham();
        

        // GET: Admin/LichKhams
        public ActionResult Index()
        {
            
            var lichKhams = ls.DSLichKham();
            return View(lichKhams.ToList());
        }

        // GET: Admin/LichKhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatLich lichKham = db.DatLiches.Find(id);
            if (lichKham == null)
            {
                return HttpNotFound();
            }
            return View(lichKham);
        }

        // POST: Admin/LichKhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatLich lichKham = db.DatLiches.Find(id);
            db.DatLiches.Remove(lichKham);
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