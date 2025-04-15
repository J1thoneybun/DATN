using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppYte.Models;
using PagedList;
using WebAppYte.Common;
using WebAppYte.DAO;
using System.Web.Helpers;
using System.Configuration;
using System.Net.Mail;
using System.Data.Entity.Validation;
namespace WebAppYte.Controllers
{
    public class HomeController : Controller
    {
        modelWeb db = new modelWeb();
       
        public ActionResult index()
        {
            return View();

        }
        
        public ActionResult Trangchu()
        {
            return View();

        }

        [HttpGet]
        public ActionResult Dangky()
        {
            ViewBag.gioitinh = new string[] { "Nam", "Nữ" };
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky([Bind(Include = "mabn,tenbn,sdt,email,diachi,ngaysinh, gioitinh, tendn,mk")] BenhNhan benhnhan)
        {
            if (ModelState.IsValid)
            {
                int check = db.BenhNhans.Where(x => x.tendn == benhnhan.tendn).Count();
                if (check == 0)
                {
                    db.BenhNhans.Add(benhnhan);
                    db.SaveChanges();
                    Session["benhnhan"] = benhnhan;
                    return RedirectToAction("Dangnhap");
                }
                else
                {
                    ViewBag.gioitinh = new string[] { "Nam", "Nữ" };
                    ViewBag.loi = "Tên đăng nhập đã tồn tại!";
                    return View(benhnhan);
                }
            }

            ViewBag.gioitinh = new string[] { "Nam", "Nữ" };
            return View(benhnhan);
        }
    
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Gioithieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection Dangnhap)
        {
            string tk = Dangnhap["tendn"].ToString();
            string mk = Dangnhap["mk"].ToString();
            var islogin = db.BenhNhans.Where(x=>x.trangthai==1).SingleOrDefault(x => x.tendn.Equals(tk) && x.mk.Equals(mk));
            var isloginAdmin = db.NguoiDungs.Where(x => x.trangthai == 1).SingleOrDefault(x => x.tendn.Equals(tk) && x.mk.Equals(mk));
            if (islogin != null)
            {
                Session["user"] = islogin;
                return RedirectToAction("Details", "BenhNhan", new { id = @islogin.mabn });
              
            }
            else if (isloginAdmin != null && isloginAdmin.quyen == 0)
            {
                Session["userAdmin"] = isloginAdmin;
                return RedirectToAction("QuanTris", "Admin");
            }
            else if (isloginAdmin != null && isloginAdmin.quyen == 1)
            {
                 Session["userBS"] = isloginAdmin;
                return RedirectToAction("Trangchu", "Home");
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Dangnhap");
            }

        }
        public class listofSegments
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        

        public ActionResult DangXuat()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DangXuatBs()
        {
            Session["userBS"] = null;
            return RedirectToAction("Index", "Home");
        }
		[HttpGet]
		public ActionResult QuenMatKhau()
		{
			return View();
		}

		[HttpPost]
		public ActionResult QuenMatKhau(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				ViewBag.Message = "Vui lòng nhập email đã đăng ký";
				return View();
			}

			// Kiểm tra trong bảng bệnh nhân
			var benhNhan = db.BenhNhans.FirstOrDefault(a => a.email == email);
			// Kiểm tra trong bảng người dùng (admin/bs)
			var nguoiDung = db.NguoiDungs.FirstOrDefault(a => a.email == email);

			if (benhNhan == null && nguoiDung == null)
			{
				ViewBag.Message = "Email không tồn tại trong hệ thống";
				return View();
			}

			// Tạo mã reset và thời hạn
			var resetCode = Guid.NewGuid().ToString();
			var expiry = DateTime.Now.AddHours(1);

			if (benhNhan != null)
			{
				benhNhan.ResetPasswordCode = resetCode;
				benhNhan.ResetPasswordCodeExpiry = expiry;
			}
			else if (nguoiDung != null)
			{
				nguoiDung.ResetPasswordCode = resetCode;
				nguoiDung.ResetPasswordCodeExpiry = expiry;
			}

			try
			{
				db.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				string errorMsg = "";
				foreach (var validationErrors in ex.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						errorMsg += $"Thuộc tính: {validationError.PropertyName} - Lỗi: {validationError.ErrorMessage}<br />";
					}
				}
				ViewBag.Message = "Lỗi khi lưu dữ liệu: <br />" + errorMsg;
				return View();
			}

