using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppYte.Models;

namespace WebAppYte.Areas.Admin.Controllers
{
    public class QuanTriDanhGiasController : Controller
    {
        // GET: Admin/ThongKeDanhGias
        private modelWeb db1 = new modelWeb();
        public ActionResult Index()
        {
            var thongkedanhGias = db1.DanhGias;
            return View(thongkedanhGias.ToList());
        }
    }
}