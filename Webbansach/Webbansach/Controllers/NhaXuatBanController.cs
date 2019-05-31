using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: NhaXuatBan
        public PartialViewResult NhaXuatBan()
        {
            return PartialView(db.NhaXuatBans.ToList());
        }
        //Hiển thị sách theo nhà xuất bản 
        public ViewResult SachTheoNXB(int MaNXB = 0)
        {
            //Kiểm tra chủ đề tồn tại hay không
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Truy xuất danh sách các quyển sách theo Nhà xuất bản
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có sách nào thuộc chủ đề này";
            }
            //Tạo viewbag danh sách nhà xuất bản 
            ViewBag.lstNXB = db.NhaXuatBans.ToList();
            return View(lstSach);
        }
    }
}