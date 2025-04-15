using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Net;
using WebAppYte.Common;
using WebAppYte.DAO;
using System.Web;
using System.Web.Mvc;
using WebAppYte.Models;

namespace WebAppYte.Controllers
{
    public class ChuyenGiaBacSiController : Controller
    {
        modelWeb db = new modelWeb();
        // GET: Trungtamyte
        public ActionResult Index()
        {         
            return View();
        }
        public ActionResult TintucHotPartial()
        {

            return PartialView();

        }
        public class listofSegments
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        public PartialViewResult ChuyenGiaBacSi(string id, string tencv, int page = 1)
        {
            TTNguoiDung nd = new TTNguoiDung();
            id = Bien.str;
            int pageSize = 5;

            List<string> segmentList = (from p in db.Khoas select p.tenkhoa).ToList();
            ViewBag.segmentList = segmentList;
            ViewBag.filter = id;

            var chucvu = new List<listofSegments>();
            listofSegments segmentItem;
            var strArr = new string[] { "Giám đốc", "Dược sĩ", "Cố vẫn chuyên môn", "Trưởng khoa", "Phó khoa", "Bác sĩ" };

            for (int index = 0; index < strArr.Length; index++)
            {
                segmentItem = new listofSegments();
                segmentItem.Text = strArr[index];
                segmentItem.Value = strArr[index];
                chucvu.Add(segmentItem);
            }
            ViewBag.chucvu = chucvu;
            ViewBag.choose = tencv;



            var list = db.Khoas.ToList();
            var nguoi_dung = nd.ListNguoiDung();


            if (list.Exists(x => x.tenkhoa == id) == true)
            {

                nguoi_dung = nguoi_dung.Where(x => x.tenkhoa == id).ToList();
            }

            if (tencv != null)
            {

				nguoi_dung = nguoi_dung.Where(x => x.chucvu != null && x.chucvu.Contains(tencv)).ToList();
			}

			var model = nguoi_dung.OrderBy(x => x.hoten).ToPagedList(page, pageSize);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ActionPostData(string filter, string tencv)
        {
            Bien.str = filter;
            Bien.tencv = tencv;
            return RedirectToAction("ChuyenGiaBacSi", new { id = Bien.str, tencv = Bien.tencv });
        }
        public PartialViewResult Search(string id, string tencv, string fi, int page = 1)
        {
            TTNguoiDung nd = new TTNguoiDung();
            fi = Bien.str;
            int pageSize = 5;

            List<string> segmentList = (from p in db.Khoas select p.tenkhoa).ToList();
            ViewBag.segmentList = segmentList;
            ViewBag.filter = fi;

            var chucvu = new List<listofSegments>();
            listofSegments segmentItem;
            var strArr = new string[] { "Giám đốc", "Dược sĩ", "Cố vẫn chuyên môn", "Trưởng khoa", "Phó khoa", "Bác sĩ" };

            for (int index = 0; index < strArr.Length; index++)
            {
                segmentItem = new listofSegments();
                segmentItem.Text = strArr[index];
                segmentItem.Value = strArr[index];
                chucvu.Add(segmentItem);
            }
            ViewBag.chucvu = chucvu;
            ViewBag.choose = tencv;

            Bien.tk = id;
            ViewBag.id = Bien.tk;
            ViewBag.txt = Bien.tk;

            if (Bien.tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }

			string searchTerm = Bien.tk.ToLower();

		
			var nguoi_dung = nd.ListNguoiDung()
								.Where(x => x.hoten.ToLower().Contains(searchTerm) || x.gioithieu.ToLower().Contains(searchTerm))
								.ToList();
			var list = db.Khoas.ToList();


            if (list.Exists(x => x.tenkhoa == fi) == true)
            {

                nguoi_dung = nguoi_dung.Where(x => x.tenkhoa == fi).ToList();
            }

            if (tencv != null)
            {

                nguoi_dung = nguoi_dung.Where(x => x.chucvu.Contains(tencv)).ToList();
            }
			if (!nguoi_dung.Any())
			{
				ViewBag.Message = "Không tìm thấy kết quả nào.";
				return PartialView(new List<TTNguoiDung>().ToPagedList(page, pageSize));
			}

			var model = nguoi_dung.OrderBy(x => x.hoten).ToPagedList(page, pageSize);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ActionSearch(string filter, string tencv)
        {

            Bien.str = filter;
            Bien.tencv = tencv;
            return RedirectToAction("Search/" + ViewBag.txt, new { });
        }
        public ActionResult TamAnhHaNoi(string id, string tencv, int page = 1)
        {
            TTNguoiDung nd = new TTNguoiDung();
            id = Bien.str;
            int pageSize = 5;

            List<string> segmentList = (from p in db.Khoas select p.tenkhoa).ToList();
            ViewBag.segmentList = segmentList;
            ViewBag.filter = id;

            var chucvu = new List<listofSegments>();
            listofSegments segmentItem;
            var strArr = new string[] { "Giám đốc", "Dược sĩ", "Cố vẫn chuyên môn", "Trưởng khoa", "Phó khoa", "Bác sĩ" };

            for (int index = 0; index < strArr.Length; index++)
            {
                segmentItem = new listofSegments();
                segmentItem.Text = strArr[index];
                segmentItem.Value = strArr[index];
                chucvu.Add(segmentItem);
            }
            ViewBag.chucvu = chucvu;
            ViewBag.choose = tencv;



            var list = db.Khoas.ToList();
            var nguoi_dung = nd.ListNguoiDung();


            if (list.Exists(x => x.tenkhoa == id) == true)
            {

                nguoi_dung = nguoi_dung.Where(x => x.tenkhoa == id).ToList();
            }

            if (tencv != null)
            {

                nguoi_dung = nguoi_dung.Where(x => x.chucvu.Contains(tencv)).ToList();
            }
          
            var model = nguoi_dung.Where(x => x.tenchinhanh =="Hà Nội").ToPagedList(page, pageSize);
            return View(model);
        }
        public ActionResult TamAnhDaNang(string id, string tencv, int page = 1)
        {
            TTNguoiDung nd = new TTNguoiDung();
            id = Bien.str;
            int pageSize = 5;

            List<string> segmentList = (from p in db.Khoas select p.tenkhoa).ToList();
            ViewBag.segmentList = segmentList;
            ViewBag.filter = id;

            var chucvu = new List<listofSegments>();
            listofSegments segmentItem;
            var strArr = new string[] { "Giám đốc", "Dược sĩ", "Cố vẫn chuyên môn", "Trưởng khoa", "Phó khoa", "Bác sĩ" };

            for (int index = 0; index < strArr.Length; index++)
            {
                segmentItem = new listofSegments();
                segmentItem.Text = strArr[index];
                segmentItem.Value = strArr[index];
                chucvu.Add(segmentItem);
            }
            ViewBag.chucvu = chucvu;
            ViewBag.choose = tencv;



            var list = db.Khoas.ToList();
            var nguoi_dung = nd.ListNguoiDung();


            if (list.Exists(x => x.tenkhoa == id) == true)
            {

                nguoi_dung = nguoi_dung.Where(x => x.tenkhoa == id).ToList();
            }

            if (tencv != null)
            {

                nguoi_dung = nguoi_dung.Where(x => x.chucvu.Contains(tencv)).ToList();
            }

            var model = nguoi_dung.Where(x => x.tenchinhanh == "Đà Nẵng").ToPagedList(page, pageSize);
            return View(model);
        }

        public ActionResult TamAnhSaiGon(string id, string tencv, int page = 1)
        {
            TTNguoiDung nd = new TTNguoiDung();
            id = Bien.str;
            int pageSize = 5;

            List<string> segmentList = (from p in db.Khoas select p.tenkhoa).ToList();
            ViewBag.segmentList = segmentList;
            ViewBag.filter = id;

            var chucvu = new List<listofSegments>();
            listofSegments segmentItem;
            var strArr = new string[] { "Giám đốc", "Dược sĩ", "Cố vẫn chuyên môn", "Trưởng khoa", "Phó khoa", "Bác sĩ" };

            for (int index = 0; index < strArr.Length; index++)
            {
                segmentItem = new listofSegments();
                segmentItem.Text = strArr[index];
                segmentItem.Value = strArr[index];
                chucvu.Add(segmentItem);
            }
            ViewBag.chucvu = chucvu;
            ViewBag.choose = tencv;



            var list = db.Khoas.ToList();
            var nguoi_dung = nd.ListNguoiDung();


            if (list.Exists(x => x.tenkhoa == id) == true)
            {

                nguoi_dung = nguoi_dung.Where(x => x.tenkhoa == id).ToList();
            }

            if (tencv != null)
            {

                nguoi_dung = nguoi_dung.Where(x => x.chucvu.Contains(tencv)).ToList();
            }

            var model = nguoi_dung.Where(x => x.tenchinhanh == "Sài Gòn").ToPagedList(page, pageSize);
            return View(model);
        }
        public ActionResult DatLichHen(string id)
        {
            TTNguoiDung nd = new TTNguoiDung();
            var nguoi_dung = nd.ListNguoiDung();
            int mand = int.Parse(id);
            var model = nguoi_dung.Find(x => x.mand == mand);
         
            return View(model);
        }

        public ActionResult Details(int id)
        {
          /*  if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            TrungTamGanNhat tt = db.TrungTamGanNhats.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            } */
            return View();
        }
        public ViewResult van()
        {
            return View();
        }
      
    }

    
}