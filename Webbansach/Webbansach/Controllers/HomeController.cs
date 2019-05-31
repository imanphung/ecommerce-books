using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;
namespace Webbansach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public ActionResult Index()
        {
            return View(db.Saches.Take(12).ToList());
        }

    }
}