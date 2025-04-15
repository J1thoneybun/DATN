using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppYte.Models;

namespace WebAppYte.Areas.Admin.Controllers
{
    public class BenhAnsController : Controller
    {
        // GET: Admin/BenhAns
        private modelWeb db = new modelWeb();

        // GET: Admin/BenhAns
        public ActionResult Index()
        {
            var benhans = db.BenhAns.Include(x => x.benhnhan)
                .Include(x => x.bacsi)
                .Where(x => x.trangthai == 1);

            return View(benhans.ToList());
        }

        // GET: Admin/BenhAns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var benhnhans = db.BenhNhans.Where(x => x.trangthai == 1);
            var bacsis = db.NguoiDungs.Where(x => x.trangthai == 1);

            ViewData["benhnhans"] = benhnhans.ToList();
            ViewData["bacsis"] = bacsis.ToList();
            BenhAn benhan = db.BenhAns.Find(id);
            if (benhan == null)
            {
                return HttpNotFound();
            }
            return View(benhan);
        }

        // GET: Admin/BenhAns/Create
        // GET: Admin/BenhAns/Create
        public ActionResult Create()
        {
			// Thay vì x => x.trangthai == 1
			var benhnhans = db.BenhNhans.Where(x => (x.trangthai ?? 0) == 1).ToList();
			var bacsis = db.NguoiDungs.Where(x => (x.trangthai ?? 0) == 1 && x.quyen == 1).ToList();

			// Kiểm tra xem danh sách bệnh nhân và bác sĩ có dữ liệu không
			if (!benhnhans.Any() || !bacsis.Any())
            {
                // Thêm thông báo lỗi nếu không có dữ liệu
                ViewBag.ErrorMessage = "Không có bệnh nhân hoặc bác sĩ nào trong hệ thống.";
            }

            ViewData["benhnhans"] = benhnhans.ToList();
            ViewData["bacsis"] = bacsis.ToList();
            return View();
        }


        // POST: Admin/BenhAns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

		public ActionResult Create(BenhAn benhan)
		{
				// Tính lại BMI từ chiều cao/cân nặng (phòng trường hợp client bị tắt JS)
				if (benhan.chieucao > 0 && benhan.cannang > 0)
				{
					benhan.bmi = benhan.cannang / (benhan.chieucao * benhan.chieucao);
				}

				// Validate ModelState
				if (!ModelState.IsValid)
				{
					// Log lỗi và hiển thị lại form
					var errors = ModelState.Values.SelectMany(v => v.Errors);
					foreach (var error in errors)
					{
						Console.WriteLine(error.ErrorMessage);
					}

					ViewData["benhnhans"] = db.BenhNhans.Where(x => x.trangthai == 1).ToList();
					ViewData["bacsis"] = db.NguoiDungs.Where(x => x.trangthai == 1 && x.quyen == 1).ToList();
					return View(benhan);
				}
			if (ModelState.IsValid) // Kiểm tra validation
			{
				try
				{
					benhan.trangthai = 1; // Đặt trạng thái mặc định
					db.BenhAns.Add(benhan);
					db.SaveChanges();
					return RedirectToAction("Index"); // Chuyển hướng nếu thành công
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Lỗi khi lưu bệnh án: " + ex.Message);
				}
			}

			// Nếu có lỗi, load lại dropdown và hiển thị form
			ViewData["benhnhans"] = db.BenhNhans.Where(x => x.trangthai == 1).ToList();
			ViewData["bacsis"] = db.NguoiDungs.Where(x => x.trangthai == 1 && x.quyen == 1).ToList();
			return View(benhan);
		}

		// GET: Admin/BenhAns/Edit/5
		// GET: Admin/BenhAns/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			// Lấy bệnh án kèm thông tin bác sĩ và bệnh nhân
			BenhAn benhan = db.BenhAns
				.Include(x => x.bacsi)
				.Include(x => x.benhnhan)
				.FirstOrDefault(x => x.maba == id && x.trangthai == 1); // Thêm điều kiện trangthai

			if (benhan == null)
			{
				return HttpNotFound();
			}
			// Tắt proxy creation để tránh dynamic proxy

			db.Configuration.ProxyCreationEnabled = false;

			// Lấy danh sách bác sĩ và bệnh nhân active
			var benhnhans = db.BenhNhans.Where(x => x.trangthai == 1).ToList();
			var bacsis = db.NguoiDungs.Where(x => x.trangthai == 1).ToList();

			// Kiểm tra nếu không có dữ liệu
			if (!benhnhans.Any() || !bacsis.Any())
			{
				ViewBag.ErrorMessage = "Không có bệnh nhân hoặc bác sĩ nào trong hệ thống";
			}

			ViewData["benhnhans"] = benhnhans;
			ViewData["bacsis"] = bacsis;

			return View(benhan);
		}

		// POST: Admin/BenhAns/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(BenhAn benhan)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Tính lại BMI nếu có thay đổi chiều cao/cân nặng
					if (benhan.chieucao > 0 && benhan.cannang > 0)
					{
						benhan.bmi = Math.Round(benhan.cannang / (benhan.chieucao * benhan.chieucao), 1);
					}

					// Đảm bảo giữ nguyên trạng thái
					benhan.trangthai = 1;

					// Cập nhật bệnh án
					db.Entry(benhan).State = EntityState.Modified;
					db.SaveChanges();

					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Lỗi khi cập nhật bệnh án: " + ex.Message);
			}

			// Nếu có lỗi, load lại dropdown
			ViewData["benhnhans"] = db.BenhNhans.Where(x => x.trangthai == 1).ToList();
			ViewData["bacsis"] = db.NguoiDungs.Where(x => x.trangthai == 1).ToList();

			return View(benhan);
		}

		// GET: Admin/BenhAns/Delete/5
		public ActionResult Delete(int? id)
        {
            BenhAn benhan = db.BenhAns.Find(id);
            benhan.trangthai = 0;
            db.Entry(benhan).State = EntityState.Modified;
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