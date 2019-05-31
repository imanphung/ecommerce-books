using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;
namespace Webbansach.Controllers
{
    public class QuanlySPController : Controller
    {
        // GET: QuanlySP
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View(db.Saches.ToList());
        }

        //Them moi sach
        [HttpGet]
        public ActionResult Themmoi()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.Machude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "Machude", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoi(Sach sach, HttpPostedFileBase fileUpload)
        {
            ViewBag.Machude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "Machude", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            //Không chọn hình ảnh
            //if(fileUpload == null)
            //{
            //    ViewBag.Thongbao = "Vui lòng chọn hình ảnh";
            //    return View();
            //}
            //Thêm vào cơ sở dữ liệu
            //Lưu tên của file
            var filename = Path.GetFileName(fileUpload.FileName);
            //Lưu đường dẫn của file
            var path = Path.Combine(Server.MapPath("~/img"), filename);
            //Kiểm tra hình ảnh tồn tại chưa
            if (System.IO.File.Exists(path))
            {
                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            sach.AnhBia = fileUpload.FileName;
            db.Saches.Add(sach);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        //chinh sua sach
        public ActionResult Chinhsua(int Masach = 0)
        {

            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Machude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "Machude", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }
        //Cập nhật lại sách
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Chinhsua(Sach sach, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);

            return RedirectToAction("Index");
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            //Lấy ra đối tượng sách theo mã 
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sach);
        }
        [HttpPost, ActionName("Xoa")]

        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}