			// Gửi email
			SendResetPasswordEmail(email, resetCode);

			ViewBag.Message = "Hướng dẫn khôi phục mật khẩu đã được gửi đến email của bạn";
			return View();
		}


		[HttpGet]
		public ActionResult DatLaiMatKhau(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return HttpNotFound();
			}

			// Kiểm tra trong cả 2 bảng
			var benhNhan = db.BenhNhans.FirstOrDefault(a => a.ResetPasswordCode == code);
			var nguoiDung = db.NguoiDungs.FirstOrDefault(a => a.ResetPasswordCode == code);

			if ((benhNhan == null || benhNhan.ResetPasswordCodeExpiry < DateTime.Now) &&
				(nguoiDung == null || nguoiDung.ResetPasswordCodeExpiry < DateTime.Now))
			{
				ViewBag.Message = "Mã khôi phục không hợp lệ hoặc đã hết hạn";
				return View("LienKetKhongHopLe");
			}

			ViewBag.ResetCode = code;
			return View();
		}

		[HttpPost]
		public ActionResult DatLaiMatKhau(string code, string newPassword)
		{
			// Kiểm tra trong bảng bệnh nhân
			var benhNhan = db.BenhNhans.FirstOrDefault(a => a.ResetPasswordCode == code);
			// Kiểm tra trong bảng người dùng
			var nguoiDung = db.NguoiDungs.FirstOrDefault(a => a.ResetPasswordCode == code);

			if ((benhNhan != null && benhNhan.ResetPasswordCodeExpiry >= DateTime.Now) ||
				(nguoiDung != null && nguoiDung.ResetPasswordCodeExpiry >= DateTime.Now))
			{
				if (benhNhan != null)
				{
					benhNhan.mk = newPassword; // Nên mã hóa mật khẩu
					benhNhan.ResetPasswordCode = null;
					benhNhan.ResetPasswordCodeExpiry = null;
				}
				else if (nguoiDung != null)
				{
					nguoiDung.mk = newPassword; // Nên mã hóa mật khẩu
					nguoiDung.ResetPasswordCode = null;
					nguoiDung.ResetPasswordCodeExpiry = null;
				}

				db.SaveChanges();

				ViewBag.Message = "Mật khẩu đã được đặt lại thành công";
				return View("DatLaiMatKhauThanhCong");
			}

			ViewBag.Message = "Mã khôi phục không hợp lệ hoặc đã hết hạn";
			return View("LienKetKhongHopLe");
		}

		private void SendResetPasswordEmail(string emailAddress, string resetCode)
		{
			try
			{
				var resetLink = Url.Action("DatLaiMatKhau", "Home",
					new { code = resetCode }, protocol: Request.Url.Scheme);

				var subject = "Khôi phục mật khẩu - Hệ thống Y tế";
				var body = $"Xin chào,<br/><br/>Chúng tôi nhận được yêu cầu khôi phục mật khẩu của bạn. " +
					"Vui lòng nhấp vào liên kết sau để đặt lại mật khẩu:<br/><br/>" +
					$"<a href='{resetLink}'>{resetLink}</a><br/><br/>" +
					"Liên kết này sẽ hết hạn sau 1 giờ.<br/><br/>" +
					"Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này.";

				// Lấy display name từ web.config
				var displayName = ConfigurationManager.AppSettings["MailDisplayName"] ?? "Hệ thống Y tế";

				using (var message = new MailMessage())
				{
					message.From = new MailAddress("ducduong.tektra@gmail.com", displayName);
					message.To.Add(emailAddress);
					message.Subject = subject;
					message.Body = body;
					message.IsBodyHtml = true;

					using (var client = new SmtpClient())
					{
						// Các thông số sẽ tự động lấy từ web.config
						client.Send(message);
					}
				}
			}
			catch (Exception ex)
			{
				// Ghi log lỗi
				System.Diagnostics.Debug.WriteLine($"Lỗi gửi email: {ex.Message}");
			}
		}

		public ActionResult DatLaiMatKhauThanhCong()
		{
			return View();
		}

		public ActionResult LienKetKhongHopLe()
		{
			return View();
		}

	}
}