using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;
namespace Webbansach.Controllers
{
    public class ChuDeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: ChuDe
        public PartialViewResult ChuDe()
        {
            return PartialView (db.ChuDes.Take(5).ToList());
        }
        public ViewResult SachTheoChuDe(int MaChuDe = 0)
        {
            //Kiểm tra chủ đề tồn tại hay không
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Truy xuất danh sách các quyển sách theo chủ đề
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Chưa có sách cho chủ đề này";
            }
            //laasy danh sachs chu de
            ViewBag.lstChuDe = db.ChuDes.ToList();
            return View(lstSach);
        }
    }
}